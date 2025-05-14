using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class RoadPrefab : MonoBehaviour
{
    [SerializeField] private RoadCheckRay[] roadChecks;
    public bool isRoad {get; private set;} = false;
    public bool IsBuilding = false;
    public bool isFirtsBuilding = false;

    private void Update()
    {
        if (!IsBuilding)
        {
            BuildingCheck();
        }
        else
        {
            isRoad=false;
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
                if (!isFirtsBuilding)
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
}
