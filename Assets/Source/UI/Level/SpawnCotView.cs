using UnityEngine;
using TMPro;

public class SpawnCotView : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _unitSpawner.SpawnCostChanged += SetTextValue;
    }

    private void SetTextValue(int value)
    {
        _text.text = value.ToString();
    }
}
