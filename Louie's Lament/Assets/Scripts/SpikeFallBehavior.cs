using UnityEngine;
using System;

public class SpikeFallBehavior : MonoBehaviour
{
    private GameObject[] spikeTrapPieces;
    private GameObject topSidePhysicalLayer;
    private Vector3 distanceToPlayer;
    private Vector3 originalLocation;
    private GameObject playerObject;
    private bool respawnTriggered = false;
    private bool destroyTimerActive = false;
    private float waitToDestroy;
    Rigidbody2D spikeBody;

    // SOURCE TO CITE:
    // https://forum.unity.com/threads/ignore-collision.4964/

    void Start()
    {
        spikeBody = gameObject.GetComponent<Rigidbody2D>();
        spikeBody.gravityScale = 0.0f;

        originalLocation = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);

        waitToDestroy = 0f;

        avoidTrapCollisions();
    }

    private void avoidTrapCollisions()
    {
        spikeTrapPieces = GameObject.FindGameObjectsWithTag("Trap Section");
        foreach (GameObject obj in spikeTrapPieces)
        {
            if (obj.GetComponent<BoxCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.GetComponent<BoxCollider2D>());
            }
        }

        topSidePhysicalLayer = GameObject.Find("Top Side Physical Layer");
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), topSidePhysicalLayer.GetComponent<BoxCollider2D>());
    }

    private void Update()
    {
        distanceToPlayer = getDistanceToPlayer();

        if (distanceToPlayer.x < 2.5)
        {
            spikeBody.gravityScale = 2.75f;
        }

        if (isDestroyTimerActive())
        {
            waitToDestroy -= Time.deltaTime;

            if (waitToDestroy < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private Vector3 getDistanceToPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            distanceToPlayer = gameObject.transform.position - playerObject.transform.position;
            return gameObject.transform.position - playerObject.transform.position;
        }
        else
        {
            return new Vector3(100f, 100f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hasRespawnTriggered() && other.gameObject.name != "Character")
        {
            gameObject.transform.position = originalLocation;
            setRespawnHasTriggered();
            waitToDestroy = 1.2f;
        }
        else
        {
            setDestroyTimerActive();
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

    private bool isDestroyTimerActive()
    {
        return destroyTimerActive;
    }

    private void setDestroyTimerActive()
    {
        destroyTimerActive = true;
    }
}