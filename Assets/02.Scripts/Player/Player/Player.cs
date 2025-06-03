using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private WayPoints wayPoints;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameEventChannelSO playerEvent;
    [SerializeField] private Collider playerCnt;
    private bool _isStart = false;
    private int _currentPointIdx = 0;

    private void Start()
    {
        playerEvent.AddListener<StartPlayerEvent>(HandleStartPlayer);
    }

    private void OnDestroy()
    {
        playerEvent.RemoveListener<StartPlayerEvent>(HandleStartPlayer);
    }

    private void HandleStartPlayer(StartPlayerEvent evt)
    {
        _isStart = evt.IsStart;
        playerCnt.isTrigger = false;
    }

    public void Update()
    {
        if (_isStart)
        {
            playerMovement.SetDestination(wayPoints[_currentPointIdx].position);
            if (playerMovement.IsArrived)
            {
                _currentPointIdx++;
                if(_currentPointIdx >= wayPoints.Length)
                {
                    _currentPointIdx--;
                }
            }
        }
    }
}
