using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindHighHPTarget : TargetFind
{
    public override GameObject FindTarget(IReadOnlyList<GameObject> objects)
    {
        return objects.OrderByDescending(enemy => enemy.GetComponent<Enemy>().CurrentHealth).FirstOrDefault();
    }
}
