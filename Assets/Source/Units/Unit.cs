using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject _avatar;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private LayerMask _unitLayer;
    [SerializeField] private int _level;
    private int _maxLevel = 5;
    private Weapon _weapon;
    private EnemySpawner _enemySpawner;
    private GameObject _currentTarget;
    private float _merdgeRadius = 1;
    [SerializeField] private Collider[] nearUnits;
    [SerializeField] private Unit _nearUnit;
    public event Action<Unit, Unit> Merged;

    public int Level => _level;

    private void OnEnable()
    {
        _enemySpawner = GetComponentInParent<LevelConfiguration>().EnemySpawner;
    }

    private void OnDisable()
    {
        _weapon.RequiredNewTarget -= FindTarget;
        Merged = null;
    }

    public void Initialize(Weapon weapon, int level)
    {
        _level = Math.Clamp(level, 0, _maxLevel);
        Debug.Log(_level);

        if (_weapon != null)
        {
            _weapon.RequiredNewTarget -= FindTarget;
        }

        _weapon = Instantiate(weapon, _rightHand.position, Quaternion.identity, _rightHand);
        _weapon.RequiredNewTarget += FindTarget;
    }
    public void Merge()
    {
        if (HasSimilarUnitsAround(out _nearUnit))
        {
            Merged?.Invoke(this, _nearUnit);
        }
    }

    public void Destoy()
    {
        Destroy(gameObject);

    }

    private bool HasSimilarUnitsAround(out Unit nearestUnit)
    {
        nearUnits = Physics.OverlapSphere(transform.position, _merdgeRadius, _unitLayer);
        var nearestUnitCollider = nearUnits.OrderBy(collider => Vector3.Distance(transform.position, collider.transform.position))
            .Where(collider => collider.TryGetComponent(out Unit nearUnits)).ToList()
            .Except(new Collider[] { this.GetComponent<Collider>() }).ToList()
            .FirstOrDefault(unit => unit.GetComponent<Unit>().Level == _level);

        if (nearestUnitCollider != null)
            nearestUnit = nearestUnitCollider.GetComponent<Unit>();
        else
            nearestUnit = null;


        return nearestUnitCollider != null;
    }

    private void FindTarget(Weapon weapon)
    {
        _currentTarget = weapon.TargetFind.FindTarget(_enemySpawner.Pool);

        if (_currentTarget != null)
        {
            weapon.SetTarget(_currentTarget);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        if (_currentTarget != null)
        {
            Gizmos.DrawLine(transform.position, _currentTarget.transform.position);
        }
    }
}
