using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _wallet.ValueChanged += SetTextValue;
    }

    private void SetTextValue(int value)
    {
        _text.text = value.ToString();
    }
}
