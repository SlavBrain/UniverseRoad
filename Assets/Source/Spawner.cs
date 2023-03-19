using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] protected List<GameObject> Templates;

    private void OnEnable()
    {
        GetExternalData();

        foreach(GameObject template in Templates)
        {
            FillPool(template);
        }
    }

    public void SetTemplates(IReadOnlyCollection<GameObject> templates)
    {
        Templates.Clear();

        foreach(GameObject template in templates)
        {
            Templates.Add(template);
        }
    }

    public GameObject SpawnObjectInParent(Transform parent)
    {
        var spawned = SpawnObject(parent.position);
        spawned.transform.SetParent(parent);
        return spawned;
    }
    public virtual GameObject SpawnObject(Vector3 spawnPosition)
    {
        var spawned=GetObject();
        SetObject(spawned, spawnPosition);
        return spawned;
    }

    protected void SetObject(GameObject template, Vector3 spawnPosition)
    {
        template.SetActive(true);
        template.transform.position = spawnPosition;
    }

    protected virtual void GetExternalData()
    {
        return;
    }
}
