using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponViewWithButton : WeaponView
{
    [SerializeField] private Button _selectButton;

    private string _selectButtonText = "Select";
    private string _unselectButtonText = "Unselect";

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(InvokeSelect);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveAllListeners();
        _weaponCard.SelectChanged -= SelectButtonTextChange;
    }

    protected override void Refresh()
    {
        base.Refresh();

        if (_weaponCard.Rang <= 0)
            _selectButton.interactable = false;
    }

    private void InvokeSelect()
    {
        _weaponCard.TryingChangeSelect();
    }

    private void SelectButtonTextChange(WeaponCard weaponCard)
    {
        if (_selectButton.TryGetComponent<TMP_Text>(out TMP_Text buttonText))
        {
            if (_weaponCard.IsSelected)
                buttonText.text = _selectButtonText;
            else
                buttonText.text = _unselectButtonText;
        }
    }

    protected override void SetWeaponCard(WeaponCard weaponCard)
    {
        base.SetWeaponCard(weaponCard);
        _weaponCard.SelectChanged += SelectButtonTextChange;
    }
}
