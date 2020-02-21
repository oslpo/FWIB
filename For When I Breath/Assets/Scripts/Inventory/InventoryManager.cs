using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Item> listOfItems;
    public List<Item> listOfQuickItems;
    public int maxListSize;
    public int maxQuickListSize;
    public List<Button> inventorySlots;
    public List<Button> quickInventorySlots;
    
    int itemsCounter = 0;
    int quickItemCounter = 0;

    private void Start()
    {
        for (int i = 0; i < maxListSize; i++)
        {
            listOfItems.Add(null);
        }
        for(int i = 0; i < maxQuickListSize; i++)
        {
            listOfQuickItems.Add(null);
        }
    }

    public void AddToList(Item item)
    {
        if (itemsCounter < maxListSize)
        {
            listOfItems[itemsCounter] = item;
            UpdateInventorySlots();
            itemsCounter++;
            if(itemsCounter < 3 && quickItemCounter < 3)
            {
                listOfQuickItems[quickItemCounter] = item;
                quickItemCounter++;
                UpdateQuickInventorySlots();
            }
     
        }
        else
        {
            Debug.Log("INVENTORY FULL");
        }
    }

    public void UseQuickInventory (int index)
    {
        listOfQuickItems[index].Use();
        RemoveFromList(listOfItems.IndexOf(listOfQuickItems[index]));
        RemoveFromQuickList(index);
    }

    void RemoveFromList(int index)
    {
        listOfItems[index] = null;
        inventorySlots.RemoveAt(index);
        itemsCounter--;
    }
    void RemoveFromQuickList (int index)
    {
        listOfQuickItems[index] = null;
        UpdateQuickInventorySlots();
        quickItemCounter--;
    }
    
    public void UpdateInventorySlots ()
    {
        int counter = 0;
        if (listOfItems[0] != null)
        {
            foreach (Item item in listOfItems)
            {
                inventorySlots[counter].GetComponent<InventorySlotController>().SetSlot(item);
                counter++;
            }
        }
        else
        {
            inventorySlots[0].GetComponent<InventorySlotController>().SetSlot(null);
        }
    }

    public void UpdateQuickInventorySlots ()
    {
        int counter = 0;
        if (listOfQuickItems[0] != null)
        {
            foreach (Item item in listOfQuickItems)
            {
                quickInventorySlots[counter].GetComponent<InventorySlotController>().SetSlot(item);
                counter++;
            }
        }
        else
        {
            quickInventorySlots[0].GetComponent<InventorySlotController>().SetSlot(null);
        }
        
    }
}
