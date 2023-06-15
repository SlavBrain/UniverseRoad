using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName ="LevelConfig",menuName ="Config/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Header("Environment")]
    [SerializeField] private EnvironmentConfig _environmentConfig;
    [SerializeField] private int _roadWidth = 30;
    [FormerlySerializedAs("_enemyConfig")]
    [Header("Enemy")]
    [SerializeField] private EnemyWaveConfig[] enemyWaveConfig;
    [Header("Player")]
    [SerializeField] private List<Weapon> _selectedWeapon;
    [Header("Level")] 
    [SerializeField] private bool _isComplete = false;
    
    public IReadOnlyList<Weapon> SelectedWeapon => _selectedWeapon;
    public EnvironmentConfig EnvironmentConfig => _environmentConfig;
    public EnemyWaveConfig[] EnemyWaveConfig => enemyWaveConfig;
    public int RoadWidth => _roadWidth;
    public bool IsComplete => _isComplete;

    public void SetWeapon(IReadOnlyCollection<WeaponCard> weaponCards)
    {
        _selectedWeapon.Clear();

        foreach(WeaponCard card in weaponCards)
        {
            card.Weapon.SetID(card.Name);
            card.Weapon.SetRang(card.Rang);
            _selectedWeapon.Add(card.Weapon);
        }
    }
}
