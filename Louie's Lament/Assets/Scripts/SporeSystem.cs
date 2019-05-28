using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeSystem : MonoBehaviour
{
    Player player;
    ParticleSystem particles;
 
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnParticleCollision(GameObject other)
    {
        StartCoroutine(SporeDeath());
    }

    IEnumerator SporeDeath()
    {
        player.GetComponent<Animator>().SetBool("Alive", false);
        yield return new WaitForSeconds(0.3f);        
        player.PlayerDeath();
    }
}
