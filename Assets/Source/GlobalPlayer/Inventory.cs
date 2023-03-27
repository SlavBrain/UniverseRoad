using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<WeaponCard> _availableWeapon;
    [SerializeField] private int _maxSelectedWeapon;

    private List<WeaponCard> _selectedWeapon=new List<WeaponCard>();

    public event Action SelectedWeaponChanged;

    public IReadOnlyList<WeaponCard> AvailableWeapon => _availableWeapon;
    public IReadOnlyList<WeaponCard> SelectedWeapon => _selectedWeapon;

    private void OnEnable()
    {
        _selectedWeapon.Clear();

        foreach(WeaponCard card in _availableWeapon)
        {
            card.TryingSelecting += AddCardInSelected;
            card.TryingUnselecting += RemoveCardFromSelected;

            if (card.IsSelected)
            {
                _selectedWeapon.Add(card);
            }
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

    public void AddCardsInAvailable(WeaponCard addingCard, int count)
    {
        WeaponCard existCard = _availableWeapon.FirstOrDefault(weapon => weapon.Name == addingCard.Name);

        if (existCard != null)
        {
            existCard.AddCount(count);
        }
        else
        {
            _availableWeapon.Add(addingCard);
            _availableWeapon[_availableWeapon.Count-1].AddCount(count);
        }
    }

    private void AddCardInSelected(WeaponCard weaponCard)
    {
        if(_selectedWeapon.Count<_maxSelectedWeapon)
        {
            _selectedWeapon.Add(weaponCard);
            weaponCard.Select();
            SelectedWeaponChanged?.Invoke();
        }
    }

    private void RemoveCardFromSelected(WeaponCard weaponCard)
    {
        for(int i=0;i<_selectedWeapon.Count;i++)
        {
            if (_selectedWeapon[i] == weaponCard)
            {
                _selectedWeapon.RemoveAt(i);
            }
        }

        SelectedWeaponChanged?.Invoke();
    }
}
