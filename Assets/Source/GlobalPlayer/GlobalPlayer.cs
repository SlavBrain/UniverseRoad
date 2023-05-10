using IJunior.TypedScenes;
using UnityEngine;

public class GlobalPlayer : MonoBehaviour, ISceneLoadHandler<LevelEndConfig>
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LevelEndConfig _levelEndConfig;

    public void OnSceneLoaded(LevelEndConfig argument)
    {
        AddRewardAfterLevel(argument);
    }

    private void AddRewardAfterLevel(LevelEndConfig config)
    {
        _wallet.AddMoney(config.RewardCoinValue);
    }
}