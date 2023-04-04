using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoView : MonoBehaviour
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
        _damageValueText.text = _weaponCard.Weapon.Bullet.Damage.ToString();
        _reloadValueText.text = _weaponCard.Weapon.ReloadTime.ToString();
        _bulletCountValueText.text = _weaponCard.Weapon.MaxBulletCount.ToString();
        _targetNameText.text = _weaponCard.Weapon.TargetFind.Name;
        _spellNameText.text = _weaponCard.Weapon.Bullet.AfterHitAction.Name;

    }
}
