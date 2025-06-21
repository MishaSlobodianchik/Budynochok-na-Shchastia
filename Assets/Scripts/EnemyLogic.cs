using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    public int spot = 0;
    public Transform[] moveSpots;

    private Vector3 originalScale;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // зберігаємо початковий масштаб
    }

    void FixedUpdate()
    {
        Vector2 targetPos = moveSpots[spot].position;
        Vector2 newPos = Vector2.MoveTowards(rb2d.position, targetPos, speed * Time.fixedDeltaTime);

        // Змінюємо напрямок спрайту без зміни розміру
        if (newPos.x < rb2d.position.x)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else if (newPos.x > rb2d.position.x)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }

        rb2d.MovePosition(newPos);

        if (Vector2.Distance(rb2d.position, targetPos) < 0.2f)
        {
            spot = (spot == 1) ? 0 : 1;
        }
    }
}

