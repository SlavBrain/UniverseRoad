using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfiguration : MonoBehaviour
{
    public static LevelConfiguration instanse=null;

    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameObject _player;

    public LevelGenerator LevelGenerator => _levelGenerator;
    public EnemySpawner EnemySpawner => _enemySpawner;
    public GameObject Player => _player;

    private void Start()
    {
        if (instanse == null)
        {
            instanse = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
