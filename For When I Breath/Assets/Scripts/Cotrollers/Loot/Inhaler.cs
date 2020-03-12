using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inhaler : Item
{
    public float restoreAmount;

    GameObject player = GameObject.FindGameObjectWithTag("Player");

    public override void Use ()
    {
        player.GetComponent<PlayerManager>().HealBreath(restoreAmount);
    }
}
