using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {            
            StartCoroutine(Transition());
        }

        IEnumerator Transition()
        {            
            yield return new WaitForSeconds(1f);
            sceneLoader.LoadNextLevel();
        }
    }
}
