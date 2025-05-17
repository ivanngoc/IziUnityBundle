using System;

namespace IziHardGames.Geometry.Domain.Vectors
{
    [Flags]
    public enum EOctagonConfiguration : byte
    {
        None = 0,
        North = 1 << 0,
        EastNorth = 1 << 1,
        East = 1 << 2,
        EastSouth = 1 << 3,

        South = 1 << 4,
        WestSouth = 1 << 5,
        West = 1 << 6,
        WestNorth = 1 << 7,

        Crest = North | East | South | West,
        Cross = EastNorth | EastSouth | WestSouth | WestNorth,
        Forward = North,
        Backward = South,
        Left = West,
        Right = East,

        HemisphereTop = West | WestNorth | North | EastNorth | East,
        HemisphereBot = West | WestSouth | South | EastSouth | East,
        HemisphereRight = North | EastNorth | East | EastSouth | South,
        HemisphereRightTop = WestNorth | North | EastNorth | East | EastSouth,

        ConeTop = WestNorth | North | EastNorth,
        ConeBot = WestSouth | South | EastSouth,

        All = byte.MaxValue,
    }
}
