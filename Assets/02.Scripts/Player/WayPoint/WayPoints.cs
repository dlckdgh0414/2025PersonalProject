using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.AI.Navigation;

[RequireComponent(typeof(LineRenderer))]
public class WayPoints : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private Transform player;
    [SerializeField] private Material channelMat;
    [SerializeField] private NavMeshSurface navMeshSurface;

    private LineRenderer _lineRenderer;
    private NavMeshAgent _agent;

    public int Length => _wayPoints.Length;
    public Transform this[int index] => _wayPoints[index].transform;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _agent = player.GetComponent<NavMeshAgent>();

        _lineRenderer.enabled = false;
        _lineRenderer.material = channelMat;
        _lineRenderer.textureMode = LineTextureMode.Tile;
        _lineRenderer.startWidth = 2f;
        _lineRenderer.endWidth = 2f;
    }

    public void SetWayPoint()
    {
        navMeshSurface.BuildNavMesh();
        _wayPoints = GetComponentsInChildren<WayPoint>();
        transform.SetParent(null);
        DrawFullNavPath();
    }

    private void DrawFullNavPath()
    {
        if (_wayPoints.Length == 0 || _agent == null) return;

        List<Vector3> fullPathPoints = new List<Vector3>();
        Vector3 currentPos = player.position;

        foreach (var wp in _wayPoints)
        {
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(currentPos, wp.transform.position, NavMesh.AllAreas, path))
            {
                if (fullPathPoints.Count > 0 && fullPathPoints[^1] == path.corners[0])
                    fullPathPoints.AddRange(path.corners[1..]);
                else
                    fullPathPoints.AddRange(path.corners);

                currentPos = wp.transform.position;
            }
        }

        if (fullPathPoints.Count < 2) return;

        _lineRenderer.positionCount = fullPathPoints.Count;
        _lineRenderer.SetPositions(fullPathPoints.ToArray());

        float totalLength = 0f;
        for (int i = 1; i < fullPathPoints.Count; i++)
            totalLength += Vector3.Distance(fullPathPoints[i - 1], fullPathPoints[i]);

        _lineRenderer.material.mainTextureScale = new Vector2(totalLength, 1);
        _lineRenderer.enabled = true;
    }
}
