using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameState : MonoBehaviour
{
    // Player Info
    [SerializeField] private int startingLives = 3;
    [SerializeField] private int lives = 3;

    // Performance Info
    float playthroughTime = 0;

    // Game Control    
    private void SetUpSingleton()
    {
        int numGameStates = FindObjectsOfType<GameState>().Length;
        if (numGameStates > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Awake()
    {
        SetUpSingleton();    
    }

    public void PlayerDeath()
    {
        lives--;
        if (lives <= 0)
        {
            startingLives++;
            /* TEMP */
            Debug.Log("Ran Out Of Lives");
            FindObjectOfType<SceneLoader>().LoadGameOver();            
        }
        else {
            FindObjectOfType<SceneLoader>().ReloadScene();
        }
    }

    public int GetLives()
    {
        return lives;
    }

    public void ResetLives()
    {
        lives = startingLives;
    }
}
