using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryAI : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] float runSpeed = 30f;                                              // Base movement speed        
    [Range(0, .5f)] [SerializeField] private float movementSmoothingFactor = .05f;      // How much to smooth out the movement

    /* Position and Velocity Initialization */
    private Rigidbody2D rigidBody;
    private Animator animator;
    private Vector3 velocity = Vector3.zero;
    private bool characterModelFacingRight = true;
    private Vector3 mousePosition;
    float targetSpeed;
    float timer = 0f;
    [SerializeField] int direction = 1;
    int turnAroundTime = 2;

    [SerializeField] GameObject mainMenu;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
       
    }

    private void Update()
    {        
        timer += Time.deltaTime;
        System.Random random = new System.Random((int)timer);
        random = new System.Random(random.Next(1, 100));
        if (timer > turnAroundTime)
        {
            direction *= -1;
            timer = 0;
            turnAroundTime = random.Next(1, 3);
        }
        
        int speedPercent = random.Next(70, 130);
        float currentSpeed = runSpeed * (speedPercent / 100f);

        if (direction < 0)
        {
            targetSpeed = -(runSpeed * (speedPercent / 100f));
        }
        else
        {
            targetSpeed = runSpeed * (speedPercent / 100f);
        }
        
        animator.SetFloat("Move Speed", Mathf.Abs(targetSpeed));
        Move();
    }    

    public void Move()
    {
        float moveSpeed = targetSpeed * Time.fixedDeltaTime;

        /* Calculate actual velocity this update frame */
        // Find initial target velocity            
        Vector3 targetVelocity = new Vector2(moveSpeed * 10f, rigidBody.velocity.y);

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

    private void FlipCharacterModel()
    {
        characterModelFacingRight = !characterModelFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
