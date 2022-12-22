using UnityEngine;

public abstract class Spawner : ObjectPool
{
    [SerializeField] GameObject[] _templates;

    private void OnEnable()
    {
        foreach(GameObject template in _templates)
        {
            Initialize(template);
        }
    }

    protected void SetObjectInParent(GameObject template, Transform parent)
    {
        template.transform.SetParent(parent);
        SetObject(template, parent.position);
    }

    protected void SetObject(GameObject template, Vector3 spawnPosition)
    {
        template.SetActive(true);
        template.transform.position = spawnPosition;
    }

    private void OnValidate()
    {
        if (_templates.Length == 0)
        {
            Debug.LogError(gameObject.name + ": need add templates for spawn");
        }
    }
}
