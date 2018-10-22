using UnityEngine;

public class LoopingSprite : MonoBehaviour
{
    [Range(1, 1000)]
    public int MovementSpeed = 10;
    public Sprite Sprite;
    public Sprite SpriteMask;

    public SortingLayer Layer;

    private GameObject _sprite1;
    private GameObject _sprite2;
    private GameObject _spriteMask;

    public void Start()
    {
        _sprite1 = transform.GetNewEmptyChild("Sprite1");
        _sprite2 = transform.GetNewEmptyChild("Sprite2");
        _spriteMask = transform.GetNewEmptyChild("SpriteMask");

        SetupSprite(_sprite1);
        SetupSprite(_sprite2, -1);
        SetupSpriteMask(_spriteMask);
    }

    public void Update()
    {
        MoveSprite(_sprite1);
        MoveSprite(_sprite2);
    }

    private void MoveSprite(GameObject sprite)
    {
        ResetPositionIfOutOfBounds(sprite);
        var speed = MovementSpeed / 1000f;

        sprite.transform.localPosition  += new Vector3(speed, 0, 0);
    }

    private void ResetPositionIfOutOfBounds(GameObject sprite)
    {
        var pos = sprite.transform.localPosition;
        var newPos = pos;
        if (pos.x > 1) newPos.x = -1;

        sprite.transform.localPosition  = newPos;
    }

    private void SetupSprite(GameObject sprite, int offset = 0)
    {
        var component = sprite.AddComponent<SpriteRenderer>();
        component.sprite = Sprite;
        component.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        sprite.transform.position = Vector3.zero;
        sprite.transform.localPosition = new Vector3(offset, 0, 0);
        component.sortingLayerName = "default";
        component.sortingOrder = 99;
    }

    private void SetupSpriteMask(GameObject spriteMask)
    {
        var component = spriteMask.AddComponent<SpriteMask>();
        component.sprite = SpriteMask;
        component.transform.localPosition = Vector3.zero;
    }
}