using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class LevelTask : MonoBehaviour, ILevelTask
{
    public event Action TaskComplited;

    public abstract void StartTracking();

    public void StopTracking()
    {
        TaskComplited?.Invoke();
    }
}
