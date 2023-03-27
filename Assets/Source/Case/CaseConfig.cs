using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "ScriptableObject/Case")]
public class CaseConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _cost;
    [SerializeField] private Drop[] _items;

    private int _maxRandomNumber;
    public string Name => _name;
    public Sprite Icon => _icon;
    public int Cost => _cost;

    public WeaponCard Open(out int count)
    {
        int randomNumber = Random.Range(0, _maxRandomNumber);

        foreach(Drop item in _items) 
        {
            if (item.DropChance > randomNumber) 
            {
                count = item.GetRandomCount;
                return item.WeaponCard;
            }
            else
            {
                randomNumber -= item.DropChance;
            }
        }

        count = 0;
        return null;
    }

    private void OnValidate()
    {
        _maxRandomNumber = 0;

        foreach (Drop item in _items)
        {
            _maxRandomNumber += item.DropChance;
            
            if(item.DropChance==0||item.MinDropCount<1||item.MaxDropCount<item.MinDropCount)
                Debug.Log(name+": Not correct parameters in drop(DropChance, MinCountDrop, MaxCountDrop");
        }
    }
}

[Serializable]
public struct Drop
{
    [SerializeField] private WeaponCard _weaponCard;
    [SerializeField] private int _dropChance;
    [SerializeField] private int _minDropCount;
    [SerializeField] private int _maxDropCount;

    public WeaponCard WeaponCard => _weaponCard;
    public int DropChance => _dropChance;
    public int MinDropCount => _minDropCount;
    public int MaxDropCount => _maxDropCount;

    public int GetRandomCount => Random.Range(_minDropCount, _maxDropCount);
}


