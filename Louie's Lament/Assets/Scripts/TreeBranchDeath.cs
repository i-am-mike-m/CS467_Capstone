using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBranchDeath : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
            player.PlayerDeath();
    }
}
