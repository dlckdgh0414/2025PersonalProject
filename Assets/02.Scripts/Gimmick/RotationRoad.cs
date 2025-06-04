using Unity.AI.Navigation;
using UnityEngine;

public class RotationRoad : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y -90, 0);
            navMeshSurface.BuildNavMesh();
        }
    }
}
