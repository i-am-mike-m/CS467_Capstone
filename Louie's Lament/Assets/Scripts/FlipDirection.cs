using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipDirection : MonoBehaviour
{
    // CITATION:
    // https://forum.unity.com/threads/how-to-flip-animation-in-2d.375921/

    // Start is called before the first frame update
    void Start()
    {
        Vector3 turnToLeft = transform.localScale;
        turnToLeft.x *= -1;
        transform.localScale = turnToLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
