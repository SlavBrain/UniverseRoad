using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;
using System.Collections.Generic;

public class UnitSpawner : MonoBehaviour,ISceneLoadHandler<GameConfig>
{
    [SerializeField] private List<Weapon> _availableWeapon;
    [SerializeField] private UnitSpawnDot[] _spawnDots;
    [SerializeField] private Button _spawnButton;
    [SerializeField] private int _currentSpawnCost;
    [SerializeField] private List<Unit> _unitTemplates;

    public int MaxUnitLevel => _unitTemplates.Count;

    public void OnSceneLoaded(GameConfig argument)
    {
        SetWeapon(argument.SelectedWeapon);
    }

    private void OnEnable()
    {
        _spawnButton.onClick.AddListener(TrySpawn);
    }

    private void OnDisable()
    {
        _spawnButton.onClick.RemoveListener(TrySpawn);
    }

    public void TrySpawn()
    {
        if (IsEnoughMoney())
        {
            if(TryFindEmptyDot(out UnitSpawnDot emptyDot))
            {
                SpawnNewUnit(GetAvailableWeapon(), emptyDot);
                ChangeSpawnCost();
            }
        }
    }

    private void SpawnNewUnit(Weapon weapon,UnitSpawnDot emptyDot)
    {
        Unit newUnit= Instantiate(_unitTemplates[0], emptyDot.transform.position, Quaternion.identity, emptyDot.transform);
        newUnit.Merged += UpgradeUnit;
        newUnit.Initialize(weapon,1,_unitTemplates.Count>1);
    }

    private void UpgradeUnit(Unit firstUnit, Unit secondUnit)
    {
        int newUnitLevel = secondUnit.Level + 1;
        UnitSpawnDot currentSpawnDot = secondUnit.GetComponentInParent<UnitSpawnDot>();
        Unit upgradedUnit = Instantiate(_unitTemplates[newUnitLevel-1], currentSpawnDot.transform.position, Quaternion.identity, currentSpawnDot.transform);
        firstUnit.Destoy();
        secondUnit.Destoy();
        upgradedUnit.Merged += UpgradeUnit;
        upgradedUnit.Initialize(GetAvailableWeapon(), newUnitLevel,_unitTemplates.Count>newUnitLevel);
    }

    private bool IsEnoughMoney()
    {
        Debug.Log("EnoughMoney");
        return true;
    }

    private void ChangeSpawnCost()
    {
        Debug.Log("SpawnCostChange");
    }

    private Weapon GetAvailableWeapon()
    {
        return _availableWeapon[Random.Range(0, _availableWeapon.Count)];
    }

    private bool TryFindEmptyDot(out UnitSpawnDot emptySpawnDot)
    {
        emptySpawnDot = null;
        var emptySpawnDots = _spawnDots.Where(emptyDot => emptyDot.transform.childCount == 0).ToArray();

        if (emptySpawnDots.Length != 0)
        {
            emptySpawnDot = emptySpawnDots[Random.Range(0,emptySpawnDots.Length)];
        }

        return emptySpawnDot != null;
    }

    private void SetWeapon(IReadOnlyList<Weapon> weapons)
    {
        foreach (Weapon weapon in weapons)
        {
            _availableWeapon.Add(weapon);
        }
    }
}
