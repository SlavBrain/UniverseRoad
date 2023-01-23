using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : Bullet
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.LogWarning("enter");
        if(other.collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Debug.LogWarning("enter enemy");
            enemy.TakeDamage(_damage);
        }

        Destroy();
    }
}
