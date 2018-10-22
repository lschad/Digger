using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public static class LevelImporter
{
    public static Map Load(string imported)
    {
        var level = JsonConvert
                    .DeserializeObject<IEnumerable<int[]>>(imported)
                    .Select(a => new TileContainer(a[0], a[1], (TileType) a[2]))
                    .ToList();


        var minX = level.OrderBy(c => c.X).Select(c => c.X).First();
        var maxX = level.OrderBy(c => c.X).Select(c => c.X).Last();
        var minY = level.OrderBy(c => c.Y).Select(c => c.Y).First();
        var maxY = level.OrderBy(c => c.Y).Select(c => c.Y).Last();
        var sizeX = maxX - minX;
        var sizeY = maxY - minY;

        var map = new TileType[sizeX + 1, sizeY + 1]; // todo: +1. really?

        foreach (var tile in level)
        {
            var x = tile.X - minX;
            var y = tile.Y - minY;
            map[x, y] = tile.Type;
        }


        return new Map(map);
    }
}

//public class LevelImporter : MonoBehaviour
//{
//    public TextAsset Level;

//    private GameObjectChildrenDictionary _parents;

//    public void Start()
//    {
//        if (Level == default(TextAsset))
//        {
//            throw new ArgumentException("Add a Level, bruh.");
//        }

//        _parents = new GameObjectChildrenDictionary(gameObject);

//        var level = JsonConvert.DeserializeObject<IEnumerable<int[]>>(Level.text);
//        foreach (var data in level)
//        {
//            var x = data[0];
//            var y = data[1];
//            var type = (TileType) data[2];

//            var tile = CreateTile(new Vector2Int(x, y), type);
//            _parents[type].transform.AddChild(tile);
//        }
//    }

//    private GameObject CreateTile(Vector2Int pos, TileType type)
//    {
//        var tile = new GameObject(pos.ToString());
//        tile.transform.localPosition = new Vector3(pos.x, pos.y, 0);
//        tile.transform.localScale = Vector3.one;

//        //tile.AddComponent<SpriteRenderer>();
//        var tr = tile.AddComponent<TileRenderer>();
//        tr.TileType = type;

//        return tile;
//    }
//}


//public class TileRenderer : MonoBehaviour
//{
//    public TileType TileType { get; set; }
//}