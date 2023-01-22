using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] private float speed = 4f;
    private Vector2 movementInput;
    private Vector3 oldpos;
    private Vector3 newpos;
    private float t = 0f;

    private void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    void Update()
    {

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

    private void GetMovementDirection()
    {
        Vector3Int newtile = tilemap.WorldToCell(transform.position + Vector3.Normalize(movementInput) * .5f);
        if (tilemap.GetTile(newtile) == null)
        {
            oldpos = transform.position;
            newpos = tilemap.CellToWorld(newtile);
            t += Time.deltaTime * speed;
        }
    }

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
