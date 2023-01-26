using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _shootingPoint;

    [SerializeField] private TargetFind _targetFind;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private Reloading _reloading;
    [SerializeField] private Bullet _bullet;

    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private float _reloadTime = 2;
    [SerializeField] private int _maxBulletCount = 30;

    private GameObject _target;
    [SerializeField]private int _bulletsCountBeforeReload;

    public event Action<Weapon> RequiredNewTarget;
    public event Action BulletsEnded;
    public event Action DoShoot;
    
    public float ReloadTime => _reloadTime;
    public float ShootDelay => _shootDelay;
    public TargetFind TargetFind => _targetFind;
    public Vector3 ShootingPoint => _shootingPoint.position;
    public Bullet Bullet => _bullet;
    public Vector3 TargetPoint => _target.transform.position;
    private bool IsBulletsHave=>_bulletsCountBeforeReload > 0;

    private void OnEnable()
    {
        FillOfBullet();
        _reloading.Reloaded += FillOfBullet;
        _shooting.TookBullet += TakeBullet;
        _shooting.ShootingEnded += Reload;
    }

    private void OnDisable()
    {
        _reloading.Reloaded -= FillOfBullet;
        _shooting.TookBullet -= TakeBullet;
        _shooting.ShootingEnded -= Reload;
    }

    private void Update()
    {
        if (IsBulletsHave)
        {
            if (_target != null)
            {
                DoShoot?.Invoke();
            }
            else
            {
                RequiredNewTarget?.Invoke(this);
            }
        }
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    private void Reload()
    {
        BulletsEnded?.Invoke();
    }

    private void TakeBullet()
    {
        _bulletsCountBeforeReload--;
    }

    private void FillOfBullet()
    {
        Debug.LogWarning("fill");
        _bulletsCountBeforeReload = _maxBulletCount;
    }
}
