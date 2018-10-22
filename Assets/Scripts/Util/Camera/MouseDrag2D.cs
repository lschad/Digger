using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MouseDrag2D : MonoBehaviour
{
    public enum MouseButton
    {
        Left = 0,
        Right = 1
    }

    public MouseButton DragButton;
    [Range(0.5f, 5.0f)]
    public float DragSpeed = 1.0f;

    private Vector3 _start;
    private bool _dragging;

  
    private Camera Camera
    {
        get { return GetComponent<Camera>(); }
    }

    private void Update()
    {
        var mb = (int) DragButton;
        if (Input.GetMouseButtonDown(mb))
        {
            _dragging = true;
            _start = Input.mousePosition;
        }

        if (_dragging)
        {
            var pos = Camera.ScreenToViewportPoint(Input.mousePosition - _start);
            var move = new Vector3(pos.x * DragSpeed, pos.y * DragSpeed, 0);

            transform.Translate(move, Space.World);
        }

        if (Input.GetMouseButtonUp(mb))
        {
            _dragging = false;
            _start = Vector3.zero;
        }
    }
}