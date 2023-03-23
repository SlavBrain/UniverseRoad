using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveProgress : MonoBehaviour
{
    private float _duration;
    private Coroutine _waitTime;

    public event Action TimeIsUp;

    public void Initialize(float time)
    {
        _duration = time;
        StartTracking();
    }

    private void StartTracking()
    {
        if (_waitTime != null)
        {
            StopCoroutine(_waitTime);
        }

        _waitTime = StartCoroutine(Waiting());
    }

    private void OnWaitingEnd()
    {
        TimeIsUp?.Invoke();
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(_duration);
        OnWaitingEnd();
    }
}
