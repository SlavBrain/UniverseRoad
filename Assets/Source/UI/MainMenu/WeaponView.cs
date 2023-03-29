using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private TMP_Text _weaponNameView;
    [SerializeField] private TMP_Text _weaponRangView;

    protected WeaponCard _weaponCard; 

    public WeaponCard WeaponCard => _weaponCard;

    public virtual void Initialize(WeaponCard weaponCard, WeaponInfoView weaponInfoView)
    {
        if (weaponCard == null)
        {
            Debug.LogWarning(gameObject.name + ": added Weapon is null");
            return;
        }

        SetWeaponCard(weaponCard);
        _weaponIcon.gameObject.SetActive(true);
        _weaponNameView.gameObject.SetActive(true);
        _weaponRangView.gameObject.SetActive(true);
        Refresh();
    }

    public void ResetToDefault()
    {
        _weaponCard = null;
        _weaponIcon.gameObject.SetActive(false);
        _weaponNameView.gameObject.SetActive(false);
        _weaponRangView.gameObject.SetActive(false);
    }

    protected virtual void Refresh()
    {
        _weaponIcon.sprite = _weaponCard.Icon;
        _weaponNameView.text = _weaponCard.Name;
        _weaponRangView.text = _weaponCard.Rang.ToString();
    }

    protected virtual void SetWeaponCard(WeaponCard weaponCard)
    {
        _weaponCard = weaponCard;
    }
}
