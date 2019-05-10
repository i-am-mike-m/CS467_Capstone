using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSpikeDeath : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            player.PlayerDeath();
        }
    }
}
