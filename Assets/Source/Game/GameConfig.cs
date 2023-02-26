using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Config")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private List<Weapon> _selectedWeapon;

    public IReadOnlyList<Weapon> SelectedWeapon => _selectedWeapon;

    public void SetWeapon(IReadOnlyCollection<WeaponCard> weaponCards)
    {
        foreach(WeaponCard card in weaponCards)
        {
            _selectedWeapon.Add(card.Weapon);
        }

        Debug.Log(_selectedWeapon.Count);
    }
}
