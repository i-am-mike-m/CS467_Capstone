using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBurst : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward * 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            player.PlayerDeath();
        }
        else
        {
            StartCoroutine(SlimeBurstDissolve());
        }
    }

    IEnumerator SlimeBurstDissolve()
    {        
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
