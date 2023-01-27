using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] protected GameObject[] _templates;

    private void OnEnable()
    {
        GetExternalData();

        foreach(GameObject template in _templates)
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

    }

    private void OnValidate()
    {
        if (_templates.Length == 0)
        {
            Debug.LogError(gameObject.name + ": need add templates for spawn");
        }
    }
}
