using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _reward;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _maxSpeed;

    [SerializeField]private int _currentHealth;
    private Vector3 _targetPoint;
    private Coroutine _moving;

    public event Action Died;

    public int CurrentHealth => _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    private void OnDisable()
    {
        if (_moving != null)
            StopCoroutine(_moving);
    }

    public void Initialization(Vector3 target)
    {
        _targetPoint = target;
        StartMoving();
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogError(gameObject.name + ": damage error");
        }

        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }

    private void StartMoving()
    {
        if (_moving != null)
            StopCoroutine(_moving);

        _moving = StartCoroutine(MovingToTarget());
    }

    private IEnumerator MovingToTarget()
    {
        while (Vector3.Distance(_targetPoint, transform.position) > 0.01)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint, Time.deltaTime * _maxSpeed);
            yield return null;
        }

        Die();
    }
}
