using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // this script manages the player stats and stuff
    [Header ("Player Stats")]
    public float maxHealth;   // the total amount of damage the player can take before dying
    public float maxBreath;   // the total amount of breath the player can have
    public float maxStamina;  // total amount of stamina the player has
    public float curHealth;   //Player's current health
    public float curBreath;          //player's current breath
    public float curStamina;         // player's current stamina

    [Header ("Asthma Mechanics")]
    public float breathDepletionRate; // the rate the breath bar depletes
    public float runDepletionRate;    // the rate the bar depletes while running
    [HideInInspector]
    public bool isRunning = false;    // Checks if the player is running

    [Header("Stat Bars")]
    public Image healthBar;
    public Image breathBar;
    public Image staminaBar;


    [Header("Enemy Interaction")]
    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }


    #endregion

    public GameObject player;

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

    void StatManager() //Manages the healthbars and the stats
    {
        HealthManager();
        BreathManager();
        StaminaManager();
    }

    void HealthManager ()
    {
        healthBar.fillAmount = curHealth / maxHealth;

        if(curHealth == 0)
        {
            //die
        }
    }

    void BreathManager()
    {
        if (!isRunning)
        {
            curBreath -= breathDepletionRate * Time.deltaTime;
        }
        else
        {
            curBreath -= runDepletionRate * Time.deltaTime;
        }

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

    public void HealDamage(float amount)
    {
        if (curHealth + amount <= maxHealth)
        {
            curHealth += amount;
        }
        else
        {
            curHealth = maxHealth;
        }
    }

    public void LoseBreath (float amount) //To be called when the player needs to lose a certain amount of breath at once
    {
        if(curBreath - amount > 0)
        {
            curBreath -= amount;
        }
        else
        {
            //die???
        }
    }

    public void HealBreath(float amount)
    {
        if (curBreath + amount <= maxBreath)
        {
            curBreath += amount;
        }
        else
        {
            curBreath = maxBreath;
        }
    }

}
