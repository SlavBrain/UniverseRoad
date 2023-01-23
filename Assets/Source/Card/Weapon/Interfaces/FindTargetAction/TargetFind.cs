using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetFind :  MonoBehaviour, ITargetFind
{
    public virtual GameObject FindTarget(IReadOnlyList<GameObject> objects)
    {
        return null;
    }
}
