/******************************************************************************
  File Name: RevealWeakness2.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that allow the player to pick up
             the "Reveal Weakness 2" upgrade and add it to their inventory.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class RevealWeakness2 : MonoBehaviour
{
    [SerializeField] private HUDManager hudManager = null;
    [SerializeField] private UpgradeManager upgradeManager = null;

    /**************************************************************************
   Function: HitByRay

Description: This function is called when the player looks at this gameObject.
             If the player presses the 'E' key while the game isn't paused, 

      Input: none

     Output: none
    **************************************************************************/
    private void HitByRay()
    {
        hudManager.DisplayPrompt(); //displays prompt to pick up upgrade
          //if cooldown is 0, the player presses E, and the game isn't paused
        if (hudManager.GetPickupCooldown() <= 0.0f && Input.GetKeyDown(KeyCode.E) && !hudManager.GetPauseStatus())
        {
            if (upgradeManager.CheckForEmptyPanel()) //find an empty upgrade slot
            {
                upgradeManager.AddToEmptyPanel(gameObject.tag); //add to slot

                Destroy(gameObject);

                hudManager.DisplayPickUpText(gameObject.tag); //display message
                hudManager.DisplayImage(gameObject.tag); //display image
            }
        }
    }


    private void PickUpUpgrade()
    {

    }
}
