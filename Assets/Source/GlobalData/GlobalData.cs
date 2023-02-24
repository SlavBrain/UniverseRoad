using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    [SerializeField] private WeaponCard[] _availableWeapon;

    public IReadOnlyList<WeaponCard> AvailableWeapon => _availableWeapon;
}
