using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponViewer : MonoBehaviour
{
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private List<WeaponCard> _allWeapon;
    [SerializeField] private List<WeaponCard> _sortedWeapon;
    [SerializeField] private WeaponViewWithButton _weapontViewTemplate;
    [SerializeField] private Transform _viewContainer;
    [SerializeField] private SelectedWeaponViewer _seletedWeaponViewer;
    [SerializeField] private WeaponInfoView _weaponInfoView;

    private void OnEnable()
    {
        Clear();

        foreach (WeaponCard card in _playerInventory.AvailableWeapon)
        {
            _allWeapon.Add(card);
        }
        
        _sortedWeapon = WeaponCardsSort(_allWeapon);
        
        foreach (WeaponCard card in _sortedWeapon)
        {
            CreateCardView(card);
        }
        
    }

    private List<WeaponCard> WeaponCardsSort(List<WeaponCard> weapons)
    {
        return weapons.OrderByDescending(weaponCard => weaponCard.Rang).ToList();
    }

    private void CreateCardView(WeaponCard weaponCard)
    {
        var newView=Instantiate(_weapontViewTemplate,_viewContainer);
        newView.Initialize(weaponCard,_weaponInfoView);
    }
    
    private void Clear()
    {
        _allWeapon.Clear();
        if (_viewContainer.childCount > 0)
        {
            foreach (Transform view in _viewContainer)
            {
                Destroy(view.gameObject);
            }
        }
    }
}
