using System.Collections.Generic;
using UnityEngine;

public class WeaponViewer : MonoBehaviour
{
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private List<WeaponCard> _allWeapon;
    [SerializeField] private WeaponViewWithButton _weapontViewTemplate;
    [SerializeField] private Transform _viewContainer;
    [SerializeField] private SelectedWeaponViewer _seletedWeaponViewer;
    [SerializeField] private WeaponInfoView _weaponInfoView;

    private void OnEnable()
    {
        Clear();

        for(int i = 0; i < _playerInventory.AvailableWeapon.Count; i++)
        {
            _allWeapon.Add(_playerInventory.AvailableWeapon[i]);
            CreateCardView(_allWeapon[i]);
        }
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
