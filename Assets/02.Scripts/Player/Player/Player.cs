using System;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private WayPoints wayPoints;
    public PlayerMovement playerMovement;
    [SerializeField] private GameEventChannelSO playerEvent;
    [SerializeField] private GameEventChannelSO audioChangeEvent;
    [SerializeField] private Collider playerCnt;
    public Transform deliveryTrm;
    [SerializeField] private bool isLoop;
    [HideInInspector] public bool isMove = false;
    private int _currentPointIdx = 0;
    [HideInInspector] public GameObject foodObj = null;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip motorcyclesfx;

    private void Start()
    {
        playerEvent.AddListener<StartPlayerEvent>(HandleStartPlayer);
    }

    private void OnDestroy()
    {
        audioChangeEvent.RaiseEvent(new AudioChangeEvent().Initializer(AudioType.SFX, null, false, 0.5f));
        playerEvent.RemoveListener<StartPlayerEvent>(HandleStartPlayer);
    }

    private async void HandleStartPlayer(StartPlayerEvent evt)
    {
        await Awaitable.WaitForSecondsAsync(2f);
        isMove = evt.IsStart;
        playerCnt.isTrigger = false;
        animator.SetBool("MOVE", true);
        audioChangeEvent.RaiseEvent(new AudioChangeEvent().Initializer(AudioType.SFX, motorcyclesfx,true,0.5f));
        wayPoints.HideLineRenderer();
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
        if (isMove)
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
