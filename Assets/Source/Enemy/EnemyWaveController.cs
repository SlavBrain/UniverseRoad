using System;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    [SerializeField] private EnemyWaveConfig[] _configs;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private WaveProgress _waveProgress;

    [SerializeField]private int _currentWave = 0;

    public event Action WaveChanged;
    public event Action WavesEnded;
    
    public EnemySpawner EnemySpawner => _enemySpawner;

    public void Initialize(LevelConfig levelConfig)
    {
        _configs = levelConfig.EnemyWaveConfig;
        StartWave();
    }

    private void StartWave()
    {
        Debug.Log(_currentWave+" wave "+Time.realtimeSinceStartup);
        _enemySpawner.Initialize(_configs[_currentWave]);
        StartTrackingWaveProgress();
    }

    private void GoToNextWave()
    {
        _waveProgress.TimeIsUp -= GoToNextWave;
        _currentWave++;
        
        if (_currentWave < _configs.Length)
        {
            StartWave();
            WaveChanged?.Invoke();
        }
        else
        {
            WavesEnded?.Invoke();
        }
    }

    private void StartTrackingWaveProgress()
    {
        _waveProgress.Initialize(_configs[_currentWave].Duration);
        _waveProgress.TimeIsUp += GoToNextWave;
    }
}
