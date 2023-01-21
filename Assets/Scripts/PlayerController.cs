using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Controls input;
    [SerializeField] Tilemap tilemap;
    [SerializeField] private float speed = 4f;
    private Vector2 movementInput;
    private Vector3 oldpos;
    private Vector3 newpos;
    private float t = 0f;

    void Awake()
    {
        input = new Controls();
    }
    private void OnEnable()
    {
        input.Player.Movement.Enable();
    }

    private void OnDisable()
    {
        input.Player.Movement.Disable();
    }

    //bool hasMoved;
    void Update()
    {
        movementInput = input.Player.Movement.ReadValue<Vector2>();

        if (t > 1f)
        {
            t = 0f;
            transform.position = newpos;
        } else if (t > 0f)
        {
            transform.position = Vector3.Lerp(oldpos, newpos, t);
            t += Time.deltaTime * speed;
        } else if (movementInput.magnitude > .5f)
        {
            if (movementInput.y == 0f) movementInput.y = movementInput.x * 0.1f;
            GetMovementDirection();
        }
    }

    public void GetMovementDirection()
    {
        Vector3Int newtile = tilemap.WorldToCell(transform.position + Vector3.Normalize(movementInput) * .5f);
        if (tilemap.GetTile(newtile) == null)
        {
            oldpos = transform.position;
            newpos = tilemap.CellToWorld(newtile);
            t += Time.deltaTime * speed;
        }
    }
}
