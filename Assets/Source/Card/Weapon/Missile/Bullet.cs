using System.Collections;
using UnityEngine;

public class Bullet : Missile
{
    [SerializeField] private float _speed=1;
    [SerializeField] private float _damage=1;

    private Vector3 target;

    private Coroutine _moving;

    public void Initialization(Vector3 newTarget)
    {
        target = newTarget;
        StartMoveToPoint();
    }

    private void Hit()
    {

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
    }
}
