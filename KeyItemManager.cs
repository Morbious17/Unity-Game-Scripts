/******************************************************************************
  File Name: KeyItemManager.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that manage the player's key items.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class KeyItemManager : MonoBehaviour
{
      //used to deselect key item slots
    [SerializeField] private UIManager uiManager = null;
      //used to check if flashlight is on
    [SerializeField] private FirstPersonController firstPersonController = null;
      //displays messages when the player tries to use items but can't
    [SerializeField] private Text invTextMessage = null;
      //these make up the key item button in inventory sub tab
    [SerializeField] private Image keyItemButtonImage = null;
    [SerializeField] private Text keyItemButtonText = null;
      //shows the image of the collected key item
    [SerializeField] private Sprite flashlightImage = null;
      //these are the 18 panels the key item slots are children of
    [SerializeField] private GameObject keyItemPanel1 = null;
    [SerializeField] private GameObject keyItemPanel2 = null;
    [SerializeField] private GameObject keyItemPanel3 = null;
    [SerializeField] private GameObject keyItemPanel4 = null;
    [SerializeField] private GameObject keyItemPanel5 = null;
    [SerializeField] private GameObject keyItemPanel6 = null;
    [SerializeField] private GameObject keyItemPanel7 = null;
    [SerializeField] private GameObject keyItemPanel8 = null;
    [SerializeField] private GameObject keyItemPanel9 = null;
    [SerializeField] private GameObject keyItemPanel10 = null;
    [SerializeField] private GameObject keyItemPanel11 = null;
    [SerializeField] private GameObject keyItemPanel12 = null;
    [SerializeField] private GameObject keyItemPanel13 = null;
    [SerializeField] private GameObject keyItemPanel14 = null;
    [SerializeField] private GameObject keyItemPanel15 = null;
    [SerializeField] private GameObject keyItemPanel16 = null;
    [SerializeField] private GameObject keyItemPanel17 = null;
    [SerializeField] private GameObject keyItemPanel18 = null;
      //these are the 18 key item slots buttons that can be clicked
    [SerializeField] private Button keyItemPanelButton1 = null;
    [SerializeField] private Button keyItemPanelButton2 = null;
    [SerializeField] private Button keyItemPanelButton3 = null;
    [SerializeField] private Button keyItemPanelButton4 = null;
    [SerializeField] private Button keyItemPanelButton5 = null;
    [SerializeField] private Button keyItemPanelButton6 = null;
    [SerializeField] private Button keyItemPanelButton7 = null;
    [SerializeField] private Button keyItemPanelButton8 = null;
    [SerializeField] private Button keyItemPanelButton9 = null;
    [SerializeField] private Button keyItemPanelButton10 = null;
    [SerializeField] private Button keyItemPanelButton11 = null;
    [SerializeField] private Button keyItemPanelButton12 = null;
    [SerializeField] private Button keyItemPanelButton13 = null;
    [SerializeField] private Button keyItemPanelButton14 = null;
    [SerializeField] private Button keyItemPanelButton15 = null;
    [SerializeField] private Button keyItemPanelButton16 = null;
    [SerializeField] private Button keyItemPanelButton17 = null;
    [SerializeField] private Button keyItemPanelButton18 = null;
      //these are the 18 key item slot images
    [SerializeField] private Image keyItemImage1 = null;
    [SerializeField] private Image keyItemImage2 = null;
    [SerializeField] private Image keyItemImage3 = null;
    [SerializeField] private Image keyItemImage4 = null;
    [SerializeField] private Image keyItemImage5 = null;
    [SerializeField] private Image keyItemImage6 = null;
    [SerializeField] private Image keyItemImage7 = null;
    [SerializeField] private Image keyItemImage8 = null;
    [SerializeField] private Image keyItemImage9 = null;
    [SerializeField] private Image keyItemImage10 = null;
    [SerializeField] private Image keyItemImage11 = null;
    [SerializeField] private Image keyItemImage12 = null;
    [SerializeField] private Image keyItemImage13 = null;
    [SerializeField] private Image keyItemImage14 = null;
    [SerializeField] private Image keyItemImage15 = null;
    [SerializeField] private Image keyItemImage16 = null;
    [SerializeField] private Image keyItemImage17 = null;
    [SerializeField] private Image keyItemImage18 = null;
      //these are the 18 key item slot names
    [SerializeField] private Text keyItemNameText1 = null;
    [SerializeField] private Text keyItemNameText2 = null;
    [SerializeField] private Text keyItemNameText3 = null;
    [SerializeField] private Text keyItemNameText4 = null;
    [SerializeField] private Text keyItemNameText5 = null;
    [SerializeField] private Text keyItemNameText6 = null;
    [SerializeField] private Text keyItemNameText7 = null;
    [SerializeField] private Text keyItemNameText8 = null;
    [SerializeField] private Text keyItemNameText9 = null;
    [SerializeField] private Text keyItemNameText10 = null;
    [SerializeField] private Text keyItemNameText11 = null;
    [SerializeField] private Text keyItemNameText12 = null;
    [SerializeField] private Text keyItemNameText13 = null;
    [SerializeField] private Text keyItemNameText14 = null;
    [SerializeField] private Text keyItemNameText15 = null;
    [SerializeField] private Text keyItemNameText16 = null;
    [SerializeField] private Text keyItemNameText17 = null;
    [SerializeField] private Text keyItemNameText18 = null;
      //displays weapon and information about it
    [SerializeField] private Image invDetailImage = null;
    [SerializeField] private Text invDetailText = null;
    [SerializeField] private Text invDetailNameText = null;

      //arrays to store each group of item slot objects
    private GameObject[] keyItemPanels = new GameObject[18];
    private Button[] keyItemPanelButtons = new Button[18];
    private Image[] keyItemImages = new Image[18];
    private Text[] keyItemNameTexts = new Text[18];

    private const string flashlightDetails = "An old, yet reliable flashlight that can be clipped to your shirt or pocket.";
    private const string flashlightOnMessage = "The flashlight is already on";
    private const string cannotUseMessage = "You cannot use that here";

    private Color visible = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color invisible = new Color(0.0f, 0.0f, 0.0f, 0.0f);

      //how long a message appears if the player tries to use a key item they can't use
    private const float defaultMessagePromptLength = 1.2f;
    private const float delta = 0.019f; //used instead of delta time when time is paused
    private float messagePromptLength;
    //NOTE: this can be reduced to limit number of slots used
    private const int panelCount = 18; //number of key item slots
    private int keyItemSlotNumber; //number of specific key item slot



    void Start ()
    {
        StoreKeyItemPanels(); //stores panels key item buttons are children of into an array
        StoreKeyItemPanelButtons(); //stores key item buttons in an array
        StoreKeyItemImages(); //stores key item images in an array
        StoreKeyItemNameTexts(); //stores key item texts in an array
	}

    void Update()
    {
        if (messagePromptLength > 0.0f) //checks if counter is over zero
        {
            DecrementMessagePromptCounter(); //keep reducing the counter
        }
        else
        {
            invTextMessage.text = ""; //removes message
        }
    }

    /**************************************************************************
   Function: GetKeyItemPanelButtons

Description: This function returns an array that contains all 18 key item 
             buttons in the key item sub tab.

      Input: none

     Output: Returns the array of key item buttons.
    **************************************************************************/
    public Button[] GetKeyItemPanelButtons()
    {
        return keyItemPanelButtons;
    }

    /**************************************************************************
   Function: SetKeyItemSlotNumber

Description: Given an integer, this function sets the active slot as the given
             integer.

      Input: keyItemNumber - integer used to set the current chosen key item 
                             slot

     Output: none
    **************************************************************************/
    public void SetKeyItemSlotNumber(int keyItemNumber)
    {
        keyItemSlotNumber = keyItemNumber;
    }

    /**************************************************************************
   Function: CheckForEmptyPanel

Description: This function checks each key item panel in order and sets the
             keyItemSlotNumber to the first empty slot found.

      Input: none

     Output: Returns true if an empty key item panel was found, otherwise, 
             returns false.
    **************************************************************************/
    public bool CheckForEmptyPanel()
    {
        keyItemSlotNumber = -1; //resets the variable
          //loops through all 18 panels
        for (int i = 0; i < panelCount; i++)
        {
            if (keyItemImages[i].sprite == null)
            {
                keyItemSlotNumber = i;
                break;
            }
        }

        return (keyItemSlotNumber != -1); //true if an empty key item slot was found
    }

    /**************************************************************************
   Function: AddToEmptyPanel

Description: Given a string, this function adds the specified key item to an 
             empty key item slot.

      Input: weaponName - string of the key item to add to an key item slot

     Output: none
    **************************************************************************/
    public void AddToEmptyPanel(string keyItemName)
    {
          //checks if key items button hasn't been revealed yet
        if(!KeyItemButtonIsVisible()) 
        {
            RevealKeyItemButton();
        }

        switch (keyItemName)
        {
            case "Flashlight":
                  //makes key item slot visible
                keyItemPanels[keyItemSlotNumber].SetActive(true);
                  //image of key item
                keyItemImages[keyItemSlotNumber].sprite = flashlightImage;
                  //makes image visible
                keyItemImages[keyItemSlotNumber].color = visible;
                  //name of key item
                keyItemNameTexts[keyItemSlotNumber].text = "Old Flashlight";
                break;
            //case "test2":
            //    keyItemPanels[keyItemSlotNumber].SetActive(true);
            //    keyItemImages[keyItemSlotNumber].sprite = ;
            //    keyItemImages[keyItemSlotNumber].color = visible;
            //    keyItemNameTexts[keyItemSlotNumber].text = "";
            //    break;
            //case "test3":
            //    keyItemPanels[keyItemSlotNumber].SetActive(true);
            //    keyItemImages[keyItemSlotNumber].sprite = ;
            //    keyItemImages[keyItemSlotNumber].color = visible;
            //    keyItemNameTexts[keyItemSlotNumber].text = "";
            //    break;
            //case "test4":
            //    keyItemPanels[keyItemSlotNumber].SetActive(true);
            //    keyItemImages[keyItemSlotNumber].sprite = ;
            //    keyItemImages[keyItemSlotNumber].color = visible;
            //    keyItemNameTexts[keyItemSlotNumber].text = "";
            //    break;
            //case "test5":
            //    keyItemPanels[keyItemSlotNumber].SetActive(true);
            //    keyItemImages[keyItemSlotNumber].sprite = ;
            //    keyItemImages[keyItemSlotNumber].color = visible;
            //    keyItemNameTexts[keyItemSlotNumber].text = "";
            //    break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
    }

    /**************************************************************************
   Function: ClearKeyItemSlot

Description: This function clears the specified item slot of all text and
             its image.

      Input: none

     Output: none
    **************************************************************************/
    public void ClearKeyItemSlot()
    {
        keyItemImages[keyItemSlotNumber].sprite = null; //removes the key item image       
        keyItemImages[keyItemSlotNumber].color = invisible; //makes the key item slot black         
        keyItemNameTexts[keyItemSlotNumber].text = ""; //removes the name text        
    }

    /**************************************************************************
   Function: RemoveDetails

Description: This function sets the detail image to invisible, sets its sprite
             to null, and sets text to an empty string.

      Input: none

     Output: none
    **************************************************************************/
    public void RemoveDetails()
    {
        invDetailImage.color = invisible;
        invDetailImage.sprite = null;
        invDetailText.text = "";
        invDetailNameText.text = "";
    }

    /**************************************************************************
   Function: DisplayDetails

Description: This function retrieves the key item name of the currently 
             selected key item slot and uses it to display that upgrade's image 
             and details in the detail panel.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayDetails()
    {
        string itemName = keyItemNameTexts[keyItemSlotNumber].text;

        switch (itemName)
        {
            case "Old Flashlight":
                invDetailImage.color = visible;
                invDetailImage.sprite = flashlightImage;
                invDetailText.text = flashlightDetails;
                invDetailNameText.text = itemName;
                break;
            //case "":
            //    invDetailImage.color = visible;
            //    invDetailImage.sprite = ;
            //    invDetailText.text = ;
            //    invDetailNameText.text = itemName;
            //    break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
    }


    /**************************************************************************
   Function: DisplayFlashlightIsOnText

Description: This function tells the player the flashlight is already on.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayFlashlightIsOnText()
    {
        invTextMessage.text = flashlightOnMessage;
        ResetCantUsePromptCounter();
    }

    /**************************************************************************
   Function: UseKeyItem

Description: This function gets the current key item slot's name to determine 
             which use function to call. If the key item can't be used, text is 
             displayed to inform the player.

      Input: none

     Output: none
    **************************************************************************/
    public void UseItem()
    {
        string keyItemName = keyItemNameTexts[keyItemSlotNumber].text;

        switch (keyItemName)
        {
            case "Old Flashlight":
                if (firstPersonController.GetFlashlightOnStatus())
                {
                    DisplayFlashlightIsOnText();
                }
                else
                {
                    firstPersonController.EnableFlashlight(true);
                }
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        uiManager.DeselectKeyItemSlot();
        RemoveDetails();
    }

    /**************************************************************************
   Function: RevealKeyItemButton

Description: This function enables the key item button image and sets its text
             to "Key Items".

      Input: none

     Output: none
    **************************************************************************/
    public void RevealKeyItemButton()
    {
        keyItemButtonImage.enabled = true;
        keyItemButtonText.text = "Key Items";
    }

    /**************************************************************************
   Function: KeyItemButtonIsVisible

Description: This function returns a bool that represents whether or not the
             key item button image is enabled.

      Input: none

     Output: Returns true if the key items button image is enabled, otherwise,
             returns false.
    **************************************************************************/
    private bool KeyItemButtonIsVisible()
    {
        return keyItemButtonImage.enabled;
    }

    /**************************************************************************
   Function: ResetMessagePromptCounter

Description: This function sets the messagePromptLength variable to its default
             value.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetCantUsePromptCounter()
    {
        messagePromptLength = defaultMessagePromptLength;
    }

    /**************************************************************************
   Function: DecrementMessagePromptCounter

Description: This function decrements the messagePromptLength variable by
             delta every time it's called.

      Input: none

     Output: none
    **************************************************************************/
    private void DecrementMessagePromptCounter()
    {
        messagePromptLength -= delta;
    }

    /**************************************************************************
   Function: StoreKeyItemPanelButtons

Description: This function stores all key item slot buttons into a single array 
             for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreKeyItemPanelButtons()
    {
        keyItemPanelButtons[0] = keyItemPanelButton1;
        keyItemPanelButtons[1] = keyItemPanelButton2;
        keyItemPanelButtons[2] = keyItemPanelButton3;
        keyItemPanelButtons[3] = keyItemPanelButton4;
        keyItemPanelButtons[4] = keyItemPanelButton5;
        keyItemPanelButtons[5] = keyItemPanelButton6;
        keyItemPanelButtons[6] = keyItemPanelButton7;
        keyItemPanelButtons[7] = keyItemPanelButton8;
        keyItemPanelButtons[8] = keyItemPanelButton9;
        keyItemPanelButtons[9] = keyItemPanelButton10;
        keyItemPanelButtons[10] = keyItemPanelButton11;
        keyItemPanelButtons[11] = keyItemPanelButton12;
        keyItemPanelButtons[12] = keyItemPanelButton13;
        keyItemPanelButtons[13] = keyItemPanelButton14;
        keyItemPanelButtons[14] = keyItemPanelButton15;
        keyItemPanelButtons[15] = keyItemPanelButton16;
        keyItemPanelButtons[16] = keyItemPanelButton17;
        keyItemPanelButtons[17] = keyItemPanelButton18;
    }

    /**************************************************************************
   Function: StoreKeyItemImages

Description: This function stores all key item slot images into a single array 
             for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreKeyItemImages()
    {
        keyItemImages[0] = keyItemImage1;
        keyItemImages[1] = keyItemImage2;
        keyItemImages[2] = keyItemImage3;
        keyItemImages[3] = keyItemImage4;
        keyItemImages[4] = keyItemImage5;
        keyItemImages[5] = keyItemImage6;
        keyItemImages[6] = keyItemImage7;
        keyItemImages[7] = keyItemImage8;
        keyItemImages[8] = keyItemImage9;
        keyItemImages[9] = keyItemImage10;
        keyItemImages[10] = keyItemImage11;
        keyItemImages[11] = keyItemImage12;
        keyItemImages[12] = keyItemImage13;
        keyItemImages[13] = keyItemImage14;
        keyItemImages[14] = keyItemImage15;
        keyItemImages[15] = keyItemImage16;
        keyItemImages[16] = keyItemImage17;
        keyItemImages[17] = keyItemImage18;
    }

    /**************************************************************************
   Function: StoreKeyItemNameTexts

Description: This function stores all key item slot name text objects into a 
             single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreKeyItemNameTexts()
    {
        keyItemNameTexts[0] = keyItemNameText1;
        keyItemNameTexts[1] = keyItemNameText2;
        keyItemNameTexts[2] = keyItemNameText3;
        keyItemNameTexts[3] = keyItemNameText4;
        keyItemNameTexts[4] = keyItemNameText5;
        keyItemNameTexts[5] = keyItemNameText6;
        keyItemNameTexts[6] = keyItemNameText7;
        keyItemNameTexts[7] = keyItemNameText8;
        keyItemNameTexts[8] = keyItemNameText9;
        keyItemNameTexts[9] = keyItemNameText10;
        keyItemNameTexts[11] = keyItemNameText12;
        keyItemNameTexts[10] = keyItemNameText11;
        keyItemNameTexts[12] = keyItemNameText13;
        keyItemNameTexts[13] = keyItemNameText14;
        keyItemNameTexts[14] = keyItemNameText15;
        keyItemNameTexts[15] = keyItemNameText16;
        keyItemNameTexts[16] = keyItemNameText17;
        keyItemNameTexts[17] = keyItemNameText18;
    }

    /**************************************************************************
   Function: StoreKeyItemPanels

Description: This function stores panels the key item buttons are children of
             into a single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreKeyItemPanels()
    {
        keyItemPanels[0] = keyItemPanel1;
        keyItemPanels[1] = keyItemPanel2;
        keyItemPanels[2] = keyItemPanel3;
        keyItemPanels[3] = keyItemPanel4;
        keyItemPanels[4] = keyItemPanel5;
        keyItemPanels[5] = keyItemPanel6;
        keyItemPanels[6] = keyItemPanel7;
        keyItemPanels[7] = keyItemPanel8;
        keyItemPanels[8] = keyItemPanel9;
        keyItemPanels[9] = keyItemPanel10;
        keyItemPanels[10] = keyItemPanel11;
        keyItemPanels[11] = keyItemPanel12;
        keyItemPanels[12] = keyItemPanel13;
        keyItemPanels[13] = keyItemPanel14;
        keyItemPanels[14] = keyItemPanel15;
        keyItemPanels[15] = keyItemPanel16;
        keyItemPanels[16] = keyItemPanel17;
        keyItemPanels[17] = keyItemPanel18;
    }
}
