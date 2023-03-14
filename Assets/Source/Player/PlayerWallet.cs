using UnityEngine;

public class PlayerWallet : Wallet
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private void OnEnable()
    {
        _enemySpawner.EnemyKilled += OnEnemyKilled;
    }

    private void OnEnemyKilled(Enemy enemy)
    {
        AddMoney(enemy.Reward);
    }
}
