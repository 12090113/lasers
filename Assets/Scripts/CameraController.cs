using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    private List<PlayerController> players;
    [SerializeField] float paddingSize = 5f, cameraSpeed = 5f, zoomSpeed = 5f, zoomOutMin = 3f, zoomOutMax = 7f;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        players = FindObjectOfType<PlayerManager>().GetPlayers();
    }

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        if (players.Count == 0) return;
        Vector3 averagePosition = Vector3.zero;
        for (int i = 0; i < players.Count; i++)
        {
            averagePosition += players[i].transform.position;
        }
        averagePosition /= players.Count;
        Vector3 cameraMoveVelocity = new Vector3(averagePosition.x, averagePosition.y, -10f) - transform.position;
        transform.position += cameraMoveVelocity * cameraSpeed * Time.deltaTime;
    }

    private void ZoomCamera()
    {
        float minX = float.PositiveInfinity;
        float maxX = float.NegativeInfinity;
        float minY = float.PositiveInfinity;
        float maxY = float.NegativeInfinity;

        for (int i = 0; i < players.Count; i++)
        {
            minX = Mathf.Min(minX, players[i].transform.position.x);
            maxX = Mathf.Max(maxX, players[i].transform.position.x);
            minY = Mathf.Min(minY, players[i].transform.position.y);
            maxY = Mathf.Max(maxY, players[i].transform.position.y);
        }

        float width = maxX - minX + paddingSize;
        float height = maxY - minY + paddingSize;

        float orthoSize = Mathf.Max(width / mainCamera.aspect * .5f, height * 0.5f);
        orthoSize = Mathf.Clamp(orthoSize, zoomOutMin, zoomOutMax);

        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, orthoSize, zoomSpeed * Time.deltaTime);
    }
}