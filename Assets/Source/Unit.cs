using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _rightHand;
    [SerializeField] private GameObject _weapon;

    public void Initialize(GameObject weapon)
    {
        _weapon = Instantiate(weapon, _rightHand.position, Quaternion.identity, _rightHand);
    }
}
