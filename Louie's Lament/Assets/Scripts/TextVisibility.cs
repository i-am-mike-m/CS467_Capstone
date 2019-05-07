using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextVisibility : MonoBehaviour
{
    public float distanceToDisplay;
    private GameObject playerObject;
    private GameObject player;
    private BoxCollider2D floorArea;
    private Vector3 distanceToPlayer;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        floorArea = gameObject.GetComponent<BoxCollider2D>();
        gameObject.GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    //original show/hide from https://answers.unity.com/questions/14165/show-and-hide-a-prefab-or-gameobject.html
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // show
            // renderer.enabled = true;
            gameObject.GetComponent<Renderer>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            // hide
            // renderer.enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
        }

        distanceToPlayer = getDistanceToPlayer();

        if (distanceToPlayer.x < distanceToDisplay)
        {
            gameObject.GetComponent<Renderer>().enabled = true;

        }
    }

    private Vector3 getDistanceToPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        distanceToPlayer = gameObject.transform.position - playerObject.transform.position;

        return gameObject.transform.position - playerObject.transform.position;
    }
}
