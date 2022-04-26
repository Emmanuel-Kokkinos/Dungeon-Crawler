using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float nextSpawnTime;

    [SerializeField] float delay = 2f;
    [SerializeField] GameObject enemy;
    [SerializeField] Transform[] spawnPoints;

    // Update is called once per frame
    void Update()
    {
        if(ShouldSpawn())
        {
            Spawn();
        }
    }

    

    bool ShouldSpawn()
    {
        return Time.time >= nextSpawnTime;
    }
    
    private void Spawn()
    {
        nextSpawnTime = Time.time + delay;
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[randomIndex];
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
