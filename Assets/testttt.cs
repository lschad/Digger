using UnityEngine;

public class testttt : MonoBehaviour
{
    [Range(0, 15)] public int X;
    [Range(0, 15)] public int Y;
    public Texture2D TEX;

    // Update is called once per frame
    public void Update()
    {
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(TEX, new Rect(X * 64, Y * 64, 64, 64), new Vector2(0.5f, 0.5f), 64);
    }
}