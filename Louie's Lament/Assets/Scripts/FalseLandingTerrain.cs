using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseLandingTerrain : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
