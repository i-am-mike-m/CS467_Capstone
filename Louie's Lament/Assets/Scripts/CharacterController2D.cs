using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] float runSpeed = 30f;                                              // Base movement speed
    [SerializeField] private float jumpForce = 500f;                                    // Force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float crouchingMoveSpeedPercent = 0.5f;      // Movement speed when crouching (percent)
    [Range(0, .3f)] [SerializeField] private float movementSmoothingFactor = .05f;      // How much to smooth out the movement
    [SerializeField] private bool airControlEnabled = true;                             // Whether or not a player can steer while jumping

    [Header("Position Checks")]
    [SerializeField] private LayerMask defineGround = 0;                                    // Layer mask for valid ground check radius targets
    [SerializeField] private Transform groundCheckLocation = null;                      // Position marking where to check if the player is grounded
    [SerializeField] private Transform ceilingCheckLocation = null;                     // Position marking where to check for ceilings
    [SerializeField] private Collider2D crouchingDisabledCollider = null;               // Collider that is disabled when crouching
    [SerializeField] private Collider2D playerAttackCollider = null;                    // Collider enabled when attacking.

    [Header("Imroved Jump Gravity")]
    [SerializeField] private float fallingGravityMultiplier = 2.5f;                     // Multiplier to increase gravity while falling
    [SerializeField] private float jumpKeyReleasedMultiplier = 2f;                      // Multiplier to increase gravity when jump key released during upward motion

    /* Position and Velocity Initialization */
    private Rigidbody2D rigidBody;
    private Animator animator;
    private Vector3 velocity = Vector3.zero;
    float currentSpeed = 0f;
    bool jumpKeyPressed = false;
    bool jumpKeyReleased = false;
    bool isCrouching = false;
    private bool isGrounded = true;    
    private bool characterModelFacingRight = true;
    private bool previouslyCrouching = false;
    const float groundCheckRadius = .2f;                                                // Radius of the overlap circle to determine if grounded
    const float ceilingCheckRadius = .2f;                                               // Radius of the overlap circle to determine able to stop crouching

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttackCollider.enabled = false;
    }

    private void FixedUpdate()
    {
        Move();
        jumpKeyPressed = false;
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
                    jumpKeyReleased = false;
                    break;
                }
            }
        }        
    }

    private void Update()
    {
        animator.SetFloat("Move Speed", Mathf.Abs(currentSpeed));

        if (rigidBody.velocity.y > 0) animator.SetBool("Jumping", true);
        else if (rigidBody.velocity.y == 0) animator.SetBool("Jumping", false);

        currentSpeed = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Check for player inputs and update state variables
        if (Input.GetButtonDown("Jump"))
        {
            jumpKeyPressed = true;
        }
        if (Input.GetButtonUp("Jump"))
        {            
            jumpKeyReleased = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
            animator.SetBool("Crouching", true);
        }
        if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
            animator.SetBool("Crouching", false);
        }
        if (isGrounded && Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Attacking", true);
            playerAttackCollider.enabled = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("Attacking", false);
            playerAttackCollider.enabled = false;
        }
    }


    public void Move()
    {
        float moveSpeed = currentSpeed * Time.fixedDeltaTime;
        
        if (isCrouching)
        {
            // Check radius around ceiling check for obstructions to standing up from crouching
            if (Physics2D.OverlapCircle(ceilingCheckLocation.position, ceilingCheckRadius, defineGround))
            {
                isCrouching = true;
            }
        }
                
        if (isGrounded || airControlEnabled)
        {            
            if (isCrouching)
            {
                moveSpeed *= crouchingMoveSpeedPercent;

                if (!previouslyCrouching) {
                    previouslyCrouching = true;
                }
                
                // Disable specified crouching collider
                if (crouchingDisabledCollider != null)
                {
                    crouchingDisabledCollider.enabled = false;
                }
            }
            else {
                // Enable specified crouching collider
                if (crouchingDisabledCollider != null)
                {
                    crouchingDisabledCollider.enabled = true;
                }

                if (previouslyCrouching)
                {
                    previouslyCrouching = false;
                }
            }

            /* Calculate actual velocity this update frame */
            // Find initial target velocity            
            Vector3 targetVelocity = new Vector2(moveSpeed * 10f, rigidBody.velocity.y);
            // Apply improved jump gravity (if applicable)            
            if (rigidBody.velocity.y < 0)
            {
                rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallingGravityMultiplier - 1) * Time.deltaTime;
            }            
            else if (rigidBody.velocity.y > 0 && jumpKeyReleased) 
            {
                rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (jumpKeyReleasedMultiplier - 1) * Time.deltaTime;
            }            
            // Smooth velocity and apply
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothingFactor);

            /* Check and update character model facing */
            // If moving right and facing left, flip character model
            if (moveSpeed > 0 && characterModelFacingRight == false)
            {                
                FlipCharacterModel();
            }
            // If moving left and facing right, flip character model
            else if (moveSpeed < 0 && characterModelFacingRight)
            {                
                FlipCharacterModel();
            }
        }
        
        if (isGrounded && jumpKeyPressed)
        {
            isGrounded = false;
            rigidBody.AddForce(new Vector2(0f, jumpForce));
            jumpKeyPressed = false;
        }
    }


    private void FlipCharacterModel()
    {        
        characterModelFacingRight = !characterModelFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}