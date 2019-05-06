using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDrop : MonoBehaviour
{
    Rigidbody2D floorBody;
    private GameObject[] groundPieces;
    private GameObject[] trapSections;

    // Start is called before the first frame update
    void Start()
    {
        floorBody = gameObject.GetComponent<Rigidbody2D>();
        floorBody.gravityScale = 0.0f;

        avoidTrapCollisions();
    }

    private void avoidTrapCollisions()
    {
        groundPieces = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject obj in groundPieces)
        {
            if (obj.GetComponent<BoxCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.GetComponent<BoxCollider2D>());
            }
        }

        trapSections = GameObject.FindGameObjectsWithTag("Trap Section");
        foreach (GameObject obj in trapSections)
        {
            if (obj.GetComponent<BoxCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.GetComponent<BoxCollider2D>());
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            floorBody.bodyType = RigidbodyType2D.Dynamic;
            floorBody.gravityScale = 2.0f;
        }
    }
}
