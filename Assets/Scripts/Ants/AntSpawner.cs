using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRateSeconds = 1.0f;
    [SerializeField] private GameObject antPrefab;
    [SerializeField] private GameObject bulletAntPrefab;
    [SerializeField] private int antSpawnRatio = 10;
    [SerializeField] private int bulletAntSpawnRatio = 1;
    [SerializeField] private Transform[] antDestinations;
    private float timeSinceLastSpawn = 0.9f;
    [SerializeField] private float spawnDecreaseFactorPerSec = 0.995f; // factor by which the spawn rate decreases every second

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnRateSeconds)
        {
            timeSinceLastSpawn = 0.0f;
            SpawnAnt();
        }

        // decrease spawn rate over time
        spawnRateSeconds *= Mathf.Pow(spawnDecreaseFactorPerSec, Time.deltaTime);
    }

    private void SpawnAnt()
    {
        Vector3 spawnPosition = transform.position;
        Vector3 destination = antDestinations[Random.Range(0, antDestinations.Length)].position;
        spawnPosition.z = 0.0f;
        int roll = Random.Range(1, antSpawnRatio + bulletAntSpawnRatio + 1);
        if (roll <= antSpawnRatio)
        {
            Instantiate(antPrefab, spawnPosition, Quaternion.identity).GetComponent<Ant>().destination = destination;
        }
        else
        {
            Instantiate(bulletAntPrefab, spawnPosition, Quaternion.identity).GetComponent<Ant>().destination = destination;
        }
    }
}
