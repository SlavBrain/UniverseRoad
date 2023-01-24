using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private TargetFind _targetFind;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private float _reloadTime = 2;
    [SerializeField] private int _numberOfMissle = 1;
    [SerializeField] private int _maxBulletCountInHorn = 30;
    [SerializeField] private GameObject _target;

    private Unit _parentUnit;
    private int _bulletsCountBeforeReload = 30;
    private int _bulletInQueue = 0;
    private bool _isShooting = false;
    private bool _isReloading = false;
    private Coroutine Shooting;

    public event Action<Weapon> RequiredNewTarget;

    public TargetFind TargetFind => _targetFind;

    private void OnEnable()
    {
        _parentUnit = GetComponentInParent<Unit>();
    }

    private void Update()
    {
        if (IsBulletsHave())
        {
            if (_target != null)
            {
                Shoot();
            }
            else
            {
                RequiredNewTarget?.Invoke(this);
            }
        }
        else
        {
            Reload();
        }
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    private bool IsBulletsHave()
    {
        return _bulletsCountBeforeReload > 0;
    }

    private void Reload()
    {
        if (!_isShooting && !_isReloading)
        {
            if (Shooting != null)
            {
                StopCoroutine(Shooting);
            }

            Shooting = StartCoroutine(Reloading());
        }
    }

    private void TakeBullet()
    {
        _bulletsCountBeforeReload--;
    }

    private void Shoot()
    {
        _bulletInQueue++;

        if (!_isShooting)
        {
            if (Shooting != null)
            {
                StopCoroutine(Shooting);
            }

            Shooting = StartCoroutine(CreatingBullets());
        }
    }

    private IEnumerator CreatingBullets()
    {
        _isShooting = true;
        WaitForSeconds delay = new WaitForSeconds(_shootDelay);

        while (_bulletInQueue > 0 && IsBulletsHave())
        {
            TakeBullet();
            Bullet bullet = Instantiate(_bullet, _shootingPoint.position, Quaternion.identity);
            bullet.Initialization(_target.transform.position);
            yield return delay;
        }

        _isShooting = false;
    }

    private IEnumerator Reloading()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        _bulletsCountBeforeReload = _maxBulletCountInHorn;
        _isReloading = false;
    }
}
