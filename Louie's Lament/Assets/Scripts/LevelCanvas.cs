using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelCanvas : MonoBehaviour
{
    GameState gameState;
    [SerializeField] TextMeshProUGUI livesText;    
    
    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        UpdateLives();        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives()
    {
        livesText.text = "x " + gameState.GetLives().ToString();        
    }
}
