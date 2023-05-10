using TMPro;
using UnityEngine;

public class SuccessEndGamePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private LevelReward _levelReward;

    private void OnEnable()
    {
        _rewardText.text = _levelReward.CoinValue.ToString();
    }
}
