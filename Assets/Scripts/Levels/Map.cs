using System;
using UnityEngine;

public class Map
{
    private readonly TileType[,] _tiles;

    public Map(TileType[,] tiles)
    {
        _tiles = tiles;
    }

    public int Height
    {
        get { return _tiles.GetLength(1); }
    }

    public int Width
    {
        get { return _tiles.GetLength(0); }
    }

    public TileType this[int x, int y]
    {
        get { return _tiles[x, y]; }
        set { throw new NotImplementedException(); }
    }
}

public static class MapExtensions
{
    public static Vector2Int IndexToPosition(this Map map, int index)
    {
        var y = index / map.Width;
        var x = index - y;

        return new Vector2Int(x, y);
    }

    public static int PositionToIndex(this Map map, Vector2Int pos)
    {
        var index = pos.x + pos.y * map.Width;
        return index;
    }

    public static int PositionToIndex(this Map map, Vector3 pos)
    {
        return PositionToIndex(map, pos.ToVector2Int());
    }
}