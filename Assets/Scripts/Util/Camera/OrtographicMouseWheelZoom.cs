using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrtographicMouseWheelZoom : MonoBehaviour
{
    [Range(0.1f, 10.0f)]
    public float ZoomSpeed = 2f;

    [Range(0, 100)]
    public int MinZoom = 10;
    [Range(1, 100)]
    public int MaxZoom = 50;

    public bool Inverted;

    private Camera Camera
    {
        get { return GetComponent<Camera>(); }
    }

    public void Update()
    {
        if (Camera.orthographic == false)
        {
            throw new Exception("Camera needs to be ortographic");
        }
        
        var zoomSpeed = ZoomSpeed;
        if (Inverted)
        {
            zoomSpeed *= -1;
        }

        var size = Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        if (Camera.orthographicSize >= MaxZoom && size > 0) return;
        if (Camera.orthographicSize <= MinZoom && size < 0) return;
        Camera.orthographicSize += size;
    }
}