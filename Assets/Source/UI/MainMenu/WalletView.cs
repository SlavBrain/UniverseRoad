using System;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _walletValue;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        SetTextValue();
        _wallet.ValueChanged += SetTextValue;
    }

    private void OnDisable()
    {
        _wallet.ValueChanged -= SetTextValue;
    }

    private void SetTextValue()
    {
        _walletValue.text = _wallet.Value.ToString();
    }

    private void SetTextValue(int value)
    {
        _walletValue.text = value.ToString();
    }
}
