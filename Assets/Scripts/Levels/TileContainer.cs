public struct TileContainer
{
    public readonly int X;
    public readonly int Y;
    public readonly TileType Type;

    public TileContainer(int x, int y, TileType type)
    {
        X = x;
        Y = y;
        Type = type;
    }
}