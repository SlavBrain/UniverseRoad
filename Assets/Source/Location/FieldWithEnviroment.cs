using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldWithEnviroment : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] private int _fillingChanse = 50;
    [SerializeField] private Transform[] _spawnPointsBigObject;
    [SerializeField] private Transform[] _spawnPointsSmallObjects;
    [SerializeField] private List<GameObject> _enviroments;

    private Spawner _bigObjectSpawner;
    private Spawner _smallObjectSpawner;    

    private void OnEnable()
    {
        TrySpawnEnviroment(_bigObjectSpawner, _spawnPointsBigObject);
        TrySpawnEnviroment(_smallObjectSpawner, _spawnPointsSmallObjects);
    }

    private void OnDisable()
    {
        foreach(GameObject enviroment in _enviroments)
        {
            enviroment.SetActive(false);
        }

        _enviroments.Clear();
    }

    public void Initialize(Spawner bigObjectSpawner, Spawner smallObjectSpawner)
    {
        _bigObjectSpawner = bigObjectSpawner;
        _smallObjectSpawner = smallObjectSpawner;
    }

    private bool TrySpawnEnviroment(Spawner spawner,Transform[] points)
    {
        if (spawner != null && points.Length != 0)
        {
            SetObjectsToSpawnPoints(spawner, points);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetObjectsToSpawnPoints(Spawner spawner, Transform[] points)
    {
        foreach (Transform point in points)
        {
            if (Random.Range(0, 100) < _fillingChanse)
            {
                GameObject enviroment=spawner.SpawnObjectInParent(point);
                _enviroments.Add(enviroment);
            }
        }
    }
}
