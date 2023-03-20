using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;
using System.Collections.Generic;
using System;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private List<Weapon> _availableWeapon;
    [SerializeField] private PlayerWallet _wallet;
    [SerializeField] private UnitSpawnDot[] _spawnDots;
    [SerializeField] private Button _spawnButton;
    [SerializeField] private int _currentSpawnCost;
    [SerializeField] private List<Unit> _unitTemplates;
    [SerializeField] private float unitRotation = 0f;

    private int _addingCostValue = 5;
    public int MaxUnitLevel => _unitTemplates.Count;

    public event Action<int> SpawnCostChanged; 

    private void OnEnable()
    {
        _spawnButton.onClick.AddListener(TrySpawn);
        SpawnCostChanged?.Invoke(_currentSpawnCost);
    }

    private void OnDisable()
    {
        _spawnButton.onClick.RemoveListener(TrySpawn);
    }

    public void Initialize(LevelConfig config)
    {
        foreach (Weapon weapon in config.SelectedWeapon)
        {
            _availableWeapon.Add(weapon);
        }
    }

    public void TrySpawn()
    {
        if (TryFindEmptyDot(out UnitSpawnDot emptyDot))
        {
            if (IsEnoughMoney())
            {
                SpawnNewUnit(GetAvailableWeapon(), emptyDot);
                ChangeSpawnCost();
            }
        }
    }

    private void SpawnNewUnit(Weapon weapon, UnitSpawnDot emptyDot)
    {
        Unit newUnit = Instantiate(_unitTemplates[0], emptyDot.transform.position, Quaternion.Euler(0, unitRotation, 0), emptyDot.transform);
        newUnit.Merged += UpgradeUnit;
        newUnit.Initialize(weapon, 1, _unitTemplates.Count > 1);
    }

    private void UpgradeUnit(Unit firstUnit, Unit secondUnit)
    {
        int newUnitLevel = secondUnit.Level + 1;
        UnitSpawnDot currentSpawnDot = secondUnit.GetComponentInParent<UnitSpawnDot>();
        Unit upgradedUnit = Instantiate(_unitTemplates[newUnitLevel - 1], currentSpawnDot.transform.position, Quaternion.Euler(0, unitRotation, 0), currentSpawnDot.transform);
        firstUnit.Destoy();
        secondUnit.Destoy();
        upgradedUnit.Merged += UpgradeUnit;
        upgradedUnit.Initialize(GetAvailableWeapon(), newUnitLevel, _unitTemplates.Count > newUnitLevel);
    }

    private bool IsEnoughMoney()
    {
        return _wallet.TrySpendMoney(_currentSpawnCost);
    }

    private void ChangeSpawnCost()
    {
        _currentSpawnCost += _addingCostValue;
        SpawnCostChanged?.Invoke(_currentSpawnCost);
    }

    private Weapon GetAvailableWeapon()
    {
        return _availableWeapon[UnityEngine.Random.Range(0, _availableWeapon.Count)];
    }

    private bool TryFindEmptyDot(out UnitSpawnDot emptySpawnDot)
    {
        emptySpawnDot = null;
        var emptySpawnDots = _spawnDots.Where(emptyDot => emptyDot.transform.childCount == 0).ToArray();

        if (emptySpawnDots.Length != 0)
        {
            emptySpawnDot = emptySpawnDots[UnityEngine.Random.Range(0, emptySpawnDots.Length)];
        }

        return emptySpawnDot != null;
    }
}
