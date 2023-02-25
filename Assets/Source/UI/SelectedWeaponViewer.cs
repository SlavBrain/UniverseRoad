using System;
using UnityEngine;

public class SelectedWeaponViewer : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private WeaponView _weaponViewTemplate;
    [SerializeField] private GameObject _viewContainer;

    private void OnEnable()
    {
        Refresh();
        _inventory.SelectedWeaponChanged += Refresh;
    }

    private void OnDisable()
    {
        _inventory.SelectedWeaponChanged -= Refresh;
    }

    private void Refresh()
    {
        Clean();

        foreach (WeaponCard weaponCard in _inventory.SelectedWeapon)
        {
            if (weaponCard != null)
                CreateNewCard(weaponCard);
        }
    }

    private void CreateNewCard(WeaponCard weaponCard)
    {
        WeaponView newView = Instantiate(_weaponViewTemplate, _viewContainer.transform);
        newView.Initialize(weaponCard);
    }

    private void Clean()
    {
        foreach (Transform child in _viewContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
