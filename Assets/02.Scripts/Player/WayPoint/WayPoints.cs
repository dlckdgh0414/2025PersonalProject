using UnityEngine;

public class WayPoints : MonoBehaviour
{

    [SerializeField]private WayPoint[] _wayPoints;

    public int Length => _wayPoints.Length;

    public Transform this[int index]
    {
        get
        {
            return _wayPoints[index].transform;
        }
    }

    public void SetWayPoint()
    {
        _wayPoints = GetComponentsInChildren<WayPoint>();
        transform.SetParent(null);
    }
}
