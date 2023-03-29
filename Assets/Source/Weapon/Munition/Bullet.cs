using System;
using System.Collections;
using UnityEngine;

public class Bullet : Munition
{
    [SerializeField] private float _speed=1;
    [SerializeField] protected int _damage=1;
    [SerializeField] private AfterHitAction _afterHitAction;
    private Vector3 target;

    private void Update()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target) < 0.01)
            {
                MovingToTarget();
            }
            else
            {
                Destroy();
            }
        }
    }

    public void Initialization(Vector3 newTarget)
    {        
        target = newTarget+new Vector3(0,0.5f,0);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void MovingToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Health.ApplyDamage(_damage);
        }
        else
        {
            enemy = null;
        }

        _afterHitAction.Action(this,enemy);
    }
}
