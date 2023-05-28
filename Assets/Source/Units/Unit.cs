using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject _avatar;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private LayerMask _unitLayer;
    [SerializeField] private int _level;
    private Weapon _weapon;
    private Animator _animator;
    private EnemySpawner _enemySpawner;
    private GameObject _currentTarget;
    private readonly float _merdgeRadius = 1;
    private Collider[] nearUnits;
    private Unit _nearUnit;
    private bool _isCanUpgrade = true;

    public event Action<Unit, Unit> Merged;

    public int Level => _level;
    public Weapon Weapon => _weapon;

    private void OnEnable()
    {
        _enemySpawner = GetComponentInParent<LevelConfigurator>().EnemySpawner;
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _weapon.RequiredNewTarget -= FindTarget;
        Merged = null;
    }

    public void Initialize(Weapon weapon, int level, bool isCanUpgrade)
    {
        _level = level;
        _isCanUpgrade = isCanUpgrade;

        if (_weapon != null)
        {
            _weapon.RequiredNewTarget -= FindTarget;
        }

        _weapon = Instantiate(weapon, _rightHand.position, Quaternion.Euler(Vector3.zero), _rightHand);
        ChangeAnimatorControllerByWeapon(_weapon);
        _weapon.RequiredNewTarget += FindTarget;
    }

    public void Merge()
    {
        if (_isCanUpgrade && HasSimilarUnitsAround(out _nearUnit))
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

    private void ChangeAnimatorControllerByWeapon(Weapon weapon)
    {
        if(weapon.AnimatorOverrideController!=null)
            _animator.runtimeAnimatorController = _weapon.AnimatorOverrideController;
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
