using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffShroom : MonoBehaviour
{
    Player player;
    ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        particles = gameObject.GetComponent<ParticleSystem>();
        particles.Stop();                
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Puff();
    }

    void Puff()
    {
        particles.Play();
        StartCoroutine(delayedDeath());        
    }

    IEnumerator delayedDeath()
    {   
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        player.GetComponent<Animator>().SetBool("Alive", false);
        yield return new WaitForSeconds(1.25f);
        player.PlayerDeath();        
    }
}
