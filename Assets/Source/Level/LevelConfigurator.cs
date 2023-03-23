using UnityEngine;
using IJunior.TypedScenes;
using System.Collections.Generic;

public class LevelConfigurator : MonoBehaviour, ISceneLoadHandler<LevelConfig>
{
    public static LevelConfigurator instanse=null;

    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private EnemyWaveController _waveController;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField]private LevelConfig _config;

    public LevelGenerator LevelGenerator => _levelGenerator;
    public EnemySpawner EnemySpawner => _waveController.EnemySpawner;
    public UnitSpawner UnitSpawner => _unitSpawner;

    public void OnSceneLoaded(LevelConfig argument)
    {
        InitializeScene(argument);
    }

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

    private void InitializeScene(LevelConfig config)
    {
        _config = config;
        _unitSpawner.Initialize(_config);
        _waveController.Initialize(_config);
        _levelGenerator.Initialize(_config);
    }
}
