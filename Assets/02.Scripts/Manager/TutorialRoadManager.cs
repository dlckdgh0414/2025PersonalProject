using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialRoadManager : MonoBehaviour
{
    [SerializeField] private PlayerInputSO bulidInput;
    [SerializeField] private Grid mapGrid;
    [SerializeField] private Transform roadTrm;
    [SerializeField] private GameEventChannelSO buildObjectUI;
    [SerializeField] private GameEventChannelSO buildObject;
    [SerializeField] private GameEventChannelSO audioChange;
    [SerializeField] private GameEventChannelSO tutorialUIEvent;
    [SerializeField] private GameObject selectUI;
    [SerializeField] private GameObject bulidUI;
    [SerializeField] private GameObject tooltipUI;
    [SerializeField] private AudioClip roadMakeSFX;
    [SerializeField] private GameObject tutorialUI;
    private RoadPrefab _roadBlackPrefab;
    private RoadPrefab _roadPrivePrefab;
    private float RotBuildObject = 0;
    private int _buildCost;
    private RoadPrefab _currentroad = null;
    private Stack<RoadPrefab> _roadHistory = new Stack<RoadPrefab>();

    public UnityEvent<bool> OnConstructionModeChage;
    public UnityEvent OnUpdateRoad;

    private bool _isConstructionMode;

    private HashSet<Vector3Int> _roadPoints;
    private MeshFilter _meshFilter;
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
        foreach (Transform child in roadTrm)
        {
            RoadPrefab road = child.GetComponent<RoadPrefab>();
            if (road != null)
            {
                Vector3Int cell = mapGrid.WorldToCell(road.transform.position);
                _roadPoints.Add(cell);
            }
        }
        bulidInput.OnBuildPressed += HandleClick;
        bulidInput.OnBuildModeChange += HandleBuildModeChange;
        bulidInput.OnRotObjectEvent += HandleRoatObject;
        bulidInput.OnDelRoadEvent += HandleDelRoad;
        buildObjectUI.AddListener<BuildObjectUI>(HadnleBuildObjectChange);
        buildObject.AddListener<BuildObjectCheck>(HandleBuildCheck);
        tutorialUIEvent.AddListener<TutorialUIEvent>(HandleTutoriaUI);
    }

    private void HandleTutoriaUI(TutorialUIEvent evt)
    {
        ConstructionMode = !evt.IsTutorial;
    }

    private void OnDestroy()
    {
        bulidInput.OnBuildPressed -= HandleClick;
        bulidInput.OnBuildModeChange -= HandleBuildModeChange;
        bulidInput.OnRotObjectEvent -= HandleRoatObject;
        bulidInput.OnDelRoadEvent -= HandleDelRoad;
        buildObjectUI.RemoveListener<BuildObjectUI>(HadnleBuildObjectChange);
        buildObject.RemoveListener<BuildObjectCheck>(HandleBuildCheck);
        tutorialUIEvent.RemoveListener<TutorialUIEvent>(HandleTutoriaUI);

    }

    private void HandleDelRoad()
    {
        if (_roadHistory.Count > 0 && ConstructionMode)
        {
            RoadPrefab roadToDelete = _roadHistory.Pop();
            Vector3Int cell = mapGrid.WorldToCell(roadToDelete.transform.position);
            _roadPoints.Remove(cell);

            buildObject.RaiseEvent(BuildEvents.DelObject.Initializer(_buildCost));
            Destroy(roadToDelete.gameObject);
        }
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
            center.y = 0.1f;
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
        if (_roadPrivePrefab != null)
        {
            Destroy(_roadPrivePrefab.gameObject);
        }
        ConstructionMode = changeModeValue;
        selectUI.SetActive(!changeModeValue);
        tooltipUI.SetActive(!changeModeValue);
        bulidUI.SetActive(changeModeValue);
    }

    private int _count = 0;
    private void HandleClick()
    {
        if (ConstructionMode == false || _roadBlackPrefab == null || _isBuilding == false) return;

        if (CanPlaceRoad(_roadPrivePrefab))
        {
            if (_roadPoints.Add(GetCellSize()))
            {
                Vector3 center = mapGrid.GetCellCenterWorld(GetCellSize());
                center.y = 0.1f;
                _currentroad = Instantiate(_roadBlackPrefab, center, Quaternion.Euler(0, RotBuildObject, 0));
                audioChange.RaiseEvent(AudioEvents.AudioChangeEvent.Initializer(AudioType.SFX, roadMakeSFX, false));
                _currentroad.transform.SetParent(roadTrm);
                _currentroad.IsBuilding = true;
                _roadHistory.Push(_currentroad);
                buildObject.RaiseEvent(BuildEvents.BuildObject.Initializer(_buildCost, true));
                Destroy(_roadPrivePrefab.gameObject);
                if(_count <= 0)
                {
                    tutorialUIEvent.RaiseEvent(TutorialEvents.TutorialUIEvent.Initializer(true));
                    _count++;
                }
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
