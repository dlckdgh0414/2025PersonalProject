using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private PlayerInputSO bulidInput;
    [SerializeField] private Grid mapGrid;
    public GameObject roadBlackPrefab;

    public UnityEvent<bool> OnConstructionModeChage;
    public UnityEvent OnUpdateRoad;

    private bool _isConstructionMode;

    private HashSet<Vector3Int> _roadPoints;
    private MeshFilter _meshFilter;

    public bool ConstructionMode
    {
        get => _isConstructionMode;
        set
        {
            _isConstructionMode = value;
            OnConstructionModeChage?.Invoke(value);
        }
    }

    private void Awake()
    {
        _roadPoints = new HashSet<Vector3Int>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = new Mesh();
        bulidInput.OnBuildPressed += HandleClick;
        bulidInput.OnBuildModeChange += HandleBuildModeChange;
    }

    private void OnDestroy()
    {
        bulidInput.OnBuildPressed -= HandleClick;
        bulidInput.OnBuildModeChange -= HandleBuildModeChange;
    }

    private void HandleBuildModeChange(bool changeModeValue)
    {
        ConstructionMode = changeModeValue;
    }

    private void HandleClick()
    {
        if (ConstructionMode == false) return;

        Vector3 worldPosition = bulidInput.GetWorldPosition();
        Vector3Int cellPoint = mapGrid.WorldToCell(worldPosition);

        if (_roadPoints.Add(cellPoint))
        {
            Vector3 center = mapGrid.GetCellCenterWorld(cellPoint);
            center.y = 0;
            GameObject road = Instantiate(roadBlackPrefab, center, Quaternion.identity);
            road.transform.SetParent(transform);

            OnUpdateRoad?.Invoke();
        }
    }
}
