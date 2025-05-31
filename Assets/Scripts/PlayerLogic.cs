using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public int Health = 3;
    private Rigidbody2D rb;
    public bool isGrounded;
    public int jumpCount = 1;
    public int Score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * MoveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            jumpCount--;
        }
        if (rb.velocity.y == 0)
        {
            jumpCount = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1;
        }

        if (other.gameObject.CompareTag("Horilka"))
        {
            Score++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Speed"))
        {
            MoveSpeed = MoveSpeed * 2;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Jump"))
        {
            JumpForce = JumpForce + 2;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Health"))
        {
            Health++;
            Destroy(other.gameObject);
        }
    }
}
