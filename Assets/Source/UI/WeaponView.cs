using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private TMP_Text _weaponNameView;

    protected WeaponCard _weaponCard; 

    public WeaponCard WeaponCard => _weaponCard;

    public void Initialize(WeaponCard weaponCard)
    {
        if (weaponCard == null)
        {
            Debug.LogWarning(gameObject.name + ": added Weapon is null");
            return;
        }

        SetWeaponCard(weaponCard);
        Refresh();
    }

    protected virtual void Refresh()
    {
        _weaponIcon.sprite = _weaponCard.Icon;
        _weaponNameView.text = _weaponCard.Name;
    }

    protected virtual void SetWeaponCard(WeaponCard weaponCard)
    {
        _weaponCard = weaponCard;
    }
}
