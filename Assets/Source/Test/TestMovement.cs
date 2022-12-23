using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [SerializeField]private float _speed=5;

    private void Update()
    {
        transform.position += new Vector3(0, 0, _speed*Time.deltaTime);
    }
}
