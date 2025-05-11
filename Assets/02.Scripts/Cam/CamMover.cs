using UnityEngine;

public class CamMover : MonoBehaviour
{
    [SerializeField] private PlayerInputSO playerInput;

    [SerializeField] private float speed;
    [SerializeField] private GameObject came;

    private void Update()
    {
        MoveCamera();
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
