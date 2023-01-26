using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReboundingAfteerHitAction : AfterHitAction
{
    [Range(1,20)]
    [SerializeField] private int _maxNumberOfRebound = 1;
    [SerializeField] private float _maxDistanceToNextTarget;
    [SerializeField] private LayerMask _enemyLayer;
    private int _currentRebound = 0;

    private void OnEnable()
    {
        _currentRebound = 0;
    }

    public override void Action(Bullet bullet,Enemy enemy)
    {
        if (_currentRebound < _maxNumberOfRebound)
        {
            if (!TryRebound(bullet,enemy))
            {
                base.Action(bullet,enemy);
            }
        }
        else
        {
            base.Action(bullet,enemy);
        }
    }

    private bool TryRebound(Bullet bullet,Enemy currentEnemy)
    {
        var enemyAround = Physics.OverlapSphere(transform.position, _maxDistanceToNextTarget, _enemyLayer).Except(new Collider[] { currentEnemy.GetComponent<Collider>()}).ToList();

        if (enemyAround.Count != 0)
        {
            bullet.Initialization(enemyAround[Random.Range(0, enemyAround.Count)].transform.position);
            _currentRebound++;
            return true;
        }
        else
        {
            return false;
        }

    }
}
