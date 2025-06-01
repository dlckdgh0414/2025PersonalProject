using Unity.AI.Navigation;
using UnityEngine;

public class DisposableRoad : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;

    private void OnDestroy()
    {
        navMeshSurface.BuildNavMesh();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Destroy(gameObject);
        }
    }
}
