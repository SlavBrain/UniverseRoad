using System;
using UnityEngine;
[Serializable]
public abstract class LevelTask : MonoBehaviour, ILevelTask
{
    public event Action TaskComplited;

    public abstract void StartTracking();

    public void StopTracking()
    {
        TaskComplited?.Invoke();
    }
}
