using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private float timeBetweenJumps = 3f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothingFactor = .05f;

    [Header("Position Checks")]
    [SerializeField] private LayerMask defineGround = 0;                                    // Layer mask for valid ground check radius targets
    [SerializeField] private LayerMask defineAggroTarget;
    [SerializeField] private Transform groundCheckLocation = null;                      // Position marking where to check if the player is grounded
    [SerializeField] private float aggroRadius = 10f;

    [SerializeField] private Player player = null;
    [SerializeField] private BoxCollider2D playerAttack = null;
    [SerializeField] GameObject slimeBurstPrefab;
    [SerializeField] private int slimeBurstCount = 15;

    /* Position and Velocity Initialization */
    private Rigidbody2D rigidBody;
    private Animator animator;
    private Vector3 velocity = Vector3.zero;
    float currentSpeed = 0f;
    const float groundCheckRadius = .2f;
    private bool isGrounded = true;
    private bool timeToJump = false;
    private bool characterModelFacingRight = false;
    private float jumpTimer = 0f;
    
    private void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        if (isGrounded && Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) < aggroRadius)
        {
            currentSpeed = gameObject.transform.position.x > player.transform.position.x ? -speed : speed;
        }
        else if (isGrounded)
        {
            currentSpeed = 0;
            jumpTimer = 0;
        }

        jumpTimer += Time.deltaTime;
        if (jumpTimer > timeBetweenJumps) timeToJump = true;

        //animator.SetFloat("Move Speed", Mathf.Abs(currentSpeed));

        //if (rigidBody.velocity.y != 0) animator.SetBool("Jumping", true);
        //else if (rigidBody.velocity.y == 0) animator.SetBool("Jumping", false);
        
        isGrounded = false;

        Collider2D[] collidersInGroundCheckRadius = Physics2D.OverlapCircleAll(groundCheckLocation.position, groundCheckRadius, defineGround);
        for (int i = 0; i < collidersInGroundCheckRadius.Length; i++)
        {
            // Check whether player has landed
            if (collidersInGroundCheckRadius[i].gameObject != gameObject)
            {
                if (collidersInGroundCheckRadius[i].gameObject.name != "Background")
                {
                    isGrounded = true;                    
                    break;
                }
            }
        }
    }
        
    public void Move()
    {
        float moveSpeed = currentSpeed * Time.fixedDeltaTime;

        if (isGrounded && timeToJump && currentSpeed != 0)
        {
            isGrounded = false;
            rigidBody.AddForce(new Vector2(0f, jumpForce));
            timeToJump = false;
            jumpTimer = 0;
        }

        if (isGrounded)
        {
            /* Calculate actual velocity this update frame */
            // Find initial target velocity            
            Vector3 targetVelocity = new Vector2(moveSpeed * 10f, rigidBody.velocity.y);

            // Smooth velocity and apply
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothingFactor);

            /* Check and update model facing */
            // If moving right and facing left, flip model
            if (moveSpeed > 0 && characterModelFacingRight == false)
            {
                FlipCharacterModel();
            }
            // If moving left and facing right, flip model
            else if (moveSpeed < 0 && characterModelFacingRight)
            {
                FlipCharacterModel();
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == playerAttack)
        {
            Burst();
            Destroy(gameObject);
        }
        else if (collision.gameObject.name == "Character")
        {
            player.PlayerDeath();
        }
    }

    private void Burst()
    {
        for (int i = 0; i < slimeBurstCount; i++)
        {
            GameObject burst = Instantiate(slimeBurstPrefab, transform.position, Quaternion.identity) as GameObject;

            System.Random random = new System.Random(i);
            int x = random.Next(-300, 300);
            if (x < 100 && x > -100) x *= 2;
            int y = random.Next(0, 400);
            if (y < 100) y *= 2;

            burst.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y));
        }
    }

    private void FlipCharacterModel()
    {
        characterModelFacingRight = !characterModelFacingRight;

        // Multiply the object's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
