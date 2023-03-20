using UnityEngine;
using IJunior.TypedScenes;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private LevelViewer _levelViewer;

    private void OnEnable()
    {
        _levelViewer.LevelViewCreated += SignToStartButton;
    }

    private void OnDisable()
    {
        _levelViewer.LevelViewCreated -= SignToStartButton;
    }

    private void SignToStartButton(LevelView _levelView)
    {
        _levelView.PlayButtonClicked += StartGame;
    }

    private void StartGame(LevelConfig levelConfig)
    {
        levelConfig.SetWeapon(_inventory.SelectedWeapon);
        LevelTemplate.Load(levelConfig);
    }
}
