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

        _lineRenderer.startWidth = 0.8f;
        _lineRenderer.endWidth = 0.8f;
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
                Vector3[] rawCorners = path.corners;
                List<Vector3> smooth = BezierSmooth(rawCorners, 4);


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

    private List<Vector3> BezierSmooth(Vector3[] corners, int steps = 5)
    {
        var pts = new List<Vector3>();
        for (int i = 0; i < corners.Length - 1; i++)
        {
            Vector3 a = corners[i];
            Vector3 b = corners[i + 1];
            Vector3 mid = (a + b) * 0.5f;
            Vector3 dir = (b - a).normalized;
            Vector3 perp = Vector3.Cross(dir, Vector3.up) * 0.3f;

            Vector3 cp1 = mid + perp;
            Vector3 cp2 = mid - perp;

            for (int j = 0; j <= steps; j++)
            {
                float t = j / (float)steps;
                Vector3 p = Mathf.Pow(1 - t, 3) * a +
                            3 * Mathf.Pow(1 - t, 2) * t * cp1 +
                            3 * (1 - t) * Mathf.Pow(t, 2) * cp2 +
                            Mathf.Pow(t, 3) * b;

                if (pts.Count == 0 || Vector3.Distance(pts[^1], p) > 0.01f)
                    pts.Add(p);
            }
        }

        if (pts.Count == 0 || Vector3.Distance(pts[^1], corners[^1]) > 0.01f)
            pts.Add(corners[^1]);

        return pts;
    }


}
