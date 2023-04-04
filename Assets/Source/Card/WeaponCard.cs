using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon", order = 1)]
public class WeaponCard : ScriptableObject
{
    private const int MaxWeaponRang = 3;
    private const string PathToWeaponCard = "Assets/Source/Card/WeaponCards/";
    [SerializeField] private CardRarity _rarity;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _count;
    [SerializeField] private int _rang;
    [SerializeField] private bool _isSelected = false;
    
    private string _localPathToWeaponStatsSO;

    public event Action<WeaponCard> TryingSelecting;
    public event Action<WeaponCard> TryingUnselecting;
    public event Action<WeaponCard> SelectChanged;

    public Weapon Weapon => _weapon;
    public Sprite Icon => _icon;
    public string Name => name;
    public int Count => _count;
    public int Rang => _rang;
    public bool IsSelected => _isSelected;

    public void AddCount(int count)
    {
        if (count <= 0)
        {
            Debug.LogWarning(name+": adding not correct count");
        }
        
        _count += count;
    }
    
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

    private void Unselect()
    {
        _isSelected = false;
        SelectChanged?.Invoke(this);
    }
}