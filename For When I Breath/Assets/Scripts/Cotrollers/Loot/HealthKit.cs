using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : Item
{
    public float restoreAmount;

    GameObject player = GameObject.FindGameObjectWithTag("Player");

    public override void Use()
    {
        player.GetComponent<PlayerManager>().HealDamage(restoreAmount);
    }
}
