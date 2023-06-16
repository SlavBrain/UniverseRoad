using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoView : MonoBehaviour, IAfterHitActionSelectionner, IFindTargetSelectioner
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _rang;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _damageValueText;
    [SerializeField] private TMP_Text _reloadValueText;
    [SerializeField] private TMP_Text _bulletCountValueText;
    [SerializeField] private TMP_Text _targetNameText;
    [SerializeField] private TMP_Text _spellNameText;
    [SerializeField] private Button _upgradeButton;
    
    private readonly Color _blockColor=Color.grey;
    private readonly Color _unblockColor=Color.white;
    private WeaponCard _weaponCard;

    private void OnEnable()
    {
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }

    private void OnDisable()
    {
        _upgradeButton.onClick.RemoveAllListeners();
    }

    public void Initialize(WeaponCard weaponCard)
    {
        _weaponCard = weaponCard;
        
        SetUIVAlues();
    }

    private void SetUIVAlues()
    {
        _icon.sprite = _weaponCard.Icon;
        _rang.text = _weaponCard.Rang.ToString();
        _name.text = _weaponCard.Name;
        _damageValueText.text = _weaponCard.Weapon.BulletDamage.ToString();
        _reloadValueText.text = _weaponCard.Weapon.ReloadTime.ToString();
        _bulletCountValueText.text = _weaponCard.Weapon.MaxBulletCount.ToString();
        
        SetUpgradeButtonAvailableState();
        
        IAfterHitActionSelectionner bulletSpellSelectioner = this as IAfterHitActionSelectionner;
        bulletSpellSelectioner.SetAfterHitAction(_weaponCard.Weapon.BulletAfterHitActionVariation);

        IFindTargetSelectioner weaponFindTargetSelectioner=this as IFindTargetSelectioner;
        weaponFindTargetSelectioner.SetFindTargetVariant(_weaponCard.Weapon.FindTargetVariant);
    }

    private void OnUpgradeButtonClick()
    {
        if (_weaponCard.CanBeUpgrade())
        {
            _weaponCard.TryUpgrade();
            SetUIVAlues();
        }
    }

    private void SetUpgradeButtonAvailableState()
    {
        if (_upgradeButton == null)
        {
            return;
        }
        
        if (_upgradeButton.TryGetComponent<Image>(out Image buttonImage))
        {
            if (_weaponCard.CanBeUpgrade())
            {
                buttonImage.color = _unblockColor;
                _upgradeButton.interactable = true;
            }
            else
            {
                buttonImage.color = _blockColor;
                _upgradeButton.interactable = false;
            }
        }
    }

    void IAfterHitActionSelectionner.OnReboundSelected()
    {
        _spellNameText.text = "Rebounding";
    }

    void IAfterHitActionSelectionner.OnNothingSelected()
    {
        _spellNameText.text = "Nothing";
    }

    void IFindTargetSelectioner.OnRandomSelected()
    {
        _targetNameText.text = "Random";
    }

    void IFindTargetSelectioner.OnNearSelected()
    {
        _targetNameText.text = "Near";
    }

    void IFindTargetSelectioner.OnLowHPSelected()
    {
        _targetNameText.text = "LowHP";
    }

    void IFindTargetSelectioner.OnHighHPSelected()
    {
        _targetNameText.text = "HighHP";
    }
}
