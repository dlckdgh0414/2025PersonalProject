using UnityEngine;

public class RoadPrefab : MonoBehaviour
{
    [SerializeField] private Transform[] rayCastTrm;
    [SerializeField] private LayerMask whatIsRoad;

    public bool CanBuildObject()
    {
        return false;
    }
}
