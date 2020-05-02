using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List <Item> itemsToSpawn;
    public List<ItemSpawnPoint> spawnPoints;
    public float spawnRate;
    public float currSpawnRate;

    private void Start()
    {
        currSpawnRate = spawnRate;
        SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        currSpawnRate -= Time.deltaTime;
        if (currSpawnRate < 0)
        {
            SpawnObjects();
            currSpawnRate = spawnRate;
        }
    }

    void SpawnObjects ()
    {
        for(int i = 0; i < spawnPoints.Count;i++)
        {
            spawnPoints[i].spawnItem();
        }

    }
}
