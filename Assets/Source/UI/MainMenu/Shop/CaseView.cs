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

    private CaseOpener _caseOpener;
    private Inventory _inventory;
    private Wallet _wallet;
    private CaseConfig _caseConfig;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnSellButtonClicked);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveAllListeners();
    }

    public void Initialize(CaseConfig config, CaseOpener caseOpener,Wallet wallet, Inventory inventory)
    {
        _caseConfig = config;
        _caseOpener = caseOpener;
        _wallet = wallet;
        _inventory = inventory;
        _icon.sprite = config.Icon;
        _caseName.text = config.Name;
        _costText.text = config.Cost.ToString();
    }

    private void OnSellButtonClicked()
    {
        if (_wallet.TrySpendMoney(_caseConfig.Cost))
        {
            WeaponCard dropedCard = _caseConfig.Open(out int cardCount);
            _inventory.AddCardsInAvailable(dropedCard,cardCount);
            _caseOpener.gameObject.SetActive(true);
            _caseOpener.Initialize(_icon.sprite,dropedCard,cardCount);
        }
    }
}
