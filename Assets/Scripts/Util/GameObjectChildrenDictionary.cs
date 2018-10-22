using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectChildrenDictionary : IEnumerable<GameObject>
{
    private readonly GameObject _parent;
    private readonly IDictionary<string, GameObject> _objects = new Dictionary<string, GameObject>();

    public GameObjectChildrenDictionary(GameObject parent)
    {
        _parent = parent;
    }

    public GameObject this[TileType type]
    {
        get { return this[type.ToString()]; }
    }

    public GameObject this[string key]
    {
        get
        {
            if (!_objects.ContainsKey(key))
            {
                _objects.Add(key, _parent.transform.GetNewEmptyChild(key));
            }

            return _objects[key];
        }
    }

    public IEnumerator<GameObject> GetEnumerator()
    {
        return _objects
               .ToList()
               .Select(kv => kv.Value)
               .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}