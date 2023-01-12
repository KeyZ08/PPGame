using Character.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CrowShallCast : MonoBehaviour
{
    public GameObject ShallPrefab;
    public Transform StartPosLeft;
    public Transform StartPosRight;
    public float speed = 3;
    
    private Transform shallTr;
    private GameObject shallObj;
    private Transform player;

    private void Awake()
    {
        shallObj = Instantiate(ShallPrefab, StartPosLeft.position, Quaternion.identity);
        shallTr = shallObj.transform;
        shallTr.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ShallCast(Directions direction)
    {
        if (direction == Directions.E)
            shallTr.position = StartPosRight.position;
        else if (direction == Directions.W)
            shallTr.position = StartPosLeft.position;
        shallObj.SetActive(true);
        var way = shallObj.AddComponent<Waypoints>();
        way.Points = GetTrajectory();
    }

    private Vector2[] GetTrajectory()
    {
        return CalculateTrajectory(shallTr.position, player.position);
    }

    private Vector2[] CalculateTrajectory(Vector3 start, Vector3 end)
    {
        var distance = Vector2.Distance(start, end);
        int pointsCount = (int)(distance * (100 / speed)) + 1;
        Vector2[] points = new Vector2[pointsCount];
        float distanceBtwPoint = distance / pointsCount;
        for (int i = 0; i < pointsCount; i++)
        {
            Vector2 _direction = (Vector2)(end - start);
            Vector2 pos = (Vector2)start + _direction.normalized * distanceBtwPoint * i;
            points[i] = pos;
        }
        return points;
    }
}
