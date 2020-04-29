using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    public List<GameObject> spawnableItems;
    public GameObject item;
    //[HideInInspector]
    bool hasSpawned = false;

    private void FixedUpdate()
    {
        if(item == null)
        {
            hasSpawned = false;
        }
    }

    public void spawnItem ()
    {
        if(!hasSpawned)
        {
            System.Random rand = new System.Random();
            int n = rand.Next(spawnableItems.Count);
            item = Instantiate(spawnableItems[n], gameObject.transform);
            hasSpawned = true;
        }
    }
}
