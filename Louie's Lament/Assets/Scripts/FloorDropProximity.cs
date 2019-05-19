using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDropProximity : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private BoxCollider2D floorArea;

    Rigidbody2D floorBody;
    private GameObject[] groundPieces;
    private GameObject[] trapPieces;

    // Start is called before the first frame update
    void Start()
    {
        floorBody = gameObject.GetComponent<Rigidbody2D>();
        floorBody.gravityScale = 0.0f;

        avoidTrapCollisions();                
        floorArea = gameObject.GetComponent<BoxCollider2D>();        
    }

    private void avoidTrapCollisions()
    {
        groundPieces = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject obj in groundPieces)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.GetComponent<BoxCollider2D>());
        }

        trapPieces = GameObject.FindGameObjectsWithTag("Trap Section");
        foreach (GameObject obj in trapPieces)
        {
            if (gameObject.GetComponent<BoxCollider2D>() != null && obj.GetComponent<BoxCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.GetComponent<BoxCollider2D>());
            }
        }


    }

    // Update is called once per frame
    private void Update()
    {        
        float size = floorArea.size.x;
        float left = floorArea.bounds.center.x - (floorArea.size.x / 2);
        float right = floorArea.bounds.center.x + (floorArea.size.x / 2);
        float top = gameObject.transform.position.y;

        bool inBounds = player.transform.position.x > left && player.transform.position.x < right;
        bool playerCloseVertically = false;
        float verticalSensitivity = 0.3f;
        float playerFeetYPosition = (player.transform.position.y - 1);

        if ((top + verticalSensitivity) > playerFeetYPosition && (top - verticalSensitivity) < playerFeetYPosition)
            playerCloseVertically = true;

        if (playerCloseVertically && inBounds)
        {

            floorBody.bodyType = RigidbodyType2D.Dynamic;
            floorBody.gravityScale = 2.0f;
        }
    }
}
