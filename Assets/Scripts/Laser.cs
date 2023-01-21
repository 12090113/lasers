using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public static int bounces = 10;
    public float offset = 0;
    Vector3[] points;
    [SerializeField] LineRenderer lineRenderer;

    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 6)*60);
    }

    void FixedUpdate()
    {
        points = new Vector3[bounces+1];
        Vector3 dir = transform.right;
        Vector3 pos = transform.position + offset * dir;
        points[0] = pos;
        bool bouncing = true;
        for (int i = 1; i <= bounces; i++)
        {
            if (bouncing)
            {
                RaycastHit2D ray = Physics2D.Raycast(pos + dir * 0.01f, dir);
                if (ray.collider != null)
                {
                    pos = ray.point;
                    dir = dir - 2f * Vector3.Dot(dir, ray.normal) * (Vector3)ray.normal;
                    if (ray.collider.tag != "Reflective")
                    {
                        bouncing = false;
                    }
                }
                else
                {
                    pos = pos + dir * 1000f;
                    bouncing = false;
                }
            }
            points[i] = pos;
        }

        lineRenderer.positionCount = bounces * 100;
        lineRenderer.SetPositions(Generate_Points(points));
    }

    Vector3[] Generate_Points(Vector3[] keyPoints, int segments = 100)
    {
        Vector3[] Points = new Vector3[(keyPoints.Length - 1) * segments + keyPoints.Length];
        for (int i = 1; i < keyPoints.Length; i++)
        {
            Points[(i - 1) * segments + i - 1] = new Vector3(keyPoints[i - 1].x, keyPoints[i - 1].y, 0);
            for (int j = 1; j <= segments; j++)
            {
                float x = keyPoints[i - 1].x;
                float y = keyPoints[i - 1].y;
                float z = 0;
                float dx = (keyPoints[i].x - keyPoints[i - 1].x) / segments;
                float dy = (keyPoints[i].y - keyPoints[i - 1].y) / segments;
                Points[(i - 1) * segments + j + i - 1] = new Vector3(x + dx * j, y + dy * j, z);
            }
        }
        Points[(keyPoints.Length - 1) * segments + keyPoints.Length - 1] = new Vector3(keyPoints[keyPoints.Length - 1].x, keyPoints[keyPoints.Length - 1].y, 0);
        return Points;
    }
}
