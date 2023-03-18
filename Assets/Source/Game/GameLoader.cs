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
        Debug.Log("loaderEnable ");
    }

    private void OnDisable()
    {
        _levelViewer.LevelViewCreated -= SignToStartButton;
    }

    private void SignToStartButton(LevelView _levelView)
    {
        Debug.Log("signTo " + _levelView);
        _levelView.PlayButtonClicked += StartGame;
    }

    private void StartGame(LevelConfig levelConfig)
    {
        Debug.Log("StartGame " + levelConfig.name);
        levelConfig.SetWeapon(_inventory.SelectedWeapon);
        TestFullLevel.Load(levelConfig);
    }
}
