using System;

[Flags]
internal enum Sides
{
    None = 0,
    Left = 2,
    TopLeft = 4,
    Top = 8,
    TopRight = 16,
    Right = 32,
    BottomRight = 64,
    Bottom = 128,
    BottomLeft = 256
}
