using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public int Health = 3;
    private Rigidbody2D rb;
    public bool isGrounded;
    public int jumpCount = 1;
    public int Score = 0;
    public SpriteRenderer spriteRenderer;
    public Text ScoreText;
    public Text HealthText;
    public bool canTakeDamage = true;
    public float invincibilityDuration = 3f;
    public float flashDelay = 0.1f;
    private bool isInvincible = false;
    public GameObject GameOverPanel;
    public GameObject WinPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * MoveSpeed, rb.velocity.y);
        if (moveInput < 0)
            spriteRenderer.flipX = false;
        else if (moveInput > 0)
            spriteRenderer.flipX = true;
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

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        Health -= damage;
        UpdateHealth();

        if (Health <= 0)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
        else
        {
            StartCoroutine(InvincibilityFlash());
        }
    }

    IEnumerator InvincibilityFlash()
    {
        isInvincible = true;

        float elapsed = 0f;

        while (elapsed < invincibilityDuration)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDelay);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDelay);

            elapsed += flashDelay * 2;
        }

        isInvincible = false;
    }
    void ResetInvincibility()
    {
    canTakeDamage = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1;
        }

        if (other.gameObject.CompareTag("Horilka"))
        {
            Destroy(other.gameObject);
            Score++;
            UpdateText();
        }

        if (other.gameObject.CompareTag("Speed"))
        {
            MoveSpeed = 10;
            Destroy(other.gameObject);
            StartCoroutine(SpeedTimer());
        }

        if (other.gameObject.CompareTag("Jump"))
        {
            JumpForce = 14;
            Destroy(other.gameObject);
            StartCoroutine(JumpTimer());
        }

        if (other.gameObject.CompareTag("Health"))
        {
            Health++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            Time.timeScale = 0;
            WinPanel.SetActive(true);
        }
        if (other.gameObject.CompareTag("Death"))
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
    }

    IEnumerator SpeedTimer()
{
    yield return new WaitForSeconds(10f);
    MoveSpeed = 5;
}

IEnumerator JumpTimer()
{
    yield return new WaitForSeconds(10f);
    JumpForce = 10;
}

    public void UpdateText()
    {
        ScoreText.text = Score.ToString();
    }
    public void UpdateHealth()
    {
        HealthText.text = Health.ToString();
    }

}
