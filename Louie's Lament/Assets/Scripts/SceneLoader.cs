using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    GameState gameState;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }

    public void LoadStartMenu()
    {
        gameState.ResetLives();
        StartCoroutine(LoadAfterTimer(0));
    }

    public void LoadNextLevel()
    {
        gameState.resetLevelTime();
        StartCoroutine(LoadAfterTimer(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadGameOver()
    {        
        StartCoroutine(GameOver());
    }

    public void ReloadScene()
    {
        gameState.resetLevelTime();
        StartCoroutine(LoadAfterTimer(SceneManager.GetActiveScene().buildIndex));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadAfterTimer(1));
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.5f);        
        gameState.ResetPlaythroughTime();
        gameState.resetLevelTime();
        SceneManager.LoadScene("Game Over");        
    }

    private IEnumerator LoadAfterTimer(int index)
    {
        yield return new WaitForSeconds(0.5f);
        LoadScene(index);
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
