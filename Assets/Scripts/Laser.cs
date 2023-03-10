using UnityEngine;
using UnityEngine.Tilemaps;

public class Laser : MonoBehaviour
{
    public static int bounces = 10;
    [SerializeField] float offset = 0;
    Vector3[] points;
    [SerializeField] LineRenderer lineRenderer;
    private int bounced = 0;
    private Tilemap tilemap;
    private float destroyTimer = 0;
    float destructionTime = 1f;

    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 6)*60);
        tilemap = FindObjectOfType<Tilemap>();
    }

    void FixedUpdate()
    {
        destroyTimer += Time.fixedDeltaTime;
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
                        bounced = i + 1;
                        if (ray.collider.tag == "Player")
                        {
                            ray.collider.GetComponent<PlayerController>().Die();
                        }
                        if (destroyTimer > destructionTime && ray.collider.tag != "Indestructible")
                        {
                            tilemap.SetTile(tilemap.WorldToCell(pos - (Vector3)ray.normal * 0.1f), null);
                            destroyTimer = 0;
                        }
                    }
                }
                else
                {
                    pos = pos + dir * 1000f;
                    bouncing = false;
                    bounced = i + 1;
                }
            }
            points[i] = pos;
        }
        Vector3[] positions = Generate_Points(points, bounced, 100);
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    Vector3[] Generate_Points(Vector3[] keyPoints, int length = 0, int segments = 100)
    {
        Vector3[] Points = new Vector3[(length - 1) * segments + length];
        if (length == 0) length = keyPoints.Length;
        for (int i = 1; i < length; i++)
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
        Points[(length - 1) * segments + length - 1] = new Vector3(keyPoints[length - 1].x, keyPoints[length - 1].y, 0);
        return Points;
    }
}
