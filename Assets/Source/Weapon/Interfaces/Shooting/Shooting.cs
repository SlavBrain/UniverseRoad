using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class Shooting : MonoBehaviour, IShooting
{
    private Weapon _weapon;
    [SerializeField]private int _bulletInQueue = 0;
    private bool _isShooting = false;
    protected Coroutine _munitionLaunching;
    public event Action ShootingEnded;
    public event Action TookBullet;

    private void OnEnable()
    {
        _weapon = GetComponent<Weapon>();
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
            Debug.Log("startShoot");
            if (_munitionLaunching != null)
            {
                StopCoroutine(_munitionLaunching);
            }

            _munitionLaunching = StartCoroutine(CreatingBullets());
        }
    }

    private IEnumerator CreatingBullets()
    {
        _isShooting = true;
        WaitForSeconds delay = new WaitForSeconds(_weapon.ShootDelay);

        while (_bulletInQueue > 0)
        {
            if (_weapon.TargetPoint != null)
            {
                Bullet bullet = Instantiate(_weapon.Bullet, _weapon.ShootingPoint, Quaternion.identity);
                _bulletInQueue--;
                bullet.Initialization(_weapon.TargetPoint);
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
