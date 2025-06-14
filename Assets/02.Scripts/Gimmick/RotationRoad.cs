using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;

public class RotationRoad : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;

    private async void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            await Awaitable.WaitForSecondsAsync(0.5f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y - 90, 0);
            navMeshSurface.BuildNavMesh();
        }
    }
}
