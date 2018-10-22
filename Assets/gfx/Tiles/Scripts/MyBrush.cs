using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomGridBrush(true, false, false, "MyBrush")]
public class MyBrush : GridBrush
{
    public GameObject FloorLayer;
    public GameObject EdibleFixedLayer;
    public GameObject EdibleFallingLayer;
    public GameObject PlayerLayer;

    [MenuItem("Assets/Create/MyBrush")]
    public static void CreateBrush()
    {
        var path = EditorUtility.SaveFilePanelInProject("Save MyBrush", "New MyBrush", "Asset", "Save MyBrush", "Assets");
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(CreateInstance<GridBrush>(), path);
        }
    }

    public override void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
    {
        DeleteIfExistsInLayer(gridLayout, EdibleFixedLayer, position);
        DeleteIfExistsInLayer(gridLayout, EdibleFallingLayer, position);
        DeleteIfExistsInLayer(gridLayout, FloorLayer, position);
        DeleteIfExistsInLayer(gridLayout, PlayerLayer, position);
    }

    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (cells.Length != 1)
        {
            throw new Exception("you must select one tile.");
        }

        var tile = cells[0].tile as ConnectedTile;
        if (tile != null)
        {
            GameObject layer;
            switch (tile.TileType)
            {
                case TileType.EdibleFalling:
                    layer = EdibleFallingLayer;
                    break;
                case TileType.EdibleFixed:
                    layer = EdibleFixedLayer;
                    break;
                case TileType.Floor:
                    layer = FloorLayer;
                    break;
                case TileType.SpawnPoint:
                    layer = PlayerLayer;
                    break;
                default:
                    layer = brushTarget;
                    break;
            }

            base.Paint(gridLayout, layer, position);
        }
    }

    private void DeleteIfExistsInLayer(GridLayout gridLayout, GameObject layer, BoundsInt position)
    {
        if (LayerHasTile(layer, position.position))
        {
            base.BoxErase(gridLayout, layer, position);
        }
    }

    private bool LayerHasTile(GameObject layer, Vector3Int position)
    {
        if (layer == null) return false;

        var tile = layer.GetComponent<Tilemap>().GetTile(position);
        return tile != null;
    }
}