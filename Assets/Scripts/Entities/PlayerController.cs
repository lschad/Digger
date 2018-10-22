using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance
    {
        get { return FindObjectOfType<PlayerController>(); }
    }

    public static Vector3 Position
    {
        get { return Instance.transform.position; }
        private set { Instance.transform.position = value; }
    }
}