using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<WeaponCard> _availableWeapon;
    [SerializeField] private WeaponCard[] _selectedWeapon;
    [SerializeField] private int _maxSelectedWeapon;

    public event Action SelectedWeaponChanged;
    public IReadOnlyList<WeaponCard> AvailableWeapon => _availableWeapon;
    public IReadOnlyCollection<WeaponCard> SelectedWeapon => _selectedWeapon;

    private void OnEnable()
    {
        _selectedWeapon = new WeaponCard[_maxSelectedWeapon];

        foreach(WeaponCard card in _availableWeapon)
        {
            card.TryingSelecting += AddCardInSelected;
            card.TryingUnselecting += RemoveCardFromSelected;
        }
    }

    private void OnDisable()
    {
        foreach (WeaponCard card in _availableWeapon)
        {
            card.TryingSelecting -= AddCardInSelected;
            card.TryingUnselecting -= RemoveCardFromSelected;
        }
    }

    private void AddCardInSelected(WeaponCard weaponCard)
    {
        if(TryFindEmptySelectSlot(out int slotNumber))
        {
            _selectedWeapon[slotNumber] = weaponCard;
            weaponCard.Select();
            SelectedWeaponChanged?.Invoke();
        }
    }

    private void RemoveCardFromSelected(WeaponCard weaponCard)
    {
        for(int i=0;i<_selectedWeapon.Length;i++)
        {
            if (_selectedWeapon[i] == weaponCard)
            {
                _selectedWeapon[i] = null;
            }
        }

        SelectedWeaponChanged?.Invoke();
    }

    private bool TryFindEmptySelectSlot(out int slotNumber)
    {
        slotNumber = 0;

        for(int i = 0; i < _selectedWeapon.Length; i++)
        {
            if (_selectedWeapon[i] == null)
            {
                slotNumber = i;
                return true;
            }
        }

        return false;
    }
}
