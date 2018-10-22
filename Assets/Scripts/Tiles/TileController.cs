using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TileController : MonoBehaviour
{
    public TileSprites Sprites;
    public TileType Type { get; set; }

    private int _lastMask = -1;
    private SpriteRenderer SpriteRenderer
    {
        get { return GetComponent<SpriteRenderer>(); }
    }

    public void OnMouseDown()
    {
        var mask = 0;

        var left = -1;
        var top = 1;
        var right = 1;
        var bottom = -1;

        if (HasTileOfSameTypeAt(left, 0)) mask += (int)Sides.Left;
        if (HasTileOfSameTypeAt(left, top)) mask += (int)Sides.TopLeft;
        if (HasTileOfSameTypeAt(0, top)) mask += (int)Sides.Top;
        if (HasTileOfSameTypeAt(right, top)) mask += (int)Sides.TopRight;
        if (HasTileOfSameTypeAt(right, 0)) mask += (int)Sides.Right;
        if (HasTileOfSameTypeAt(right, bottom)) mask += (int)Sides.BottomRight;
        if (HasTileOfSameTypeAt(0, bottom)) mask += (int)Sides.Bottom;
        if (HasTileOfSameTypeAt(left, bottom)) mask += (int)Sides.BottomLeft;


        Debug.Log(SpriteRenderer.sprite.name);
        Debug.Log((Sides)mask);
    }

    public void Update()
    {
        var mask = GetMask();

        if (!Equals(_lastMask, mask))
        {
            _lastMask = mask;
            var sprite = MaskToSprite(mask);
            SpriteRenderer.sprite = sprite;
        }
    }

    private int GetMask()
    {
        var mask = 0;

        var left = -1;
        var top = 1;
        var right = 1;
        var bottom = -1;

        if (HasTileOfSameTypeAt(left, 0)) mask += (int)Sides.Left;
        if (HasTileOfSameTypeAt(left, top)) mask += (int)Sides.TopLeft;
        if (HasTileOfSameTypeAt(0, top)) mask += (int)Sides.Top;
        if (HasTileOfSameTypeAt(right, top)) mask += (int)Sides.TopRight;
        if (HasTileOfSameTypeAt(right, 0)) mask += (int)Sides.Right;
        if (HasTileOfSameTypeAt(right, bottom)) mask += (int)Sides.BottomRight;
        if (HasTileOfSameTypeAt(0, bottom)) mask += (int)Sides.Bottom;
        if (HasTileOfSameTypeAt(left, bottom)) mask += (int)Sides.BottomLeft;

        return mask;
    }

    private bool HasTileOfSameTypeAt(int x, int y)
    {
        return LevelController.Instance.HasTileOfSameTypeAt(this, x, y);
    }

    private Sprite MaskToSprite(int mask)
    {
        return Sprites.GetSprite(Type, mask);
    }
}