using UnityEngine;

public class RoadCheckRay : MonoBehaviour
{

    [SerializeField] private LayerMask whatIsRoad;
    [SerializeField] private float distance = 1f;
    public Collider roadCnt;
    public bool isFirtsBuilding = false;
    public bool RoadCheck()
    {
        bool isRoad = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance, whatIsRoad);
        roadCnt = hit.collider;
        return isRoad;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.forward * distance);
    }
}
