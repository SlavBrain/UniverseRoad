using UnityEngine;
using IJunior.TypedScenes;

public class MainMenuLoader : MonoBehaviour
{
    [SerializeField] private LevelEndConfig _config;
    
    public void ExitToMainMenu()
    {
        MainMenu.Load(_config);
    }
}
