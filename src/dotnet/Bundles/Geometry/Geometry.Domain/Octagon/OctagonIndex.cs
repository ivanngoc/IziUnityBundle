namespace IziHardGames.Geometry.Domain.Vectors
{
    public readonly struct OctagonIndex
    {
        public const int COUNT = 8;
        public const int INT_NONE = -1;
        public const int INT_NORTH = 0;
        public const int INT_EAST_NORTH = 1;
        public const int INT_EAST = 2;
        public const int INT_EAST_SOUTH = 3;
        public const int INT_SOUTH = 4;
        public const int INT_WEST_SOUTH = 5;
        public const int INT_WEST = 6;
        public const int INT_WEST_NORTH = 7;

        public static readonly OctagonIndex None = new OctagonIndex(INT_NONE);
        public static readonly OctagonIndex North = new OctagonIndex(INT_NORTH);
        public static readonly OctagonIndex EastNorth = new OctagonIndex(INT_EAST_NORTH);
        public static readonly OctagonIndex East = new OctagonIndex(INT_EAST);
        public static readonly OctagonIndex EastSouth = new OctagonIndex(INT_EAST_SOUTH);
        public static readonly OctagonIndex South = new OctagonIndex(INT_SOUTH);
        public static readonly OctagonIndex WestSouth = new OctagonIndex(INT_WEST_SOUTH);
        public static readonly OctagonIndex West = new OctagonIndex(INT_WEST);
        public static readonly OctagonIndex WestNorth = new OctagonIndex(INT_WEST_NORTH);

        public readonly int value;

        public OctagonIndex(int value)
        {
            this.value = value;
        }

        public static implicit operator int(OctagonIndex val) => val.value;
        public static implicit operator OctagonIndex(int val) => new OctagonIndex(val);
    }
}
