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
    private bool _isFirstBuilding = false;

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
        if (ConstructionMode && _roadBlackPrefab != null)
        {
            Vector3 placementPos = bulidInput.GetWorldPosition();
            if(_roadPrivePrefab == null)
            {
                _roadPrivePrefab = Instantiate(_roadBlackPrefab);
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
            Destroy(_roadPrivePrefab.gameObject);
        }
        ConstructionMode = changeModeValue;
        buildUI.SetActive(!changeModeValue);
    }

    private void HandleClick()
    {
        if (ConstructionMode == false || _roadBlackPrefab ==null) return;

        Vector3 worldPosition = bulidInput.GetWorldPosition();
        Vector3Int cellPoint = mapGrid.WorldToCell(worldPosition);

        if (CanPlaceRoad(_roadPrivePrefab))
        {
            if (_roadPoints.Add(cellPoint))
            {
                Vector3 center = mapGrid.GetCellCenterWorld(cellPoint);
                center.y = 0.5f;
                RoadPrefab road = Instantiate(_roadBlackPrefab, center, Quaternion.Euler(0,RotBuildObject,0));
                road.transform.SetParent(transform);
                road.IsBuilding = true;
                if (!_isFirstBuilding)
                {
                    _isFirstBuilding = true;
                }

                OnUpdateRoad?.Invoke();
            }
        }
    }

    public bool CanPlaceRoad(RoadPrefab roadPrefab)
    {
        if (!_isFirstBuilding)
        {
            _isFirstBuilding = true;
            return true;
        }

        return roadPrefab.RoadCheck();
    }
}
