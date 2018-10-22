using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject TileControllerPrefab;
    public TextAsset Level;

    public Map CurrentMap { get; private set; }

    private readonly IDictionary<Vector2Int, GameObject> _tiles = new Dictionary<Vector2Int, GameObject>();

    public void Start()
    {
        CurrentMap = LevelImporter.Load(Level.text);

        for (var x = 0; x < CurrentMap.Width; x++)
        {
            for (var y = 0; y < CurrentMap.Height; y++)
            {
                var type = CurrentMap[x, y];
                if (type == TileType.None)
                {
                    continue;
                }


                var tile = Instantiate(TileControllerPrefab);
                tile.name = x + "," + y;
                tile.transform.SetParent(transform);
                tile.transform.localPosition = new Vector3(x, y, 0);
                tile.GetComponent<TileController>().Type = type;
                _tiles.Add(new Vector2Int(x, y), tile);
            }
        }
    }

    public static LevelController Instance
    {
        get { return FindObjectOfType<LevelController>(); }
    }

    private bool CompareType(Vector2Int pos, TileType typeA)
    {
        if (!_tiles.ContainsKey(pos))
        {
            return false;
        }

        var typeB = _tiles[pos].GetComponent<TileController>().Type;

        return Equals(typeA, typeB);
    }

    public bool HasTileOfSameTypeAt(TileController tile, int x, int y)
    {
        var pos = new Vector2Int((int)tile.transform.localPosition.x + x, (int)tile.transform.localPosition.y + y);
        if (pos.x < 0 || pos.x >= CurrentMap.Width || pos.y < 0 || pos.y >= CurrentMap.Height)
        {
            return false;
        }

        var type = tile.GetComponent<TileController>().Type;
        return CompareType(pos, type);
    }
}