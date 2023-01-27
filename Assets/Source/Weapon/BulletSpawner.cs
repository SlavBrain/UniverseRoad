using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class BulletSpawner : Spawner
{
    private Weapon _weapon;
    protected override void GetExternalData()
    {
        _weapon = GetComponent<Weapon>();
        _templates[0] = _weapon.Bullet.gameObject;
        _capacity = _weapon.MaxBulletCount;
        _container = _weapon.gameObject;

    }
}
