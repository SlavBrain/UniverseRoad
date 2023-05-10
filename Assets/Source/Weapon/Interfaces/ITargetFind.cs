using System.Collections.Generic;
using UnityEngine;

public interface ITargetFind
{
    public abstract GameObject FindTarget(IReadOnlyList<GameObject> objects);
}
