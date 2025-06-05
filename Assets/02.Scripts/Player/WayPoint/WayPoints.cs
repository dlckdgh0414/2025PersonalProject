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
        _lineRenderer.alignment = LineAlignment.TransformZ;
        _lineRenderer.alignment = LineAlignment.View;
        _lineRenderer.numCornerVertices = 5;
        _lineRenderer.numCapVertices = 3;

        _lineRenderer.startWidth = 0.4f;
        _lineRenderer.endWidth = 0.4f;
        _lineRenderer.startColor = Color.yellow;
        _lineRenderer.endColor = Color.yellow;
        _lineRenderer.material.color = Color.yellow;
    }

    private void Update()
    {
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
                List<Vector3> smooth = CatmullRomPath(path.corners, 10);

                if (fullPathPoints.Count > 0 && fullPathPoints[^1] == smooth[0])
                    fullPathPoints.AddRange(smooth.GetRange(1, smooth.Count - 1));
                else
                    fullPathPoints.AddRange(smooth);

                currentPos = wp.transform.position;
            }
        }

        if (fullPathPoints.Count < 2) return;

        _lineRenderer.positionCount = fullPathPoints.Count;
        _lineRenderer.SetPositions(fullPathPoints.ToArray());
        _lineRenderer.enabled = true;
    }

    private List<Vector3> CatmullRomPath(Vector3[] corners, int resolution = 10)
    {
        List<Vector3> smoothPoints = new List<Vector3>();

        for (int i = 0; i < corners.Length - 1; i++)
        {
            Vector3 p0 = i == 0 ? corners[i] : corners[i - 1];
            Vector3 p1 = corners[i];
            Vector3 p2 = corners[i + 1];
            Vector3 p3 = i + 2 < corners.Length ? corners[i + 2] : corners[i + 1];

            for (int j = 0; j < resolution; j++)
            {
                float t = j / (float)resolution;
                Vector3 point = 0.5f * (
                    2f * p1 +
                    (-p0 + p2) * t +
                    (2f * p0 - 5f * p1 + 4f * p2 - p3) * (t * t) +
                    (-p0 + 3f * p1 - 3f * p2 + p3) * (t * t * t)
                );

                smoothPoints.Add(point);
            }
        }

        smoothPoints.Add(corners[^1]); 
        return smoothPoints;
    }
}
