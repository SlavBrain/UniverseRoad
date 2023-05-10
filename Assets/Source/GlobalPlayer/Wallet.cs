using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _value=0;
    
    public event Action<int> ValueChanged;

    public int Value => _value;

    private void Start()
    {
        ValueChanged?.Invoke(_value);
    }

    public void AddMoney(int addingValue)
    {
        if (addingValue <= 0)
        {
            Debug.LogError(gameObject.name + ": addingValue <=0");
            return;
        }

        _value += addingValue;
        ValueChanged?.Invoke(_value);
    }

    public bool TrySpendMoney(int value)
    {
        if (value < 0)
        {
            Debug.LogError(gameObject.name + ": removingValue <=0");
            return false;
        }

        if (_value >= value)
        {
            _value -= value;
            ValueChanged?.Invoke(_value);
            return true;
        }
        else
        {
            return false;
        }
    }
}
