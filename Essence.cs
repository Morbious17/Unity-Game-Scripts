/******************************************************************************
  File Name: Essence.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that enable the player to pick up
             essence, the game's currency.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
      //used to communicate with the HUD to display prompt
    [SerializeField] private HUDManager hudManager = null;
      //used to communiate with the inventory manager to add ammo to collection
    [SerializeField] private ItemManager itemManager = null;
      //values of each essence
    private const int BlueEssenceSmallValue = 1;    //old value is 1
    private const int BlueEssenceMedValue = 7;      //old value is 2
    private const int BlueEssenceLargeValue = 20;   //old value is 5
    private const int YellowEssenceSmallValue = 2;  //old value is 7
    private const int YellowEssenceMedValue = 10;   //old value is 10
    private const int YellowEssenceLargeValue = 25; //old value is 15
    private const int RedEssenceSmallValue = 5;     //old value is 20
    private const int RedEssenceMedValue = 15;      //old value is 25
    private const int RedEssenceLargeValue = 30;    //old value is 30
    private int essenceValue; //value of this specific essence

    void Start()
    {
          //retrieves the HUD and InventoryManager
        hudManager = GameObject.Find("HUD Canvas").GetComponent<HUDManager>();
        itemManager = GameObject.Find("InvItemsPanel").GetComponent<ItemManager>();
    }

    /**************************************************************************
   Function: GetEssenceValue

Description: This function checks the tag of this essence and sets the
             essenceValue to the value associated with that essence's tag.
             Spawned essence adds "Clone #" to each name.

      Input: none

     Output: none
   **************************************************************************/
    public void GetEssenceValue()
    {
        switch (gameObject.tag)
        {
            case "BlueEssenceS":
                essenceValue = BlueEssenceSmallValue;
                break;
            case "BlueEssenceM":
                essenceValue = BlueEssenceMedValue;
                break;
            case "BlueEssenceL":
                essenceValue = BlueEssenceLargeValue;
                break;
            case "YellowEssenceS":
                essenceValue = YellowEssenceSmallValue;
                break;
            case "YellowEssenceM":
                essenceValue = YellowEssenceMedValue;
                break;
            case "YellowEssenceL":
                essenceValue = YellowEssenceLargeValue;
                break;
            case "RedEssenceS":
                essenceValue = RedEssenceSmallValue;
                break;
            case "RedEssenceM":
                essenceValue = RedEssenceMedValue;
                break;
            case "RedEssenceL":
                essenceValue = RedEssenceLargeValue;
                break;
            default:
                //Debug.Log("Invalid Essence Name");
                break;
        }
    }

    /**************************************************************************
   Function: HitByRay

Description: This function is called when the player looks at this gameObject.
             If the player presses the 'E' key, they pick up the essence.

      Input: none

     Output: none
    **************************************************************************/
    private void HitByRay()
    {
        hudManager.DisplayPrompt(); //displays prompt to pick up item
          //checks if player pressed E key while game isn't paused
        if (hudManager.GetPickupCooldown() <= 0.0f && Input.GetKeyDown(KeyCode.E) && !hudManager.GetPauseStatus())
        {
              //checks if player got some essence for the first time
            if(itemManager.GetCurrentInventoryItemCount(gameObject.tag) <= 0)
            {
                itemManager.SetEssenceSlot(); //reveals currency slot
            }
              //checks which essence this script is attached to and assigned
              //the appropriate value
            GetEssenceValue();
              //adds the value of this essence to the inventory
            itemManager.AddToEssenceCount(essenceValue);
              //updates currency slot
            itemManager.UpdateEssenceText();
            Destroy(gameObject); //destroy this gameObject

            hudManager.DisplayPickUpText(gameObject.tag);
            hudManager.DisplayImage(gameObject.tag);
        }
    }
}
