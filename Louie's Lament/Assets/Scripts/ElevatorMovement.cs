﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    private GameObject[] groundPieces;
    private GameObject elevatorContainer;
    private GameObject player;
    private BoxCollider2D elevatorBoundingBox;
    private float minHeight;
    private float maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        minHeight = GameObject.Find("Elevator1_BottomHeightMarker").transform.position.y;
        maxHeight = GameObject.Find("Elevator1_TopHeightMarker").transform.position.y;

        elevatorBoundingBox = gameObject.GetComponent<BoxCollider2D>();
        player = GameObject.Find("Character");

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
            if (playerOnElevator() && !elevatorAtDestination())
            {
                raiseElevator();
            }
            else if (!elevatorAtBase())
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

        return !(top < maxHeight);
    }

    private bool elevatorAtBase()
    {
        float top = elevatorBoundingBox.bounds.center.y + (elevatorBoundingBox.size.y / 2);

        return !(top > minHeight);
    }

    private void raiseElevator()
    {
        gameObject.transform.Translate(Vector3.up * 2 * Time.deltaTime);
    }

    private void lowerElevator()
    {
        gameObject.transform.Translate(Vector3.down * 2 * Time.deltaTime);
    }
}