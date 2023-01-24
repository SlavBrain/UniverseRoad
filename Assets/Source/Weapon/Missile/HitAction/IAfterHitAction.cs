using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAfterHitAction
{
    abstract void Action(Bullet bullet,Enemy enemy);
}
