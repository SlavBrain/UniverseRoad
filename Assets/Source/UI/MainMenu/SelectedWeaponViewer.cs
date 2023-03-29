using UnityEngine;
using System.Linq;

public class SelectedWeaponViewer : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    //[SerializeField] private WeaponView _weaponViewTemplate;
    [SerializeField] private WeaponView[] _weaponViews;
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

        if (_inventory.SelectedWeapon.Count > 0)
        {
            foreach (WeaponCard weaponCard in _inventory.SelectedWeapon)
            {
                if (weaponCard != null)
                    SetWeaponView(weaponCard);
            }
        }
    }

    private void SetWeaponView(WeaponCard weaponCard)
    {
        if (TryFindEmptyView(out WeaponView view))
        {
            view.Initialize(weaponCard,null);
        }
    }

    private bool TryFindEmptyView(out WeaponView emptyView)
    {
        emptyView = _weaponViews.FirstOrDefault(view => view.WeaponCard == null);

        return emptyView != null;
    }

    private void FillEmptyView(WeaponView view)
    {
        view.ResetToDefault();
    }

    private void Clean()
    {
        foreach (WeaponView view in _weaponViews)
        {
            view.ResetToDefault();
        }
    }
}
