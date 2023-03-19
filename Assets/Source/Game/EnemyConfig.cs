using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Config/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private List<GameObject> _enemysTemplates;
    [SerializeField] private float _spawnDelay;

    public IReadOnlyList<GameObject> EnemysTemplates => (IReadOnlyList<GameObject>)_enemysTemplates;
    public float SpawnDelay => _spawnDelay;

    private void OnValidate()
    {
        foreach(GameObject template in _enemysTemplates)
        {
            if(!template.TryGetComponent<Enemy>(out Enemy enemy))
            {
                Debug.Log(template.gameObject.name + " is not Enemy");
                _enemysTemplates.Remove(template);
            }
        }
    }
}
