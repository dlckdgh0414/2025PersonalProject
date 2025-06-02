using System.Linq;
using UnityEngine;

public class RoadPrefab : MonoBehaviour
{
    [SerializeField] private Transform[] roadChecks;

    public bool isRoad { get; private set; } = false;
    public bool IsBuilding = false;
    [SerializeField] private LayerMask whatIsRoad,whatIsGround;
    [SerializeField] private float distance = 1f;

    private void Update()
    {
        if (!IsBuilding)
        {
            isRoad = RoadCheck();
        }
        else
        {
            isRoad = false;
        }
    }

    public bool RoadCheck()
    {
        foreach (var check in roadChecks)
        {
            if (Physics.Raycast(check.position, check.forward, out RaycastHit hit, distance, whatIsRoad))
            {
                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitGround, 0.6f, whatIsGround))
                {
                    return true;
                }

            }
        }
        return false;
    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (roadChecks != null)
        {
            foreach (var check in roadChecks)
            {
                if (check != null)
                    Gizmos.DrawRay(check.position, check.forward * distance);
            }
        }
    }

#endif
}
