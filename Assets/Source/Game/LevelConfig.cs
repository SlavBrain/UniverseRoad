using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName ="LevelConfig",menuName ="Config/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Header("Enviroment")]
    [SerializeField] private EnviromentConfig _enveromentConfig;
    [FormerlySerializedAs("_enemyConfig")]
    [Header("Enemy")]
    [SerializeField] private EnemyWaveConfig[] enemyWaveConfig;
    [Header("Player")]
    [SerializeField] private List<Weapon> _selectedWeapon;
    [SerializeField] private int _maxHealth=100;
    [Header("Level")]

    [SerializeField] private int _roadWidth = 30;
    
    public IReadOnlyList<Weapon> SelectedWeapon => _selectedWeapon;
    public EnviromentConfig EnviromentConfig => _enveromentConfig;
    public EnemyWaveConfig[] EnemyWaveConfig => enemyWaveConfig;

    public void SetWeapon(IReadOnlyCollection<WeaponCard> weaponCards)
    {
        _selectedWeapon.Clear();

        foreach(WeaponCard card in weaponCards)
        {
            _selectedWeapon.Add(card.Weapon);
        }
    }
}
