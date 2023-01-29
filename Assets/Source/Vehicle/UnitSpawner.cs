using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _availableWeapon;
    [SerializeField] private UnitSpawnDot[] _spawnDots;
    [SerializeField] private Button _spawnButton;
    [SerializeField] private int _currentSpawnCost;
    [SerializeField] private Unit _unitTemplate;    

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

    private void SpawnNewUnit(GameObject weapon,UnitSpawnDot emptyDot)
    {
        Unit newUnit= Instantiate(_unitTemplate, emptyDot.transform.position, Quaternion.identity, emptyDot.transform);
        newUnit.Merged += UpgradeUnit;
        newUnit.Initialize(weapon,1);
    }

    private void UpgradeUnit(Unit firstUnit, Unit secondUnit)
    {
        UnitSpawnDot currentSpawnDot = secondUnit.GetComponentInParent<UnitSpawnDot>();
        Unit upgradedUnit = Instantiate(_unitTemplate, currentSpawnDot.transform.position, Quaternion.identity, currentSpawnDot.transform);
        firstUnit.Destoy();
        secondUnit.Destoy();
        upgradedUnit.Merged += UpgradeUnit;
        upgradedUnit.Initialize(GetAvailableWeapon(), secondUnit.Level+1);
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

    private GameObject GetAvailableWeapon()
    {
        return _availableWeapon[Random.Range(0, _availableWeapon.Length)];
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
}
