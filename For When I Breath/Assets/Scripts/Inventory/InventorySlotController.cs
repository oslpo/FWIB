using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    public Item item;
    public Text text;
    public Image imageSprite;

    private void Start()
    {
        if (item != null)
        {
            SetSlot(item);
        }
    }
    public void Use ()
    {
        Debug.Log("ITEM!!!");
    }

    public void SetSlot (Item newItem)
    {
        if(newItem != null)
        {
            item = newItem;
            text.text = newItem.itemName;
            imageSprite.sprite = newItem.itemSprite;
        }
        else
        {
            item = null;
            text.text = null;
            imageSprite.sprite = null;
        }
    }
}
