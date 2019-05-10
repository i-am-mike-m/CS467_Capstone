using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballMovement : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("cw");
            TurnCounterClockwise();
        }

    }

    private void TurnCounterClockwise()
    {
        transform.Rotate(Vector3.forward*15);
        //transform.Rotate(Vector3.forward, 15*Time.deltaTime, Space.World);
    }

    void FixedUpdate()
    {
        //transform.position += transform.right * -2 * Time.deltaTime;
        transform.Translate(Vector3.left * Time.deltaTime * 3, Space.World);
        TurnCounterClockwise();

        //MoveLeft();

    }

}
