using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadPrefab : MonoBehaviour
{
    [SerializeField] private Transform[] roadChecks;

    public bool isRoad { get; private set; } = false;
    public bool IsBuilding = false;
    [SerializeField] private LayerMask whatIsRoad,whatIsGround,whatIsBuilding;
    [SerializeField] private float distance = 1f;
    [SerializeField] private Vector3 boxSize = new Vector3(1f, 1f, 1f);
    [SerializeField] private int cost;
    [SerializeField] private GameEventChannelSO buildObject;

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

    public void DestoryRoad(Grid grid,HashSet<Vector3Int> mapcell)
    {
        Vector3Int cell = grid.WorldToCell(gameObject.transform.position);
        mapcell.Remove(cell);
        buildObject.RaiseEvent(BuildEvents.DelObject.Initializer(cost));
        Destroy(gameObject);
    }


    public bool IsBuildingNearby()
    {
        Vector3 center = transform.position;
        Vector3 halfExtents = boxSize * 0.5f;
        Quaternion orientation = Quaternion.identity;

        Collider[] hits = Physics.OverlapBox(center, halfExtents, orientation, whatIsBuilding);
        return hits.Length > 0;
    }


    public bool RoadCheck()
    {
        if (IsBuildingNearby())
        {
            return false;
        }

        foreach (var check in roadChecks)
        {
            if (Physics.Raycast(check.position, check.forward, out RaycastHit hit, distance, whatIsRoad))
            {
                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitGround, 0.9f, whatIsGround))
                {
                    return true;
                }

            }
        }
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            isRoad = false;
        }
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

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, boxSize* 0.5f);
    }

#endif
}
