using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item pickUp;
    public float pickUpRadius;

    public Item PickUpItem()
    {
        return pickUp;
    }
}
