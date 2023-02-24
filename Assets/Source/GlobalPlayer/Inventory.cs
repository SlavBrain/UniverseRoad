using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> _availableWeapon;
    [SerializeField] private List<GameObject> _selectedWeapon;
}
