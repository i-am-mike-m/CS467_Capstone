using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownwardSpikes : MonoBehaviour
{
    [SerializeField] private float targetDistance = 0.35f;
    double target;
    float speed = 0f;
    private Vector3 velocity = Vector3.zero;
    float movementSmoothingFactor = 0.05f;

    private void Start()
    {
        target = transform.position.y - targetDistance;        
    }

    // Update is called once per frame
    void Update()
    {        
        if (speed < 0) Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        speed = -60f;
    }

    void Move()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        if (transform.position.y <= target)
        {
            speed = 0;
            rigidBody.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rigidBody.bodyType = RigidbodyType2D.Kinematic;

            float moveSpeed = speed * Time.fixedDeltaTime;
            Vector3 targetVelocity = new Vector2(0, moveSpeed * 10f);
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothingFactor);
        }
    }
}
