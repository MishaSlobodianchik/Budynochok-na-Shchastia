using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public Rigidbody2D rb2d;   
    public float speed;
    public int spot = 0;
    public Transform[] moveSpots;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
{
    Vector2 targetPos = moveSpots[spot].position;
    Vector2 newPos = Vector2.MoveTowards(rb2d.position, targetPos, speed * Time.fixedDeltaTime);
    rb2d.MovePosition(newPos);

    if (Vector2.Distance(rb2d.position, targetPos) < 0.2f)
    {
        spot = (spot == 1) ? 0 : 1;
    }
}
}

