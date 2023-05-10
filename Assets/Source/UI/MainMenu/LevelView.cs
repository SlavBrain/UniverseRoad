using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Button _playButton;

    public event Action<LevelConfig> PlayButtonClicked;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();
        PlayButtonClicked = null;
    }

    public void Initialize(int levelNumber, LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
        SetLevelNumberText(levelNumber);
    }

    private void SetLevelNumberText(int levelNumber)
    {
        _levelNumberText.text = levelNumber.ToString();
    }

    private void OnPlayButtonClick()
    {
        PlayButtonClicked?.Invoke(_levelConfig);
    }
}
