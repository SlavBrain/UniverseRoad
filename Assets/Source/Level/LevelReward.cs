using UnityEngine;

public class LevelReward : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private int _currentCoinValue;
    [SerializeField] private LevelEndConfig _levelEndConfig;
    public int CoinValue => _currentCoinValue;

    private void OnEnable()
    {
        _currentCoinValue = 0;
        _enemySpawner.EnemyKilled += OnEnemyKilled;
    }

    private void OnEnemyKilled(Enemy enemy)
    {
        ChangeCoinValue(enemy.GlobalReward);
    }

    private void ChangeCoinValue(int value)
    {
        _currentCoinValue += value;
        _levelEndConfig.SetRewardCoinValue(_currentCoinValue);
    }
}
