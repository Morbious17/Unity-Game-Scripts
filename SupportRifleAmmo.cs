/******************************************************************************
  File Name: SupportRifleAmmo.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that enable the player to pick up
             support rifle ammo.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportRifleAmmo : MonoBehaviour
{
      //used to communicate with the HUD to display prompt
    [SerializeField] private HUDManager hudManager = null;
      //used to communiate with the item manager to add ammo to collection
    [SerializeField] private ItemManager itemManager = null;
      //used to break out of infinite loops
    private const float defaultTimerLength = 10;
    private float watchDogTimer;
    private int numberOfRoundsInBox; //number of bullets in this gameObject
    private int excessAmmo; //number of rounds that won't fit in current item slot

    void Start()
    {
        hudManager = GameObject.Find("HUD Canvas").GetComponent<HUDManager>();
        itemManager = GameObject.Find("InvItemsPanel").GetComponent<ItemManager>();
          //number of rounds this box of ammo contains when it spawns
        numberOfRoundsInBox = Random.Range(4, 8); 
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

          //if player presses E key while looking at item
        if (hudManager.GetPickupCooldown() <= 0.0f && Input.GetKeyDown(KeyCode.E) && !hudManager.GetPauseStatus())
        {
              //if there's no space in an occupied slot and there's no empty slots
            if (!itemManager.CheckForOccupiedPanel(gameObject.tag) && !itemManager.CheckForEmptyPanel())
            {
                hudManager.DisplayNoRoomText();
            }

              //this function is called continuously until there's no room or
              //all of the item is picked up and a timer is included in case
              //this becomes an infinite loop
            while (numberOfRoundsInBox > 0 &&
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
          //gives itemManager the number of kits from this pickup
        itemManager.GetPickupItemCount(numberOfRoundsInBox);

        if (itemManager.CheckForEmptyPanel()) //checks if there's an empty item slot
        {
              //checks if there's an item slot that already has some rifle rounds
            if (itemManager.CheckForOccupiedPanel(gameObject.tag))
            {
                if (itemManager.GetPanelItemCount() + numberOfRoundsInBox <=
                    itemManager.GetMaxSupportPanelRifleAmmo())
                {
                      //add ammo to partially filled item slot
                    itemManager.AddToItemPanel(gameObject.tag);
                      //add ammo to total count
                    itemManager.AddToSupportRifleAmmoCount(numberOfRoundsInBox);
                    numberOfRoundsInBox = 0;
                    Destroy(gameObject);
                }
                else
                {
                      //calculates excess ammo
                    excessAmmo = (itemManager.GetPanelItemCount() + numberOfRoundsInBox) -
                                  itemManager.GetMaxSupportPanelRifleAmmo();

                    itemManager.GetPickupItemCount(numberOfRoundsInBox - excessAmmo);
                      //fills partial item slot to capacity
                    itemManager.AddToItemPanel(gameObject.tag);
                      //adds part of rounds to total
                    itemManager.AddToSupportRifleAmmoCount(numberOfRoundsInBox - excessAmmo);
                      //store index of empty item slot
                    itemManager.CheckForEmptyPanel();

                    if (excessAmmo <= itemManager.GetMaxPanelRifleAmmo())
                    {
                          //give item manager excess ammo count
                        itemManager.GetPickupItemCount(excessAmmo);
                          //add excess ammo to empty item slot
                        itemManager.AddToEmptyPanel(gameObject.tag);
                          //add excess ammo to total ammo count
                        itemManager.AddToSupportRifleAmmoCount(excessAmmo);
                        numberOfRoundsInBox = 0;
                        excessAmmo = 0;
                        Destroy(gameObject);
                    }

                    numberOfRoundsInBox = excessAmmo; //sets remaining numer of rounds
                    excessAmmo = 0; //clears excess variable
                }
            }
              //checks if number of shells doesn't fit in a single empty slot
            else if (numberOfRoundsInBox > itemManager.GetMaxSupportPanelRifleAmmo())
            {
                  //calculates excess shells
                excessAmmo = (numberOfRoundsInBox - itemManager.GetMaxSupportPanelRifleAmmo());
                  //gives item manager the number of shells to put in an emtpy slot
                itemManager.GetPickupItemCount(numberOfRoundsInBox - excessAmmo);
                itemManager.CheckForEmptyPanel(); //get empy item slot index
                  //fills partial item slot to capacity
                itemManager.AddToEmptyPanel(gameObject.tag);
                  //adds part of shells to total
                itemManager.AddToSupportRifleAmmoCount(numberOfRoundsInBox - excessAmmo);
                  //excess is the remaining number of shells in this pickup
                numberOfRoundsInBox = excessAmmo;
                excessAmmo = 0; //resets excess
            }
            else
            {
                  //store empty panel index
                itemManager.CheckForEmptyPanel();
                  //add rounds to new item slot
                itemManager.AddToEmptyPanel(gameObject.tag);
                  //add rounds to total count
                itemManager.AddToSupportRifleAmmoCount(numberOfRoundsInBox);
                numberOfRoundsInBox = 0;
                Destroy(gameObject);
            }

            hudManager.DisplayPickUpText(gameObject.tag);
            hudManager.DisplayImage(gameObject.tag);
        }
          //checks if there's an item slot with room for more rifle rounds
        else if (itemManager.CheckForOccupiedPanel(gameObject.tag))
        {
              //checks if this pickup all fits in partially filled item slot
            if (itemManager.GetPanelItemCount() + numberOfRoundsInBox <=
                itemManager.GetMaxSupportPanelRifleAmmo())
            {
                  //add rounds to partially filled panel
                itemManager.AddToItemPanel(gameObject.tag);
                  //add rounds to total count
                itemManager.AddToSupportRifleAmmoCount(numberOfRoundsInBox);
                numberOfRoundsInBox = 0;
                Destroy(gameObject);
            }
            else
            {
                  //calculates excess ammo
                excessAmmo = (itemManager.GetPanelItemCount() + numberOfRoundsInBox) -
                              itemManager.GetMaxSupportPanelRifleAmmo();

                itemManager.GetPickupItemCount(numberOfRoundsInBox - excessAmmo);
                  //fills partial item slot to capacity
                itemManager.AddToItemPanel(gameObject.tag);
                  //adds part of rounds to total
                itemManager.AddToSupportRifleAmmoCount(numberOfRoundsInBox - excessAmmo);
                  //excess is the remaining number of rounds in this pickup
                numberOfRoundsInBox = excessAmmo;
                excessAmmo = 0; //resets excess
            }

            hudManager.DisplayPickUpText(gameObject.tag);
            hudManager.DisplayImage(gameObject.tag);
        }

          //number should never be less than 0, but this is just in case
        if(numberOfRoundsInBox <= 0)
        {
            Destroy(gameObject);
        }
    }
}
