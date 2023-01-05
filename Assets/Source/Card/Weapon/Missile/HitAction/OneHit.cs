using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHit : MonoBehaviour, IHit
{
    public void Hit()
    {
        Debug.Log("Bullet HIT");
    }
}
