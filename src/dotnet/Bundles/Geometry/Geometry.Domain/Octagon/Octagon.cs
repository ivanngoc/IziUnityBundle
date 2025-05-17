using System;

namespace IziHardGames.Geometry.Domain.Vectors
{
    public static class OctagonConstantsXZ
    {
        private readonly static Vector3IntRO offsetNorth = Vector3IntRO.forward;
        private readonly static Vector3IntRO offsetEastNorth = Vector3IntRO.forward + Vector3IntRO.right;
        private readonly static Vector3IntRO offsetEast = Vector3IntRO.right;
        private readonly static Vector3IntRO offsetEastSouth = Vector3IntRO.back + Vector3IntRO.right;
        private readonly static Vector3IntRO offsetSouth = Vector3IntRO.back;
        private readonly static Vector3IntRO offsetWestSouth = Vector3IntRO.left + Vector3IntRO.back;
        private readonly static Vector3IntRO offsetWest = Vector3IntRO.left;
        private readonly static Vector3IntRO offsetWestNorth = Vector3IntRO.left + Vector3IntRO.forward;
    }

    /// <summary>
    /// Квадрат с 8 направлениями (направления сторон + диагональные)
    /// </summary>
    public unsafe struct Octagon<T>
    {
        private T north;
        private T eastNorth;
        private T east;
        private T eastSouth;

        private T south;
        private T westSouth;
        private T west;
        private T westNorth;

        public T this[OctagonIndex index]
        {
            get
            {
                switch (index.value)
                {
                    case OctagonIndex.INT_NORTH: return north;
                    case OctagonIndex.INT_EAST_NORTH: return eastNorth;
                    case OctagonIndex.INT_EAST: return east;
                    case OctagonIndex.INT_EAST_SOUTH: return eastSouth;
                    case OctagonIndex.INT_SOUTH: return south;
                    case OctagonIndex.INT_WEST_SOUTH: return westSouth;
                    case OctagonIndex.INT_WEST: return west;
                    case OctagonIndex.INT_WEST_NORTH: return westNorth;
                    default: throw new ArgumentOutOfRangeException(index.ToString());
                }
            }
            set
            {
                switch (index.value)
                {
                    case OctagonIndex.INT_NORTH: north = value; break;
                    case OctagonIndex.INT_EAST_NORTH: eastNorth = value; break;
                    case OctagonIndex.INT_EAST: east = value; break;
                    case OctagonIndex.INT_EAST_SOUTH: eastSouth = value; break;
                    case OctagonIndex.INT_SOUTH: south = value; break;
                    case OctagonIndex.INT_WEST_SOUTH: westSouth = value; break;
                    case OctagonIndex.INT_WEST: west = value; break;
                    case OctagonIndex.INT_WEST_NORTH: westNorth = value; break;
                    default: throw new ArgumentOutOfRangeException(index.ToString());
                }
            }
        }
    }
}
