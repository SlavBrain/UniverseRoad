using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private float _reloadTime = 2;
    [SerializeField] private int _numberOfMissle = 1;
    [SerializeField] private int _maxBulletCountInHorn = 30;
    //[SerializeField] private Transform _target;
    Vector3 _target = new Vector3(0, 0, 40);

    private int _bulletsCountBeforeReload = 30;
    private int _bulletInQueue = 0;
    private bool _isShooting = false;
    private bool _isReloading = false;
    private Coroutine Shooting;

    private void Update()
    {
        if (IsBulletsHave())
        {
            Shoot();
        }
        else
        {
            Reload();
        }
    }

    private bool IsBulletsHave()
    {
        return _bulletsCountBeforeReload > 0;
    }

    private void Reload()
    {
        if (!_isShooting&&!_isReloading)
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

        while (_bulletInQueue > 0&&IsBulletsHave())
        {
            TakeBullet();
            Bullet bullet = Instantiate(_bullet, _shootingPoint.position, Quaternion.identity);
            bullet.Initialization(_target);
            yield return delay;
        }

        _isShooting = false;
    }

    private IEnumerator Reloading()
    {
        Debug.Log("Reload");
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        _bulletsCountBeforeReload = _maxBulletCountInHorn;
        _isReloading = false;
    }
}
