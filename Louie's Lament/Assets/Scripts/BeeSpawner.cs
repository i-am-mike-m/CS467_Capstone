using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    [SerializeField] float timer = 0f;
    [SerializeField] float timeBetweenSpawns = 2.0f;
    [SerializeField] GameObject beePrefab;    
    private bool spawnerEnabled = false;

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (spawnerEnabled && timer > timeBetweenSpawns)
        {
            SpawnBee();
            timer = 0f;
        }
    }

    void SpawnBee()
    {
        GameObject bee = Instantiate(beePrefab, transform.position, Quaternion.identity) as GameObject;
        bee.transform.position += new Vector3(0, Random.Range(0f, 0.5f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spawnerEnabled = true;
    }
}
