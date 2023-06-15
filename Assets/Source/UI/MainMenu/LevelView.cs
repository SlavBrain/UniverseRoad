using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Button _playButton;

    private Color blockColor= Color.gray;
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

    public void Initialize(int levelNumber, LevelConfig levelConfig, bool isAvailable)
    {
        _levelConfig = levelConfig;
        SetLevelNumberText(levelNumber);

        if (!isAvailable)
        {
            SetViewUnavailable();
        }
    }

    private void SetLevelNumberText(int levelNumber)
    {
        _levelNumberText.text = levelNumber.ToString();
    }

    private void OnPlayButtonClick()
    {
        PlayButtonClicked?.Invoke(_levelConfig);
    }

    private void SetViewUnavailable()
    {
        _playButton.onClick.RemoveAllListeners();
        PlayButtonClicked = null;

        if (_playButton.TryGetComponent<Image>(out Image buttonImage))
        {
            buttonImage.color = blockColor;
        }
    }
}
