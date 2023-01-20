using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Vector3 _targetPoint;
    private Coroutine _moving;

    private void OnDisable()
    {
        if (_moving != null)
            StopCoroutine(_moving);
    }

    public void Initialization(Vector3 target)
    {
        _targetPoint = target;
        StartMoving();
    }

    private void StartMoving()
    {
        if (_moving != null)
            StopCoroutine(_moving);

        _moving = StartCoroutine(MovingToTarget());
    }

    private IEnumerator MovingToTarget()
    {
        while (Vector3.Distance(_targetPoint, transform.position) > 0.01)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint, Time.deltaTime * _maxSpeed);
            yield return null;
        }
    }
}
