using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Case")]
public class CaseConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _cost;
    [SerializeField] private Drop[] _items;

    public string Name => _name;
    public Sprite Icon => _icon;
    public int Cost => _cost;
    
}

[Serializable]
public struct Drop
{
    [SerializeField] private WeaponCard _weaponCard;
    [SerializeField] private int _dropChance;
    [SerializeField] private int _minDropCount;
    [SerializeField] private int _maxDropCount;
}


