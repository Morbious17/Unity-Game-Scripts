/******************************************************************************
  File Name: Fuel.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that enable the player to pick up
             fuel.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
      //used to communicate with the HUD to display prompt
    [SerializeField] private HUDManager hudManager = null;
      //used to communiate with the inventory manager to add ammo to collection
    [SerializeField] private ItemManager itemManager = null;
      //used to break out of infinite loops
    private const float defaultTimerLength = 10;
    private float watchDogTimer;
    private int numberOfFuelCans = 1; //how many cans of fuel this gameObject is worth
    private int excessFuel;

    void Start ()
    {
        hudManager = GameObject.Find("HUD Canvas").GetComponent<HUDManager>();
        itemManager = GameObject.Find("InvItemsPanel").GetComponent<ItemManager>();
        watchDogTimer = defaultTimerLength;
    }

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
        hudManager.DisplayPrompt(); //displays prompt to pick up item

        if (hudManager.GetPickupCooldown() <= 0.0f && Input.GetKeyDown(KeyCode.E) && !hudManager.GetPauseStatus())
        {
              //if there's no space in an occupied slot and there's no empty slots
            if(!itemManager.CheckForOccupiedPanel(gameObject.tag) && !itemManager.CheckForEmptyPanel())
            {
                hudManager.DisplayNoRoomText();
            }

              //this function is called continuously until there's no room or
              //all of the item is picked up
            while (numberOfFuelCans > 0 &&
                   (itemManager.CheckForOccupiedPanel(gameObject.tag) ||
                   itemManager.CheckForEmptyPanel()) && watchDogTimer > 0)
            {
                PickUpItem();
                watchDogTimer -= Time.deltaTime;
            }
              //NOTE: PickUpItem function was written in a way to only be called
              //once. Since a while loop works, it could be rewritten to be much
              //smaller.

            if (watchDogTimer <= 0)
            {
                //Debug.Log("WatchDogTimer reached 0!");
            }

            watchDogTimer = defaultTimerLength;
        }
    }

    /**************************************************************************
   Function: PickUpItem

Description: This function gives the ItemManager this pickup's number of items
             and checks to see if there's room. If so, the item is either 
             placed in a partially filled item slot or a new one.

      Input: none

     Output: none
    **************************************************************************/
    private void PickUpItem()
    {
          //gives itemManager the number of fuel cans from this pickup
        itemManager.GetPickupItemCount(numberOfFuelCans);

        if (itemManager.CheckForEmptyPanel()) //checks if there's an empty item slot
        {
              //checks if there's an item slot that already has some fuel cans
            if (itemManager.CheckForOccupiedPanel(gameObject.tag))
            {
                if (itemManager.GetPanelItemCount() + numberOfFuelCans <=
                    itemManager.GetMaxPanelFuel())
                {
                      //add fuel cans to partially filled item slot
                    itemManager.AddToItemPanel(gameObject.tag);
                      //add fuel cans to total count
                    itemManager.AddToFuelCount(numberOfFuelCans);
                    numberOfFuelCans = 0;
                    Destroy(gameObject);
                }
                else
                {
                      //calculates excess fuel cans
                    excessFuel = (itemManager.GetPanelItemCount() + numberOfFuelCans) -
                                  itemManager.GetMaxPanelFuel();

                    itemManager.GetPickupItemCount(numberOfFuelCans - excessFuel);
                      //fills partial item slot to capacity
                    itemManager.AddToItemPanel(gameObject.tag);
                      //adds part of fuel cans to total
                    itemManager.AddToFuelCount(numberOfFuelCans - excessFuel);
                      //store index of empty item slot
                    itemManager.CheckForEmptyPanel();

                    if (excessFuel <= itemManager.GetMaxPanelFuel())
                    {
                          //give item manager excess fuel can count
                        itemManager.GetPickupItemCount(excessFuel);
                          //add excess fuel cans to empty item slot
                        itemManager.AddToEmptyPanel(gameObject.tag);
                          //add excess fuel cans to total fuel can count
                        itemManager.AddToFuelCount(excessFuel);
                        numberOfFuelCans = 0;
                        excessFuel = 0;
                        Destroy(gameObject);
                    }

                    numberOfFuelCans = excessFuel; //sets remaining number of fuel cans
                    excessFuel = 0; //clears excess varaible
                }
            }
              //if number of fuel cans is greater than what fits in a single item slot
            else if (numberOfFuelCans > itemManager.GetMaxPanelFuel())
            {
                  //calculates excess fuel cans
                excessFuel = (numberOfFuelCans - itemManager.GetMaxPanelFuel());
                  //gives item manager the number of fuel cans to put in an empty slot
                itemManager.GetPickupItemCount(numberOfFuelCans - excessFuel);
                itemManager.CheckForEmptyPanel(); //get empy item slot index
                  //fills partial item slot to capacity
                itemManager.AddToEmptyPanel(gameObject.tag);
                  //adds part of fuel cans to total
                itemManager.AddToFuelCount(numberOfFuelCans - excessFuel);
                  //excess is the remaining number of fuel cans in this pickup
                numberOfFuelCans = excessFuel;
                excessFuel = 0; //resets excess
            }
            else
            {
                  //store empty panel index
                itemManager.CheckForEmptyPanel();
                  //add rounds to new item slot
                itemManager.AddToEmptyPanel(gameObject.tag);
                itemManager.AddToFuelCount(numberOfFuelCans);
                numberOfFuelCans = 0;
                Destroy(gameObject);
            }

            hudManager.DisplayPickUpText(gameObject.tag);
            hudManager.DisplayImage(gameObject.tag); 
        }
          //checks if there's an item slot with room for more fuel cans
        else if (itemManager.CheckForOccupiedPanel(gameObject.tag))
        {
              //checks if this pickup all fits in partially filled item slot
            if (itemManager.GetPanelItemCount() + numberOfFuelCans <=
                itemManager.GetMaxPanelFuel())
            {
                  //add fuel to partially filled panel
                itemManager.AddToItemPanel(gameObject.tag);
                  //add fuel to total count
                itemManager.AddToFuelCount(numberOfFuelCans);
                numberOfFuelCans = 0;
                Destroy(gameObject);
            }
            else
            {
                  //calculates excess fuel cans
                excessFuel = (itemManager.GetPanelItemCount() + numberOfFuelCans) -
                              itemManager.GetMaxPanelFuel();

                itemManager.GetPickupItemCount(numberOfFuelCans - excessFuel);
                  //fills partial item slot to capacity
                itemManager.AddToItemPanel(gameObject.tag);
                  //adds part of fuel cans to total
                itemManager.AddToFuelCount(numberOfFuelCans - excessFuel);
                  //excess is the remaining number of fuel cans in this pickup
                numberOfFuelCans = excessFuel;
                excessFuel = 0; //resets excess
            }

            hudManager.DisplayPickUpText(gameObject.tag);
            hudManager.DisplayImage(gameObject.tag);
        }

          //number should never be less than 0, but this is just in case
        if (numberOfFuelCans <= 0)
        {
            Destroy(gameObject);
        }
    }
}
