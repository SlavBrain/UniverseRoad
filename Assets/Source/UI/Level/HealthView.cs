using UnityEngine;
using TMPro;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _health.ValueChanged += SetTextValue;
    }

    private void SetTextValue(int value)
    {
        _text.text = value.ToString();
    }
}
