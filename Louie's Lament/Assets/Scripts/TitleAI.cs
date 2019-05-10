using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAI : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] float runSpeed = 15f;                                              // Base movement speed        
    [Range(0, .5f)] [SerializeField] private float movementSmoothingFactor = .05f;      // How much to smooth out the movement

    /* Position and Velocity Initialization */
    private Rigidbody2D rigidBody;
    private Animator animator;
    private Vector3 velocity = Vector3.zero;        
    private bool characterModelFacingRight = true;
    private Vector3 mousePosition;
    float targetSpeed;

    [SerializeField] GameObject mainMenu;
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
    }

    private void Start()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetSpeed = runSpeed;
    }

    private void Update()
    {
        if (mainMenu.activeInHierarchy == true)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Mathf.Abs(mousePosition.x - transform.position.x) < 0.35)
            {
                targetSpeed = 0;
            }
            else if (mousePosition.x < transform.position.x)
            {
                targetSpeed = -runSpeed;
            }
            else
            {
                targetSpeed = runSpeed;
            }

            animator.SetFloat("Move Speed", Mathf.Abs(targetSpeed));
            Move();
        }
        else
        {
            targetSpeed = 0;            
            animator.SetFloat("Move Speed", Mathf.Abs(targetSpeed));
            Move();
        }
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
