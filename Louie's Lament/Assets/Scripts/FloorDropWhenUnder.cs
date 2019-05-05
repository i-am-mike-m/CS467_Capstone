using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDropWhenUnder : MonoBehaviour
{
    private GameObject playerObject;
    private GameObject player;
    private BoxCollider2D floorArea;
    private Vector3 distanceToPlayer;
    Rigidbody2D floorBody;
    private GameObject[] groundPieces;

    // Start is called before the first frame update
    void Start()
    {
        floorBody = gameObject.GetComponent<Rigidbody2D>();
        floorBody.gravityScale = 0.0f;

        avoidTrapCollisions();

        player = GameObject.FindGameObjectWithTag("Player");
        floorArea = gameObject.GetComponent<BoxCollider2D>();
    }

    private void avoidTrapCollisions()
    {
        groundPieces = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject obj in groundPieces)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.GetComponent<BoxCollider2D>());
        }
    }

    // Update is called once per frame
    private void Update()
    {

        distanceToPlayer = getDistanceToPlayer();

        if (distanceToPlayer.x < 1.5)
        {
            floorBody.bodyType = RigidbodyType2D.Dynamic;
            floorBody.gravityScale = 2.0f;
        }
    }

    private Vector3 getDistanceToPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        distanceToPlayer = gameObject.transform.position - playerObject.transform.position;

        return gameObject.transform.position - playerObject.transform.position;
    }
}

