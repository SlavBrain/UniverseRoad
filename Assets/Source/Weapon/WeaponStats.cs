using UnityEngine;

[CreateAssetMenu(fileName ="WeaponStats/NewStats")]
public class WeaponStats : ScriptableObject
{
    [Header("WeaponStats")]
    [SerializeField] private TargetFind _targetFind;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private Reloading _reloading;
    [SerializeField] private Bullet _bullet;

    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private float _reloadTime = 2;
    [SerializeField] private int _maxBulletCount = 30;

    [Header("BulletStats")]
    [SerializeField] private float _speed = 1;
    [SerializeField] protected int _damage = 1;
    [SerializeField] private AfterHitAction _afterHitAction;

    public int DPS => (int)(_damage * _shootDelay * _speed);
    public float ReloadTime => _reloadTime;
    public int MaxBulletCount => _maxBulletCount;
    public TargetFind TargetFind => _targetFind;
    public AfterHitAction Spell => _afterHitAction;
}
