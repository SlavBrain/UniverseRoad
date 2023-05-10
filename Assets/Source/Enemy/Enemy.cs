using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _reward;
    [SerializeField] private int _globalReward=1;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Health _health;
    [SerializeField] private int _damage = 1;
    private float _minDistanseToTarget = 0.01f;
    private Health _target;

    public event Action<Enemy> Died;

    public Health Health => _health;
    public int Reward => _reward;
    public int GlobalReward => _globalReward;

    private void OnEnable()
    {
        _health.Die += OnDie;
    }

    private void OnDisable()
    {
        _health.Die -= OnDie;
    }

    private void Update()
    {
        if (Vector3.Distance(_target.transform.position, transform.position) > _minDistanseToTarget)
        {
            MoveToTarget();
        }
        else
        {
            TakeDamage();
            OnDie();
        }
    }

    public void Initialization(Health target)
    {
        _target = target;
    }

    private void TakeDamage()
    {
        _target.ApplyDamage(_damage);
    }

    private void OnDie()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _maxSpeed);
    }
}
