using System;
using UnityEngine;

public class ChannelMake : MonoBehaviour
{
    [SerializeField] private PlayerInputSO playerInput;
    [SerializeField] private WayPoints wayPoints;
    [SerializeField] private WayPoint wayPoint;
    private WayPoint _way;

    private void OnEnable()
    {
        playerInput.OnBuildPressed += HandelChannelBuild;
    }

    private void OnDisable()
    {
        playerInput.OnBuildPressed -= HandelChannelBuild;
    }

    private void HandelChannelBuild()
    {
       Vector3 mousePos = playerInput.GetWorldPosition();
        if (playerInput.IsBuildChannel)
        {
            _way = Instantiate(wayPoint,mousePos, Quaternion.identity);
            _way.transform.SetParent(wayPoints.transform);
            playerInput.IsBuildChannel = false;
        }
    }
}
