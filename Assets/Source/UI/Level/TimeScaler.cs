using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale=0;
    }

    public void ActivateGame()
    {
        Time.timeScale = 1;
    }
}
