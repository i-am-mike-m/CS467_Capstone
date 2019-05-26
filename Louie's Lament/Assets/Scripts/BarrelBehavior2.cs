using UnityEngine;
using System;

public class BarrelBehavior2 : MonoBehaviour
{
    private GameObject barrel;
    private Vector3 distanceToPlayer;
    private Vector3 originalLocation;
    private GameObject playerObject;
    Rigidbody2D barrelBody;

    void Start()
    {
        barrelBody = gameObject.GetComponent<Rigidbody2D>();
        barrelBody.gravityScale = 0.0f;

        originalLocation = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);

        avoidGroundCollisions();
    }

    private void avoidGroundCollisions()
    {
        barrel = gameObject;

        GameObject groundLayer = GameObject.Find("BaseGroundColliderRight");
        GameObject topLayer = GameObject.Find("Top Side Physical Layer");

        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), groundLayer.GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), topLayer.GetComponent<BoxCollider2D>());
    }

    private void Update()
    {
        distanceToPlayer = getDistanceToPlayer();

        if (distanceToPlayer.x < 1.5)
        {
            barrelBody.gravityScale = 2.75f;
        }
    }

    private Vector3 getDistanceToPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        distanceToPlayer = gameObject.transform.position - playerObject.transform.position;

        return gameObject.transform.position - playerObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            FindObjectOfType<Player>().PlayerDeath();
        }
    }
}