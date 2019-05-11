using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField] GameState gameState;
    [SerializeField] Sprite deadSprite;

    bool isAlive = true;

    /*
    [Header("SFX/VFX")]
    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0f, 1f)] float deathSFXVolume = 0.8f;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] [Range(0f, 1f)] float fireSFXVolume = 0.05f;
    */

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();        
    }
    
    public void PlayerDeath()
    {
        if (isAlive)
        {
            isAlive = false;
            StartCoroutine(Die());
        }
        
        //ParticleSystem deathStars = Instantiate(deathVFX, gameObject.transform.position, Quaternion.identity) as ParticleSystem;
        //AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        //FindObjectOfType<SceneLoader>().LoadGameOver();
    }
    
    public bool GetIsAlive()
    {
        return isAlive;
    }

    IEnumerator Die()
    {   
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        yield return new WaitForSeconds(1.25f);
        gameState.PlayerDeath();                
    }
}
