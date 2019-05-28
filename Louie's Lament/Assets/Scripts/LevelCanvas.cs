using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelCanvas : MonoBehaviour
{
    GameState gameState;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI totalTime;
    [SerializeField] TextMeshProUGUI levelTime;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        UpdateLives();        
    }
    // Update is called once per frame
    void Update()
    {
        float time_total = gameState.GetTotalTime();
        float time_level = gameState.GetLevelTime();

        int total_min = (int)time_total / 60;
        int total_sec = (int)time_total % 60;
        if (total_min >= 1 && total_sec >= 10) totalTime.text = total_min.ToString() + ":" + total_sec.ToString();
        else if (total_min >= 1) totalTime.text = total_min.ToString() + ":0" + total_sec.ToString();
        else if (total_min < 1 && total_sec < 10) totalTime.text = "0:0" + total_sec.ToString();
        else totalTime.text = "0:" + total_sec.ToString();

        int level_min = (int)time_level / 60;
        int level_sec = (int)time_level % 60;
        if (level_min >= 1 && level_sec >= 10) levelTime.text = level_min.ToString() + ":" + level_sec.ToString();
        else if (level_min >= 1) levelTime.text = level_min.ToString() + ":0" + level_sec.ToString();
        else if (level_min < 1 && level_sec < 10) levelTime.text = "0:0" + level_sec.ToString();
        else levelTime.text = "0:" + level_sec.ToString();
    }

    public void UpdateLives()
    {
        livesText.text = "x " + gameState.GetLives().ToString();        
    }
}
