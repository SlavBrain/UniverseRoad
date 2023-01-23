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
                Spawn(GetAvailableWeapon(), emptyDot);
                ChangeSpawnCost();
            }
        }
    }

    private void Spawn(GameObject weapon,UnitSpawnDot emptyDot)
    {
        Unit newUnit= Instantiate(_unitTemplate, emptyDot.transform.position, Quaternion.identity, emptyDot.transform);
        newUnit.Initialize(weapon);
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
