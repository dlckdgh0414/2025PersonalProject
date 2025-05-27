using System;
using UnityEngine;
using UnityEngine.AI;
 public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float speed;
    [SerializeField] private float stopOffset = 0.05f;

    [SerializeField] private const float RotateSpeed = 10f;

    private void Awake()
    {
        agent.speed = speed;
    }

    public bool IsArrived => !agent.pathPending &&  agent.remainingDistance < agent.stoppingDistance + stopOffset;
   public float RemainDistance => agent.pathPending ? -1 : agent.remainingDistance;

    public void SetStop(bool isStop) => agent.isStopped = isStop;
    public void SetVelocity(Vector3 velocity) => agent.velocity = velocity;
    public void SetSpeed(float speed) => agent.speed = speed;
    public void SetDestination(Vector3 destination) => agent.SetDestination(destination);
}
