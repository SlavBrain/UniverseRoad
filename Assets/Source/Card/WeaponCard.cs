using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="ScriptableObject/Weapon", order =1)]
public class WeaponCard : ScriptableObject
{
    [SerializeField] private CardRarity _rarity;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;

    public Sprite Icon => _icon;
    public string Name => name;
}
