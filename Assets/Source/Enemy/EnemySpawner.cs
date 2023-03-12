using System;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private float _spawningDelay=1;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _spawningWidth=1;

    private float _elapsedTime = 0;

    public event Action<Enemy> EnemyKilled;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _spawningDelay)
        {
            SpawnEnemy();
            _elapsedTime = 0;
        }
    }

    private void SpawnEnemy()
    {
        var spawnedEnemy=SpawnObject(GetSpawnPoint()).GetComponent<Enemy>();
        spawnedEnemy.Died += OnEnemyDied;
        spawnedEnemy.Initialization(_target.transform.position);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        EnemyKilled?.Invoke(enemy);
    }

    private Vector3 GetSpawnPoint()
    {
        return transform.position + new Vector3(UnityEngine.Random.Range(-_spawningWidth / 2, _spawningWidth / 2),0,0);
    }

    private void OnValidate()
    {
        foreach(GameObject template in Templates)
        {
            if (!template.GetComponent<Enemy>())
            {
                Debug.LogError(gameObject.name + ": added GameObject is not Enemy");
            }
        }
    }
}
