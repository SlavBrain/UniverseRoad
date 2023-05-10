using UnityEngine;

[CreateAssetMenu(menuName = "Config/rewardConfig")]
public class LevelEndConfig : ScriptableObject
{
    [SerializeField] private int _rewardCoinValue;

    public int RewardCoinValue => _rewardCoinValue;

    public void SetRewardCoinValue(int value)
    {
        _rewardCoinValue = value;
    }
}
