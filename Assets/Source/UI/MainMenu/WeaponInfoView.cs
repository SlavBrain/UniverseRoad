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
    
    private WeaponCard _weaponCard;

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
        
        IAfterHitActionSelectionner bulletSpellSelectionner = this as IAfterHitActionSelectionner;
        bulletSpellSelectionner.SetAfterHitAction(_weaponCard.Weapon.BulletAfterHitActionVariation);

        IFindTargetSelectioner weaponFindTargetSelectioner=this as IFindTargetSelectioner;
        weaponFindTargetSelectioner.SetFindTargetVariant(_weaponCard.Weapon.FindTargetVariant);
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
