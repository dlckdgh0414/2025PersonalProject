using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private PlayerInputSO bulidInput;
    [SerializeField] private Grid mapGrid;
    [SerializeField] private Transform roadTrm;
    [SerializeField] private GameEventChannelSO buildObjectUI;
    [SerializeField] private GameEventChannelSO buildObject;
    [SerializeField] private GameObject selectUI;
    [SerializeField] private GameObject bulidUI;
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
    private bool _isBuilding = true;

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
        buildObject.AddListener<BuildObjectCheck>(HandleBuildCheck);
        selectUI.SetActive(true);
        bulidUI.SetActive(false);
    }

    private void OnDestroy()
    {
        bulidInput.OnBuildPressed -= HandleClick;
        bulidInput.OnBuildModeChange -= HandleBuildModeChange;
        bulidInput.OnRotObjectEvent -= HandleRoatObject;
        buildObjectUI.RemoveListener<BuildObjectUI>(HadnleBuildObjectChange);
        buildObject.RemoveListener<BuildObjectCheck>(HandleBuildCheck);

    }

    private void HandleBuildCheck(BuildObjectCheck check)
    {
        _isBuilding = check.IsBuild;
    }

    private void Update()
    {
        if (ConstructionMode && _roadBlackPrefab != null && _isBuilding)
        {
            buildObject.RaiseEvent(BuildEvents.BuildObject.Initializer(_buildCost));
            Vector3 center = mapGrid.GetCellCenterWorld(GetCellSize());
            if (_roadPrivePrefab == null)
            {
                _roadPrivePrefab = Instantiate(_roadBlackPrefab, center, Quaternion.Euler(0, RotBuildObject, 0));
            }
            center.y = 0.5f;
            _roadPrivePrefab.transform.position = center;
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
        selectUI.SetActive(!changeModeValue);
        bulidUI.SetActive(changeModeValue);
    }

    private void HandleClick()
    {
        if (ConstructionMode == false || _roadBlackPrefab ==null || _isBuilding == false) return;

        if (CanPlaceRoad(_roadPrivePrefab))
        {
            if (_roadPoints.Add(GetCellSize()))
            {
                Vector3 center = mapGrid.GetCellCenterWorld(GetCellSize());
                center.y = 0.5f;
                RoadPrefab road = Instantiate(_roadBlackPrefab, center, Quaternion.Euler(0,RotBuildObject,0));
                road.transform.SetParent(roadTrm);
                road.IsBuilding = true;
                buildObject.RaiseEvent(BuildEvents.BuildObject.Initializer(_buildCost,true));
                Destroy(_roadPrivePrefab.gameObject);
                OnUpdateRoad?.Invoke();
            }
        }
    }

    private Vector3Int GetCellSize()
    {
        Vector3 worldPosition = bulidInput.GetWorldPosition();
        Vector3Int cellPoint = mapGrid.WorldToCell(worldPosition);
        return cellPoint;
    }

    public bool CanPlaceRoad(RoadPrefab roadPrefab)
    {
        return roadPrefab.RoadCheck();
    }
}
