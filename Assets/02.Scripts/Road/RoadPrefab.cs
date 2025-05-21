using UnityEngine;

public class RoadPrefab : MonoBehaviour
{
    [SerializeField] private Transform[] roadChecks;
    public bool isRoad { get; private set; } = false;
    public bool IsBuilding = false;
    [SerializeField] private LayerMask whatIsRoad;
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
                return true;
                
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsBuilding)
        {
            isRoad = false;
        }
    }


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
}
