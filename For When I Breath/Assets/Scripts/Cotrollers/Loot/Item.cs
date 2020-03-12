using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject 
{
    //Scriptable object for lootable objects 
    // Inhalers, healthkits, food
    public string itemName;
    public string itemDescription;

    public static InventoryManager inventory;
    
    public virtual void Use ()
    {

    }
}
