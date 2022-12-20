using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("SpawningObjects")][Space]
    [SerializeField] private EnviromentsForSpawn[] _bigObjects;
    [SerializeField] private EnviromentsForSpawn[] _smallObjects;

    [Header("GenerateSettings")][Space]
    [SerializeField] private int _countPregeneratedPlatforms;
    [SerializeField] private int _countPassedPlatform;
    [SerializeField] private int _roadWidth;
    [SerializeField] private int _offRoad÷idth;

    [Header("PlatformSetting")][Space]
    [SerializeField] private GameObject[] _roadCellTemplate;
    [SerializeField] private int _RoadCellSize;
    [SerializeField] private GameObject[] _offroadCellTemplate;
    [SerializeField] private int _offroadCellSize;

    private HashSet<Vector3Int> _mapMatrix = new HashSet<Vector3Int>();

    private Vector3 GridToWorldPosition(Vector3Int gridPosition)
    {
        return new Vector3(
            gridPosition.x * _cellSize,
            gridPosition.y * _cellSize,
            gridPosition.z * _cellSize);
    }

    private Vector3Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int(
            (int)(worldPosition.x / _cellSize),
            (int)(worldPosition.y / _cellSize),
            (int)(worldPosition.z / _cellSize));
    }
}

[Serializable]
internal struct EnviromentsForSpawn
{
    [SerializeField] private GameObject[] enviroment;
    [Range(0, 100)] [SerializeField] private int _chanse;
}