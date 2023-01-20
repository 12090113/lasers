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
        //transform.right = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
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
                }
                else
                {
                    pos = pos + dir * 1000f;
                    bouncing = false;
                }
            }
            points[i] = pos;
        }

        lineRenderer.positionCount = bounces;
        lineRenderer.SetPositions(points);
    }
}
