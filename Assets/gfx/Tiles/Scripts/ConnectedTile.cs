using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New ConnectedTile", menuName = "Digger/Tiles/ConnectedTile")]
public sealed class ConnectedTile : EzTile
{
    public Sprite[] Sprites;

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        var left = HasAdjacentTileOfSameType(tilemap, location, -1, 0) ? "0" : "1";
        var topleft = HasAdjacentTileOfSameType(tilemap, location, -1, 1) ? "0" : "1";
        var top = HasAdjacentTileOfSameType(tilemap, location, 0, 1) ? "0" : "1";
        var topright = HasAdjacentTileOfSameType(tilemap, location, 1, 1) ? "0" : "1";
        var right = HasAdjacentTileOfSameType(tilemap, location, 1, 0) ? "0" : "1";
        var bottomright = HasAdjacentTileOfSameType(tilemap, location, 1, -1) ? "0" : "1";
        var bottom = HasAdjacentTileOfSameType(tilemap, location, 0, -1) ? "0" : "1";
        var bottomleft = HasAdjacentTileOfSameType(tilemap, location, -1, -1) ? "0" : "1";

        var spriteName = left + topleft + top + topright + right + bottomright + bottom + bottomleft;
        var sprite = Sprites.ToList().SingleOrDefault(s => s.name == spriteName);
        tileData.sprite = sprite;
    }

    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        for (var yd = -1; yd <= 1; yd++)
        {
            for (var xd = -1; xd <= 1; xd++)
            {
                var position = new Vector3Int(location.x + xd, location.y + yd, location.z);
                if (tilemap.GetTile(position) == this)
                {
                    tilemap.RefreshTile(position);
                }
            }
        }
    }

    private bool HasAdjacentTileOfSameType(ITilemap tilemap, Vector3Int location, int x, int y)
    {
        var tile = tilemap.GetTile(location + new Vector3Int(x, y, 0));
        if (tile == null)
        {
            return false;
        }

        return tile.name == name;
    }
}
