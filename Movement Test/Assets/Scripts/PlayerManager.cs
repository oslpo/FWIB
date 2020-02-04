using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // this script manages the player stats and stuff

    public float maxHealth;   // the total amount of damage the player can take before dying
    public float maxBreath;   // the total amount of breath the player can have
    public float maxStamina;  // total amount of stamina the player has
    public float curHealth;   //Player's current health
    float curBreath;   //player's current breath
    float curStamina;  // player's current stamina

    public Image healthBar;
    public Image breathBar;
    public Image staminaBar;

    private void Start()
    {
        curHealth = maxHealth;
        curBreath = maxBreath;
        curStamina = maxStamina;
    }

    private void Update()
    {
        StatManager();
    }

    void StatManager()
    {
        HealthManager();
        BreathManager();
        StaminaManager();
    }
    void HealthManager()
    {
        healthBar.fillAmount = curHealth / maxHealth;

        if(curHealth == 0)
        {
            //die
        }
    }

    void BreathManager()
    {
        breathBar.fillAmount = curBreath / maxBreath;

        if (curHealth == 0)
        {
            //die
        }
    }

    void StaminaManager()
    {
        staminaBar.fillAmount = curStamina / maxStamina;

        if (curHealth == 0)
        {
            //die
        }
    }

    public void TakeDamage(float damage)
    {
        if (curHealth - damage > 0)
        {
            curHealth -= damage;
        }
        else
        {
            //die
        }
    }

}
