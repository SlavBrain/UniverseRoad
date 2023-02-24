using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private WeaponCard _weaponCard;
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private TMP_Text _weaponNameView;

    public void Initialize(WeaponCard weaponCard)
    {
        if (weaponCard == null)
        {
            Debug.LogWarning(gameObject.name + ": added Weapon is null");
            return;
        }

        _weaponCard = weaponCard;
        Refresh();
    }

    private void Refresh()
    {
        _weaponIcon.sprite = _weaponCard.Icon;
        _weaponNameView.text = _weaponCard.Name;
    }
}
