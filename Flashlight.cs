/******************************************************************************
  File Name: Flashlight.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains a function for picking up and enabling the
             flashlight.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Flashlight : MonoBehaviour
{
      //the player the flashlight is attached to
    [SerializeField] private FirstPersonController firstPersonController = null;
    [SerializeField] private KeyItemManager keyItemManager = null;
      //displays prompt to pick up flashlight
    [SerializeField] private HUDManager hudManager = null;

    void Start()
    {
        hudManager = GameObject.Find("HUD Canvas").GetComponent<HUDManager>();
        keyItemManager = GameObject.Find("InvKeyItemsPanel").GetComponent<KeyItemManager>();
    }

    /**************************************************************************
   Function: HitByRay

Description: This function is called when the flashlight is hit by the player's
             raycast. If the player presses the E key and doesn't possess a
             flashlight, the player 'collects' a flashlight.

      Input: none

     Output: none
    **************************************************************************/
    private void HitByRay()
    {
        hudManager.DisplayPrompt(); //displays prompt to pick up flashlight
          //if player presses E while pointing at flashlight
        if (hudManager.GetPickupCooldown() <= 0.0f && Input.GetKeyDown(KeyCode.E) && !hudManager.GetPauseStatus())
        {
            if (firstPersonController.GetOldFlashlight()) //does player have old flashlight?
            {
                firstPersonController.UpgradeFlashlight(); //upgrades player flashlight
                PickUpKeyItem();
            }
            else
            {  
                //TODO: remove this when done testing
                firstPersonController.SetOldFlashlight(true); //enables flashlight component
                firstPersonController.EnableFlashlight(true); //turns flashlight on
                PickUpKeyItem();
            }
        }
    }

    /**************************************************************************
   Function: PickUpKeyItem

Description: This function checks they KeyItemManager for an empty slot. Then
             it adds this item to the first available empty slot.

      Input: none

     Output: none
    **************************************************************************/
    private void PickUpKeyItem()
    {
          //NOTE: there won't be a need for an else statement because the number
          //of key items and key item slots in the game will be equal
        if(keyItemManager.CheckForEmptyPanel()) //finds an empty slot
        {
              //adds key item to a key item slot
            keyItemManager.AddToEmptyPanel(gameObject.tag);
            Destroy(gameObject);

            hudManager.DisplayPickUpText(gameObject.tag);
            hudManager.DisplayImage(gameObject.tag);
        }
    }
}
