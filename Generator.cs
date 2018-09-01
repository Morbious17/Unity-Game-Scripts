/******************************************************************************
  File Name: Generator.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that enable interactions and
             functionality of generators that require fuel from the player.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
      //door, light, or other object powered by the generator
    [SerializeField] private GameObject firstPartToActivate = null;
    [SerializeField] private GameObject secondPartToActivate = null;
      //used to tell player if they need fuel
    [SerializeField] private HUDManager hudManager = null;
      //used to check for and use fuel
    [SerializeField] private ItemManager itemManager = null;
    private bool activated; //is true if generator is on

    /**************************************************************************
   Function: HitByRay

Description: This function is called when the player looks at this gameObject.
         If the player presses the 'E' key while the game isn't paused, the
         pickup function is called.

      Input: none

     Output: none
    **************************************************************************/
    private void HitByRay()
    {
        if(!activated)
        {
            hudManager.DisplayPrompt();

            if (hudManager.GetPickupCooldown() <= 0.0f && Input.GetKeyDown(KeyCode.E) && !hudManager.GetPauseStatus())
            {
                //if the generator hasn't been turned on and player has fuel
                if (!activated && itemManager.GetCurrentInventoryItemCount("Fuel") > 0)
                {
                    ActivateGenerator(); //use fuel and turn the generator on
                    hudManager.ClearPrompt(); //immediately remove prompt to turn on generator
                    //TODO: add hudmanager function to tell player generator was turned on
                }
                else
                {
                    hudManager.ExamineObject(gameObject.tag);
                    //hudManager.DisplayNeedFuelText(); //tell the player they need fuel
                }
            }
        }
    }

    /**************************************************************************
   Function: ActivateGenerator

Description: This function checks if a component has been assigned to each of
             the two parts. If so, they are activated. Then 1 can of fuel is
             removed from the inventory and the activated bool is set to true
             to prevent fuel from being used on this generator again.

      Input: none

     Output: none
    **************************************************************************/
    private void ActivateGenerator()
    {
          //checks if there's an object to be activated
        if(firstPartToActivate)
        {
            firstPartToActivate.SetActive(true);
        }
          //check if the object this script is attached to has a 2nd part
        if(secondPartToActivate)
        {
            secondPartToActivate.SetActive(true);
        }

        itemManager.UseFuel(); //removes one fuel from inventory
        activated = true; //generator is on, player can't use fuel on it again
    }


    /**************************************************************************
   Function: ActivateGeneratorFromInventory

Description: This function is the same as activate generator function, except
             that it is called from the use fuel from inventory function in
             the item manager.

      Input: none

     Output: none
    **************************************************************************/
    private void ActivateGeneratorFromInventory()
    {
          //checks if there's an object to be activated
        if (firstPartToActivate)
        {
            firstPartToActivate.SetActive(true);
        }
          //check if the object this script is attached to has a 2nd part
        if (secondPartToActivate)
        {
            secondPartToActivate.SetActive(true);
        }

        hudManager.ClearPrompt(); //immediately remove prompt to turn on generator
        activated = true; //generator is on, player can't use fuel on it again
    }
}
