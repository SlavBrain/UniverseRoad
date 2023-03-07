using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] protected GameObject[] Templates;

    private void OnEnable()
    {
        GetExternalData();

        foreach(GameObject template in Templates)
        {
            Initialize(template);
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

    private void OnValidate()
    {
        if (Templates.Length == 0)
        {
            Debug.LogError(gameObject.name + ": need add templates for spawn");
        }
    }
}
