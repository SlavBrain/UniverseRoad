using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterHitAction : MonoBehaviour, IAfterHitAction
{
    public virtual void Action(Bullet bullet,Enemy enemy)
    {
        bullet.Destroy();
    }
}
