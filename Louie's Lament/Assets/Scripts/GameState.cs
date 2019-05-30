using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameState : MonoBehaviour
{
    public static GameState state;

    [Header("Player Info")]
    [SerializeField] private int startingLives = 3;
    [SerializeField] private int lives = 3;
        
    private float playthroughTime = 0;
    private float levelTime = 0;
    private float volumePercent;
    [SerializeField] private bool graderModeEnabled;

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
            volumePercent = 100;
            graderModeEnabled = false;
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

    public void ToggleGraderMode()
    {
        graderModeEnabled = !graderModeEnabled;        
    }

    public bool GetGraderModeEnabled()
    {
        return graderModeEnabled;
    }
}
