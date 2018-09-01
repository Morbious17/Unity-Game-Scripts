/******************************************************************************
  File Name: UpgradeManager.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that manage the player's upgrades.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
      //used to deselect upgrade slot after equipping an upgrade
    [SerializeField] private UIManager uiManager = null;
      //used to reveal survival note about weak points 1
    [SerializeField] private SurvivalNoteManager survivalNoteManager = null;
      //used to increase max stamina, apply caps to stamina regen, and increase max health
    [SerializeField] private HUDManager hudManager = null;
      //these make up the upgrade button in inventory sub tab
    [SerializeField] private Image upgradeButtonImage = null;
    [SerializeField] private Text upgradeButtonText = null;
    //displayed when an upgrade is examined 
    //TODO: Consider removing these and hiding the detail text. Images of these aren't important in upgrade tab
    [SerializeField] private Sprite weakness1Image = null;
      //displays name of upgrade in stat panel
    [SerializeField] private Text upgradePanelNameText = null;
      //displays the upgrade's current level
    [SerializeField] private Text upgradePanelLevelAmountText = null;
      //displays description of what the upgrade looks and feels like
    [SerializeField] private Text upgradePanelDescriptionDetailText = null;
      //displays how heavy the upgrade's burden is
    [SerializeField] private Text upgradePanelBurdenAmountText = null;
      //displays the range of the upgrade's effect if applicable
    [SerializeField] private Text upgradePanelRangeAmountText = null;
      //displays use type of upgrade (automatic or manual)
    [SerializeField] private Text upgradePanelUseTypeDetailText = null;
      //displays what the upgrade does when used
    [SerializeField] private Text upgradePanelEffectDetailText = null;
      //used to show detailed information on examined upgrade
    [SerializeField] private Image invDetailImage = null;
    [SerializeField] private Text invDetailText = null;
    [SerializeField] private Text invDetailNameText = null;
      //used to display the player's stamina in upgrade menu
    [SerializeField] private Slider staminaBar = null;
      //used to position stamina bar after it increases in size
    [SerializeField] private RectTransform staminaRectTransform = null;
      //used to show stamina cap on upgrade menu when an upgrade is equipped
    [SerializeField] private Slider staminaCapMarker = null;
      //used to ensure marker's slider is the same size as stamina bar
    [SerializeField] private RectTransform staminaCapRectTransform = null;
      //used to signify where the stamina is capped
    [SerializeField] private GameObject staminaCapMarkerHandle = null;
    //displays messages when the player tries to equip upgrades but can't
    [SerializeField] private Text invTextMessage = null;
      //these are the 18 upgrade slots the buttons are children of
    [SerializeField] private GameObject upgradePanel1 = null;
    [SerializeField] private GameObject upgradePanel2 = null;
    [SerializeField] private GameObject upgradePanel3 = null;
    [SerializeField] private GameObject upgradePanel4 = null;
    [SerializeField] private GameObject upgradePanel5 = null;
    [SerializeField] private GameObject upgradePanel6 = null;
    [SerializeField] private GameObject upgradePanel7 = null;
    [SerializeField] private GameObject upgradePanel8 = null;
    [SerializeField] private GameObject upgradePanel9 = null;
    [SerializeField] private GameObject upgradePanel10 = null;
    [SerializeField] private GameObject upgradePanel11 = null;
    [SerializeField] private GameObject upgradePanel12 = null;
    [SerializeField] private GameObject upgradePanel13 = null;
    [SerializeField] private GameObject upgradePanel14 = null;
    [SerializeField] private GameObject upgradePanel15 = null;
    [SerializeField] private GameObject upgradePanel16 = null;
    [SerializeField] private GameObject upgradePanel17 = null;
    [SerializeField] private GameObject upgradePanel18 = null;
      //these are the 18 upgrade slot buttons that can be clicked
    [SerializeField] private Button upgradePanelButton1 = null;
    [SerializeField] private Button upgradePanelButton2 = null;
    [SerializeField] private Button upgradePanelButton3 = null;
    [SerializeField] private Button upgradePanelButton4 = null;
    [SerializeField] private Button upgradePanelButton5 = null;
    [SerializeField] private Button upgradePanelButton6 = null;
    [SerializeField] private Button upgradePanelButton7 = null;
    [SerializeField] private Button upgradePanelButton8 = null;
    [SerializeField] private Button upgradePanelButton9 = null;
    [SerializeField] private Button upgradePanelButton10 = null;
    [SerializeField] private Button upgradePanelButton11 = null;
    [SerializeField] private Button upgradePanelButton12 = null;
    [SerializeField] private Button upgradePanelButton13 = null;
    [SerializeField] private Button upgradePanelButton14 = null;
    [SerializeField] private Button upgradePanelButton15 = null;
    [SerializeField] private Button upgradePanelButton16 = null;
    [SerializeField] private Button upgradePanelButton17 = null;
    [SerializeField] private Button upgradePanelButton18 = null;
      //these are the 18 upgrade slot images
    [SerializeField] private Image upgradeImage1 = null;
    [SerializeField] private Image upgradeImage2 = null;
    [SerializeField] private Image upgradeImage3 = null;
    [SerializeField] private Image upgradeImage4 = null;
    [SerializeField] private Image upgradeImage5 = null;
    [SerializeField] private Image upgradeImage6 = null;
    [SerializeField] private Image upgradeImage7 = null;
    [SerializeField] private Image upgradeImage8 = null;
    [SerializeField] private Image upgradeImage9 = null;
    [SerializeField] private Image upgradeImage10 = null;
    [SerializeField] private Image upgradeImage11 = null;
    [SerializeField] private Image upgradeImage12 = null;
    [SerializeField] private Image upgradeImage13 = null;
    [SerializeField] private Image upgradeImage14 = null;
    [SerializeField] private Image upgradeImage15 = null;
    [SerializeField] private Image upgradeImage16 = null;
    [SerializeField] private Image upgradeImage17 = null;
    [SerializeField] private Image upgradeImage18 = null;
      //these are the 18 upgrade slot names
    [SerializeField] private Text upgradeNameText1 = null;
    [SerializeField] private Text upgradeNameText2 = null;
    [SerializeField] private Text upgradeNameText3 = null;
    [SerializeField] private Text upgradeNameText4 = null;
    [SerializeField] private Text upgradeNameText5 = null;
    [SerializeField] private Text upgradeNameText6 = null;
    [SerializeField] private Text upgradeNameText7 = null;
    [SerializeField] private Text upgradeNameText8 = null;
    [SerializeField] private Text upgradeNameText9 = null;
    [SerializeField] private Text upgradeNameText10 = null;
    [SerializeField] private Text upgradeNameText11 = null;
    [SerializeField] private Text upgradeNameText12 = null;
    [SerializeField] private Text upgradeNameText13 = null;
    [SerializeField] private Text upgradeNameText14 = null;
    [SerializeField] private Text upgradeNameText15 = null;
    [SerializeField] private Text upgradeNameText16 = null;
    [SerializeField] private Text upgradeNameText17 = null;
    [SerializeField] private Text upgradeNameText18 = null;
      //these show what level each upgrade is
    [SerializeField] private Text upgradeLevelText1 = null;
    [SerializeField] private Text upgradeLevelText2 = null;
    [SerializeField] private Text upgradeLevelText3 = null;
    [SerializeField] private Text upgradeLevelText4 = null;
    [SerializeField] private Text upgradeLevelText5 = null;
    [SerializeField] private Text upgradeLevelText6 = null;
    [SerializeField] private Text upgradeLevelText7 = null;
    [SerializeField] private Text upgradeLevelText8 = null;
    [SerializeField] private Text upgradeLevelText9 = null;
    [SerializeField] private Text upgradeLevelText10 = null;
    [SerializeField] private Text upgradeLevelText11 = null;
    [SerializeField] private Text upgradeLevelText12 = null;
    [SerializeField] private Text upgradeLevelText13 = null;
    [SerializeField] private Text upgradeLevelText14 = null;
    [SerializeField] private Text upgradeLevelText15 = null;
    [SerializeField] private Text upgradeLevelText16 = null;
    [SerializeField] private Text upgradeLevelText17 = null;
    [SerializeField] private Text upgradeLevelText18 = null;

      //these are enabled when an upgrade in this item slot is equipped
    [SerializeField] private Image upgradeEquippedImage1 = null;
    [SerializeField] private Image upgradeEquippedImage2 = null;
    [SerializeField] private Image upgradeEquippedImage3 = null;
    [SerializeField] private Image upgradeEquippedImage4 = null;
    [SerializeField] private Image upgradeEquippedImage5 = null;
    [SerializeField] private Image upgradeEquippedImage6 = null;
    [SerializeField] private Image upgradeEquippedImage7 = null;
    [SerializeField] private Image upgradeEquippedImage8 = null;
    [SerializeField] private Image upgradeEquippedImage9 = null;
    [SerializeField] private Image upgradeEquippedImage10 = null;
    [SerializeField] private Image upgradeEquippedImage11 = null;
    [SerializeField] private Image upgradeEquippedImage12 = null;
    [SerializeField] private Image upgradeEquippedImage13 = null;
    [SerializeField] private Image upgradeEquippedImage14 = null;
    [SerializeField] private Image upgradeEquippedImage15 = null;
    [SerializeField] private Image upgradeEquippedImage16 = null;
    [SerializeField] private Image upgradeEquippedImage17 = null;
    [SerializeField] private Image upgradeEquippedImage18 = null;

      //arrays to store each group of upgrade slot objects
    private GameObject[] upgradePanels = new GameObject[18];
    private Button[] upgradePanelButtons = new Button[18];
    private Image[] upgradeImages = new Image[18];
    private Text[] upgradeNameTexts = new Text[18];
    private Text[] upgradeLevelTexts = new Text[18];
    private Image[] upgradeEquippedImages = new Image[18];
      //TODO: change these to descriptions of the physical objects.
    private const string weakness1Description = "A strange trinket that gives off a faint glow. It's warm to the touch.";
    private const string weakness2Description = "A more potent trinket. Your hands tingle as you hold it.";

    private const string weakness1Effect = "Reveals the weak point of weaker enemies when within range of them";
    private const string weakness2Effect = "Reveals the weak point of stronger enemies as well as weaker ones when within range of them";

    private Color visible = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color invisible = new Color(0.0f, 0.0f, 0.0f, 0.0f);
      //these colors are used on the upgrade level text
    private Color defaultLevelColor = new Color(0.0f, 0.0f, 0.0f, 1.0f); //black
    private Color maxLevelColor = new Color(1.0f, 1.0f, 0.0f, 1.0f); //yellow
      //these colors are used on burden amount text
    private Color veryHeavyColor = new Color(0.588f, 0.0f, 0.0f, 1.0f); //burgundy
    private Color heavyColor = new Color(1.0f, 0.0f, 0.0f, 1.0f); //red
    private Color moderateColor = new Color(1.0f, 0.392f, 0.0f, 1.0f); //orange
    private Color lightColor = new Color(1.0f, 1.0f, 0.0f, 1.0f); //yellow
    private Color veryLightColor = new Color(0.0f, 1.0f, 0.0f, 1.0f); //green
    private Color defaultPanelLevelColor = new Color(1.0f, 1.0f, 1.0f, 1.0f); //white

      //how long a message appears if the player tries to equip an upgrade when they can't
    private const float defaultCantEquipLength = 3.0f;
    private const float delta = 0.019f; //used instead of delta time when time is paused
    private float cantEquipLength;
      //number of upgrade slots. Can be reduced and used in find empty slots to reduce number of slots used
    private const int panelCount = 18; 
    private const int maxLevel = 3; //highest level an upgrade can reach
    private const int levelOne = 1; //to remove the magic number from function
    private const int levelTwo = 2; //to remove the magic number from function
    private const int startingLevel = 1; //level at which an upgrade starts
      //used to determine what color of text burden amount should be
    private const int veryHeavyThreshold = 40;
    private const int heavyThreshold = 30;
    private const int moderateThreshold = 20;
    private const int lightThreshold = 10;

      //variables associated with the "Reveal Weakness 1" upgrade
    private const int weakness1LevelOneStaminaPenalty = 20; //amount of stamina subtracted from max while equipped
    private const int weakness1LevelTwoStaminaPenalty = 15;
    private const int weakness1LevelThreeStaminaPenalty = 8;  
    private const int weakness1LevelOneRange = 10; //how close the player must be to reveal weakness
    private const int weakness1LevelTwoRange = 15;
    private const int weakness1LevelThreeRange = 20;
    private const int weakness1LevelTwoNeededExperience = 2; //needed to level up upgrade once
    private const int weakness1LevelThreeNeededExperience = 5; //needed to level up upgrade a second time
    private const int weakness1LevelTwoStaminaBoost = 10;
    private const int weakness1LevelThreeStaminaBoost = 20;
    private int weakness1UpgradeLevel; //current level of weakness 1 upgrade
    private int weakness1UpgradeCurrentExperience; //current experience of weakness 1 upgrade
    private bool weakness1Equipped; //set to true when weakness1 is equipped
      //varaibles associated with the "Reveal Weakness 2" upgrade
    private const int weakness2LevelOneStaminaPenalty = 30; //amount of stamina subtracted from max while equipped
    private const int weakness2LevelTwoStaminaPenalty = 25;
    private const int weakness2LevelThreeStaminaPenalty = 15;    
    private const int weakness2LevelOneRange = 10; //how close the player must be to reveal weakness
    private const int weakness2LevelTwoRange = 15;
    private const int weakness2LevelThreeRange = 20;
    private const int weakness2LevelTwoNeededExperience = 2; //needed to level up upgrade once
    private const int weakness2LevelThreeNeededExperience = 5; //needed to level up upgrade a second time
    private const int weakness2LevelTwoStaminaBoost = 10;
    private const int weakness2LevelThreeStaminaBoost = 20;
    private int weakness2UpgradeLevel; //current level of weakness 2 upgrade
    private int weakness2UpgradeCurrentExperience; //current experience of weakness 2 upgrade
    private bool weakness2Equipped; //set to true when weakness2 is equipped

    private int upgradeSlotNumber; //number of specific upgrade slot



    void Start ()
    {
        StoreUpgradePanels(); //stores all panels in an array
        StoreUpgradePanelButtons(); //stores upgrade buttons in an array
        StoreUpgradeImages(); //stores upgrade images in an array
        StoreUpgradeNameTexts(); //stores upgrade name texts in an array
        StoreUpgradeLevelTexts(); //stores upgrade level texts in an array
        StoreUpgradeEquippedImages(); //stores upgrade equipped images in an array
	}

    void Update()
    {
        if (cantEquipLength > 0.0f) //checks if counter is over zero
        {
            DecrementCantEquipPromptCounter(); //keep reducing the counter
        }
        else
        {
            invTextMessage.text = ""; //removes message
        }
    }

    /**************************************************************************
   Function: GetInventoryStaminaBar

Description: This function returns the slider representing the player's stamina
             that's displayed in the upgrade menu.

      Input: none

     Output: Returns the slider representing the player's stamina bar in the 
             upgrade menu.
    **************************************************************************/
    public Slider GetInventoryStaminaBar()
    {
        return staminaBar;
    }

    /**************************************************************************
   Function: GetInventoryStaminaBarRectTransform

Description: This function returns the RectTransform component of the stamina
             bar that's displayed in the upgrade menu.

      Input: none

     Output: Returns the RectTransform of the upgrade menu's stamina bar.
    **************************************************************************/
    public RectTransform GetInventoryStaminaBarRectTransform()
    {
        return staminaRectTransform;
    }

    /**************************************************************************
   Function: GetInventoryStaminaCapMarker

Description: This function returns the slider on top of the upgrade menu's
             stamina bar that's used to display the current max stamina

      Input: none

     Output: Returns the slider of the upgrade menu's stamina cap marker
    **************************************************************************/
    public Slider GetInventoryStaminaCapMarker()
    {
        return staminaCapMarker;
    }

    /**************************************************************************
   Function: GetInventoryStaminaBarCapRectTransform

Description: This function returns the RectTransform component of the invisible
             slider that's placed on top of the stamina slider in the upgrade 
             menu.

      Input: none

     Output: Returns the RectTransform of the upgrade menu's stamina cap
             marker.
    **************************************************************************/
    public RectTransform GetInventoryStaminaBarCapRectTransform()
    {
        return staminaCapRectTransform;
    }

    /**************************************************************************
   Function: EnableStaminaMarker

Description: Given a bool, this function either displays or hides the handle
             that's used to mark the player's current max stamina.

      Input: enable - bool that hides or enables the invisible slider's
                      handle

     Output: none
    **************************************************************************/
    public void EnableStaminaMarker(bool enable)
    {
        staminaCapMarkerHandle.SetActive(enable);
    }

    /**************************************************************************
   Function: GetUpgradePanelButtons

Description: This function returns an array that contains all 18 upgrade 
             buttons in the upgrade sub tab.

      Input: none

     Output: Returns the array of upgrade buttons.
    **************************************************************************/
    public Button[] GetUpgradePanelButtons()
    {
        return upgradePanelButtons;
    }

    /**************************************************************************
   Function: SetUpgradeSlotNumber

Description: Given an integer, this function sets the active slot as the given
             integer.

      Input: upgradeNumber - integer used to set the current chosen upgrade 
                             slot

     Output: none
    **************************************************************************/
    public void SetUpgradeSlotNumber(int upgradeNumber)
    {
        upgradeSlotNumber = upgradeNumber;
    }

    /**************************************************************************
   Function: CheckForEmptyPanel

Description: This function checks each upgrade panel in order and sets the
             upgradeSlotNumber to the first empty slot found.

      Input: none

     Output: Returns true if an empty upgrade panel was found, otherwise, 
             returns false.
    **************************************************************************/
    public bool CheckForEmptyPanel()
    {
        upgradeSlotNumber = -1; //resets the variable
          //loops through all 18 panels
        for (int i = 0; i < panelCount; i++)
        {
            if (upgradeImages[i].sprite == null)
            {
                upgradeSlotNumber = i;
                break;
            }
        }
          //true if an empty upgrade slot was found
        return (upgradeSlotNumber != -1); 
    }

    /**************************************************************************
   Function: AddToEmptyPanel

Description: Given a string, this function adds the specified upgrade to an 
             empty upgrade slot.

      Input: upgrade - string of the upgrade to add to an upgrade slot

     Output: none
    **************************************************************************/
    public void AddToEmptyPanel(string upgradeName)
    {
          //checks if upgrade button hasn't been revealed yet
        if(!UpgradeButtonIsVisible())
        {
            RevealupgradeButton();
        }

        switch (upgradeName)
        {
            case "Weakness1":
                  //makes upgrade slot visible
                upgradePanels[upgradeSlotNumber].SetActive(true);
                  //image of upgrade
                upgradeImages[upgradeSlotNumber].sprite = weakness1Image;
                  //makes image visible
                upgradeImages[upgradeSlotNumber].color = visible;
                  //name of upgrade
                upgradeNameTexts[upgradeSlotNumber].text = "Reveal Weakness 1";
                break;
            case "Weakness2":
                upgradePanels[upgradeSlotNumber].SetActive(true);
                upgradeImages[upgradeSlotNumber].sprite = weakness1Image; //TODO: replace with weakness2 image
                upgradeImages[upgradeSlotNumber].color = visible;
                upgradeNameTexts[upgradeSlotNumber].text = "Reveal Weakness 2";
                break;
            //case "test3":
            //    upgradePanels[upgradeSlotNumber].SetActive(true);
            //    upgradeImages[upgradeSlotNumber].sprite = ;
            //    upgradeImages[upgradeSlotNumber].color = visible;
            //    upgradeNameTexts[upgradeSlotNumber].text = "";
            //    break;
            //case "test4":
            //    upgradePanels[upgradeSlotNumber].SetActive(true);
            //    upgradeImages[upgradeSlotNumber].sprite = ;
            //    upgradeImages[upgradeSlotNumber].color = visible;
            //    upgradeNameTexts[upgradeSlotNumber].text = "";
            //    break;
            //case "test5":
            //    upgradePanels[upgradeSlotNumber].SetActive(true);
            //    upgradeImages[upgradeSlotNumber].sprite = ;
            //    upgradeImages[upgradeSlotNumber].color = visible;
            //    upgradeNameTexts[upgradeSlotNumber].text = "";
            //    break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

          //sets upgrade to starting level and experience
        SetUpgradeToDefaultLevelAndExp(upgradeNameTexts[upgradeSlotNumber].text);
        SetUpgradeSlotLevel();
    }

    /**************************************************************************
   Function: RemoveUpgradeStatPanelDetails

Description: This function sets all the strings in the stat panel to empty.

      Input: none

     Output: none
    **************************************************************************/
    public void RemoveUpgradeStatPanelDetails()
    {
        upgradePanelNameText.text = "";
        upgradePanelLevelAmountText.text = "";
        upgradePanelDescriptionDetailText.text = "";
        upgradePanelBurdenAmountText.text = "";
        upgradePanelRangeAmountText.text = "";
        upgradePanelUseTypeDetailText.text = "";
        upgradePanelEffectDetailText.text = "";
    }

    /**************************************************************************
   Function: DisplayUpgradeStatDetails

Description: This function retrieves the upgrade name of the currently selected
             upgrade slot and uses it to display that upgrade's stats in the
             upgrade stat panel.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayUpgradeStatPanelDetails()
    {
        string upgradeName = upgradeNameTexts[upgradeSlotNumber].text;

        switch (upgradeName)
        {
            case "Reveal Weakness 1":
                upgradePanelNameText.text = upgradeName; //assigns upgrade name
                SetUpgradePanelLevelText(upgradeName); //sets level text and color
                upgradePanelDescriptionDetailText.text = weakness1Description;
                  //gets burden amount and sets text to appropriate color
                upgradePanelBurdenAmountText.text = GetBurdenAmount(GetSingleUpgradeStaminaPenalty(upgradeName));
                  //gets current range
                upgradePanelRangeAmountText.text = GetWeakness1Range().ToString() + " Meters";
                  //retrieves method of use (automatically or manually)
                upgradePanelUseTypeDetailText.text = GetUpgradeUseType(upgradeName);
                  //displays what the upgrade does
                upgradePanelEffectDetailText.text = weakness1Effect;
                break;
            case "Reveal Weakness 2":
                upgradePanelNameText.text = upgradeName;
                SetUpgradePanelLevelText(upgradeName);
                upgradePanelDescriptionDetailText.text = weakness2Description;
                upgradePanelBurdenAmountText.text = GetBurdenAmount(GetSingleUpgradeStaminaPenalty(upgradeName));
                upgradePanelRangeAmountText.text = GetWeakness2Range().ToString() + " Meters";
                upgradePanelUseTypeDetailText.text = GetUpgradeUseType(upgradeName);
                upgradePanelEffectDetailText.text = weakness2Effect;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
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

Description: This function retrieves the upgrade name of the currently selected
             upgrade slot and uses it to display that upgrade's image and
             details in the detail panel.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayDetails()
    {
        string itemName = upgradeNameTexts[upgradeSlotNumber].text;

        switch (itemName)
        {
            case "Reveal Weakness 1":
                invDetailImage.color = visible;
                invDetailImage.sprite = weakness1Image;
                invDetailText.text = weakness1Description;
                invDetailNameText.text = itemName;
                break;
            case "Reveal Weakness 2":
                invDetailImage.color = visible;
                invDetailImage.sprite = weakness1Image; //TODO: replace with image 2
                invDetailText.text = weakness2Description;
                invDetailNameText.text = itemName;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
    }

    /**************************************************************************
   Function: ClearUpgradeSlot

Description: This function clears the specified upgrade slot of all text and
             its image.

      Input: none

     Output: none
    **************************************************************************/
    public void ClearUpgradeSlot()
    {
        upgradeImages[upgradeSlotNumber].sprite = null; //removes the upgrade image       
        upgradeImages[upgradeSlotNumber].color = invisible; //makes the upgrade slot black         
        upgradeNameTexts[upgradeSlotNumber].text = ""; //removes the name text        
    }

    /**************************************************************************
   Function: CheckIfUpgradeCanBeEquipped

Description: This function gets the player's max stamina and subtracts the
             current penalty plus the selected upgrade's penalty. If the
             result is greater than or equip to the smallest max stamina
             allowed, the upgrade is equipped. Otherwise, a message is
             displayed telling the player they can't equip it.

      Input: none

     Output: none
    **************************************************************************/
    public void CheckIfUpgradeCanBeEquipped()
    {
        if((hudManager.GetMaxStamina() - GetStaminaPenalty()  - GetCurrentUpgradeStaminaPenalty()) >= 
            hudManager.GetSmallestMaxStaminaAllowed())
        {
            EquipUpgrade(); //upgrade can be equipped, so equip it
        }
        else
        {
            DisplayCantEquipUpgradeText(); //can't be equipped, display message
        }
    }

    /**************************************************************************
   Function: CheckIfUpgradeCanBeEquipped

Description: This function retrieves the name of the currently selected
             upgrade slot and uses that to check the level of that upgrade.
             Then this function returns the penalty of this upgrade's current
             level.

      Input: none

     Output: Returns stamina penalty of currently selected upgrade.
    **************************************************************************/
    public float GetCurrentUpgradeStaminaPenalty()
    {
        string upgradeName = upgradeNameTexts[upgradeSlotNumber].text;

        switch (upgradeName)
        {
            case "Reveal Weakness 1":
                switch(weakness1UpgradeLevel)
                {
                    case 1:
                        return weakness1LevelOneStaminaPenalty;
                    case 2:
                        return weakness1LevelTwoStaminaPenalty;
                    case 3:
                        return weakness1LevelThreeStaminaPenalty;
                    default:
                        //Debug.Log("This shouldn't happen!");
                        break;
                }
                break;
            case "Reveal Weakness 2":
                switch (weakness2UpgradeLevel)
                {
                    case 1:
                        return weakness2LevelOneStaminaPenalty;
                    case 2:
                        return weakness2LevelTwoStaminaPenalty;
                    case 3:
                        return weakness2LevelThreeStaminaPenalty;
                    default:
                        //Debug.Log("This shouldn't happen!");
                        break;
                }
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        return 0; //This should never happen
    }

    /**************************************************************************
   Function: EquipUpgrade

Description: This function retrieves the name of the currently clicked slot
             and sets the bool of that slot's upgrade equip bool to true. Then
             this function enabled the equipped red border around the current
             upgrade slot.

      Input: none 

     Output: none
    **************************************************************************/
    public void EquipUpgrade()
    {
        string upgradeName = upgradeNameTexts[upgradeSlotNumber].text;

        if(upgradeName != "")
        {
              //enable equipped image
            upgradeEquippedImages[upgradeSlotNumber].gameObject.SetActive(true);
        }

        switch (upgradeName)
        {
            case "Reveal Weakness 1":
                weakness1Equipped = true;
                break;
            case "Reveal Weakness 2":
                weakness2Equipped = true;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        hudManager.RegenerateStamina(); //updates what value stamina currently is

        uiManager.DeselectUpgradeSlot();
        RemoveUpgradeStatPanelDetails();
    }

    /**************************************************************************
   Function: EquipWeakness1

Description: This function sets the weakness1's equip bool to true.

      Input: none 

     Output: none
    **************************************************************************/
    public void EquipWeakness1()
    {
        weakness1Equipped = true;
          //item slot number is still the slot this upgraded was added because
          //it was added to an empty slot then this function was immediately
          //called afterwards
        EquipUpgrade();

        survivalNoteManager.RevealNote(1); //reveals 2nd note
    }

    /**************************************************************************
   Function: UnequipUpgrade

Description: This function retrieves the name of the currently clicked slot
             and sets the bool of that slot's upgrade equip bool to false.

      Input: none 

     Output: none
    **************************************************************************/
    public void UnequipUpgrade()
    {
        string upgradeName = upgradeNameTexts[upgradeSlotNumber].text;
          //hide equipped image
        upgradeEquippedImages[upgradeSlotNumber].gameObject.SetActive(false);

        switch (upgradeName)
        {
            case "Reveal Weakness 1":
                weakness1Equipped = false;
                break;
            case "Reveal Weakness 2":
                weakness2Equipped = false;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        hudManager.RegenerateStamina();

        uiManager.DeselectUpgradeSlot();
        RemoveUpgradeStatPanelDetails();
    }

    /**************************************************************************
   Function: GetWeakness1UpgradeEquippedStatus

Description: This function returns the bool that represents whether or not the
             Reveal Weakness 1 upgrade is equipped.

      Input: none 

     Output: Returns true if Reveal Weakness 1 is equipped, otherwise, returns
             false.
    **************************************************************************/
    public bool GetWeakness1UpgradeEquippedStatus()
    {
        return weakness1Equipped;
    }

    /**************************************************************************
   Function: GetWeakness2UpgradeEquippedStatus

Description: This function returns the bool that represents whether or not the
             Reveal Weakness 2 upgrade is equipped.

      Input: none 

     Output: Returns true if Reveal Weakness 2 is equipped, otherwise, returns
             false.
    **************************************************************************/
    public bool GetWeakness2UpgradeEquippedStatus()
    {
        return weakness2Equipped;
    }

    /**************************************************************************
   Function: AddToUpgradeExperience

Description: Given a string and an integer, this function adds the integer to
             the specified upgrade's current experience. Then the
             CheckForUpgradeLevelUp function is called on the same upgrade.

      Input: upgradeName - string of the upgrade to add experience to
             experience  - integer to add to the upgrade's current experience

     Output: none
    **************************************************************************/
    public void AddToUpgradeExperience(string upgradeName, int experience)
    {
        switch(upgradeName)
        {
            case "Reveal Weakness 1":
                weakness1UpgradeCurrentExperience += experience;
                break;
            case "Reveal Weakness 2":
                weakness2UpgradeCurrentExperience += experience;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        CheckForUpgradeLevelUp(upgradeName);
    }

    /**************************************************************************
   Function: GetStaminaPenalty

Description: This function checks to see which upgrades are equipped and what
             levels those equipped upgrades are at. Then the appropriate
             penalties are added together and returned.

      Input: none

     Output: Returns the total amount of stamina to prevent the player from
             regenerating.
    **************************************************************************/
    public int GetStaminaPenalty()
    {
        int staminaPenalty = 0;

          //TODO: check every upgrade bool and add its upgrade's current level's penalty to variable to return
        if(weakness1Equipped)
        {
            switch(weakness1UpgradeLevel)
            {
                case 1:
                    staminaPenalty += weakness1LevelOneStaminaPenalty;
                    break;
                case 2:
                    staminaPenalty += weakness1LevelTwoStaminaPenalty;
                    break;
                case 3:
                    staminaPenalty += weakness1LevelThreeStaminaPenalty;
                    break;
                default:
                    //Debug.Log("This shouldn't happen!");
                    break;
            }
        }

        if(weakness2Equipped)
        {
            switch(weakness2UpgradeLevel)
            {
                case 1:
                    staminaPenalty += weakness2LevelOneStaminaPenalty;
                    break;
                case 2:
                    staminaPenalty += weakness2LevelTwoStaminaPenalty;
                    break;
                case 3:
                    staminaPenalty += weakness2LevelThreeStaminaPenalty;
                    break;
                default:
                    //Debug.Log("This shouldn't happen!");
                    break;
            }
        }

        return staminaPenalty;
    }

    /**************************************************************************
   Function: AnyUpgradeIsEquipped

Description: This function returns an evaluation of every upgrade's equipped
             bool.

      Input: none

     Output: Returns true if any upgrade is equipped, otherwise, returns false.
    **************************************************************************/
    public bool AnyUpgradeIsEquipped()
    {
          //TODO: check every upgrade equipped bool
        return weakness1Equipped || weakness2Equipped;// || weakness3Equipped
    }

    /**************************************************************************
   Function: GetWeakness1Range

Description: This function checks Reveal Weakness 1's current level and returns 
             the integer representing its range at that level.

      Input: none

     Output: Returns the current range of the Reveal Weakness 1 upgrade.
    **************************************************************************/
    public float GetWeakness1Range()
    {
        switch(weakness1UpgradeLevel)
        {
            case 1:
                return weakness1LevelOneRange;
            case 2:
                return weakness1LevelTwoRange;
            case 3:
                return weakness1LevelThreeRange;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        return 0; //this should never happen
    }

    /**************************************************************************
   Function: GetWeakness2Range

Description: This function checks Reveal Weakness 2's current level and returns 
             the integer representing its range at that level.

      Input: none

     Output: Returns the current range of the Reveal Weakness 2 upgrade.
    **************************************************************************/
    public float GetWeakness2Range()
    {
        switch (weakness2UpgradeLevel)
        {
            case 1:
                return weakness2LevelOneRange;
            case 2:
                return weakness2LevelTwoRange;
            case 3:
                return weakness2LevelThreeRange;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        return 0; //this should never happen
    }

    /**************************************************************************
   Function: DisplayCantEquipUpgradeText

Description: This function tells the player they can't equip the selected
             upgrade.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayCantEquipUpgradeText()
    {
        invTextMessage.text = "Equipping this would place too big a burden on you.\nUnequip something first.";
        ResetCantEquipPromptCounter();
    }

    /**************************************************************************
   Function: RevealUpgradeButton

Description: This function enables the upgrade button image and sets its text
             to "Upgrades".

      Input: none

     Output: none
    **************************************************************************/
    public void RevealupgradeButton()
    {
        upgradeButtonImage.enabled = true;
        upgradeButtonText.text = "Upgrades";
    }

    /**************************************************************************
   Function: UpgradeButtonIsVisible

Description: This function returns a bool that represents whether or not the
             upgrade button image is enabled.

      Input: none

     Output: Returns true if the upgrade button image is enabled, otherwise,
             returns false.
    **************************************************************************/
    private bool UpgradeButtonIsVisible()
    {
        return upgradeButtonImage.enabled;
    }

    /**************************************************************************
   Function: GetBurdenAmount

Description: Given an integer, this function uses it to determine what the
             burden text will be and what color the font will be.

      Input: staminaBurden - integer used to determine text and font color

     Output: none
    **************************************************************************/
    private string GetBurdenAmount(int staminaBurden)
    {
        if(staminaBurden >= veryHeavyThreshold)
        {
            upgradePanelBurdenAmountText.color = veryHeavyColor;
            return "Very Heavy";
        }
        else if(staminaBurden >= heavyThreshold)
        {
            upgradePanelBurdenAmountText.color = heavyColor;
            return "Heavy";
        }
        else if(staminaBurden >= moderateThreshold)
        {
            upgradePanelBurdenAmountText.color = moderateColor;
            return "Moderate";
        }
        else if(staminaBurden >= lightThreshold)
        {
            upgradePanelBurdenAmountText.color = lightColor;
            return "Light";
        }
        else
        {
            upgradePanelBurdenAmountText.color = veryLightColor;
            return "Very Light";
        }
    }

    /**************************************************************************
   Function: GetSingleUpgradeStaminaPenalty

Description: Given a string, this function returns the current stamina penalty
             of the specified upgrade.

      Input: upgradeName - string of upgrade to get current stamina penalty of

     Output: Returns the specified upgrade's current stamina penalty.
    **************************************************************************/
    private int GetSingleUpgradeStaminaPenalty(string upgradeName)
    {
        switch(upgradeName)
        {
            case "Reveal Weakness 1":
                if(weakness1UpgradeLevel == maxLevel)
                {
                    return weakness1LevelThreeStaminaPenalty;
                }
                else if(weakness1UpgradeLevel == levelTwo)
                {
                    return weakness1LevelTwoStaminaPenalty;
                }
                else
                {
                    return weakness1LevelOneStaminaPenalty;
                }
            case "Reveal Weakness 2":
                if (weakness2UpgradeLevel == maxLevel)
                {
                    return weakness2LevelThreeStaminaPenalty;
                }
                else if (weakness2UpgradeLevel == levelTwo)
                {
                    return weakness2LevelTwoStaminaPenalty;
                }
                else
                {
                    return weakness2LevelOneStaminaPenalty;
                }
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        return -1; //this should never happen
    }

    /**************************************************************************
   Function: GetUpgradeUseType

Description: Given a string, this function returns one of two strings based
             on the specified upgrade.

      Input: upgradeName - string of upgrade to get evaluate

     Output: Returns the specified upgrade's method of use.
    **************************************************************************/
    private string GetUpgradeUseType(string upgradeName)
    {
        switch(upgradeName)
        {
            case "Reveal Weakness 1":
            case "Reveal Weakness 2":
                return "Automatic";
            case "":
                return "Manual";
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        return "Error!"; //this should never happen
    }

    /**************************************************************************
   Function: ResetCantEquipPromptCounter

Description: This function sets the cantEquipLength variable to its default
             value.

      Input: none

     Output: none
    **************************************************************************/
        private void ResetCantEquipPromptCounter()
    {
        cantEquipLength = defaultCantEquipLength;
    }

    /**************************************************************************
   Function: DecrementCantEquipPromptCounter

Description: This function decrements the cantequipLength variable every frame.

      Input: none

     Output: none
    **************************************************************************/
    private void DecrementCantEquipPromptCounter()
    {
        cantEquipLength -= delta;
    }

    /**************************************************************************
   Function: CheckForUpgradeLevelUp

Description: This a string, this function checks if the specified upgrade is
             at the next level but hasn't been leveled up yet. If true, the
             upgrade is leveled up and stamina is upgraded.

      Input: upgradeName - string of upgrade to check

     Output: none
    **************************************************************************/
    private void CheckForUpgradeLevelUp(string upgradeName)
    {
        switch(upgradeName)
        {
            case "Reveal Weakness 1":
                if(weakness1UpgradeCurrentExperience >= weakness1LevelTwoNeededExperience &&
                   weakness1UpgradeLevel == levelOne)
                {
                    ++weakness1UpgradeLevel; //upgrade is now level 2
                    FindUpgrade(upgradeName); //find slot of this upgrade
                    SetUpgradeSlotLevel(); //set its current level text and color

                    hudManager.UpgradeStamina(weakness1LevelTwoStaminaBoost);
                    hudManager.DisplayUpgradeLevelUpText();
                }
                else if(weakness1UpgradeCurrentExperience >= weakness1LevelThreeNeededExperience &&
                        weakness1UpgradeLevel == levelTwo)
                {
                    ++weakness1UpgradeLevel; //upgrade is now level 3
                    FindUpgrade(upgradeName); //find slot of this upgrade
                    SetUpgradeSlotLevel(); //set its current level text and color

                    hudManager.UpgradeStamina(weakness1LevelThreeStaminaBoost);
                    hudManager.DisplayUpgradeLevelUpText();
                }
                break;
            case "Reveal Weakness 2":
                if (weakness2UpgradeCurrentExperience >= weakness2LevelTwoNeededExperience &&
                    weakness2UpgradeLevel == levelOne)
                {
                    ++weakness2UpgradeLevel; //upgrade is now level 2
                    FindUpgrade(upgradeName); //find slot of this upgrade
                    SetUpgradeSlotLevel(); //set its current level text and color

                    hudManager.UpgradeStamina(weakness2LevelTwoStaminaBoost);
                    hudManager.DisplayUpgradeLevelUpText();
                }
                else if (weakness2UpgradeCurrentExperience >= weakness2LevelThreeNeededExperience &&
                         weakness2UpgradeLevel == levelTwo)
                {
                    ++weakness2UpgradeLevel; //upgrade is now level 3
                    FindUpgrade(upgradeName); //find slot of this upgrade
                    SetUpgradeSlotLevel(); //set its current level text and color

                    hudManager.UpgradeStamina(weakness2LevelThreeStaminaBoost);
                    hudManager.DisplayUpgradeLevelUpText();
                }
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
    }

    /**************************************************************************
   Function: SetUpgradeToDefaultLevelAndExp

Description: Given a string, this function sets the given upgrade to its
             starting level and experience.

      Input: upgradeName - string of upgrade to check

     Output: none
    **************************************************************************/
    private void SetUpgradeToDefaultLevelAndExp(string upgradeName)
    {
        switch(upgradeName)
        {
            case "Reveal Weakness 1":
                weakness1UpgradeLevel = startingLevel; //starting level
                weakness1UpgradeCurrentExperience = 0; //starting experience
                break;
            case "Reveal Weakness 2":
                weakness2UpgradeLevel = startingLevel;
                weakness2UpgradeCurrentExperience = 0;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        hudManager.RegenerateStamina(); //updates stamina bars
    }

    /**************************************************************************
   Function: EnableUpgradeStaminaMarker

Description: Given a bool, this function either hides or enables the handle
             of the slider placed on top of the upgrade menu's stamina bar
             used to signify current max stamina.

      Input: enable - bool used to hide or enable the stamina cap slider's
                      handle

     Output: none
    **************************************************************************/
    public void EnableUpgradeStaminaMarker(bool enable)
    {
        staminaCapMarkerHandle.SetActive(enable);
    }

    /**************************************************************************
   Function: FindUpgrade

Description: Given a string, this function searches all upgrades slots for
             the specified upgrade. If it's found, the slot number is set
             to the slot the upgrade was found. Then an expression is return
             to inform the function caller if the upgrade was found.

      Input: upgradeName - string of upgrade to search for

     Output: Returns true if upgrade was found, otherwise, returns false.
    **************************************************************************/
    private bool FindUpgrade(string upgradeName)
    {
        upgradeSlotNumber = -1; //reset slot number

        for (int i = 0; i < panelCount; i++)
        {
            if(upgradeNameTexts[i].text == upgradeName)
            {
                upgradeSlotNumber = i;
                break;
            }
        }

        return upgradeSlotNumber != -1; //true if upgrade was found
    }

     /**************************************************************************
   Function: SetUpgradeSlotLevel

Description: This function checks the current slot's upgrade and retrieves its
             current level. It displays the level and sets the color to a
             default color if it's not max, otherwise it's set to the max
             level color.

      Input: none

     Output: none
    **************************************************************************/   
    private void SetUpgradeSlotLevel()
    {
          //gets current upgrade's name
        string currentUpgrade = upgradeNameTexts[upgradeSlotNumber].text;
        int currentLevel = 0; //will contain current upgrade's level

        switch(currentUpgrade)
        {
            case "Reveal Weakness 1":
                currentLevel = weakness1UpgradeLevel;
                break;
            case "Reveal Weakness 2":
                currentLevel = weakness2UpgradeLevel;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }


        if(currentLevel == maxLevel) //sets color to yellow is max, otherwise black
        {
            upgradeLevelTexts[upgradeSlotNumber].color = maxLevelColor;
            upgradeLevelTexts[upgradeSlotNumber].text = "Lv. MAX";
        }
        else
        {
            upgradeLevelTexts[upgradeSlotNumber].color = defaultLevelColor;
              //stores current level as a string
            upgradeLevelTexts[upgradeSlotNumber].text = "Lv. " + currentLevel.ToString();
        }
    }

    /**************************************************************************
   Function: SetUpgradePaneLevelText

Description: Given a string, this function check the specified upgrade's
             current level and sets the level string to that level. If the
             upgrade is at max level, the font color is changed.

      Input: currentUpgrade - string of upgrade to get current level of

     Output: none
    **************************************************************************/
    private void SetUpgradePanelLevelText(string currentUpgrade)
    {
        int currentLevel = 0; //will contain current upgrade's level

        switch (currentUpgrade)
        {
            case "Reveal Weakness 1":
                currentLevel = weakness1UpgradeLevel;
                break;
            case "Reveal Weakness 2":
                currentLevel = weakness2UpgradeLevel;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }


        if (currentLevel == maxLevel) //sets color to yellow is max, otherwise black
        {
            upgradePanelLevelAmountText.color = maxLevelColor;
            upgradePanelLevelAmountText.text = "MAX";
        }
        else
        {
            upgradePanelLevelAmountText.color = defaultPanelLevelColor;
              //stores current level as a string
            upgradePanelLevelAmountText.text = currentLevel.ToString();
        }
    }

    /**************************************************************************
   Function: StoreUpgradePanelButtons

Description: This function stores all upgrade slot buttons into a single array 
             for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreUpgradePanelButtons()
    {
        upgradePanelButtons[0] = upgradePanelButton1;
        upgradePanelButtons[1] = upgradePanelButton2;
        upgradePanelButtons[2] = upgradePanelButton3;
        upgradePanelButtons[3] = upgradePanelButton4;
        upgradePanelButtons[4] = upgradePanelButton5;
        upgradePanelButtons[5] = upgradePanelButton6;
        upgradePanelButtons[6] = upgradePanelButton7;
        upgradePanelButtons[7] = upgradePanelButton8;
        upgradePanelButtons[8] = upgradePanelButton9;
        upgradePanelButtons[9] = upgradePanelButton10;
        upgradePanelButtons[10] = upgradePanelButton11;
        upgradePanelButtons[11] = upgradePanelButton12;
        upgradePanelButtons[12] = upgradePanelButton13;
        upgradePanelButtons[13] = upgradePanelButton14;
        upgradePanelButtons[14] = upgradePanelButton15;
        upgradePanelButtons[15] = upgradePanelButton16;
        upgradePanelButtons[16] = upgradePanelButton17;
        upgradePanelButtons[17] = upgradePanelButton18;
    }

    /**************************************************************************
   Function: StoreUpgradeImages

Description: This function stores all upgrade slot images into a single array 
             for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreUpgradeImages()
    {
        upgradeImages[0] = upgradeImage1;
        upgradeImages[1] = upgradeImage2;
        upgradeImages[2] = upgradeImage3;
        upgradeImages[3] = upgradeImage4;
        upgradeImages[4] = upgradeImage5;
        upgradeImages[5] = upgradeImage6;
        upgradeImages[6] = upgradeImage7;
        upgradeImages[7] = upgradeImage8;
        upgradeImages[8] = upgradeImage9;
        upgradeImages[9] = upgradeImage10;
        upgradeImages[10] = upgradeImage11;
        upgradeImages[11] = upgradeImage12;
        upgradeImages[12] = upgradeImage13;
        upgradeImages[13] = upgradeImage14;
        upgradeImages[14] = upgradeImage15;
        upgradeImages[15] = upgradeImage16;
        upgradeImages[16] = upgradeImage17;
        upgradeImages[17] = upgradeImage18;
    }

    /**************************************************************************
   Function: StoreUpgradeNameTexts

Description: This function stores all upgrade slot name text objects into a 
             single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreUpgradeNameTexts()
    {
        upgradeNameTexts[0] = upgradeNameText1;
        upgradeNameTexts[1] = upgradeNameText2;
        upgradeNameTexts[2] = upgradeNameText3;
        upgradeNameTexts[3] = upgradeNameText4;
        upgradeNameTexts[4] = upgradeNameText5;
        upgradeNameTexts[5] = upgradeNameText6;
        upgradeNameTexts[6] = upgradeNameText7;
        upgradeNameTexts[7] = upgradeNameText8;
        upgradeNameTexts[8] = upgradeNameText9;
        upgradeNameTexts[9] = upgradeNameText10;
        upgradeNameTexts[11] = upgradeNameText12;
        upgradeNameTexts[10] = upgradeNameText11;
        upgradeNameTexts[12] = upgradeNameText13;
        upgradeNameTexts[13] = upgradeNameText14;
        upgradeNameTexts[14] = upgradeNameText15;
        upgradeNameTexts[15] = upgradeNameText16;
        upgradeNameTexts[16] = upgradeNameText17;
        upgradeNameTexts[17] = upgradeNameText18;
    }

    /**************************************************************************
   Function: StoreUpgradeLevelTexts

Description: This function stores all upgrade slot level text objects into a 
             single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreUpgradeLevelTexts()
    {
        upgradeLevelTexts[0] = upgradeLevelText1;
        upgradeLevelTexts[1] = upgradeLevelText2;
        upgradeLevelTexts[2] = upgradeLevelText3;
        upgradeLevelTexts[3] = upgradeLevelText4;
        upgradeLevelTexts[4] = upgradeLevelText5;
        upgradeLevelTexts[5] = upgradeLevelText6;
        upgradeLevelTexts[6] = upgradeLevelText7;
        upgradeLevelTexts[7] = upgradeLevelText8;
        upgradeLevelTexts[8] = upgradeLevelText9;
        upgradeLevelTexts[9] = upgradeLevelText10;
        upgradeLevelTexts[11] = upgradeLevelText12;
        upgradeLevelTexts[10] = upgradeLevelText11;
        upgradeLevelTexts[12] = upgradeLevelText13;
        upgradeLevelTexts[13] = upgradeLevelText14;
        upgradeLevelTexts[14] = upgradeLevelText15;
        upgradeLevelTexts[15] = upgradeLevelText16;
        upgradeLevelTexts[16] = upgradeLevelText17;
        upgradeLevelTexts[17] = upgradeLevelText18;
    }

    /**************************************************************************
   Function: StoreUpgradeEquippedImages

Description: This function stores all upgrade slot equipped images into a 
             single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreUpgradeEquippedImages()
    {
        upgradeEquippedImages[0] = upgradeEquippedImage1;
        upgradeEquippedImages[1] = upgradeEquippedImage2;
        upgradeEquippedImages[2] = upgradeEquippedImage3;
        upgradeEquippedImages[3] = upgradeEquippedImage4;
        upgradeEquippedImages[4] = upgradeEquippedImage5;
        upgradeEquippedImages[5] = upgradeEquippedImage6;
        upgradeEquippedImages[6] = upgradeEquippedImage7;
        upgradeEquippedImages[7] = upgradeEquippedImage8;
        upgradeEquippedImages[8] = upgradeEquippedImage9;
        upgradeEquippedImages[9] = upgradeEquippedImage10;
        upgradeEquippedImages[10] = upgradeEquippedImage11;
        upgradeEquippedImages[11] = upgradeEquippedImage12;
        upgradeEquippedImages[12] = upgradeEquippedImage13;
        upgradeEquippedImages[13] = upgradeEquippedImage14;
        upgradeEquippedImages[14] = upgradeEquippedImage15;
        upgradeEquippedImages[15] = upgradeEquippedImage16;
        upgradeEquippedImages[16] = upgradeEquippedImage17;
        upgradeEquippedImages[17] = upgradeEquippedImage18;
    }

    /**************************************************************************
   Function: StoreUpgradePanels

Description: This function stores all upgrade panels the buttons are children
             of into a single array for eacy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreUpgradePanels()
    {
        upgradePanels[0] = upgradePanel1;
        upgradePanels[1] = upgradePanel2;
        upgradePanels[2] = upgradePanel3;
        upgradePanels[3] = upgradePanel4;
        upgradePanels[4] = upgradePanel5;
        upgradePanels[5] = upgradePanel6;
        upgradePanels[6] = upgradePanel7;
        upgradePanels[7] = upgradePanel8;
        upgradePanels[8] = upgradePanel9;
        upgradePanels[9] = upgradePanel10;
        upgradePanels[10] = upgradePanel11;
        upgradePanels[11] = upgradePanel12;
        upgradePanels[12] = upgradePanel13;
        upgradePanels[13] = upgradePanel14;
        upgradePanels[14] = upgradePanel15;
        upgradePanels[15] = upgradePanel16;
        upgradePanels[16] = upgradePanel17;
        upgradePanels[17] = upgradePanel18;
    }
}
