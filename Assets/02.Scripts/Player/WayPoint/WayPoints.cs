using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WayPoints : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private Transform player; // 플레이어 Transform 지정
    [SerializeField] private Material channelMat;
    private LineRenderer _lineRenderer;

    public int Length => _wayPoints.Length;
    public Transform this[int index] => _wayPoints[index].transform;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _lineRenderer.material = channelMat; 
        _lineRenderer.textureMode = LineTextureMode.Tile;
        _lineRenderer.startWidth = 0.4f;
        _lineRenderer.endWidth = 0.4f;
        _lineRenderer.startColor = Color.yellow;
        _lineRenderer.endColor = Color.yellow;
        _lineRenderer.material.color = Color.yellow;
    }

    public void SetWayPoint()
    {
        _wayPoints = GetComponentsInChildren<WayPoint>();
        transform.SetParent(null);
        DrawPath();
    }

    private void DrawPath()
    {
        if (_wayPoints == null || _wayPoints.Length == 0 || player == null) return;

        _lineRenderer.positionCount = _wayPoints.Length + 1; // +1 for player
        _lineRenderer.SetPosition(0, player.position); // 시작점은 플레이어 위치

        for (int i = 0; i < _wayPoints.Length; i++)
        {
            _lineRenderer.SetPosition(i + 1, _wayPoints[i].transform.position);
        }

        _lineRenderer.enabled = true;
    }
}
