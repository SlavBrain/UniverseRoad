using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("LevelSettings")]
    [SerializeField] private int _roadWidth = 3;
    [SerializeField] private int _offRoadWidth = 3;
    [SerializeField] private int _viewDistanceForward = 10;
    [SerializeField] private int _viewDistanceBackward = 5;

    [Header("GeneratorSettings")]
    [SerializeField] private Transform _viewPoint;
    [SerializeField] private FieldGenerator _roadGenerator;
    [SerializeField] private FieldWithEnviromentGenerator _offRoadGenerator;
    [SerializeField] private float _cellSize = 1;

    private HashSet<Vector3Int> _mapMatrix = new HashSet<Vector3Int>();
    private Queue<GameObject> _visibleCell=new Queue<GameObject>();
    private int _nessesaryCellCount;

    private void OnEnable()
    {
        _nessesaryCellCount = WorldDistanseToGridDistanse(_roadWidth + _offRoadWidth)*2 * WorldDistanseToGridDistanse(_viewDistanceBackward + _viewDistanceForward);
    }

    private void Update()
    {
        FillRadius(_viewPoint.position);
    }

    private void FillRadius(Vector3 center)
    {
        var cellCountOnAxisX = WorldDistanseToGridDistanse(_roadWidth + _offRoadWidth);
        var cellCountOnAxisZForward = WorldDistanseToGridDistanse(_viewDistanceForward);
        var cellCountOnAxisZBackward = WorldDistanseToGridDistanse(_viewDistanceBackward);
        var fillAreaCenter = WorldToGridPosition(center-transform.position);

        for (int z = cellCountOnAxisZBackward; z > -cellCountOnAxisZForward; z--)
        {
            for (int x = -cellCountOnAxisX; x <= cellCountOnAxisX; x++)
            {
                TryCreateField(fillAreaCenter + new Vector3Int(x, 0, z));
            }
        }
    }

    private void TryCreateField(Vector3Int gridPosition)
    {
        if (_mapMatrix.Contains(gridPosition))
            return;
        else
        {
            _mapMatrix.Add(gridPosition);
            Vector3 spawnPosition = GridToWorldPosition(gridPosition)+transform.position;
            GameObject template;

            if (Mathf.Abs(gridPosition.x) <= _roadWidth / _cellSize)
            {
                template = _roadGenerator.SpawnObject(spawnPosition);
            }
            else
            {
                template = _offRoadGenerator.SpawnObject(spawnPosition);
            }
            
            _visibleCell.Enqueue(template);
            CheckPresenceUnnessaseryCells();
        }
    }

    private void CheckPresenceUnnessaseryCells()
    {
        if (_visibleCell.Count >= _nessesaryCellCount&&WorldDistanseToGridDistanse(_viewDistanceBackward+_viewDistanceForward)>1)
        {
            DeactivateCellRow();
        }
    }

    private void DeactivateCellRow()
    {
        int _countCellInRow = WorldDistanseToGridDistanse((_roadWidth + _offRoadWidth) * 2);

        for(int i = 0; i<= _countCellInRow; i++)
        {
            var cell = _visibleCell.Dequeue();
            cell.SetActive(false);
        }
    }

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

    private int WorldDistanseToGridDistanse(float worldDistanse)
    {
        return (int)(worldDistanse / _cellSize);
    }
}
