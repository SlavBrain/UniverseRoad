using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class FindNearTarget : TargetFind
{
    public override GameObject FindTarget(IReadOnlyList<GameObject> objects)
    {
        return objects.OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position)).FirstOrDefault();
    }
}
