using UnityEngine;

[CreateAssetMenu(menuName ="Card/Weapon")]
public class WeaponStats : CardStats
{
    [Header("VisualizationStats")]
    [SerializeField] private Missile _missile;
    [Header("GameStats")]
    [SerializeField] private float _shootDelay=0.5f;
    [SerializeField] private float _reloadTime=1;
    [SerializeField] private int _numberOfMissle=1;
    [Header("ActionStats")]
    [SerializeField] private IShoot _shootAction;
    [SerializeField] private ITargetFind _findTargetAction;

    private void OnValidate()
    {
        Debug.LogWarning("fix _shootAction and _findTargetAction");
    }
}
