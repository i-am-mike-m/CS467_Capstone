using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickElevatorMovement : MonoBehaviour
{
    private GameObject[] groundPieces;
    private GameObject elevatorContainer;
    private GameObject player;
    private GameObject spikesContainer;
    private BoxCollider2D elevatorBoundingBox;
    private float minHeight;
    private float midHeight;
    private float maxHeight;
    private float delayTimer;
    private float spikeExtendTimer;
    private bool spikesTriggered = false;
    private bool spikesExtended = false;
    private bool waitForNow = false;
    private bool trapLoaded = true;

    // Start is called before the first frame update
    void Start()
    {
        minHeight = GameObject.Find("TrickElevator1_BottomHeightMarker").transform.position.y;
        midHeight = GameObject.Find("TrickElevator1_MidHeightMarker").transform.position.y;
        maxHeight = GameObject.Find("TrickElevator1_TopHeightMarker").transform.position.y;
        spikesContainer = GameObject.Find("Elevator Spikes Container");

        elevatorBoundingBox = gameObject.GetComponent<BoxCollider2D>();
        player = GameObject.Find("Character");

        delayTimer = 0.82f;
        spikeExtendTimer = 0.3f;

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

                if (elevatorPassedMidpoint() && trapIsLoaded()) {
                    setTrapInMotion();
                }

            }
            else if (waitingOnTrap())
            {
                decayDelayTimer();

                if (timerExpired() && !spikesExtended)
                {
                    extendSpikes();
                }
                else if (timerExpired() && spikesExtended)
                {
                    retractSpikes();

                    if (spikesContainer.GetComponent<BoxCollider2D>().bounds.min.y <= elevatorBoundingBox.bounds.min.y + 0.1)
                    {
                        waitForNow = false;
                    }
                }
            }
            else if (!playerOnElevator() && !elevatorAtBase())
            {
                lowerElevator();

                if (elevatorBelowMidpoint())
                {
                    resetTrap();
                }
            }
        }
    }

    private void extendSpikes()
    {
        if (!spikesExtended && spikesContainer.GetComponent<BoxCollider2D>().bounds.min.y < elevatorBoundingBox.bounds.max.y)
        {
            spikesContainer.transform.Translate(Vector3.up * 4 * Time.deltaTime);
        }
        else if (spikesContainer.GetComponent<BoxCollider2D>().bounds.min.y >= elevatorBoundingBox.bounds.max.y)
        {
            spikesExtended = true;
        }
    }

    private void retractSpikes()
    {
        if (spikesContainer.GetComponent<BoxCollider2D>().bounds.min.y > elevatorBoundingBox.bounds.min.y + 0.1)
        {
            spikesContainer.transform.Translate(Vector3.down * 4 * Time.deltaTime);
        }
    }

    private bool trapIsLoaded()
    {
        return trapLoaded;
    }

    private void raiseElevator()
    {
        gameObject.transform.Translate(Vector3.up * 2 * Time.deltaTime);
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

        return top >= maxHeight;
    }

    private bool waitingOnTrap()
    {
        return waitForNow;
    }

    private bool elevatorPassedMidpoint()
    {
        float top = elevatorBoundingBox.bounds.center.y + (elevatorBoundingBox.size.y / 2);

        return (top > midHeight) && !waitingOnTrap();
    }

    private bool elevatorBelowMidpoint()
    {
        float top = elevatorBoundingBox.bounds.center.y + (elevatorBoundingBox.size.y / 2);

        return top < midHeight;
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
        trapLoaded = false;
    }

    private void lowerElevator()
    {
        gameObject.transform.Translate(Vector3.down * 2 * Time.deltaTime);
    }

    private void resetTrap()
    {
        delayTimer = 0.82f;
        spikeExtendTimer = 0.3f;
        waitForNow = false;
        spikesExtended = false;
        spikesTriggered = false;
        trapLoaded = true;
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