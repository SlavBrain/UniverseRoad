using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private EnemyWaveController _waveController;
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _successEndGamePanel;
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject _loseEndGamePanel;
    [SerializeField] private TimeScaler _timeScaler;

    private void OnEnable()
    {
        _waveController.WavesEnded += SuccessGameEnd;
        _health.Die += LoseGameEnd;
    }

    private void OnDisable()
    {
        _waveController.WavesEnded -= SuccessGameEnd;
        _health.Die -= LoseGameEnd;
    }

    private void SuccessGameEnd()
    {
        Debug.Log(" end "+Time.realtimeSinceStartup);
        _successEndGamePanel.SetActive(true);
        _inGameUI.SetActive(false);
        _timeScaler.PauseGame();
    }

    private void LoseGameEnd()
    {
        _loseEndGamePanel.SetActive(true);
        _inGameUI.SetActive(false);
        _timeScaler.PauseGame();
    }
}
