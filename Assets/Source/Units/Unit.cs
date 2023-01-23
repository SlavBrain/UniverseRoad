using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Weapon _weapon;

    [SerializeField]private EnemySpawner _enemySpawner;
    private GameObject _currentTarget;

    private void OnEnable()
    {
        _enemySpawner = GetComponentInParent<LevelConfiguration>().EnemySpawner;
    }

    private void Update()
    {
        if (_currentTarget == null)
        {
            FindTarget();
        }
    }

    public void Initialize(GameObject weapon)
    {
        _weapon = Instantiate(weapon, _rightHand.position, Quaternion.identity, _rightHand).GetComponent<Weapon>();
    }

    private void FindTarget()
    {
        _currentTarget = _weapon.TargetFind.FindTarget(_enemySpawner.Pool);

        if (_currentTarget != null)
        {
            _weapon.SetTarget(_currentTarget);
        }
    }
}
