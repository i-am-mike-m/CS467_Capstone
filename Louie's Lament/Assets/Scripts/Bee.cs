using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] private float speed = -20f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothingFactor = .05f;

    private Player player;
    
    private Rigidbody2D rigidBody;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float beeLifespan = 12f;
    private float beeLifeTime = 0f;
       
    private void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        Move();
        beeLifeTime += Time.deltaTime;
        if (beeLifeTime > beeLifespan)
        {
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        float moveSpeed = speed * Time.fixedDeltaTime;

        /* Calculate actual velocity this update frame */
        // Find initial target velocity            
        Vector3 targetVelocity = new Vector2(moveSpeed * 10f, rigidBody.velocity.y);

        // Smooth velocity and apply
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothingFactor);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       player.PlayerDeath();     
    }
}
