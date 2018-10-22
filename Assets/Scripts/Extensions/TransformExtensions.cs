using UnityEngine;

public static class TransformExtensions
{
    public static void AddChild(this Transform parent, GameObject child, bool worldPositionStays = true)
    {
        child.transform.SetParent(parent, worldPositionStays);
    }

    /// <remarks>
    ///     https://stackoverflow.com/questions/46358717/how-to-loop-through-and-destroy-all-children-of-a-game-object-in-unity
    /// </remarks>
    public static void DestroyChildren(this Transform parent)
    {
        var allChildren = new GameObject[parent.childCount];

        for (var i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            allChildren[i] = child.gameObject;
        }
        
        foreach (var child in allChildren)
        {
            Object.DestroyImmediate(child.gameObject);
        }
    }

    public static GameObject GetNewEmptyChild(this Transform parent, string name = "")
    {
        var child = new GameObject(name);
        parent.AddChild(child);
        return child;
    }

}
