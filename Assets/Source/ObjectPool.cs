using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject Container;
    [SerializeField] protected int Capacity;
    private List<GameObject> _pool = new List<GameObject>();

    public IReadOnlyList<GameObject> Pool => _pool;

    protected void Initialize(GameObject prefab)
    {
        for(int i = 0; i < Capacity; i++)
        {
            AddObject(prefab);
        }        
    }

    protected GameObject GetObject()
    {
        if(TryGetObject(out GameObject result))
        {
            return result;
        }
        else
        {            
            return AddObject();
        }

    }

    private bool TryGetObject(out GameObject result)
    {
        result = _pool.OrderBy(objectInPool =>  Guid.NewGuid()).FirstOrDefault(objectInPool => objectInPool.activeSelf == false);

        return result != null;
    }

    private GameObject AddObject(GameObject prefab)
    {
        GameObject spawned = Instantiate(prefab, Container.transform);
        spawned.SetActive(false);
        _pool.Add(spawned);
        return spawned;
    }

    private GameObject AddObject()
    {
        if (_pool.Count == 0)
          throw new System.Exception();

        GameObject spawned = Instantiate(_pool[UnityEngine.Random.Range(0,_pool.Count)], Container.transform);
        spawned.SetActive(false);
        _pool.Add(spawned);
        return spawned;
    }

    private void OnValidate()
    {
        if (Container == null)
        {
            Debug.LogError(gameObject.name+": need add pool container for ");
        }

        if (Capacity == 0)
        {
            Debug.LogWarning(gameObject.name + ": object pool capacity is 0");
        }
    }
}
