using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrapDeath : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            player.PlayerDeath();
        }
    }
}
