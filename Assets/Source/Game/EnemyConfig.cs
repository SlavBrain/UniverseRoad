using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Config/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private Enemy[] _enemysTemplates;
    [SerializeField] private float _spawnDelay;
}
