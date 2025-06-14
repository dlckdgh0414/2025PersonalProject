using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.AI.Navigation;
using DG.Tweening;

[RequireComponent(typeof(LineRenderer))]
public class WayPoints : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private Transform player;
    [SerializeField] private Material channelMat;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private float lineHeight = 0.1f;
    [SerializeField] private float dashLength = 0.3f;
    [SerializeField] private float gapLength = 0.2f;

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
        _lineRenderer.alignment = LineAlignment.View;
        _lineRenderer.numCornerVertices = 5;
        _lineRenderer.numCapVertices = 3;
        _lineRenderer.startWidth = 0.8f;
        _lineRenderer.endWidth = 0.8f;
        _lineRenderer.startColor = Color.yellow;
        _lineRenderer.endColor = Color.yellow;
        _lineRenderer.material.color = Color.yellow;
    }

    public void HideLineRenderer()
    {
        _lineRenderer.enabled = false;
    }

    public void SetWayPoint()
    {
        navMeshSurface.BuildNavMesh();
        _wayPoints = GetComponentsInChildren<WayPoint>();
        transform.SetParent(null);
        DrawNavMeshDottedPath();
    }

    private void DrawNavMeshDottedPath()
    {
        if (_wayPoints.Length == 0 || _agent == null) return;

        var allPathPoints = new List<Vector3>();
        Vector3 currentPos = player.position;


        for (int i = 0; i < _wayPoints.Length; i++)
        {
            Vector3 targetPos = _wayPoints[i].transform.position;

            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(currentPos, targetPos, NavMesh.AllAreas, path))
            {
                int startIndex = (i == 0) ? 0 : 1;

                for (int j = startIndex; j < path.corners.Length; j++)
                {
                    Vector3 point = path.corners[j];
                    point.y += lineHeight;
                    allPathPoints.Add(point);
                }

                currentPos = targetPos;
            }
            else
            {
                // NavMesh 경로를 찾을 수 없으면 직선으로 연결
                if (allPathPoints.Count == 0)
                {
                    allPathPoints.Add(currentPos + Vector3.up * lineHeight);
                }
                Vector3 point = targetPos;
                point.y += lineHeight;
                allPathPoints.Add(point);
                currentPos = targetPos;
            }
        }

        if (allPathPoints.Count < 2) return;

        var dottedPoints = ConvertPathToDottedLine(allPathPoints);

        _lineRenderer.positionCount = dottedPoints.Count;
        _lineRenderer.SetPositions(dottedPoints.ToArray());
        _lineRenderer.enabled = true;
    }

    private List<Vector3> ConvertPathToDottedLine(List<Vector3> pathPoints)
    {
        var dottedPoints = new List<Vector3>();

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Vector3 start = pathPoints[i];
            Vector3 end = pathPoints[i + 1];

            var segmentDottedPoints = CreateDashedLine(start, end);

            int startIdx = (i == 0) ? 0 : 1;
            for (int j = startIdx; j < segmentDottedPoints.Count; j++)
            {
                dottedPoints.Add(segmentDottedPoints[j]);
            }
        }

        return dottedPoints;
    }

    private List<Vector3> CreateDashedLine(Vector3 start, Vector3 end)
    {
        var points = new List<Vector3>();

        Vector3 direction = (end - start).normalized;
        float totalDistance = Vector3.Distance(start, end);
        float currentDistance = 0f;

        bool isDash = true;
        Vector3 lastPoint = start;

        points.Add(start);

        while (currentDistance < totalDistance)
        {
            float segmentLength = isDash ? dashLength : gapLength;
            float remainingDistance = totalDistance - currentDistance;

            if (segmentLength > remainingDistance)
            {
                segmentLength = remainingDistance;
            }

            currentDistance += segmentLength;
            Vector3 nextPoint = start + direction * currentDistance;

            if (isDash)
            {
                points.Add(nextPoint);
            }
            else
            {
                points.Add(lastPoint);
                points.Add(nextPoint);
            }

            lastPoint = nextPoint;
            isDash = !isDash;
        }

        if (points.Count > 0 && Vector3.Distance(points[points.Count - 1], end) > 0.01f)
        {
            points.Add(end);
        }

        return points;
    }
}