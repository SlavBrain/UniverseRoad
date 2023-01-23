using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;
    private List<GameObject> _pool = new List<GameObject>();

    public IReadOnlyList<GameObject> Pool => _pool;

    protected void Initialize(GameObject prefab)
    {
        for(int i = 0; i < _capacity; i++)
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
        result = _pool.FirstOrDefault(objectInPool => objectInPool.activeSelf == false);

        return result != null;
    }

    private GameObject AddObject(GameObject prefab)
    {
        GameObject spawned = Instantiate(prefab, _container.transform);
        spawned.SetActive(false);
        _pool.Add(spawned);
        return spawned;
    }

    private GameObject AddObject()
    {
        if (_pool.Count == 0)
          throw new System.Exception();

        GameObject spawned = Instantiate(_pool[Random.Range(0,_pool.Count)], _container.transform);
        spawned.SetActive(false);
        _pool.Add(spawned);
        return spawned;
    }

    private void OnValidate()
    {
        if (_container == null)
        {
            Debug.LogError(gameObject.name+": need add pool container for ");
        }

        if (_capacity == 0)
        {
            Debug.LogWarning(gameObject.name + ": object pool capacity is 0");
        }
    }
}
