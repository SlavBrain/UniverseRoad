using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] LevelTask _levelTask;

    private void OnEnable()
    {
        _endGamePanel.gameObject.SetActive(false);
        _levelTask.StartTracking();
        _levelTask.TaskComplited += EndLevel;
    }

    private void EndLevel()
    {
        Time.timeScale = 0;
        _endGamePanel.gameObject.SetActive(true);
    }
}
