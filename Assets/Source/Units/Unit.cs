using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Weapon _weapon;

    [SerializeField] private EnemySpawner _enemySpawner;
    private GameObject _currentTarget;

    private void OnEnable()
    {
        _enemySpawner = GetComponentInParent<LevelConfiguration>().EnemySpawner;
    }

    private void OnDisable()
    {
        _weapon.RequiredNewTarget -= FindTarget;
    }

    public void Initialize(GameObject weapon)
    {
        if (_weapon != null)
        {
            _weapon.RequiredNewTarget -= FindTarget;
        }

        _weapon = Instantiate(weapon, _rightHand.position, Quaternion.identity, _rightHand).GetComponent<Weapon>();
        _weapon.RequiredNewTarget += FindTarget;
    }

    private void FindTarget(Weapon weapon)
    {
        _currentTarget = weapon.TargetFind.FindTarget(_enemySpawner.Pool);

        if (_currentTarget != null)
        {
            weapon.SetTarget(_currentTarget);
        }
    }
}
