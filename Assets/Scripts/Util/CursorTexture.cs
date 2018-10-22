using UnityEngine;

public class CursorTexture : MonoBehaviour
{
    public float MovementSpeed = float.MaxValue;
    public Texture2D Texture;

    public void Start()
    {
        UnityEngine.Cursor.SetCursor(Texture, Vector2.zero, CursorMode.Auto);
    }
}