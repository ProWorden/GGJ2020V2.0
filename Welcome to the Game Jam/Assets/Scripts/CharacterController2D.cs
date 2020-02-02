using System.Collections;
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

    public Animator anim;

    public int player = 1;

    string horizontal = "Horizontal";
    string vertical = "Vertical";
    string jump = "Jump";

    // Start is called before the first frame update
    void Awake()
    {
        boxCol = GetComponent<BoxCollider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        horizontal = horizontal + player;
        vertical = vertical + player;
        jump = jump + player;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
       
        float moveInput = Input.GetAxisRaw(horizontal);
        float acceleration = grounded ? walkAcceleration : airAcceleration;
        float deceleration = grounded ? groundDeceleration : 0;
        transform.Translate(velocity * Time.deltaTime);

        if(grounded)
        {
            velocity.y = 0;

            if (Input.GetButtonDown(jump))
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

        anim.SetBool("Walking", moveInput != 0);
        anim.SetBool("Grounded", grounded);

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCol.size, 0);

        grounded = false;

        foreach (Collider2D hit in hits)
        {
            if (hit == boxCol)
                continue;
            ColliderDistance2D colliderDistance = hit.Distance(boxCol);
            if(colliderDistance.isOverlapped)
            {
                
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
               

                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                }



                if(!grounded && !Input.GetButtonDown(jump) && velocity.y > 0)
                {
                    velocity.y = -velocity.y * 0.01f;
                }
                else if (!grounded && !Input.GetButtonDown(jump))
                {
                    velocity.x = -velocity.x * 0.5f;
                }
              


            }
        }
    }

    void Flip()
    {
        float moveDirection = Input.GetAxisRaw(horizontal);
        if (moveDirection < 0)
        {
            sr.flipX = false;

        }
        else if (moveDirection > 0)
        {
            sr.flipX = true;
        }

    }

}
