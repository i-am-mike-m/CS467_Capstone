using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBounceShroom : MonoBehaviour
{
    [SerializeField] float bounceForce = 850f;
    [SerializeField] Player player;
    [SerializeField] CharacterController2D playerControl;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float currentBounce = bounceForce;
        playerControl.forceReleaseJumpKey();
        if (player.GetComponent<Rigidbody2D>().velocity.y > 0) currentBounce *= 0.25f;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, currentBounce));
    }
}
