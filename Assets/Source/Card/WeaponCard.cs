using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon", order = 1)]
public class WeaponCard : ScriptableObject
{
    private const int MaxWeaponRang = 3;
    private const string PathToWeaponCard = "Assets/Source/Card/WeaponCards/";
    [SerializeField] private CardRarity _rarity;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _count = 0;
    [SerializeField] private int _rang = 0;
    [SerializeField] private bool _isSelected = false;
    [SerializeField] private List<WeaponStats> RankedStats;
    private string _pathToWeaponStatsSO;
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

    public void CreateWeaponStatsSO()
    {
        FindFolderWithWeaponStatsSO();
        CreateWeaponStatsScriptableObjectInFolder();
    }

    private void Unselect()
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

    private void FindFolderWithWeaponStatsSO()
    {

        SetPathToFolderWithWeaponStatsSO();

        if (!Directory.Exists(_pathToWeaponStatsSO))
        {
            Directory.CreateDirectory(_pathToWeaponStatsSO);
        }
    }

    private void SetPathToFolderWithWeaponStatsSO()
    {
        
        _pathToWeaponStatsSO = Environment.CurrentDirectory + PathToWeaponCard + name;
        _localPathToWeaponStatsSO = PathToWeaponCard + name;
        Debug.Log(_localPathToWeaponStatsSO);
    }

    private void CreateWeaponStatsScriptableObjectInFolder()
    {
        string[] allFileInFolder = Directory.GetFiles(_pathToWeaponStatsSO);

        for (int i = 1; i <= MaxWeaponRang; i++)
        {
            if (RankedStats.Count < i)
            {
                RankedStats.Add(null);
            }

            if (!HasWeaponStatsSO(i))
            {
                string weaponStatSOName = name + "Rang" + i;

                if (!TryFindExistingFile(allFileInFolder, weaponStatSOName))
                {
                    RankedStats[i-1]=CreateNewWeaponStatsSO(i);
                }
            }
        }
    }

    private bool TryFindExistingFile(string[] filesInFolder, string nesessaryFile)
    {
        foreach (string file in filesInFolder)
        {
            if (file.Contains(nesessaryFile))
            {
                Debug.Log("Find " + nesessaryFile);
                return true;
            }
        }

        return false;
    }

    private bool HasWeaponStatsSO(int rang)
    {
        Debug.Log(rang);
        return RankedStats[rang - 1] != null;
    }

    private WeaponStats CreateNewWeaponStatsSO(int rang)
    {
        WeaponStats newWeaponStats = ScriptableObject.CreateInstance<WeaponStats>();
        Debug.Log(_localPathToWeaponStatsSO + SetWeaponStatsSOName(rang));
        AssetDatabase.CreateAsset(newWeaponStats, _localPathToWeaponStatsSO + SetWeaponStatsSOName(rang));
        AssetDatabase.SaveAssets();

        return newWeaponStats;
    }

    private string SetWeaponStatsSOName(int rang)
    {
        return "/" + name + "Rang" + rang+".asset";
    }
}