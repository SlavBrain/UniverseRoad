using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue = 10;
    [SerializeField]private int _currentValue;

    public int CurrentValue => _currentValue;

    public event Action Die;
    public event Action<int> ValueChanged;

    private void Start()
    {
        _currentValue = _maxValue;
        ValueChanged?.Invoke(_currentValue);
    }

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            Debug.LogWarning(gameObject.name + ": apply negative damage");

        if (damage >= _currentValue)
        {
            _currentValue = 0;
            Die?.Invoke();
        }
        else
        {
            _currentValue -= damage;
        }

        ValueChanged?.Invoke(_currentValue);
    }
}
