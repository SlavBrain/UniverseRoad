using UnityEngine;
using IJunior.TypedScenes;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Button _startPlayButton;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameConfig config;

    private void OnEnable()
    {
        _startPlayButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _startPlayButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        config.SetWeapon(_inventory.SelectedWeapon);
        TestFullLevel.Load(config);
    }
}
