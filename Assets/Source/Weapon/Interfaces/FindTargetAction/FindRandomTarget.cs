using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindRandomTarget : TargetFind
{
    public override GameObject FindTarget(IReadOnlyList<GameObject> objects)
    {
        var availableObjects = objects.Where(enemy => enemy.gameObject.activeSelf).ToList();

        if (availableObjects.Count != 0)
        {
            return availableObjects[Random.Range(0, availableObjects.Count)];
        }
        else
        {
            return null;
        }
    }
}
