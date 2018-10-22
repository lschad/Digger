using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelExporter : MonoBehaviour
{
    public IEnumerable<int[]> Tiles
    {
        get
        {
            var output = new List<int[]>();

            foreach (var tilemap in Tilemaps)
            {
                for (var x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
                {
                    for (var y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
                    {
                        var tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                        if (tile != null)
                        {
                            var t = (int) (TileType) Enum.Parse(typeof(TileType), tile.name);
                            var tileContainer = new[] {x, y, t};
                            output.Add(tileContainer);
                        }
                    }
                }
            }

            return output;
        }
    }

    private IEnumerable<Tilemap> Tilemaps
    {
        get { return GetComponentsInChildren<Tilemap>(); }
    }
}

[CustomEditor(typeof(LevelExporter))]
public class TilemapToSpriteThingyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var level = (LevelExporter) target;
        if (GUILayout.Button("Export to JSON"))
        {
            var path = EditorUtility.SaveFilePanelInProject("Export Level", level.name + ".json", "json", "hehexd");
            if (!string.IsNullOrEmpty(path))
            {
                var exported = JsonConvert.SerializeObject(level.Tiles);
                File.WriteAllText(path, exported);
            }
        }
    }
}