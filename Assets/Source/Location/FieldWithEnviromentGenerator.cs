using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldWithEnviromentGenerator : Spawner
{
    [SerializeField] private Spawner _bigObjectSpawner;
    [SerializeField] private Spawner _smallObjectSpawner;

    public void Initialize(LevelConfig config)
    {
        SetTemplates(config.EnvironmentConfig.OffRoadTemplates);
        _bigObjectSpawner.SetTemplates(config.EnvironmentConfig.BigObjectTemplates);
        _smallObjectSpawner.SetTemplates(config.EnvironmentConfig.SmallObjectTemplates);
    }

    public override GameObject SpawnObject(Vector3 spawnPosition)
    {
        var spawned = GetObject();
        var spawnedField=spawned.GetComponent<FieldWithEnviroment>();
        spawnedField.Initialize(_bigObjectSpawner, _smallObjectSpawner);
        SetObject(spawned, spawnPosition);
        return spawned;
    }

    private void OnValidate()
    {
        foreach(GameObject template in Templates)
        {
            if(!template.TryGetComponent<FieldWithEnviroment>(out FieldWithEnviroment field))
            {
                Debug.LogError(gameObject.name + ": object in generator not FieldWithEnviroment");
            }
        }
    }
}
