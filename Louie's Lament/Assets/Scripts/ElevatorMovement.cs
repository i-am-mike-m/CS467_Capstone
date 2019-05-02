using System.Collections;
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
        //elevatorContainer = GameObject.Find("Elevator Container");
        //minHeight = elevatorContainer.GetComponent("Elevator1_BottomHeightMarker").transform.position.y;
        //maxHeight = elevatorContainer.GetComponent("Elevator1_TopHeightMarker").transform.position.y;



        minHeight = GameObject.Find("Elevator1_BottomHeightMarker").transform.position.y;
        maxHeight = GameObject.Find("Elevator1_TopHeightMarker").transform.position.y;

        elevatorBoundingBox = gameObject.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        avoidGroundCollisions();
    }

    private void avoidGroundCollisions()
    {
        groundPieces = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject obj in groundPieces)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.GetComponent<BoxCollider2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        float size = elevatorBoundingBox.size.x;
        float left = elevatorBoundingBox.bounds.center.x - (elevatorBoundingBox.size.x / 2);
        float right = elevatorBoundingBox.bounds.center.x + (elevatorBoundingBox.size.x / 2);
        float top = elevatorBoundingBox.bounds.center.y + (elevatorBoundingBox.size.y / 2);

        if (player.transform.position.x > left && player.transform.position.x < right)
        {
            if (player.transform.position.y > gameObject.transform.position.y && top < maxHeight)
            {
                gameObject.transform.Translate(Vector3.up * 2 * Time.deltaTime);
            }
        }
        else
        {
            if (top > minHeight)
            {
                gameObject.transform.Translate(Vector3.down * 2 * Time.deltaTime);
            }
        }
    }
}
