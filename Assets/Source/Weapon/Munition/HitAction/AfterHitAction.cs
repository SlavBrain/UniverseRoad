using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterHitAction : MonoBehaviour, IAfterHitAction
{
    [SerializeField] private string _name;

    public string Name => _name;
    
    public virtual void Action(Bullet bullet,Enemy enemy)
    {
        bullet.Destroy();
    }
}
