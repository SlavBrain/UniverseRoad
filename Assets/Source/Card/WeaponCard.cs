using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon", order = 1)]
public class WeaponCard : ScriptableObject
{
    [SerializeField] private CardRarity _rarity;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _count;
    [SerializeField] private int _rang;
    [SerializeField] private bool _isSelected = false;

    private readonly int[] _cardCountsToUpgrade = {3, 5, 10, 15};
    
    public event Action<WeaponCard> TryingSelecting;
    public event Action<WeaponCard> TryingUnselecting;
    public event Action<WeaponCard> SelectChanged;

    public Weapon Weapon => _weapon;
    public Sprite Icon => _icon;
    public string Name => name;
    public int Count => _count;
    public int Rang => _rang;
    public bool IsSelected => _isSelected;
    
    public bool CanBeUpgrade()
    {
        if (_rang <= 0||_rang-1>=_cardCountsToUpgrade.Length)
        {
            return false;
        }
        else
        {
            return _count >= _cardCountsToUpgrade[_rang - 1];
        }
    }

    public void AddCount(int count)
    {
        if (count <= 0)
        {
            Debug.LogWarning(name+": adding not correct count");
        }

        if (_rang == 0)
        {
            _rang = 1;
        }
        
        _count += count;
    }

    public bool TryUpgrade()
    {
        if (_count >= _cardCountsToUpgrade[_rang - 1])
        {
            _count -= _cardCountsToUpgrade[_rang - 1];
            _rang++;
            return true;
        }
        else
        {
            return false;
        }
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