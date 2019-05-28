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
        gameState.resetLevelTime();
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

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.5f);
        //SceneManager.LoadScene("Game Over");
        gameState.ResetPlaythroughTime();
        SceneManager.LoadScene(0); // TEMPORARY UNTIL WE MAKE GAME OVER
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
