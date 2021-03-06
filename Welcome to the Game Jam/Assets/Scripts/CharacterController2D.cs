﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float speed;

    public float walkAcceleration;

    public float airAcceleration;

    public float groundDeceleration;

    public float jumpHeight;

    public BoxCollider2D boxCol;

    public Vector2 velocity;

    private bool grounded;

    public SpriteRenderer sr;

    public bool collided = true;

    // Start is called before the first frame update
    void Awake()
    {
        boxCol = GetComponent<BoxCollider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
       
        float moveInput = Input.GetAxisRaw("Horizontal");
        float acceleration = grounded ? walkAcceleration : airAcceleration;
        float deceleration = grounded ? groundDeceleration : 0;
        transform.Translate(velocity * Time.deltaTime);

        if(grounded)
        {
            velocity.y = 0;

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
               
            }
        }

        velocity.y += Physics2D.gravity.y * Time.deltaTime;

            if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0.0f, deceleration * Time.deltaTime);
        }

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCol.size, 0);

        grounded = false;

        foreach (Collider2D hit in hits)
        {
            if (hit == boxCol)
                continue;
            ColliderDistance2D colliderDistance = hit.Distance(boxCol);
            if(colliderDistance.isOverlapped)
            {
                collided = true;
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                }
            }
        }
    }

    void Flip()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal");
        if (moveDirection < 0)
        {
            sr.flipX = true;

        }
        else if (moveDirection > 0)
        {
            sr.flipX = false;
        }

    }

}
