using System;
using System.Threading;
using IziHardGames.Geometry.Domain.Vectors;
using IziHardGames.IziLiquiMap.Contracts;
using IziHardGames.Pools.Contracts;
using IziHardGames.Geometry.Domain.Tilemap;
using IziHardGames.IziLiquiMap.Domain.Value_Objects;

namespace IziHardGames.IziLiquiMap.Domain.Models
{
    public class Cell : ILiquiMapCell, IReusable
    {
        public float Delta { get => delta; }
        public float Level { get => level; }
        public float LevelUncommited { get => levelUncommited; }
        public MapIndex Index { get => index; }
        /// <summary>
        /// Commited stacks at start of frame (loop) or at the end (after commiting)
        /// </summary>
        public CellStacks Stacks { get => stacks; }
        public CellStacksDynamic StacksDynamic { get => stacksDynamic; }
        /// <summary>
        /// Changes during current frame
        /// </summary>
        public int StacksChanges { get => stacksChanges; }
        public int StacksAvailable { get => stacksAvailable; }
        public int Reserves { get => reserves; }
        public int Potential { get => reserves + stacksDynamic.Value; }

        public MapPosition TilePosition { get => position; }
        public ref MapPosition TilePositionRef { get => ref position; }
        public Vector3RO Center { get => center; }
        public ref Vector3RO CenterRef { get => ref center; }
        public LiquiMap Map { get => map ?? throw new NullReferenceException(); }

        public Cell? this[TileIndex index]
        {
            get => octagon[index.value];
            set
            {
                octagon[index.value] = value;
            }
        }

        public Cell? this[OctagonIndex index]
        {
            get => octagon[index];
            set
            {
                octagon[index] = value;
            }
        }

        public Octagon<Cell?> octagon;
        private float delta;
        private float level;
        private float levelUncommited;
        private MapIndex index;
        private CellStacks stacks;
        private CellStacksDynamic stacksDynamic;
        private int stacksChanges;
        private int stacksAvailable;
        private int reserves;
        private MapPosition position;
        private Vector3RO center;
        private LiquiMap? map;

        public void Initilize(LiquiMap map, MapIndex index, MapPosition tilePosition, Vector3RO center)
        {
            this.index = index;
            this.position = tilePosition;
            this.center = center;
            this.map = map;
        }

        public void Add(float weight)
        {
            levelUncommited += weight;
        }

        public void Substract(float weight)
        {
            levelUncommited -= weight;
        }

        public void SpreadByStacks()
        {
            //int countAcceptors = 0;
            //int acceptorsConfig = 0;
            //for (int i = 0; i < 8; i++)
            //{
            //    if (octagon[i]?.IsCanAcceptStack(this) ?? false)
            //    {
            //        countAcceptors++;
            //        acceptorsConfig |= 1 << i;
            //    }
            //}
            //var sumCapacityToPread =  ;
            throw new NotImplementedException();

        }
        public void Spread()
        {
            if (level > 0)
            {
                var sumCapacityToPread = 0f;
                var octagonConfiguration = 0;
                var countOfRecipients = float.Epsilon;
                for (int i = 0; i < 8; i++)
                {
                    if (octagon[i]?.IsRecipientFor(this, out var capacity) ?? false)
                    {
                        sumCapacityToPread += capacity;
                        octagonConfiguration |= (1 << i);
                        countOfRecipients++;
                    }
                }
                sumCapacityToPread = level / countOfRecipients;
                for (int i = 0; i < 8; i++)
                {
                    if ((octagonConfiguration & (1 << i)) == 0)
                    {
                        Transfuse(octagon[i]!, sumCapacityToPread);
                    }
                }
            }
        }

        private void Transfuse(Cell cell, float value)
        {
            levelUncommited -= value;
            cell.Accept(value);
        }

        private void Accept(float value)
        {
            levelUncommited += value;
        }

        public void Begin()
        {
            stacksDynamic = stacks;
            stacksChanges = default;
            levelUncommited = level;
        }
        public void Commit()
        {
            delta = levelUncommited - level;
            level = levelUncommited;
            stacks = new CellStacks(stacksDynamic.value);
        }

        private bool IsRecipientFor(Cell cell, out float maxAcceptCapacity)
        {
            maxAcceptCapacity = cell.level - this.level;
            return maxAcceptCapacity > 0;
        }

        public EnumeratorOfNeighboursByPotentialThenByDistance EnumerateByDistance(Vector3RO position)
        {
            return new EnumeratorOfNeighboursByPotentialThenByDistance(this, position);
        }

        internal bool IsCanAcceptStack(Cell from, out int diff)
        {
            diff = this.stacks.Value - from.stacks.Value;
            return diff > 0;
        }

        internal void Stack()
        {
            stacksDynamic.Increment();
            Interlocked.Increment(ref stacksChanges);
        }

        internal void Unstack()
        {
            stacksDynamic.Decrement();
            Interlocked.Decrement(ref stacksChanges);
        }

        /// <summary>
        /// Checkin after reserve
        /// </summary>
        public void Checkin()
        {
            Interlocked.Decrement(ref reserves);
        }

        public void Reserve()
        {
            Interlocked.Decrement(ref stacksAvailable);
            Interlocked.Increment(ref reserves);
        }
        public bool TryToReserve()
        {
            if (stacksAvailable > 0)
            {
                Reserve();
                return true;
            }
            return false;
        }

        public Vector3RO NeighbourIndexToCenterPos(OctagonIndex index)
        {
            var diagonalOfCell = map!.diagonalOfCell;
            var offset = GetOffsetOfTilePosition(index);
            var tilePositionOfNeighbour = offset + position.position.vector;
            var originOfNeighbour = new Vector3RO(tilePositionOfNeighbour.x * diagonalOfCell.x, tilePositionOfNeighbour.y * diagonalOfCell.y, tilePositionOfNeighbour.z * diagonalOfCell.z);
            var result = originOfNeighbour + map!.diagonalOfCell * 0.5f;
            return result;
        }
        /// <summary>
        /// сколько нужно прибавить к центру этой ячейки чтобы получить центр указанного соседа
        /// </summary>
        public Vector3RO NeighbourIndexToOffset(EOctagonConfiguration configuration)
        {
            throw new System.NotImplementedException();
        }

        public Vector3IntRO GetOffsetOfTilePosition(OctagonIndex index)
        {
            switch (index.value)
            {
                case OctagonIndex.INT_NORTH: return new Vector3IntRO(0, 0, 1);
                case OctagonIndex.INT_EAST_NORTH: return new Vector3IntRO(1, 0, 1);
                case OctagonIndex.INT_EAST: return new Vector3IntRO(1, 0, 0);
                case OctagonIndex.INT_EAST_SOUTH: return new Vector3IntRO(1, 0, -1);
                case OctagonIndex.INT_SOUTH: return new Vector3IntRO(0, 0, -1);
                case OctagonIndex.INT_WEST_SOUTH: return new Vector3IntRO(-1, 0, -1);
                case OctagonIndex.INT_WEST: return new Vector3IntRO(-1, 0, 0);
                case OctagonIndex.INT_WEST_NORTH: return new Vector3IntRO(-1, 0, 1);
                default: throw new ArgumentOutOfRangeException(index.ToString());
            }
        }

        internal void BindAs(Cell newCell, OctagonIndex index)
        {
            octagon[index] = newCell;
            var oppositeIndex = ToOppositeIndex(index);
            newCell[oppositeIndex] = this;
        }

        private OctagonIndex ToOppositeIndex(OctagonIndex flag)
        {
            switch (flag)
            {
                case OctagonIndex.INT_NORTH: return OctagonIndex.South;
                case OctagonIndex.INT_EAST_NORTH: return OctagonIndex.WestSouth;
                case OctagonIndex.INT_EAST: return OctagonIndex.West;
                case OctagonIndex.INT_EAST_SOUTH: return OctagonIndex.WestNorth;
                case OctagonIndex.INT_SOUTH: return OctagonIndex.North;
                case OctagonIndex.INT_WEST_SOUTH: return OctagonIndex.EastNorth;
                case OctagonIndex.INT_WEST: return OctagonIndex.East;
                case OctagonIndex.INT_WEST_NORTH: return OctagonIndex.EastSouth;
                default: throw new ArgumentOutOfRangeException(flag.ToString());
            }
        }

        private EOctagonConfiguration ToOppositeFlag(OctagonIndex index)
        {
            switch (index.value)
            {
                case OctagonIndex.INT_NORTH: return EOctagonConfiguration.South;
                case OctagonIndex.INT_EAST_NORTH: return EOctagonConfiguration.WestSouth;
                case OctagonIndex.INT_EAST: return EOctagonConfiguration.West;
                case OctagonIndex.INT_EAST_SOUTH: return EOctagonConfiguration.WestNorth;
                case OctagonIndex.INT_SOUTH: return EOctagonConfiguration.North;
                case OctagonIndex.INT_WEST_SOUTH: return EOctagonConfiguration.EastNorth;
                case OctagonIndex.INT_WEST: return EOctagonConfiguration.East;
                case OctagonIndex.INT_WEST_NORTH: return EOctagonConfiguration.EastSouth;
                default: throw new ArgumentOutOfRangeException(index.ToString());
            }
        }
    }
}
