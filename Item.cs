/******************************************************************************
  File Name: Item.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains a function for manipulating the properties of
             each consumable item.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private HUDManager hudManager = null;
    private string itemType;

    void Start ()
    {
        itemType = gameObject.tag; //retrieves the gameObject's item type
	}


    void Update ()
    {

	}

    /**************************************************************************
   Function: HitByRay

Description: This function is called when the player's raycast touches this
             gameObject. The HUD displays a prompt and when the E key is
             pressed, the item is picked up.

      Input: none

     Output: none
    **************************************************************************/
    private void HitByRay()
    {
        hudManager.DisplayPrompt();

        if(Input.GetKeyDown(KeyCode.E))
        {
            //TODO: check item count before trying to take item
            TakeItem(itemType);

            Destroy(gameObject);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            hudManager.UpgradeHealth(10.0f);
            hudManager.UpgradeStamina(20.0f);
        }
    }

    /**************************************************************************
   Function: TakeItem

Description: Given a string, this function calls the specific pick up function
             for that item type.

      Input: itemType - string of the gameObject's tag

     Output: none
    **************************************************************************/
    private void TakeItem(string itemType)
    {
        switch(itemType)
        {
            case "SmallFirstAidKit":

                break;
            case "LargeFirstAidKit":

                break;
            case "PistolAmmo":

                break;
            case "ShotgunAmmo":

                break;
            case "RifleAmmo":

                break;
            case "Fuel":

                break;
            case "SupportSmallAidKit":

                break;
            case "SupportLargeAidKit":

                break;
            case "SupportPistolAmmo":

                break;
            case "SupportShotgunAmmo":

                break;
            case "SupportRifleAmmo":

                break;
            case "SmallCrystal":

                break;
            case "MediumCrystal":

                break;
            case "LargeCrystal":

                break;
        }
    }
}
