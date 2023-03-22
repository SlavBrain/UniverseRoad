using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName ="LevelConfig",menuName ="Config/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Header("Enviroment")]
    [SerializeField] private EnviromentConfig _enveromentConfig;
    [Header("Enemy")]
    [SerializeField] private EnemyConfig _enemyConfig;
    [Header("Player")]
    [SerializeField] private List<Weapon> _selectedWeapon;
    [SerializeField] private int _maxHealth=100;
    [FormerlySerializedAs("_levelTask")]
    [Header("Level")]
    [SerializeField] private LevelTaskList levelLevelTask;

    [SerializeField] private int _roadWidth = 30;
    
    public IReadOnlyList<Weapon> SelectedWeapon => _selectedWeapon;
    public EnviromentConfig EnviromentConfig => _enveromentConfig;
    public EnemyConfig EnemyConfig => _enemyConfig;

    public void SetWeapon(IReadOnlyCollection<WeaponCard> weaponCards)
    {
        _selectedWeapon.Clear();

        foreach(WeaponCard card in weaponCards)
        {
            _selectedWeapon.Add(card.Weapon);
        }
    }
}
