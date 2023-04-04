using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private int _currentRang;
    [SerializeField] private WeaponСharacteristics[] _characteristics;

    [SerializeField]private GameObject _target;
    private int _bulletsCountBeforeReload;
    private Reloading _reloading;

    public event Action<Weapon> RequiredNewTarget;
    public event Action BulletsEnded;
    public event Action DoShoot;
    
    public Vector3 TargetPoint => _target.transform.position;
    public Vector3 ShootingPoint => _shootingPoint.position;
    public float ReloadTime => _characteristics[_currentRang].ReloadTime;
    public float ShootDelay => _characteristics[_currentRang].ShootDelay;
    public int MaxBulletCount => _characteristics[_currentRang].MaxBulletCount;
    public TargetFind TargetFind => _characteristics[_currentRang].TargetFind;
    public Bullet Bullet => _characteristics[_currentRang].Bullet;
    private Shooting _shooting => _characteristics[_currentRang].Shooting;
    private bool IsBulletsHave=>_bulletsCountBeforeReload > 0;

    private void Awake()
    {
        _reloading = new Reloading(this);
    }

    private void OnEnable()
    {
        transform.SetLocalPositionAndRotation(transform.localPosition, Quaternion.Euler(Vector3.zero));
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
            if (_target!=null&&_target.activeSelf)
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

    public void SetRang(int rang)
    {
        _currentRang = Mathf.Clamp(rang, 1, _characteristics.Length)-1;
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
        _bulletsCountBeforeReload = MaxBulletCount;
    }

    private void OnValidate()
    {
        if (_characteristics.Length == 0)
        {
            Debug.LogError(gameObject.name+": not found WeaponCharacteristics");
        }
    }

    [Serializable]
    private struct WeaponСharacteristics
    {
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _shootDelay;
        [SerializeField] private int _maxBulletCount;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private TargetFind _targetFind;
        [SerializeField] private Shooting _shooting;

        public float ReloadTime => _reloadTime;
        public float ShootDelay => _shootDelay;
        public int MaxBulletCount => _maxBulletCount;
        public Bullet Bullet => _bullet;
        public TargetFind TargetFind => _targetFind;
        public Shooting Shooting => _shooting;

    }
}


