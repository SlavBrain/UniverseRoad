using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<WeaponCard> _purchasedWeapon;
    [SerializeField] private List<WeaponCard> _selectedWeapon;
}
