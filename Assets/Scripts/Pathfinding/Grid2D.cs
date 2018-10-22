using System;

public class Grid2D
{
    private readonly NodeBase[,] _grid;

    public Grid2D(NodeBase[,] grid)
    {
        _grid = grid;
    }

    public int Height
    {
        get { return _grid.GetLength(1); }
    }

    public int Width
    {
        get { return _grid.GetLength(0); }
    }

    public NodeBase this[int x, int y]
    {
        get
        {
            return _grid[x, y];
        }
        set { throw new NotImplementedException(); }
    }
}