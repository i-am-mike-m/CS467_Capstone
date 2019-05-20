using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballMovement : MonoBehaviour
{
    Player player;
    public AudioSource wompwomp;

    public float distanceToFireAndShow;
    private BoxCollider2D floorArea;
    private Vector3 distanceToPlayer;
    private GameObject playerObject;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        wompwomp = GetComponent<AudioSource>();

        floorArea = gameObject.GetComponent<BoxCollider2D>();
        gameObject.GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("cw");
            TurnCounterClockwise();
        }*/

    }

    private void TurnCounterClockwise()
    {
        transform.Rotate(Vector3.forward*15);
        //transform.Rotate(Vector3.forward, 15*Time.deltaTime, Space.World);
    }

    void FixedUpdate()
    {
        if (isMoving)
            Move();
        else
        {
            distanceToPlayer = getDistanceToPlayer();
            //Debug.Log("distance x: " + distanceToPlayer.x);
            //Debug.Log("distance to display: " + distanceToFireAndShow);

            if (distanceToPlayer.x < distanceToFireAndShow)
            {
                isMoving = true;
                gameObject.GetComponent<Renderer>().enabled = true;

            }
        }



    }

    private void Move()
    {

        //transform.position += transform.right * -2 * Time.deltaTime;
        transform.Translate(Vector3.left * Time.deltaTime * 3, Space.World);
        TurnCounterClockwise();

        //MoveLeft();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            wompwomp.Play();
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
