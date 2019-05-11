using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSwap : MonoBehaviour
{
    [SerializeField] GameObject baseText;
    [SerializeField] GameObject newText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        baseText.GetComponent<TextMeshPro>().enabled = false;
        newText.GetComponent<TextMeshPro>().enabled = true;
    }
}
