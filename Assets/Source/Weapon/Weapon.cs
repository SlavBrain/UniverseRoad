using System;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour,IFindTargetSelectioner
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private WeaponСharacteristics[] _characteristics;
    
    private int _currentRang;
    private GameObject _target;
    private int _bulletsCountBeforeReload;
    private Reloading _reloading;
    private TargetFind _targetFind;
    public event Action<Weapon> RequiredNewTarget;
    public event Action BulletsEnded;
    public event Action DoShoot;
    
    public Vector3 TargetPoint => _target.transform.position;
    public Vector3 ShootingPoint => _shootingPoint.position;
    public float ReloadTime => _characteristics[_currentRang].ReloadTime;
    public float ShootDelay => _characteristics[_currentRang].ShootDelay;
    public int MaxBulletCount => _characteristics[_currentRang].MaxBulletCount;
    public FindTargetVariations FindTargetVariant => _characteristics[_currentRang].FindTargetVariant;
    private Shooting _shooting => _characteristics[_currentRang].Shooting;
    
    public Bullet Bullet => _characteristics[_currentRang].BulletСharacteristics.Bullet;
    public float BulletSpeed => _characteristics[_currentRang].BulletСharacteristics.Speed;
    public int BulletDamage => _characteristics[_currentRang].BulletСharacteristics.Damage;
    public AfterHitActionVariation BulletAfterHitActionVariation => _characteristics[_currentRang].BulletСharacteristics.AfterHitActionVariant;

    public TargetFind TargetFind => _targetFind;
    private bool IsBulletsHave=>_bulletsCountBeforeReload > 0;

    private void Awake()
    {
        _reloading = new Reloading(this);
        
        IFindTargetSelectioner weaponFindTargetSelectioner =this as IFindTargetSelectioner;
        weaponFindTargetSelectioner.SetFindTargetVariant(FindTargetVariant);
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
        [SerializeField] private FindTargetVariations _findTargetVariant;
        [SerializeField] private Shooting _shooting;
        [SerializeField] private BulletСharacteristics _bulletСharacteristics;
        
        public float ReloadTime => _reloadTime;
        public float ShootDelay => _shootDelay;
        public int MaxBulletCount => _maxBulletCount;
        public FindTargetVariations FindTargetVariant => _findTargetVariant;
        public Shooting Shooting => _shooting;
        public BulletСharacteristics BulletСharacteristics => _bulletСharacteristics;
    }
    
    [Serializable]
    private struct BulletСharacteristics
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private AfterHitActionVariation _afterHitActionVariant;
        
        public Bullet Bullet => _bullet;
        public AfterHitActionVariation AfterHitActionVariant => _afterHitActionVariant;
        public float Speed => _speed;
        public int Damage => _damage;
    }

    void IFindTargetSelectioner.OnRandomSelected()
    {
        _targetFind = gameObject.AddComponent(typeof(FindRandomTarget)) as TargetFind;
    }

    void IFindTargetSelectioner.OnNearSelected()
    {
        _targetFind = gameObject.AddComponent(typeof(FindNearTarget)) as TargetFind;
    }

    void IFindTargetSelectioner.OnLowHPSelected()
    {
        _targetFind = gameObject.AddComponent(typeof(FindLowHPTarget)) as TargetFind;
    }

    void IFindTargetSelectioner.OnHighHPSelected()
    {
        _targetFind = gameObject.AddComponent(typeof(FindHighHPTarget)) as TargetFind;
    }
}


