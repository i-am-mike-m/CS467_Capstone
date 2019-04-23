using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStartMenu()
    {
        FindObjectOfType<GameState>().ResetLives();
        StartCoroutine(LoadAfterTimer(0));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadAfterTimer(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadGameOver()
    {
        StartCoroutine(GameOver());
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadAfterTimer(SceneManager.GetActiveScene().buildIndex));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.5f);
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
