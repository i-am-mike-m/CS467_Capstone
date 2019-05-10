using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanText : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.PlayerDeath();
    }
}
