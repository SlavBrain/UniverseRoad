using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

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
    [FormerlySerializedAs("RankedStats")] [SerializeField] private List<WeaponStats> _rankedStats=new List<WeaponStats>();
    
    private string _localPathToWeaponStatsSO;

    public event Action<WeaponCard> TryingSelecting;
    public event Action<WeaponCard> TryingUnselecting;
    public event Action<WeaponCard> SelectChanged;

    public Weapon Weapon => _weapon;
    public Sprite Icon => _icon;
    public string Name => name;
    public WeaponStats Stats => _rankedStats[_rang - 1];
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

#if UNITY_EDITOR

    public void CreateWeaponStatsSO()
    {
        FindFolderWithWeaponStatsSO();
        CreateWeaponStatsScriptableObjectInFolder();
    }

    private void OnValidate()
    {
        if (_rankedStats.Count == 0)
        {
            Debug.LogWarning(this.name + ": RankedStats is empty");
        }
    }

    private void FindFolderWithWeaponStatsSO()
    {

        SetPathToFolderWithWeaponStatsSO();
        
        if (!Directory.Exists(_localPathToWeaponStatsSO))
        {
            Directory.CreateDirectory(_localPathToWeaponStatsSO);
        }
    }

    private void SetPathToFolderWithWeaponStatsSO()
    {
        _localPathToWeaponStatsSO = PathToWeaponCard + name;
    }

    private void CreateWeaponStatsScriptableObjectInFolder()
    {
        string[] allFileInFolder = Directory.GetFiles(_localPathToWeaponStatsSO);

        for (int i = 1; i <= MaxWeaponRang; i++)
        {
            if (_rankedStats.Count < i)
            {
                _rankedStats.Add(null);
            }

            if (!HasWeaponStatsSO(i))
            {
                string weaponStatSOName = name + "Rang" + i+".asset";

                if (TryFindExistingFile(allFileInFolder, weaponStatSOName,out WeaponStats existWeaponStats))
                {
                    _rankedStats[i - 1] = existWeaponStats;
                }
                else
                {
                    _rankedStats[i-1]=CreateNewWeaponStatsSO(i);
                }
            }
        }
    }

    private bool TryFindExistingFile(string[] filesInFolder, string neсessaryFile,out WeaponStats weaponCard)
    {
        weaponCard = null;
        
        foreach (string file in filesInFolder)
        {
            if (file.Contains(neсessaryFile))
            {
                var necessaryWeaponStats = AssetDatabase.LoadAssetAtPath<WeaponStats>(_localPathToWeaponStatsSO + "/" + neсessaryFile);
                
                if (necessaryWeaponStats != null)
                {
                    weaponCard = necessaryWeaponStats;
                    return true;
                }
            }
        }

        return false;
    }

    private bool HasWeaponStatsSO(int rang)
    {
        Debug.Log(rang);
        return _rankedStats[rang - 1] != null;
    }

    private WeaponStats CreateNewWeaponStatsSO(int rang)
    {
        WeaponStats newWeaponStats = ScriptableObject.CreateInstance<WeaponStats>();
        Debug.Log(_localPathToWeaponStatsSO + SetWeaponStatsSOName(rang));
        AssetDatabase.CreateAsset(newWeaponStats, _localPathToWeaponStatsSO + SetWeaponStatsSOName(rang));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        return newWeaponStats;
    }

    private string SetWeaponStatsSOName(int rang)
    {
        return "/" + name + "Rang" + rang+".asset";
    }

#endif
}