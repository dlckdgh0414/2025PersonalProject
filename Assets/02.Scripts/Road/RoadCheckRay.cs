using UnityEngine;

public class RoadCheckRay : MonoBehaviour
{

    [SerializeField] private LayerMask whatIsRoad;
    [SerializeField] private float distance = 1f;
    public bool RoadCheck()
    {
        bool isRoad = Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, distance, whatIsRoad);
        Debug.Log(isRoad);
        return isRoad;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.forward * distance);
    }
}
