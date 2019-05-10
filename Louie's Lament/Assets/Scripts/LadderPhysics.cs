using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPhysics : MonoBehaviour
{
    private GameObject player;
    private BoxCollider2D ladderCollider;
    private BoxCollider2D playerCollider;
    private Rigidbody2D playerRigidBody2D;
    private Vector3 playerLocation;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {       
    }

    // CITATION:
    // https://forum.unity.com/threads/2d-ladder-in-unity.469709/

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;

        float velocityX = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX, 0f);

        if (Input.GetKey(KeyCode.W))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 7f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -7f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3.0f;
    }
}
