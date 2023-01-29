using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDragger : MonoBehaviour
{
    private const float _maxRayDistance = 1000f;

    [SerializeField] private LayerMask _unitLayer;

    [SerializeField] private bool _isDragging;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _selectedObject;

    private Vector3 _previousTargetPosition;

    private Coroutine _dragging;

    private void OnEnable()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (Input.GetMouseButtonUp(0)&&_isDragging)
        {
            StopDragging();
        }
    }

    private void StartDragging()
    {
        if (TryTakeObject(out _selectedObject))
        {
            _isDragging = true;

            if (_dragging != null)
            {
                StopCoroutine(_dragging);
            }

            _dragging = StartCoroutine(DraggingTest());
        }
    }

    private void StopDragging()
    {
        _isDragging = false;
        _selectedObject.GetComponent<Unit>().Merge();
        _selectedObject.transform.position = _previousTargetPosition;
    }

    private bool TryTakeObject(out GameObject selectedObject)
    {
        selectedObject = null;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray.origin, ray.direction,out hit,_maxRayDistance,_unitLayer))
        {
            _selectedObject = hit.collider.gameObject;
        }

        return _selectedObject != null;
    }

    private IEnumerator DraggingTest()
    {
        _previousTargetPosition = _selectedObject.transform.position;
        LayerMask _ignoreUnitMask = ~_unitLayer;        
        RaycastHit hit;

        while (_isDragging)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out hit,1000, _ignoreUnitMask))
            {
                _selectedObject.transform.position = new Vector3(hit.point.x, _previousTargetPosition.y, hit.point.z);
            }

            yield return null;
        }
    }
}
