using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyViewer : MonoBehaviour
{
    [SerializeField] private Wallet _playerWallet;
    [SerializeField] private TMP_Text _valueText;

    private void OnEnable()
    {
        _playerWallet.ValueChanged += SetValue;
    }

    private void OnDisable()
    {
        _playerWallet.ValueChanged -= SetValue;
    }

    private void SetValue(int value)
    {
        _valueText.text = value.ToString();
    }
}
