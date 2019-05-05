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
        if (collision.collider.gameObject.tag == "Trap Section")
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        gameState.PlayerDeath();
        Destroy(gameObject);
        
        //ParticleSystem deathStars = Instantiate(deathVFX, gameObject.transform.position, Quaternion.identity) as ParticleSystem;
        //AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        //FindObjectOfType<SceneLoader>().LoadGameOver();
    }

    /*
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            constantFireCoroutine = StartCoroutine(ConstantFire());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(constantFireCoroutine);
        }
    }

    IEnumerator ConstantFire()
    {
        while (true)
        {
            Vector2 laserStartPoint = new Vector2(transform.position.x, transform.position.y + 1f);
            AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, fireSFXVolume);
            // Quaternion.identity is the rotation argument and means to use the current existing rotation
            GameObject laser = Instantiate(basicBlueLaserPrefab, laserStartPoint, Quaternion.identity) as GameObject;            
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);

            yield return new WaitForSeconds(rateOfFire);
        }
    }   
    */    
}
