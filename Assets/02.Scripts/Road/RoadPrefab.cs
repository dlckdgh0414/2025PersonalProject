using UnityEngine;

public class RoadPrefab : MonoBehaviour
{
    [SerializeField] private RoadCheckRay[] roadChecks;
    public bool isRoad {get; private set;} = false;
    public bool IsBuilding = false;

    private void Update()
    {
        if (!IsBuilding)
        {
            BuildingCheck();
        }
    }

    public void BuildingCheck()
    {
        foreach (RoadCheckRay road in roadChecks)
        {
            if (road.RoadCheck())
            {
                isRoad = true;
            }
            else
            {
                isRoad = false;
            }
        }
    }
}
