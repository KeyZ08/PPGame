using System.Drawing;
using System.Linq;
using TMPro;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public GameObject Target;
    private Transform _targetPos;
    public float maxLength;

    public Transform ItemPosition;
    public Transform StartPosition;
    public GameObject PointPrefab;
    public int PointsCount = 10;
    
    private Transform[] _trajectoryPoints;
    private SpriteRenderer[] _pointsRenderer;
    private SpriteRenderer targetRenderer;

    private bool _isVisible;

    void Awake()
    {
        _targetPos = Target.transform;
        targetRenderer = Target.GetComponent<SpriteRenderer>();
        CreateTrajectoryPoints();
        Hide();
    }

    private void CreateTrajectoryPoints()
    {
        _trajectoryPoints = new Transform[PointsCount];
        _pointsRenderer = new SpriteRenderer[PointsCount];
        for (int i = 0; i < PointsCount; i++)
        {
            _trajectoryPoints[i] = Instantiate(PointPrefab, transform).transform;
            _pointsRenderer[i] = _trajectoryPoints[i].GetComponent<SpriteRenderer>();
        }
        _pointsRenderer[0].enabled = false;
        _pointsRenderer[PointsCount - 1].enabled = false;
    }

    void Update()
    {
        if (!_isVisible) return;
        _targetPos.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var trajectory = CalculateTrajectory(PointsCount);

        for (int i = 0; i < _trajectoryPoints.Length; i++)
            _trajectoryPoints[i].position = trajectory[i];
    }

    private Vector3[] CalculateTrajectory(int pointsCount)
    {
        Vector3[] points = new Vector3[pointsCount];
        float distance = Vector2.Distance(_targetPos.position, StartPosition.position);
        if (distance > maxLength)
        {
            _targetPos.position = StartPosition.position + (_targetPos.position - StartPosition.position) * (maxLength / distance);
            distance = maxLength;
        }
        float distanceBtwPoint = distance / pointsCount;

        for (int i = 0; i < pointsCount; i++)
            points[i] = CalculatePointGravitySimulator(i * distanceBtwPoint);

        //корректировка положения точек (поднятие их на нужный уровень и смещение в сторону чтобы точки старта и назначения соответствовали первой и последней точкам дуги)
        var dirY = _targetPos.position.y - points[pointsCount - 1].y;
        var dirX = _targetPos.position.x - points[pointsCount - 1].x;
        for (int i = pointsCount - 1; i > 0; i--)
        {
            points[i] += Vector3.up * (dirY * i / (pointsCount - 1));
            points[i] += Vector3.right * (dirX * i / (pointsCount - 1));
        }
        return points;
    }

    private Vector2 CalculatePointGravitySimulator(float distance)
    {
        Vector2 _direction = (Vector2)(_targetPos.position - StartPosition.position);
        Vector2 pos = (Vector2)StartPosition.position
            + _direction.normalized * distance
            + Vector2.down * distance * distance * 0.3f;
        return pos;
    }

    public Vector2[] GetPoints()
    {
        float _distance = Vector2.Distance(_targetPos.position, StartPosition.position);
        return CalculateTrajectory((int)(_distance * 10) + 1).Select(p => (Vector2)p).ToArray();
    }

    public void Show()
    {
        _isVisible = true;
        Update();
        targetRenderer.enabled = true;
        for (int point = 1; point < PointsCount - 1; point++)
            _pointsRenderer[point].enabled = true;
    }

    public void Hide()
    {
        _isVisible = false;
        targetRenderer.enabled = false;
        for (int point = 1; point < PointsCount - 1; point++)
            _pointsRenderer[point].enabled = false;
    }
}
