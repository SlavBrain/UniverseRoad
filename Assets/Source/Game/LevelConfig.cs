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
    [Header("Level")]
    [SerializeField] private int _roadWidth = 30;
    
    public IReadOnlyList<Weapon> SelectedWeapon => _selectedWeapon;
    public EnviromentConfig EnviromentConfig => _enveromentConfig;
    public EnemyWaveConfig[] EnemyWaveConfig => enemyWaveConfig;
    public int RoadWidth => _roadWidth;

    public void SetWeapon(IReadOnlyCollection<WeaponCard> weaponCards)
    {
        _selectedWeapon.Clear();

        foreach(WeaponCard card in weaponCards)
        {
            card.Weapon.SetRang(card.Rang);
            _selectedWeapon.Add(card.Weapon);
        }
    }
}
