using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    
    public float walkSpeed;
    Vector2 direction;

    [SerializeField]
    float speedFactor;

    private bool stopMove = false;


    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (!stopMove)
        {

            Vector2 vel = direction * walkSpeed;

            body.velocity = vel.normalized * speedFactor;

            
        }
        else
        {
            
            body.velocity = direction * 0;
        }
    }

    public void setMovement(bool b)
    {
        stopMove = b;
    }
}
