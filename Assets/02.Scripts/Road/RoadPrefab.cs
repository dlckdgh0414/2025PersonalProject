using UnityEngine;

public class RoadPrefab : MonoBehaviour
{
    [SerializeField] private Transform[] roadChecks;
    [SerializeField] private Transform[] groundChecks;
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
                foreach(var groundCheck in groundChecks)
                {
                    if (Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hitGround, 1, whatIsGround))
                    {
                            isRoad = true;
                            return true;
                        
                    }
                }
                
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
