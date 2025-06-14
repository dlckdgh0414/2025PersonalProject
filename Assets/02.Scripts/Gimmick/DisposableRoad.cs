using System;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;

public class DisposableRoad : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;

    private void OnDestroy()
    {
        navMeshSurface.BuildNavMesh();
    }

    private async void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            await Awaitable.WaitForSecondsAsync(1.5f);
            Destroy(gameObject);
        }
    }
}
