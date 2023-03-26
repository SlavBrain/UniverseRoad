using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaseView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _caseName;
    [SerializeField] private Button _sellButton;
    [SerializeField] private TMP_Text _costText;

    private CaseConfig _caseConfig;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnSellButtonClicked);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveAllListeners();
    }

    public void Initialize(CaseConfig config)
    {
        _caseConfig = config;
        _icon.sprite = config.Icon;
        _caseName.text = config.Name;
        _costText.text = config.Cost.ToString();
    }

    private void OnSellButtonClicked()
    {
        Debug.Log("SellButtonClicked");
    }
}
