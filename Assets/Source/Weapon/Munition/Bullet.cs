using System.Collections;
using UnityEngine;

public class Bullet : Munition
{
    [SerializeField] private float _speed=1;
    [SerializeField] protected int _damage=1;
    [SerializeField] private AfterHitAction _afterHitAction;
    private Vector3 target;

    private Coroutine _moving;

    public void Initialization(Vector3 newTarget)
    {        
        target = newTarget+new Vector3(0,0.5f,0);
        StartMoveToPoint();
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }


    private void StartMoveToPoint()
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MovingToTarget());
    }

    private IEnumerator MovingToTarget()
    {
        while (Vector3.Distance(transform.position, target) >0.01)
        {
            transform.position = Vector3.MoveTowards(transform.position,target,_speed*Time.deltaTime);
            yield return null;
        }

        Destroy();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Debug.Log("hit");
            enemy.Health.ApplyDamage(_damage);
        }
        else
        {
            enemy = null;
        }

        _afterHitAction.Action(this,enemy);
    }
}
