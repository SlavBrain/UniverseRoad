using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon", order = 1)]
public class WeaponCard : ScriptableObject
{
    [SerializeField] private CardRarity _rarity;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _count=0;
    [SerializeField] private int _rang = 0;
    [SerializeField] private bool _isSelected=false;
    [SerializeField] private List<WeaponStats> RankedStats;

    public event Action<WeaponCard> TryingSelecting;
    public event Action<WeaponCard> TryingUnselecting;
    public event Action<WeaponCard> SelectChanged;

    public Weapon Weapon => _weapon;
    public Sprite Icon => _icon;
    public string Name => name;
    public int Count => _count;
    public int Rang => _rang;
    public bool IsSelected => _isSelected;

    public void TryingChangeSelect()
    {
        if (IsSelected)
        {
            TryingUnselecting?.Invoke(this);
            Unselect();
        }
        else
        {
            TryingSelecting?.Invoke(this);
        }
    }

    public void Select()
    {
        _isSelected = true;
        SelectChanged?.Invoke(this);
    }

    public void Unselect()
    {
        _isSelected = false;
        SelectChanged?.Invoke(this);
    }

    private void OnValidate()
    {
        if (RankedStats.Count == 0)
        {
            Debug.LogWarning(this.name + ": RankedStats is empty");
        }
    }
}