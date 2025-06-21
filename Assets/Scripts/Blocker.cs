using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public PlayerLogic playerLogic;
    public int goal;
    void FixedUpdate()
    {
        if (playerLogic.Score == goal)
        {
            Destroy(gameObject);
        }
    }
}
