using UnityEngine;
using System.Linq;

public class ReboundingAfterHitAction : NothingAfterHitAction
{
    private const string HashEnemyLayer = "Enemy";
    
    private int _maxNumberOfRebound;
    private float _maxDistanceToNextTarget;
    private Bullet _bullet;
    private LayerMask _enemyLayer;
    private int _currentRebound = 0;

    public ReboundingAfterHitAction(Bullet bullet,int maxNumberOfRebound=2, float maxDistanceToNextTarget=100f)
    {
        if (maxNumberOfRebound <= 0)
            maxNumberOfRebound = 1;
        
        if (maxDistanceToNextTarget < 0)
            maxDistanceToNextTarget = 0;

        _bullet = bullet;
        _maxNumberOfRebound = maxNumberOfRebound;
        _maxDistanceToNextTarget = maxDistanceToNextTarget;
        _enemyLayer=LayerMask.GetMask(HashEnemyLayer);
        Reset();
    }
    
    private void Reset()
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
        var enemyAround = Physics.OverlapSphere(_bullet.transform.position, _maxDistanceToNextTarget, _enemyLayer).Except(new Collider[] { currentEnemy.GetComponent<Collider>()}).ToList();

        if (enemyAround.Count != 0)
        {
            bullet.SetTargetPoint(enemyAround[Random.Range(0, enemyAround.Count)].transform.position);
            _currentRebound++;
            return true;
        }
        else
        {
            return false;
        }
    }
}
