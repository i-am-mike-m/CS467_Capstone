using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerCandy : MonoBehaviour
{
    Player player;
    public float DistanceToFall;
    private BoxCollider2D floorArea;
    private Rigidbody2D body;
    private Vector3 distanceToPlayer;
    private GameObject playerObject;
    private bool isMoving = false;
    private bool moved = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        body = gameObject.GetComponent<Rigidbody2D>();
        floorArea = gameObject.GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    void FixedUpdate()
    {
        if (isMoving && moved == false)
        {
            Move();
            moved = true;
        }
        else
        {
            distanceToPlayer = getDistanceToPlayer();
            //Debug.Log("distance x: " + distanceToPlayer.x);
            //Debug.Log("distance to display: " + DistanceToFall);

            if (distanceToPlayer.x < DistanceToFall)
            {
                isMoving = true;

            }
        }



    }

    private void Move()
    {

        //transform.position += transform.right * -2 * Time.deltaTime;
        //transform.Translate(Vector3.left * Time.deltaTime * 3, Space.World);
        //TurnCounterClockwise();
        //body.AddForceAtPosition(Vector2.down * 10, Vector2.left);
        body.gravityScale = 3;

        //MoveLeft();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            player.PlayerDeath();

        }
    }

    private Vector3 getDistanceToPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        distanceToPlayer = gameObject.transform.position - playerObject.transform.position;


        return gameObject.transform.position - playerObject.transform.position;

    }

}

