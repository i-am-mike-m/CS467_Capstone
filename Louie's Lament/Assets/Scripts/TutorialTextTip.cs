using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTextTip : MonoBehaviour
{
    TextMeshPro tmp;

    private void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        tmp.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            tmp.enabled = true;            
        }
    }
}
