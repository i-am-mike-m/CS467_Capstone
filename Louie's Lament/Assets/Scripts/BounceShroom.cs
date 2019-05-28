using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceShroom : MonoBehaviour
{
    [SerializeField] float waitTime = 2f;
    [SerializeField] float downTime = 0f;
    [SerializeField] float bounceForce = 850f;

    [SerializeField] Sprite active;
    [SerializeField] Sprite inactive;

    bool readyToBounce;
    Player player;
    Rigidbody2D rigidbody;
            
    // Start is called before the first frame update
    void Start()
    {
        readyToBounce = true;
        player = FindObjectOfType<Player>();
        rigidbody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!readyToBounce)
        {
            downTime += Time.deltaTime;
            if (downTime >= waitTime)
            {
                readyToBounce = true;
                downTime = 0;
                gameObject.GetComponent<SpriteRenderer>().sprite = active;
                gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (readyToBounce)
        {
            readyToBounce = false;
            Bounce();
        }
    }

    void Bounce()
    {
        float currentBounce = bounceForce;
        if (rigidbody.velocity.y >= 0)
        {
            currentBounce /= 2;
        }
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, currentBounce));
        gameObject.GetComponent<SpriteRenderer>().sprite = inactive;

        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
}
