using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLevelTask : LevelTask
{
    [SerializeField] private int _numberOfKills = 1;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField]private int _currentNumberOfKills = 0;
    private int _minNumberOfKills = 1;

    private void OnEnable()
    {
        if (_numberOfKills <= 0)
            _numberOfKills = _minNumberOfKills;
    }

    public override void StartTracking()
    {
        _enemySpawner.EnemyKilled += AddKill;
    }

    private void AddKill(Enemy enemy)
    {
        _currentNumberOfKills++;

        if (IsNumberOfKillsReach())
            StopTracking();
    }

    private bool IsNumberOfKillsReach()
    {
        return _currentNumberOfKills >= _numberOfKills;
    }
}
