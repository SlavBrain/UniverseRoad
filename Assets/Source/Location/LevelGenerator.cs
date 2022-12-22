using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("LevelSettings")]
    [SerializeField] private int _roadWidth;
    [SerializeField] private int _offRoadwdth;
    [SerializeField] private int _viewDistanceForward;
    [SerializeField] private int _viewDistanceBackward;

    [Header("GeneratorSettings")]
    [SerializeField] private Transform _viewPoint;
    [SerializeField] private FieldGenerator _roadGenerator;
    [SerializeField] private FieldGenerator _offRoadGenerator;


    private void OnValidate()
    {
        if (_roadGenerator == null)
        {
            Debug.LogError(gameObject.name + ": missing RoadGenerator");
        }
        if (_offRoadGenerator == null)
        {
            Debug.LogError(gameObject.name + ": missing OffRoadGenerator");
        }
    }
}
