using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> listOfItems;
    public int maxListSize;


    public void AddToList(Item item)
    {
        if (listOfItems.Count < maxListSize)
        {
            listOfItems.Add(item);
        }
        else
        {
            Debug.Log("INVENTORY FULL");
        }
    }
    
}
