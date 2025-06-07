using System;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private WayPoints wayPoints;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameEventChannelSO playerEvent;
    [SerializeField] private Collider playerCnt;
    [SerializeField] private bool isLoop;
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

    private async void HandleStartPlayer(StartPlayerEvent evt)
    {
        await Awaitable.WaitForSecondsAsync(2f);
        _isStart = evt.IsStart;
        playerCnt.isTrigger = false;
    }

    public void Update()
    {
        if (isLoop)
        {
            playerMovement.SetDestination(wayPoints[_currentPointIdx].position);
            if (playerMovement.IsArrived)
            {
                _currentPointIdx++;
                if (_currentPointIdx >= wayPoints.Length)
                {
                    _currentPointIdx = (_currentPointIdx + 1) % wayPoints.Length;
                }
            }
        }
        if (_isStart)
        {
            playerMovement.SetDestination(wayPoints[_currentPointIdx].position);
            if (playerMovement.IsArrived && !isLoop)
            {
                _currentPointIdx++;
                if (_currentPointIdx >= wayPoints.Length)
                {
                    _currentPointIdx--;
                }
            }
           
        }

    }
}
