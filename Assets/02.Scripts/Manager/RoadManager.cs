using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private PlayerInputSO bulidInput;
    [SerializeField] private Grid mapGrid;
    [SerializeField] private GameEventChannelSO buildObjectUI;
    [SerializeField] private GameObject buildUI;
    private RoadPrefab _roadBlackPrefab;
    private RoadPrefab _roadPrivePrefab;
    private float RotBuildObject =0;
    private int _buildCost;

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
        bulidInput.OnRotObjectEvent += HandleRoatObject;
        buildObjectUI.AddListener<BuildObjectUI>(HadnleBuildObjectChange);
        buildUI.SetActive(true);
    }

    private void OnDestroy()
    {
        bulidInput.OnBuildPressed -= HandleClick;
        bulidInput.OnBuildModeChange -= HandleBuildModeChange;
        bulidInput.OnRotObjectEvent -= HandleRoatObject;
        buildObjectUI.AddListener<BuildObjectUI>(HadnleBuildObjectChange);

    }

    private void Update()
    {
        if (ConstructionMode)
        {
            Vector3 placementPos = bulidInput.GetWorldPosition();
            if(_roadPrivePrefab == null)
            {
                _roadPrivePrefab = Instantiate(_roadBlackPrefab);
                _roadPrivePrefab.gameObject.SetActive(true);
            }
            placementPos.y = 0.5f;
            _roadPrivePrefab.transform.position = placementPos;
            _roadPrivePrefab.transform.rotation = Quaternion.Euler(0, RotBuildObject, 0); 
        }
    }

    private void HandleRoatObject()
    {
        RotBuildObject += 90;
    }

    private void HadnleBuildObjectChange(BuildObjectUI evnt)
    {
        _roadPrivePrefab = null;
        _roadBlackPrefab = evnt.buildObject;
        _buildCost = evnt.buildCost;
    }

    private void HandleBuildModeChange(bool changeModeValue)
    {
        if(_roadPrivePrefab != null)
        {
            _roadPrivePrefab.gameObject.SetActive(changeModeValue);    
        }
        ConstructionMode = changeModeValue;
        buildUI.SetActive(!changeModeValue);
    }

    private void HandleClick()
    {
        if (ConstructionMode == false || _roadBlackPrefab ==null) return;

        Vector3 worldPosition = bulidInput.GetWorldPosition();
        Vector3Int cellPoint = mapGrid.WorldToCell(worldPosition);

        if (_roadPoints.Add(cellPoint))
        {
            Vector3 center = mapGrid.GetCellCenterWorld(cellPoint);
            center.y = 0.5f;
            RoadPrefab road = Instantiate(_roadBlackPrefab, center, Quaternion.Euler(0,RotBuildObject,0));
            road.transform.SetParent(transform);
            road.IsBuilding = true;

            OnUpdateRoad?.Invoke();
        }
    }
}
