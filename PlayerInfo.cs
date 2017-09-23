using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerInfo : MonoBehaviour
{
    //This script contains values of the player's stats, such as health and stamina.

    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider StaminaSlider;

    public float currentHealth;
    public float currentMaxHealth;
    public const float defaultMaxHealth = 100;

    public float currentStamina;
    public float currentMaxStamina;
    //This is after stamina has been used.
    public float staminaCooldown1;
    //This is if stamina is used up completely. It will be larger than cooldown 1.
    public float staminaCooldown2;
    public const float defaultMaxStamina = 100;
    public float staminaRegenWait;

    public float PipeAttack1StamCost = 10;
    public float PipeAttack2StamCost = 7;
    public float PipeAttack3StamCost = 20;

	void Start ()
    {
        currentMaxHealth = 100;
        currentMaxStamina = 100;
        currentHealth = defaultMaxHealth;
        currentStamina = defaultMaxStamina;
	}

	void Update ()
    {
        //Ensures current health and stamina never stay at negative values.
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentStamina < 0)
        {
            currentStamina = 0;
            staminaCooldown2 = 6;
        }

        //Makes the cooldown countdown to 0 and makes sure it doesn't go negative.
        if (staminaCooldown1 > 0)
        {
            staminaCooldown1 -= Time.deltaTime;
        }
        else if (staminaCooldown1 < 0)
        {
            staminaCooldown1 = 0;
        }

        //Makes the cooldown countdown to 0 and makes sure it doesn't go negative.
        if (staminaCooldown2 > 0)
        {
            staminaCooldown2 -= Time.deltaTime;
        }
        else if (staminaCooldown2 < 0)
        {
            staminaCooldown2 = 0;
        }

        //Ensures current health and stamina don't go over their current max values.
        if (currentHealth > currentMaxHealth)
        {
            currentHealth = currentMaxHealth;
        }

        if (currentStamina > currentMaxStamina)
        {
            currentStamina = currentMaxStamina;
        }


        if (currentStamina < currentMaxStamina && staminaRegenWait > 0)
        {
            staminaRegenWait -= Time.deltaTime;
        }
        else if (currentStamina < currentMaxStamina && staminaRegenWait < 0)
        {
            staminaRegenWait = 0;
        }

        if (currentStamina < currentMaxStamina && staminaRegenWait == 0)
        {
            currentStamina += Time.deltaTime * 3.5f;
        }

        //Debug.Log("currentHealth: " + currentHealth);
        //Debug.Log("currentStamina: " + currentStamina);
	}


}
