/******************************************************************************
  File Name: SupportLargeAidKit.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that enable the player to pick up
             support large aid kits.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportLargeAidKit : MonoBehaviour
{
      //used to communicate with the HUD to display prompt
    [SerializeField] private HUDManager hudManager = null;
      //used to update item slots and add large kits to collection
    [SerializeField] private ItemManager itemManager = null;
      //used to break out of infinite loops
    private const float defaultTimerLength = 10;
    private float watchDogTimer;
      //how many large aid kits this item is worth
    private int numberOfFirstAidKits = 1; //default number of kits in a pickup
    private int excessKits; //remaining kits in pickup

    void Start()
    {
        hudManager = GameObject.Find("HUD Canvas").GetComponent<HUDManager>();
        itemManager = GameObject.Find("InvItemsPanel").GetComponent<ItemManager>();
        watchDogTimer = defaultTimerLength;
    }



    /**************************************************************************
   Function: HitByRay

Description: This function is called when the player looks at this gameObject.
             If the player presses the 'E' key while the game isn't paused, the
             ItemManager checks to see if there's room. If so, the item is
             either placed in a partially filled item slot or a new one.

      Input: none

     Output: none
    **************************************************************************/
    private void HitByRay()
    {
        hudManager.DisplayPrompt(); //displays prompt to pick up item

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
            while (numberOfFirstAidKits > 0 &&
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
        itemManager.GetPickupItemCount(numberOfFirstAidKits);

        if (itemManager.CheckForEmptyPanel()) //checks if there's an empty item slot
        {
              //checks if there's an item slot that already has some kits
            if (itemManager.CheckForOccupiedPanel(gameObject.tag))
            {
                if (itemManager.GetPanelItemCount() + numberOfFirstAidKits <=
                    itemManager.GetMaxSupportPanelLargeAidKit())
                {
                      //add kits to partially filled item slot
                    itemManager.AddToItemPanel(gameObject.tag);
                      //add kits to total count
                    itemManager.AddToSupportLargeAidCount(numberOfFirstAidKits);
                    numberOfFirstAidKits = 0;
                    Destroy(gameObject);
                }
                else
                {
                      //calculates excess kits
                    excessKits = (itemManager.GetPanelItemCount() + numberOfFirstAidKits) -
                                  itemManager.GetMaxSupportPanelLargeAidKit();
                      //gives item manger number of kits to use
                    itemManager.GetPickupItemCount(numberOfFirstAidKits - excessKits);
                      //fills partial item slot to capacity
                    itemManager.AddToItemPanel(gameObject.tag);
                      //adds part of kits to total
                    itemManager.AddToSupportLargeAidCount(numberOfFirstAidKits - excessKits);
                      //store index of empty item slot
                    itemManager.CheckForEmptyPanel();

                      //checks if excess fits in a single item slot
                    if (excessKits <= itemManager.GetMaxSupportPanelLargeAidKit())
                    {
                          //give item manager excess kit count
                        itemManager.GetPickupItemCount(excessKits);
                          //add excess kits to empty item slot
                        itemManager.AddToEmptyPanel(gameObject.tag);
                          //add excess kits to total kit count
                        itemManager.AddToSupportLargeAidCount(excessKits);
                        numberOfFirstAidKits = 0;
                        excessKits = 0;
                        Destroy(gameObject);
                    }

                    numberOfFirstAidKits = excessKits; //sets remaining number of kits
                    excessKits = 0; //clears excess variable
                }
            }
              //checks if number of kits doesn't fit in a single empty slot
            else if (numberOfFirstAidKits > itemManager.GetMaxSupportPanelLargeAidKit())
            {
                  //calculates excess kits
                excessKits = (numberOfFirstAidKits - itemManager.GetMaxSupportPanelLargeAidKit());
                  //gives item manager the number of kits to put in an emtpy slot
                itemManager.GetPickupItemCount(numberOfFirstAidKits - excessKits);
                itemManager.CheckForEmptyPanel(); //get empy item slot index
                  //fills partial item slot to capacity
                itemManager.AddToEmptyPanel(gameObject.tag);
                  //adds part of kits to total
                itemManager.AddToSupportLargeAidCount(numberOfFirstAidKits - excessKits);
                  //excess is the remaining number of kits in this pickup
                numberOfFirstAidKits = excessKits;
                excessKits = 0; //resets excess
            }
            else
            {
                  //store empty panel index
                itemManager.CheckForEmptyPanel();
                  //add rounds to new item slot
                itemManager.AddToEmptyPanel(gameObject.tag);
                  //adds kits to total count
                itemManager.AddToSupportLargeAidCount(numberOfFirstAidKits);
                numberOfFirstAidKits = 0;
                Destroy(gameObject);
            }

            hudManager.DisplayPickUpText(gameObject.tag);
            hudManager.DisplayImage(gameObject.tag);
        }
          //checks if there's an item slot with room for more kits
        else if (itemManager.CheckForOccupiedPanel(gameObject.tag))
        {
              //checks if this pickup all fits in partially filled item slot
            if (itemManager.GetPanelItemCount() + numberOfFirstAidKits <= itemManager.GetMaxSupportPanelLargeAidKit())
            {
                  //adds kits to partially filled item slot
                itemManager.AddToItemPanel(gameObject.tag);
                  //adds kits to total count
                itemManager.AddToSupportLargeAidCount(numberOfFirstAidKits);
                numberOfFirstAidKits = 0;
                Destroy(gameObject);
            }
            else
            {
                  //calculates excess kits
                excessKits = (itemManager.GetPanelItemCount() + numberOfFirstAidKits) -
                              itemManager.GetMaxSupportPanelLargeAidKit();
                  //gives item manager number of kits to add to partially filled item slot
                itemManager.GetPickupItemCount(numberOfFirstAidKits - excessKits);
                  //fills partial item slot to capacity
                itemManager.AddToItemPanel(gameObject.tag);
                  //adds part of kits to total
                itemManager.AddToSupportLargeAidCount(numberOfFirstAidKits - excessKits);
                  //excess is the remaining number of kits in this pickup
                numberOfFirstAidKits = excessKits;
                excessKits = 0; //resets excess
            }

            hudManager.DisplayPickUpText(gameObject.tag);
            hudManager.DisplayImage(gameObject.tag);
        }

          //number should never be less than 0, but this is just in case
        if (numberOfFirstAidKits <= 0)
        {
            Destroy(gameObject);
        }
    }
}
