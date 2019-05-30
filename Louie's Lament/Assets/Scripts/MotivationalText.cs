using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MotivationalText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;

    string text1 = "You are SO amazing!";
    string text2 = "Nobody ever makes it this far!";
    string text3 = "That was incredible!";
    string text4 = "You're almost as good as my kid brother!";
    float timer = 0f;

    private void Start()
    {
        tmp.text = text1;
    }
        
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3f && timer < 6f)
        {
            tmp.text = text2;
        }
        else if (timer > 6f && timer < 9f)
        {
            tmp.text = text3;
        }
        else if (timer > 9f && timer < 12f)
        {
            tmp.text = text4;
        }
        else if (timer > 12f)
        {
            timer = 0;
            tmp.text = text1;
        }
    }
}
