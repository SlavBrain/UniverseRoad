using UnityEngine;

[CreateAssetMenu(menuName ="Card/Weapon")]
public class WeaponStats : CardStats
{
    [SerializeField] private GameObject _model;
    [SerializeField] private Missile _missile;
    [SerializeField] private IShoot _shootAction;
    [SerializeField] private IFindTarget _findTargetAction;
    [SerializeField] private float _shootDelay=0.5f;
    [SerializeField] private float _reloadTime=1;
    [SerializeField] private int _numberOfMissle=1;
    [SerializeField] private AnimatorOverrideController _animatorOverrideController;

    private void OnValidate()
    {
        Debug.LogWarning("fix _shootAction and _findTargetAction");
    }
}
