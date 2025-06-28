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
    public int Goal;
    public SpriteRenderer spriteRenderer;
    public Text ScoreText;
    public Text HealthText;
    public Text GoalText;
    public bool canTakeDamage = true;
    public float invincibilityDuration = 3f;
    public float flashDelay = 0.1f;
    private bool isInvincible = false;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public GameObject PausePanel;
    public GameObject PauseButton;

    public AudioSource audioSource;

    public AudioClip HorilkaSound;
    public AudioClip DamageSound;
    public AudioClip HealSound;
    public AudioClip PowerUpSound;
    public AudioClip WinSound;
    public AudioClip JumpSound;

    public void PlayHorilka() => audioSource.PlayOneShot(HorilkaSound);
    public void PlayDamage() => audioSource.PlayOneShot(DamageSound);
    public void PlayHeal() => audioSource.PlayOneShot(HealSound);
    public void PlayPowerUp() => audioSource.PlayOneShot(PowerUpSound);
    public void PlayWin() => audioSource.PlayOneShot(WinSound);
    public void PlayJump() => audioSource.PlayOneShot(JumpSound);

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
            PlayJump();
            jumpCount--;
        }

        if (rb.velocity.y == 0)
        {
            jumpCount = 1;
        }
        if (Score == Goal)
        {
            GoalText.color = new Color32(31, 152, 10, 225);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        Health -= damage;
        PlayDamage();
        UpdateHealth();

        if (Health <= 0)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
            PauseButton.SetActive(false);
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
            PlayHorilka();
            UpdateText();
        }

        if (other.gameObject.CompareTag("Speed"))
        {
            MoveSpeed = 10;
            Destroy(other.gameObject);
            PlayPowerUp();
            StartCoroutine(SpeedTimer());
        }

        if (other.gameObject.CompareTag("Jump"))
        {
            JumpForce = 14;
            Destroy(other.gameObject);
            PlayPowerUp();
            StartCoroutine(JumpTimer());
        }

        if (other.gameObject.CompareTag("Health"))
        {
            Health++;
            Destroy(other.gameObject);
            PlayHeal();
            UpdateHealth();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            Time.timeScale = 0;
            WinPanel.SetActive(true);
            PauseButton.SetActive(false);
            PlayWin();
        }
        if (other.gameObject.CompareTag("Death"))
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
            PauseButton.SetActive(false);
            PlayDamage();
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
    public void Pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        PauseButton.SetActive(false);

    }
    public void Continue()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
    }
}
