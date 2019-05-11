using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameState state;   
    [SerializeField] GameObject pauseMenu;

    bool active = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!active)
            {
                Pause();
            }
            else if (active)
            {
                Unpause();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        active = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        active = false;
    }
}
