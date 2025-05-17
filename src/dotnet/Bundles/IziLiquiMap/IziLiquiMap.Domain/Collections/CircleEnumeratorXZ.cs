using System;
using System.Collections;
using System.Collections.Generic;
using IziHardGames.Geometry.Domain.Tilemap;

namespace IziHardGames.IziLiquiMap.Domain.Models
{
    public struct CircleEnumeratorXZ : IEnumerator<TilePosition>
    {
        public TilePosition Current { get; private set; }
        object IEnumerator.Current { get => throw new NotImplementedException(); }

        public int Index => index;
        public int IndexOfSide => indexOfSide;
        public int SideOffset => sideOffset;

        private int index;
        /// <summary>
        /// Size Of circle
        /// </summary>
        public readonly int count;
        /// <summary>
        /// 0 - left<br/>
        /// 1 - top<br/>
        /// 2 - right<br/>
        /// 3 - bot<br/>
        /// </summary>
        private int indexOfSide;
        private int sideOffset;

        public readonly TilePosition cornerAtLeftBot;
        public readonly TilePosition cornerAtLeftTop;
        public readonly TilePosition cornerAtRightTop;
        public readonly TilePosition cornerAtRightBot;

        public CircleEnumeratorXZ(TilePosition center, int radius)
        {
            int side = radius * 2;
            int sideOverlaped = side + 1;
            int minX = center.x - radius;
            int minZ = center.z - radius;
            this.cornerAtLeftBot = new TilePosition(minX, 0, minZ);
            this.cornerAtLeftTop = new TilePosition(minX, 0, minZ + side);
            this.cornerAtRightTop = new TilePosition(minX + side, 0, cornerAtLeftTop.z);
            this.cornerAtRightBot = new TilePosition(cornerAtRightTop.x, 0, minZ);
            Current = default;
            index = -1;
            indexOfSide = 0;
            sideOffset = 0;
            count = side * 4;
        }

        public bool MoveNext()
        {
            index++;
            if (index < count)
            {
                // clockwise from min (left bot)
                switch (indexOfSide)
                {
                    case 0:
                        {
                            var result = new TilePosition(cornerAtLeftBot.x, 0, cornerAtLeftBot.z + sideOffset);
                            sideOffset++;
                            if ((cornerAtLeftBot.z + sideOffset) > cornerAtLeftTop.z)
                            {
                                sideOffset = 1;
                                indexOfSide = 1;
                            }
                            Current = result;
                            return true;
                        }
                    case 1:
                        {
                            var result = new TilePosition(cornerAtLeftTop.x + sideOffset, 0, cornerAtLeftTop.z);
                            sideOffset++;
                            if ((cornerAtLeftTop.x + sideOffset) > cornerAtRightTop.x)
                            {
                                sideOffset = -1;
                                indexOfSide = 2;
                            }
                            Current = result;
                            return true;
                        }
                    case 2:
                        {
                            var result = new TilePosition(cornerAtRightTop.x, 0, cornerAtRightTop.z + sideOffset);
                            sideOffset--;
                            if ((cornerAtRightTop.z + sideOffset) < cornerAtRightBot.z)
                            {
                                sideOffset = -1;
                                indexOfSide = 3;
                            }
                            Current = result;
                            return true;
                        }
                    case 3:
                        {
                            var result = new TilePosition(cornerAtRightBot.x + sideOffset, 0, cornerAtRightBot.z);
                            sideOffset--;
                            if ((cornerAtRightBot.x + sideOffset) < cornerAtLeftBot.x)
                            {
                                indexOfSide = 4;
                            }
                            Current = result;
                            return true;
                        }
                    default: return false;
                }
            }
            return false;
        }

        public void Reset()
        {
            indexOfSide = 0;
            sideOffset = 0;
            index = -1;
            Current = default;
        }


        public void Dispose()
        {

        }
    }
}
