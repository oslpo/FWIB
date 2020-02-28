using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    public Item item;
    public Text text;
    public Image imageSprite;
    public bool isSelected;
    public bool isFull = false;

    Color inActiveColor;
    Color activeColor = Color.cyan;

    private void Start()
    {
        if (item != null)
        {
            SetSlot(item);
            isFull = true;
        }
        else
        {
            text.text = "";
            imageSprite.color = Color.clear ;
        }
        inActiveColor = this.GetComponent<Image>().color;
    }
    public void Use ()
    {
        Debug.Log("ITEM!!!");
        item.Use();
        SetSlot(null);
    }

    private void FixedUpdate()
    {
        if(isSelected)
        {
            this.GetComponent<Image>().color = activeColor;
        }
        else
        {
            this.GetComponent<Image>().color = inActiveColor;
        }
    }

    public void SetSlot (Item newItem)
    {
        if(newItem != null)
        {
            item = newItem;
            text.text = newItem.itemName;
            imageSprite.sprite = newItem.itemSprite;
            imageSprite.color = Color.white;
            isFull = true;
        }
        else
        {
            item = null;
            text.text = null;
            imageSprite.sprite = null;
            imageSprite.color = Color.clear;
            isFull = false;
        }
    }

    public void SelectSlot(bool status)
    {
        isSelected = status;
    }
}
