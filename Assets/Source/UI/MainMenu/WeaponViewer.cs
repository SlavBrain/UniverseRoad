using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponViewer : MonoBehaviour
{
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private List<WeaponCard> _allWeapon;
    [SerializeField] private WeaponViewWithButton _weapontViewTemplate;
    [SerializeField] private GameObject _viewContainer;
    [SerializeField] private SelectedWeaponViewer _seletedWeaponViewer;

    public event Action<WeaponViewWithButton>  CreatedCardView;    

    private void OnEnable()
    {
        _allWeapon.Clear();

        for(int i = 0; i < _playerInventory.AvailableWeapon.Count; i++)
        {
            _allWeapon.Add(_playerInventory.AvailableWeapon[i]);
            CreateCardView(_allWeapon[i]);
        }
    }

    private void CreateCardView(WeaponCard weaponCard)
    {
        var newView=Instantiate(_weapontViewTemplate,_viewContainer.transform);
        newView.Initialize(weaponCard);
        CreatedCardView?.Invoke(newView);
    }
}
