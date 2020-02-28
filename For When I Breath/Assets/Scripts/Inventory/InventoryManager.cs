using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Item> startingItems;
    public int maxQuickListSize;
    public List<InventorySlotController> inventorySlots;
    public List<InventorySlotController> quickInventorySlots;
    public GameObject inventoryMenu;
    
    int itemsCounter = 0;
    int quickItemCounter = 0;
    int selectedIndex;
    private void Start()
    {
        if(startingItems.Count > 0)
        {
            foreach(Item item in startingItems)
            {
                AddToList(item);
            }
        }
        inventorySlots[0].SelectSlot(true);
        selectedIndex = 0;
    }

    private void Update()
    {
        if(inventoryMenu.active == true)
        {
            GetSlotSelection();
            AddToQuickFromInv();
        }
    }

    public void UseItem()
    {
        inventorySlots[selectedIndex].Use();
        RemoveFromList(selectedIndex);
    }

    public void AddToList(Item item)
    {
        if (quickItemCounter < 1)
        {
            quickInventorySlots[quickItemCounter].SetSlot(item);
            quickItemCounter++;
        }
        else if (itemsCounter < inventorySlots.Count)
        {
            inventorySlots[itemsCounter].SetSlot(item);
            itemsCounter++;
        }
        else
        {
            Debug.Log("INVENTORY FULL");
        }
    }

    public void UseQuickInventory (int index)
    {
        quickInventorySlots[index].Use();
        RemoveFromQuickList(index);
    }

    void RemoveFromList(int index)
    {
        inventorySlots[index].SetSlot(null);
        itemsCounter--;
    }
    void RemoveFromQuickList (int index)
    {
        if (quickInventorySlots[index] != null)
        {
            quickInventorySlots[index].SetSlot(null);
            quickItemCounter--;
        }
    }

    void GetSlotSelection()
    {
        if (Input.GetKeyDown(KeyCode.D)) //right
        {
            if (selectedIndex != inventorySlots.Count - 1)
            {
                inventorySlots[selectedIndex].SelectSlot(false);
                selectedIndex += 1;
                inventorySlots[selectedIndex].SelectSlot(true);
            }
            else
            {
                inventorySlots[selectedIndex].SelectSlot(false);
                selectedIndex = 0;
                inventorySlots[selectedIndex].SelectSlot(true);
            }
        }
        else if(Input.GetKeyDown(KeyCode.A)) // left
        {
            if (selectedIndex != 0)
            {
                inventorySlots[selectedIndex].SelectSlot(false);
                selectedIndex -= 1;
                inventorySlots[selectedIndex].SelectSlot(true);
            }
            else
            {
                inventorySlots[selectedIndex].SelectSlot(false);
                selectedIndex = inventorySlots.Count - 1;
                inventorySlots[selectedIndex].SelectSlot(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Down
        {
            if (selectedIndex < inventorySlots.Count - 2)
            {
                inventorySlots[selectedIndex].SelectSlot(false);
                selectedIndex += 2;
                inventorySlots[selectedIndex].SelectSlot(true);
            }
            else
            {
                inventorySlots[selectedIndex].SelectSlot(false);
                selectedIndex = Mathf.Abs(selectedIndex - inventorySlots.Count);
                inventorySlots[selectedIndex].SelectSlot(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W)) // Up
        {
            if (selectedIndex > 1)
            {
                inventorySlots[selectedIndex].SelectSlot(false);
                selectedIndex -= 2;
                inventorySlots[selectedIndex].SelectSlot(true);
            }
            else
            {
                inventorySlots[selectedIndex].SelectSlot(false);
                selectedIndex = Mathf.Abs(selectedIndex + 4);
                inventorySlots[selectedIndex].SelectSlot(true);
            }
        }
    }

    void AddToQuickFromInv()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(inventorySlots[selectedIndex].isFull)
            {
                if(quickItemCounter < 1)
                {
                    AddToList(inventorySlots[selectedIndex].item);
                    RemoveFromList(selectedIndex);
                }
                else
                {
                    Item hold = quickInventorySlots[0].item;
                    RemoveFromQuickList(0);
                    AddToList(inventorySlots[selectedIndex].item);
                    inventorySlots[selectedIndex].SetSlot(hold);
                    return;
                }
            }
        }
    }
}
