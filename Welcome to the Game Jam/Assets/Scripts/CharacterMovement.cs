using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController cc;
    public float moveSpeed;
    float horizontalMove = 0.0f;
    private Vector2 velocity;
    public float gravity = -9.81f;
    public float jumpHeight;
    public bool isGrounded = true;
    private bool facingRight = true;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    private int jumpCount = 0;
    private int numberJumps = 1;
    float moveDirection;
    public SpriteRenderer sr;
    public Transform test;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        UpdatePhysics();
        Jump();
        Flip();
        test.position =  new Vector3(transform.position.x, transform.position.y, -0.3f);
    }
    
    void Jump()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCount = 0;
            
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumpCount++;
        }
    }

    void UpdatePhysics()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        moveDirection = Input.GetAxisRaw("Horizontal");
        horizontalMove = Input.GetAxis("Horizontal");
        Vector2 move = transform.right * horizontalMove;

        cc.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

        move.Scale(new Vector2(0.85f, 0.0f));
    }

    void Flip()
    {
        if(moveDirection < 0 )
        {
            sr.flipX = true;
            
        }
        else if(moveDirection > 0 )
        {
            sr.flipX = false;
        }

    }
}
