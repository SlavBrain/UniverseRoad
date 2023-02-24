using System.Collections.Generic;
using UnityEngine;

public class WeaponViewer : MonoBehaviour
{
    [SerializeField] private GlobalData _globalData;
    [SerializeField] private List<WeaponCard> _allWeapon;
    [SerializeField] private WeaponView _weapontViewTemplate;
    [SerializeField] private GameObject _viewContainer;

    private void OnEnable()
    {
        _allWeapon.Clear();

        for(int i = 0; i < _globalData.AvailableWeapon.Count; i++)
        {
            _allWeapon.Add(_globalData.AvailableWeapon[i]);
            CreateCardView(_allWeapon[i]);
        }
    }

    private void CreateCardView(WeaponCard weaponCard)
    {
        var newView=Instantiate(_weapontViewTemplate,_viewContainer.transform);
        newView.Initialize(weaponCard);
    }
}
