using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "New RandomTile", menuName = "Digger/Tiles/RandomTile")]
public class RandomTile : EzTile
{
    public Sprite[] Sprites;

    public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
    {
        base.GetTileData(location, tileMap, ref tileData);
        if (Sprites != null && Sprites.Length > 0)
        {
            long hash = location.x;
            hash = hash + 0xabcd1234 + (hash << 15);
            hash = (hash + 0x0987efab) ^ (hash >> 11);
            hash ^= location.y;
            hash = hash + 0x46ac12fd + (hash << 7);
            hash = (hash + 0xbe9730af) ^ (hash << 11);
            Random.InitState((int) hash);
            tileData.sprite = Sprites[Mathf.FloorToInt(Sprites.Length * Random.value)];
        }
    }
}