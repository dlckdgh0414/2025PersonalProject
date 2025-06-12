using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float speed;
    [SerializeField] private float stopOffset = 0.05f;

    [SerializeField] private float maxLeanAngle = 25f;
    [SerializeField] private float leanSpeed = 5f;

    [SerializeField] private Transform modelTransform;
    [SerializeField] private Transform dirveCam;

    private void Awake()
    {
        agent.speed = speed;
    }

    private void Update()
    {
        ApplyLeaningEffect();
        ApplyDriveCam();
    }

    private void ApplyLeaningEffect()
    {
        if (modelTransform == null) return;

        Vector3 currentDir = agent.velocity.normalized;
        Vector3 desiredDir = (agent.steeringTarget - transform.position).normalized;

        if (agent.velocity.magnitude < 0.1f || desiredDir == Vector3.zero)
        {
            Vector3 euler = modelTransform.localEulerAngles;
            euler.z = Mathf.LerpAngle(euler.z, 0f, Time.deltaTime * leanSpeed);
            modelTransform.localEulerAngles = euler;
            return;
        }

        float angleDelta = Vector3.SignedAngle(currentDir, desiredDir, Vector3.up);
        float speedFactor = Mathf.Clamp01(agent.velocity.magnitude / agent.speed);
        float targetLean = Mathf.Clamp(-angleDelta, -35f, 35f) * speedFactor;

        Vector3 currentEuler = modelTransform.localEulerAngles;
        currentEuler.z = Mathf.LerpAngle(currentEuler.z, targetLean, Time.deltaTime * leanSpeed);
        modelTransform.localEulerAngles = currentEuler;
        
    }
    private void ApplyDriveCam()
    {
        if (dirveCam == null) return;

        Vector3 currentDir = agent.velocity.normalized;
        Vector3 desiredDir = (agent.steeringTarget - transform.position).normalized;

        if (agent.velocity.magnitude < 0.1f || desiredDir == Vector3.zero)
        {
            Vector3 euler = dirveCam.localEulerAngles;
            euler.z = Mathf.LerpAngle(euler.z, 0f, Time.deltaTime * leanSpeed);
            dirveCam.localEulerAngles = euler;
            return;
        }

        float angleDelta = Vector3.SignedAngle(currentDir, desiredDir, Vector3.up);
        float speedFactor = Mathf.Clamp01(agent.velocity.magnitude / agent.speed);
        float targetLean = Mathf.Clamp(-angleDelta, -60f, 60f) * speedFactor;

        Vector3 currentEuler = dirveCam.localEulerAngles;
        currentEuler.z = Mathf.LerpAngle(currentEuler.z, targetLean, Time.deltaTime * leanSpeed);
        dirveCam.localEulerAngles = currentEuler;

    }

    public bool IsArrived => !agent.pathPending && agent.remainingDistance < agent.stoppingDistance + stopOffset;
    public float RemainDistance => agent.pathPending ? -1 : agent.remainingDistance;
    public void SetStop(bool isStop) => agent.isStopped = isStop;
    public void SetVelocity(Vector3 velocity) => agent.velocity = velocity;
    public void SetSpeed(float speed) => agent.speed = speed;
    public void SetDestination(Vector3 destination) => agent.SetDestination(destination);
}
