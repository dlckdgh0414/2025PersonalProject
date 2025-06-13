using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Input/PlayerInput")]
public class PlayerInputSO : ScriptableObject, InputSystem_Actions.IPlayerActions
{

    public Action OnBuildPressed;
    public Action<bool> OnBuildModeChange;
    public Action OnRotObjectEvent;
    public Action<float> OnZoomOutCamEvent;
    public Action OnDelRoadEvent;

    private InputSystem_Actions _controls;

    [SerializeField] private LayerMask whatIsGround;

    public Vector2 MovementKey { get; private set; }
    public bool IsBuildChannel { get; set; } = false;
    public RoadPrefab clickRoadPrefab { get; set; } = null;

    private Vector3 _worldPosition;
    private Vector2 _screenPosition;

    private int _changeCount = 0;

    private void OnEnable()
    {
        if(_controls == null)
        {
            _controls = new InputSystem_Actions();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    public void OnBuild(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnBuildPressed?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movekey = context.ReadValue<Vector2>();
        MovementKey = movekey;
    }

    public Vector3 GetWorldPosition()
    {
        Camera mainCam = Camera.main;
        Debug.Assert(mainCam != null, "No main camera in this scene");

        Ray cameraRay = mainCam.ScreenPointToRay(_screenPosition);
        if (Physics.Raycast(cameraRay, out RaycastHit hit, mainCam.farClipPlane, whatIsGround))
        {
            if (hit.collider.TryGetComponent(out RoadPrefab road))
            {
                IsBuildChannel = true;
                clickRoadPrefab = road;
            }
            else
            {
                IsBuildChannel = false;
                clickRoadPrefab = null;
            }

            if (hit.collider.TryGetComponent(out GimmickToolTip gimmick))
            {
                gimmick.RaiseGimickToolTip();
            }
            _worldPosition = hit.point;
        }

        return _worldPosition;
    }

    public void OnPointer(InputAction.CallbackContext context)
    {
        _screenPosition = context.ReadValue<Vector2>();
    }

    public void OnChangeBuildMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(_changeCount == 0)
            {
                OnBuildModeChange?.Invoke(true);
                _changeCount = 1;
            }
            else
            {
                OnBuildModeChange?.Invoke(false);
                _changeCount = 0;
            }
        }
    }

    public void OnRotObject(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnRotObjectEvent?.Invoke();
        }
    }

    public void OnMouseScrollY(InputAction.CallbackContext context)
    {
        float scroll = context.ReadValue<float>();
        OnZoomOutCamEvent?.Invoke(scroll);
    }

    public void OnDelRoad(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnDelRoadEvent?.Invoke();
        }
    }
}
