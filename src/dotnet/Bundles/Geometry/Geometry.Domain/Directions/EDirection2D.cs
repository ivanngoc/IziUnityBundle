using System;

namespace IziHardGames.Geometry.Domain.Directions
{
    [Flags]
    public enum EDirection2D
    {
        None = 0,
        Top = 1,
        RightTop = 1 << 0,
        Righ = 1 << 1,
        RighBottom = 1 << 2,
        Bottom = 1 << 3,
        LeftBottom = 1 << 4,
        Left = 1 << 5,
        LeftTop = 1 << 6,
    }
}
