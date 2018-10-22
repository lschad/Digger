using UnityEngine;

public static class GameObjectExtensions
{
    public static bool HasComponent<T>(this GameObject obj)
    {
        return obj.GetComponent<T>() != null;
    }
}