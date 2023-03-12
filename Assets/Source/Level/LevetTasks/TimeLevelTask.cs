using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLevelTask : LevelTask
{
    [SerializeField] private float _surviveTime=60;

    private float _minSurviveTime = 1f;
    private Coroutine _waitTime;

    private void OnEnable()
    {
        if (_surviveTime <= 0)
            _surviveTime = _minSurviveTime;
    }

    public override void StartTracking()
    {
        if (_waitTime != null)
            StopCoroutine(_waitTime);

        _waitTime = StartCoroutine(WaitingTime());
    }

    private IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(_surviveTime);
        StopTracking();
    }
}
