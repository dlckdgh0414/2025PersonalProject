using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class ChannelMake : MonoBehaviour
{
    [SerializeField] private PlayerInputSO playerInput;
    [SerializeField] private WayPoints wayPoints;
    [SerializeField] private WayPoint wayPoint;
    private Stack<WayPoint> _wayHistory = new Stack<WayPoint>();
    private WayPoint _way;

    private void OnEnable()
    {
        playerInput.OnBuildPressed += HandelChannelBuild;
        playerInput.OnDelRoadEvent += HandleChannelDestory;
    }

    private void OnDisable()
    {
        playerInput.OnBuildPressed -= HandelChannelBuild;
        playerInput.OnDelRoadEvent -= HandleChannelDestory;
    }

    private void HandleChannelDestory()
    {
        if(_wayHistory.Count > 0)
        {
            Destroy(_wayHistory.Pop());
            wayPoints.SetWayPoint();
        }
    }

    private void HandelChannelBuild()
    {
       Vector3 mousePos = playerInput.GetWorldPosition();
        if (playerInput.IsBuildChannel)
        {
            _way = Instantiate(wayPoint,mousePos, Quaternion.identity);
            _way.transform.SetParent(wayPoints.transform);
            _wayHistory.Push(_way);
            playerInput.IsBuildChannel = false;
        }
        wayPoints.SetWayPoint();
    }
}
