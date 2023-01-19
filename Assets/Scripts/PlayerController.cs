using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += speed * Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += speed * Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += speed * Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += speed * Vector2.right;
        }
    }
}
