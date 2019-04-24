using UnityEngine;
using System;

public class SpikeFallBehavior: MonoBehaviour
{
    private GameObject[] spikeTrapPieces;
    private Vector3 distanceToPlayer;
    private Vector3 originalLocation;
    private GameObject playerObject;
    private bool respawnTriggered = false;
    Rigidbody2D spikeBody;

    // SOURCE TO CITE:
    // https://forum.unity.com/threads/ignore-collision.4964/

    void Start()
    {
        spikeBody = gameObject.GetComponent<Rigidbody2D>();
        spikeBody.gravityScale = 0.0f;

        originalLocation = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);

        avoidTrapCollisions();
    }

    private void avoidTrapCollisions()
    {
        spikeTrapPieces = GameObject.FindGameObjectsWithTag("Trap Section");
        foreach (GameObject obj in spikeTrapPieces)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.GetComponent<BoxCollider2D>());
        }
    }

    private void Update()
    {
        distanceToPlayer = getDistanceToPlayer();

        if (distanceToPlayer.x < 3)
        {
            spikeBody.gravityScale = 2.0f;
        }
    }

    private Vector3 getDistanceToPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        distanceToPlayer = gameObject.transform.position - playerObject.transform.position;

        return gameObject.transform.position - playerObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hasRespawnTriggered())
        {
            gameObject.transform.position = originalLocation;
            setRespawnHasTriggered();
        }
    }

    private bool hasRespawnTriggered()
    {
        return respawnTriggered;
    }

    private void setRespawnHasTriggered()
    {
        respawnTriggered = true;
    }
}