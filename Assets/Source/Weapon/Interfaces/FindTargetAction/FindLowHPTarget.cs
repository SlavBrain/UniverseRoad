using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindLowHPTarget : TargetFind
{
    public override GameObject FindTarget(IReadOnlyList<GameObject> objects)
    {
        return objects.OrderBy(enemy => enemy.GetComponent<Enemy>().CurrentHealth).FirstOrDefault();
    }
}
