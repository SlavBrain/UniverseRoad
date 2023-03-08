using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(BulletSpawner))]
public class Shooting : MonoBehaviour, IShooting
{
    private BulletSpawner _bulletSpawner;
    private Weapon _weapon;
    private int _bulletInQueue = 0;
    private bool _isShooting = false;
    private Animator _unitAnimator;
    private Coroutine _munitionLaunching;
    private string HashAnimatorIsShoot = "IsShoot";
    public event Action ShootingEnded;
    public event Action TookBullet;

    private void OnEnable()
    {
        GetComponentInParent<Unit>().TryGetComponent<Animator>(out _unitAnimator);
        _weapon = GetComponent<Weapon>();
        _bulletSpawner = GetComponent<BulletSpawner>();
        _weapon.DoShoot += Shoot;
    }

    private void OnDisable()
    {
        _weapon.DoShoot -= Shoot;
    }

    public void Shoot()
    {
        TookBullet?.Invoke();
        _bulletInQueue++;

        if (!_isShooting)
        {
            if (_munitionLaunching != null)
            {
                StopCoroutine(_munitionLaunching);
            }

            _munitionLaunching = StartCoroutine(CreatingBullets());
        }
    }

    private void PlayShootAnimation()
    {
        if (_unitAnimator != null)
            _unitAnimator.SetTrigger(HashAnimatorIsShoot);
    }

    private IEnumerator CreatingBullets()
    {
        _isShooting = true;
        WaitForSeconds delay = new WaitForSeconds(_weapon.ShootDelay);

        while (_bulletInQueue > 0)
        {
            if (_weapon.TargetPoint != null)
            {
                _bulletInQueue--;
                Bullet bullet = _bulletSpawner.SpawnObject(_weapon.ShootingPoint).GetComponent<Bullet>();
                bullet.Initialization(_weapon.TargetPoint);
                PlayShootAnimation();
                yield return delay;
            }
            else
            {
                yield return null;
            }
        }

        ShootingEnded.Invoke();
        _isShooting = false;
    }
}
