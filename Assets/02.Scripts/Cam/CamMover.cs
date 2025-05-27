using System;
using Unity.Cinemachine;
using UnityEngine;

public class CamMover : MonoBehaviour
{
    [SerializeField] private PlayerInputSO playerInput;

    [SerializeField] private float speed;
    [SerializeField] private CinemachineCamera came;

    private void Awake()
    {
        playerInput.OnZoomOutCamEvent += HandleZoomOutCam;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void OnDestroy()
    {
        playerInput.OnZoomOutCamEvent -= HandleZoomOutCam;
    }

    private void HandleZoomOutCam(float zoomOut)
    {
        came.Lens.FieldOfView = Mathf.Clamp(came.Lens.FieldOfView + (zoomOut * 10f), 60f,  150f);
    }

    private void MoveCamera()
    {
        Vector3 forward = Vector3.forward * playerInput.MovementKey.y;
        Vector3 right = Vector3.right * playerInput.MovementKey.x;

        Quaternion rotation = Quaternion.Euler(0, came.transform.eulerAngles.y, 0);
        Vector3 moveDirection = rotation * (forward + right);

        transform.position += moveDirection.normalized * speed * Time.deltaTime;
    }
}
