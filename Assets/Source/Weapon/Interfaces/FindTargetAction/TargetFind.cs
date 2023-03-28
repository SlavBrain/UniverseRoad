using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetFind :  MonoBehaviour, ITargetFind
{
    [SerializeField] private string _name;

    public string Name => _name;
    
    public virtual GameObject FindTarget(IReadOnlyList<GameObject> objects)
    {
        return null;
    }
}
