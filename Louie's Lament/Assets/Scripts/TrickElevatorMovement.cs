using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickElevatorMovement : MonoBehaviour
{
    private GameObject[] groundPieces;
    private GameObject elevatorContainer;
    private GameObject player;
    private BoxCollider2D elevatorBoundingBox;
    private float minHeight;
    private float midHeight;
    private float maxHeight;
    private float delayTimer;
    private bool spikesTriggered = false;
    private bool waitForNow = false;

    // Start is called before the first frame update
    void Start()
    {
        minHeight = GameObject.Find("TrickElevator1_BottomHeightMarker").transform.position.y;
        midHeight = GameObject.Find("TrickElevator1_MidHeightMarker").transform.position.y;
        maxHeight = GameObject.Find("TrickElevator1_TopHeightMarker").transform.position.y;

        elevatorBoundingBox = gameObject.GetComponent<BoxCollider2D>();
        player = GameObject.Find("Character");

        delayTimer = 1.8f;

        avoidGroundCollisions();
    }

    private void avoidGroundCollisions()
    {
        groundPieces = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject obj in groundPieces)
        {
            if (obj != null)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.GetComponent<BoxCollider2D>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (playerOnElevator() && !elevatorAtDestination() && !waitingOnTrap())
            {
                raiseElevator();
            }
            else if (waitingOnTrap())
            {
                decayDelayTimer();

                if (timerExpired())
                {
                    GameObject.Find("Elevator Spikes Container").transform.Translate(Vector3.up * 4 * Time.deltaTime);
                }
            }
            else if (!playerOnElevator() && !elevatorAtBase())
            {
                lowerElevator();
            }
        }
    }

    private bool playerOnElevator()
    {
        float size = elevatorBoundingBox.size.x;
        float left = elevatorBoundingBox.bounds.center.x - (elevatorBoundingBox.size.x / 2);
        float right = elevatorBoundingBox.bounds.center.x + (elevatorBoundingBox.size.x / 2);

        return ((player.transform.position.x > left && player.transform.position.x < right)
               &&
               (player.transform.position.y > gameObject.transform.position.y));
    }

    private bool elevatorAtDestination()
    {
        float top = elevatorBoundingBox.bounds.center.y + (elevatorBoundingBox.size.y / 2);

        return !(top < maxHeight) && waitingOnTrap();
    }

    private bool waitingOnTrap()
    {
        return waitForNow;
    }

    private void raiseElevator()
    {
        gameObject.transform.Translate(Vector3.up * 2 * Time.deltaTime);

        if (elevatorPassedMidpoint()) {
            setTrapInMotion();
        }
    }

    private bool elevatorPassedMidpoint()
    {
        float top = elevatorBoundingBox.bounds.center.y + (elevatorBoundingBox.size.y / 2);

        return (top > midHeight + 1) && !waitingOnTrap();
    }

    private bool elevatorAtBase()
    {
        float top = elevatorBoundingBox.bounds.center.y + (elevatorBoundingBox.size.y / 2);

        return !(top > minHeight);
    }

    private void setTrapInMotion()
    {
        waitForNow = true;
        spikesTriggered = true;
    }

    private void lowerElevator()
    {
        gameObject.transform.Translate(Vector3.down * 2 * Time.deltaTime);
    }

    private void resetTrap()
    {
        delayTimer = 1.8f;
        waitForNow = false;
        spikesTriggered = false;
    }

    private void decayDelayTimer()
    {
        delayTimer -= Time.deltaTime;
    }

    private bool timerExpired()
    {
        return spikesTriggered && delayTimer <= 0;
    }
}