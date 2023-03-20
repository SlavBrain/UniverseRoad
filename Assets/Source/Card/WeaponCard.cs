using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon", order = 1)]
public class WeaponCard : ScriptableObject
{
    private const int maxWeaponRang = 3;
    private const string pathToWeaponCard = "/Assets/Source/Card/WeaponCards/";
    [SerializeField] private CardRarity _rarity;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _count = 0;
    [SerializeField] private int _rang = 0;
    [SerializeField] private bool _isSelected = false;
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

    public void CreateWeaponStatsSO()
    {
        CreateWeaponCardScriptableObjectInFolder(FindFolderWithWeaponStatsSO());
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

    private string FindFolderWithWeaponStatsSO()
    {
        string currentFileDirection = Environment.CurrentDirectory;
        string rangStatsSOPath = currentFileDirection + pathToWeaponCard + name;

        if (!Directory.Exists(rangStatsSOPath))
        {
            Directory.CreateDirectory(rangStatsSOPath);
        }

        return rangStatsSOPath;
    }

    private void CreateWeaponCardScriptableObjectInFolder(string path)
    {
        string[] allFileInFolder = Directory.GetFiles(path);

        for (int i = 1; i <= maxWeaponRang; i++)
        {
            string weaponStatSOName = name + "Rang" + i;
            TryFindExistingFile(allFileInFolder, weaponStatSOName);
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
}