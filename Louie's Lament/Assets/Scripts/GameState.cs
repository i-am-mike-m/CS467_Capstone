using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameState : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private int startingLives = 5;
    [SerializeField] private int lives = 5;

    [Header("Time Info")]
    [SerializeField] private float playthroughTime = 0;     // Serialized to view in inspector
    [SerializeField] private float levelTime = 0;           // Serialized to view in inspector

    [Header("Settings")]    
    [SerializeField] private float volumePercent = 100;       // Serialized to view in inspector
    [SerializeField] private bool graderModeEnabled = false;

    // Establish singleton game state that persists between scenes and reloads    
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

    public void SetVolume(float v)
    {
        volumePercent = v;
    }

    public void resetLevelTime()
    {
        levelTime = 0;
    }

    public void PrintPlaythroughTime()
    {
        Debug.Log("Playthrough time: " + playthroughTime);
        Debug.Log("Level time: " + levelTime);
    }

    void Awake()
    {
        SetUpSingleton();        
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();        
        if (currentScene.buildIndex != 0 && currentScene.name != "Game Over") { 
            playthroughTime += Time.deltaTime;
            levelTime += Time.deltaTime;
        }
    }

    public void PlayerDeath()
    {
        LevelCanvas levelCanvas = FindObjectOfType<LevelCanvas>();
        
        lives--;
        levelCanvas.UpdateLives();

        if (lives <= 0)
        {
            startingLives++;
            
            if (graderModeEnabled)
            {
                Debug.Log("Ran Out Of Lives");
                ResetLives();
                FindObjectOfType<SceneLoader>().ReloadScene();
            }
            else
            {
                ResetLives();                
                FindObjectOfType<SceneLoader>().LoadGameOver();            
            }
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

    public float GetTotalTime()
    {
        return playthroughTime;
    }

    public float GetLevelTime()
    {
        return levelTime;
    }

    public void ResetPlaythroughTime()
    {
        playthroughTime = 0f;
    }

    public void toggleGraderMode()
    {
        graderModeEnabled = !graderModeEnabled;
    }
}
