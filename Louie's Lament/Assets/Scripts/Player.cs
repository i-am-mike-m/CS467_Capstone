using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField] GameState gameState;

    /*
    [Header("Projectile")]
    [SerializeField] GameObject basicBlueLaserPrefab;
    [SerializeField] float laserSpeed = 5f;
    [SerializeField] float rateOfFire = 0.1f;
    */
    /*
    [Header("Resources")]
    [SerializeField] float health = 500;
    */
    /*
    [Header("SFX/VFX")]
    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0f, 1f)] float deathSFXVolume = 0.8f;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] [Range(0f, 1f)] float fireSFXVolume = 0.05f;
    */
    
    Coroutine constantFireCoroutine;
                
    // Start is called before the first frame update
    void Start()
    {
        //gameState = FindObjectOfType<GameState>();        
    }
        
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.otherCollider.gameObject.name.ToString());
        // Debug.Log(collision.collider.gameObject.name.ToString());

        if (collision.collider.gameObject.tag == "Trap Section")
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        gameState.PlayerDeath();
        Destroy(gameObject);
    }  
}
