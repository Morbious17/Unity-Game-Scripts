/******************************************************************************
  File Name: ItemManager.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that manage the player's usable
             items.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController = null;
      //used to communicate between this script and hudManager
    [SerializeField] private HUDManager hudManager = null;
    [SerializeField] private UIManager uiManager = null;
      //used to display the player's health in item menu
    [SerializeField] private Slider healthBar = null;
      //used to position health bar after it increases in size
    [SerializeField] private RectTransform healthRectTransform = null;
      //used to show detailed information on examined item
    [SerializeField] private Image invDetailImage = null;
    [SerializeField] private Text invDetailText = null;
    [SerializeField] private Text invDetailNameText = null;
      //displays messages when the player tries to use items but can't
    [SerializeField] private Text invTextMessage = null;
      //used to communicate with the player's guns
    [SerializeField] private Pistol pistol = null;
    [SerializeField] private Shotgun shotgun = null;
    [SerializeField] private Rifle rifle = null;
      //images used to populate item slots
    [SerializeField] private Sprite largeFirstAidKitImage = null;
    [SerializeField] private Sprite smallFirstAidKitImage = null;
    [SerializeField] private Sprite pistolAmmoImage = null;
    [SerializeField] private Sprite shotgunAmmoImage = null;
    [SerializeField] private Sprite rifleAmmoImage = null;
    [SerializeField] private Sprite fuelImage = null;
    [SerializeField] private Sprite essenceImage = null;

      //parent of all currency objects
    [SerializeField] private Image currencyPanel = null;
    [SerializeField] private Image currencyImage = null;
    [SerializeField] private Text currencyNameText = null;
    [SerializeField] private Text currencyCountText = null;

      //these are the 18 item slots (they're buttons that can be clicked)
    [SerializeField] private Button itemPanel1 = null;
    [SerializeField] private Button itemPanel2 = null;
    [SerializeField] private Button itemPanel3 = null;
    [SerializeField] private Button itemPanel4 = null;
    [SerializeField] private Button itemPanel5 = null;
    [SerializeField] private Button itemPanel6 = null;
    [SerializeField] private Button itemPanel7 = null;
    [SerializeField] private Button itemPanel8 = null;
    [SerializeField] private Button itemPanel9 = null;
    [SerializeField] private Button itemPanel10 = null;
    [SerializeField] private Button itemPanel11 = null;
    [SerializeField] private Button itemPanel12 = null;
    [SerializeField] private Button itemPanel13 = null;
    [SerializeField] private Button itemPanel14 = null;
    [SerializeField] private Button itemPanel15 = null;
    [SerializeField] private Button itemPanel16 = null;
    [SerializeField] private Button itemPanel17 = null;
    [SerializeField] private Button itemPanel18 = null;
      //these are the 18 item slot images
    [SerializeField] private Image itemImage1 = null;
    [SerializeField] private Image itemImage2 = null;
    [SerializeField] private Image itemImage3 = null;
    [SerializeField] private Image itemImage4 = null;
    [SerializeField] private Image itemImage5 = null;
    [SerializeField] private Image itemImage6 = null;
    [SerializeField] private Image itemImage7 = null;
    [SerializeField] private Image itemImage8 = null;
    [SerializeField] private Image itemImage9 = null;
    [SerializeField] private Image itemImage10 = null;
    [SerializeField] private Image itemImage11 = null;
    [SerializeField] private Image itemImage12 = null;
    [SerializeField] private Image itemImage13 = null;
    [SerializeField] private Image itemImage14 = null;
    [SerializeField] private Image itemImage15 = null;
    [SerializeField] private Image itemImage16 = null;
    [SerializeField] private Image itemImage17 = null;
    [SerializeField] private Image itemImage18 = null;
      //these are the 18 item slot names
    [SerializeField] private Text itemNameText1 = null;
    [SerializeField] private Text itemNameText2 = null;
    [SerializeField] private Text itemNameText3 = null;
    [SerializeField] private Text itemNameText4 = null;
    [SerializeField] private Text itemNameText5 = null;
    [SerializeField] private Text itemNameText6 = null;
    [SerializeField] private Text itemNameText7 = null;
    [SerializeField] private Text itemNameText8 = null;
    [SerializeField] private Text itemNameText9 = null;
    [SerializeField] private Text itemNameText10 = null;
    [SerializeField] private Text itemNameText11 = null;
    [SerializeField] private Text itemNameText12 = null;
    [SerializeField] private Text itemNameText13 = null;
    [SerializeField] private Text itemNameText14 = null;
    [SerializeField] private Text itemNameText15 = null;
    [SerializeField] private Text itemNameText16 = null;
    [SerializeField] private Text itemNameText17 = null;
    [SerializeField] private Text itemNameText18 = null;
      //these are the 18 item slot names
    [SerializeField] private Text itemCountText1 = null;
    [SerializeField] private Text itemCountText2 = null;
    [SerializeField] private Text itemCountText3 = null;
    [SerializeField] private Text itemCountText4 = null;
    [SerializeField] private Text itemCountText5 = null;
    [SerializeField] private Text itemCountText6 = null;
    [SerializeField] private Text itemCountText7 = null;
    [SerializeField] private Text itemCountText8 = null;
    [SerializeField] private Text itemCountText9 = null;
    [SerializeField] private Text itemCountText10 = null;
    [SerializeField] private Text itemCountText11 = null;
    [SerializeField] private Text itemCountText12 = null;
    [SerializeField] private Text itemCountText13 = null;
    [SerializeField] private Text itemCountText14 = null;
    [SerializeField] private Text itemCountText15 = null;
    [SerializeField] private Text itemCountText16 = null;
    [SerializeField] private Text itemCountText17 = null;
    [SerializeField] private Text itemCountText18 = null;
      //arrays to store each group of item slot objects
    private Button[] itemPanels = new Button[18];
    private Image[] itemImages = new Image[18];
    private Text[] itemNameTexts = new Text[18];
    private Text[] itemCountTexts = new Text[18];

    private Color visible = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color invisible = new Color(0.0f, 0.0f, 0.0f, 0.0f);
      //used to make filled item slot counts green
    private Color fullColor = Color.green;
    private RaycastHit hit; //retrieved from first person controller
      //details of each item when it is examined
    private const string largeAidKitDetails = "A first aid kit filled with several essential supplies for treating severe wounds. Restores a large amount of health.";
    private const string smallAidKitDetails = "A small pouch containing supplies for treating minor wounds. Restores a small amount of health.";
    private const string pistolAmmoDetails = "Standard ammunition used by handguns.";
    private const string shotgunAmmoDetails = "Standard buckshot used by shotguns.";
    private const string rifleAmmoDetails = "Standard ammunition used by rifles.";
    private const string fuelDetails = "A can of fuel that can be used to power generators.";
    private const string supportLargeAidKitDetails = "A large first aid kit that was left by someone looking out for you.";
    private const string supportSmallAidKitDetails = "A small first aid kit that was left by someone looking out for you.";
    private const string supportPistolAmmoDetails = "Some pistol rounds left by someone looking out for you.";
    private const string supportShotgunAmmoDetails = "Some shotgun shells left by someone looking out for you.";
    private const string supportRifleAmmoDetails = "Some rifle rounds left by someone looking out for you.";
      //how long a message appears if the player tries to use an item they can't use
    private const float defaultCantUseLength = 1.2f;
    private const float delta = 0.019f; //used instead of delta time when time is paused
    private float cantUseLength;
      //max number of each item that can be in a single item slot
    private const int maxSmallFirstAidPanelCount = 3;
    private const int maxLargeFirstAidPanelCount = 3;
    private const int maxPistolAmmoPanelCount = 40;
    private const int maxShotgunAmmoPanelCount = 24;
    private const int maxRifleAmmoPanelCount = 15;
    private const int maxFuelPanelCount = 1;
      //amount of health healed by the health kits
    private const int smallFirstAidHealAmount = 20;
    private const int largeFirstAidHealAmount = 50;
    private const int panelCount = 18; //number of item slots
      //max number of bullets that fit in each gun before reloading is required
    private const int maxShotgunShellCount = 8;
    private const int maxRifleMagazineCount = 5;
      //current number of bullets in each gun
    private int currentShotgunShellCount;
    private int currentRifleMagazineCount;

    private int itemSlotNumber; //number of specific item slot
    private int pickupItemCount; //amount of item about to be picked up
      //current number of each item
    private int smallFirstAidCount;
    private int largeFirstAidCount;
    private int pistolAmmoCount;
    private int shotgunAmmoCount;
    private int rifleAmmoCount;
    private int fuelCount;
    private int essenceCount;
      //NOTE: these have the same max panel counts as their regular counterparts
    private int supportSmallAidCount;
    private int supportLargeAidCount;
    private int supportPistolAmmoCount;
    private int supportShotgunAmmoCount;
    private int supportRifleAmmoCount;

    private bool essenceSlotSet; //currency isn't revealed if this is false

    void Start()
    {
        StoreItemPanels(); //stores all item slot buttons in an array
        StoreItemImages(); //stores all item slot images in an array
        StoreItemNameTexts(); //stores all item name text objects in an array
        StoreItemCountTexts(); //stores all item count text objects in an array
    }

    void Update()
    {
        hit = firstPersonController.GetRaycastHit();

        if (cantUseLength > 0.0f) //checks if counter is over zero
        {
            DecrementCantUsePromptCounter(); //keep reducing the counter
        }
        else
        {
            invTextMessage.text = ""; //removes message
        }
    }

    /**************************************************************************
   Function: EssenceSlotEnabled

Description: This function returns the status of whether or not the essence
             text, image, and current count are visible in the item menu.

      Input: none

     Output: Returns true if the essence slot has been enabled and is visible,
             otherwise, returns false.
    **************************************************************************/
    public bool EssenceSlotEnabled()
    {
        return essenceSlotSet;
    }

    /**************************************************************************
   Function: EnableCurrencyPanel

Description: Given a bool, this function either hides or displays the panel
             containing the currency count, image, and text based on the bool.

      Input: enable - bool used to hide or display the currency panel

     Output: none
    **************************************************************************/
    public void EnableCurrencyPanel(bool enable)
    {
        if(enable)
        {
            currencyPanel.gameObject.SetActive(true);
        }
        else
        {
            currencyPanel.gameObject.SetActive(false);
        }
    }

    /**************************************************************************
   Function: GetInventoryHealthBar

Description: This function returns the slider representing the player's health
             that's displayed in the item menu.

      Input: none

     Output: Returns the slider representing the player's health bar in the 
             item menu.
    **************************************************************************/
    public Slider GetInventoryHealthBar()
    {
        return healthBar;
    }

    /**************************************************************************
   Function: GetInventoryHealthBarRectTransform

Description: This function returns the RectTransform component of the health
             bar that's displayed in the item menu.

      Input: none

     Output: Returns the RectTransform of the item menu's health bar.
    **************************************************************************/
    public RectTransform GetInventoryHealthBarRectTransform()
    {
        return healthRectTransform;
    }

    /**************************************************************************
   Function: GetItemPanels

Description: This function returns an array that contains all 18 item buttons
             in the item sub tab.

      Input: none

     Output: Returns the array of item buttons.
    **************************************************************************/
    public Button[] GetItemPanels()
    {
        return itemPanels;
    }

    /**************************************************************************
   Function: SetItemSlotNumber

Description: Given an integer, this function sets the active slot as the given
             integer.

      Input: itemNumber - integer used to set the current chosen item slot

     Output: none
    **************************************************************************/
    public void SetItemSlotNumber(int itemNumber)
    {
        itemSlotNumber = itemNumber;
    }

    /**************************************************************************
   Function: UseSmallFirstAidKit

Description: This function adds the small first aid kit's heal amount to the
             player's current health before decrementing the small first aid
             kit count.

      Input: none

     Output: none
    **************************************************************************/
    public void UseSmallFirstAidKit()
    {
          //convert item slot's count string to an integer
        int tempCount = int.Parse(itemCountTexts[itemSlotNumber].text);

        hudManager.SetCurrentHealth(smallFirstAidHealAmount);
        --smallFirstAidCount;
        --tempCount; //decrement the item slot's count

        if (tempCount <= 0) //accounting for possibility of count going below zero
        {
            ClearItemSlot(); //empty the current item slot
            uiManager.HideItemSubMenu();
            uiManager.DeselectActiveItemPanel();
        }
        else
        {
              //convert count back to a string
            itemCountTexts[itemSlotNumber].text = tempCount.ToString();
              //item slot is no longer full
            itemCountTexts[itemSlotNumber].color = visible;
        }

    }

    /**************************************************************************
   Function: UseSupportSmallAidKit

Description: This function adds the small first aid kit's heal amount to the
             player's current health before decrementing the support small 
             first aid kit count.

      Input: none

     Output: none
    **************************************************************************/
    public void UseSupportSmallAidKit()
    {
          //convert item slot's count string to an integer
        int tempCount = int.Parse(itemCountTexts[itemSlotNumber].text);

        hudManager.SetCurrentHealth(smallFirstAidHealAmount);
        --supportSmallAidCount;
        --tempCount; //decrement the item slot's count

        if (tempCount <= 0) //accounting for possibility of count going below zero
        {
            ClearItemSlot(); //empty the current item slot
            uiManager.HideItemSubMenu();
            uiManager.DeselectActiveItemPanel();
        }
        else
        {
              //convert count back to a string
            itemCountTexts[itemSlotNumber].text = tempCount.ToString();
              //item slot is no longer full
            itemCountTexts[itemSlotNumber].color = visible;
        }
    }

    /**************************************************************************
   Function: UseLargeFirstAidKit

Description: This function adds the large first aid kit's heal amount to the
             player's current health before decrementing the large first aid
             kit count.

      Input: none

     Output: none
    **************************************************************************/
    public void UseLargeFirstAidKit()
    {
          //convert item slot's count string to an integer
        int tempCount = int.Parse(itemCountTexts[itemSlotNumber].text);
          //heal the player
        hudManager.SetCurrentHealth(largeFirstAidHealAmount);
        --largeFirstAidCount; //decrement total large first aid count
        --tempCount; //decrement the item slot's count

        if (tempCount <= 0) //accounting for possibility of count going below zero
        {
            ClearItemSlot(); //empty the current item slot
            uiManager.HideItemSubMenu();
            uiManager.DeselectActiveItemPanel();
        }
        else
        {
              //convert count back to a string
            itemCountTexts[itemSlotNumber].text = tempCount.ToString();
              //item slot is no longer full
            itemCountTexts[itemSlotNumber].color = visible;
        }
    }

    /**************************************************************************
   Function: UseSupportLargeAidKit

Description: This function adds the large first aid kit's heal amount to the
             player's current health before decrementing the support large aid
             kit count.

      Input: none

     Output: none
    **************************************************************************/
    public void UseSupportLargeAidKit()
    {
          //convert item slot's count string to an integer
        int tempCount = int.Parse(itemCountTexts[itemSlotNumber].text);

        hudManager.SetCurrentHealth(largeFirstAidHealAmount);
        --supportLargeAidCount;
        --tempCount; //decrement the item slot's count

        if (tempCount <= 0) //accounting for possibility of count going below zero
        {
            ClearItemSlot(); //empty the current item slot
            uiManager.HideItemSubMenu();
            uiManager.DeselectActiveItemPanel();
        }
        else
        {
              //convert count back to a string
            itemCountTexts[itemSlotNumber].text = tempCount.ToString();
              //item slot is no longer full
            itemCountTexts[itemSlotNumber].color = visible;
        }
    }

    /**************************************************************************
   Function: UseFuelFromInventory

Description: This function checks if the player is looking at a generator.
             Then it finds the item slot with fuel in it and uses it to
             activate the generator before removing the fuel from the item
             slot.

      Input: none

     Output: none
    **************************************************************************/
    public void UseFuelFromInventory()
    {
        if(hit.collider.tag == "Generator")
        {
            CheckForFuel(); //finds index of item slot with fuel in it

              //convert item slot's count string to an integer
            int tempCount = int.Parse(itemCountTexts[itemSlotNumber].text);

            --fuelCount; //decrement total fuel count
            --tempCount; //decrement the item slot's count
              //activates generator from inventory
            hit.transform.SendMessage("ActivateGeneratorFromInventory");

            if (tempCount <= 0) //accounting for possibility of count going below zero
            {
                ClearItemSlot(); //empty the current item slot
                uiManager.HideItemSubMenu();
                uiManager.DeselectActiveItemPanel();
            }
            else
            {
                //convert count back to a string
                itemCountTexts[itemSlotNumber].text = tempCount.ToString();
                //item slot is no longer full
                itemCountTexts[itemSlotNumber].color = visible;
            }
        }
    }

    /**************************************************************************
   Function: UseFuel

Description: This function finds the item slot with fuel in it, uses it, and
             decrements number of fuel cans by 1.

      Input: none

     Output: none
    **************************************************************************/
    public void UseFuel()
    {
        CheckForFuel(); //finds index of item slot with fuel in it

          //convert item slot's count string to an integer
        int tempCount = int.Parse(itemCountTexts[itemSlotNumber].text);

        --fuelCount; //decrement total fuel count
        --tempCount; //decrement the item slot's count

        if (tempCount <= 0) //accounting for possibility of count going below zero
        {
            ClearItemSlot(); //empty the current item slot
            uiManager.HideItemSubMenu();
            uiManager.DeselectActiveItemPanel();
        }
        else
        {
              //convert count back to a string
            itemCountTexts[itemSlotNumber].text = tempCount.ToString();
              //item slot is no longer full
            itemCountTexts[itemSlotNumber].color = visible;
        }
    }

    /**************************************************************************
   Function: AddToSmallFirstAidCount

Description: Given a count, this function adds that count to the current small
             first aid count.

      Input: count - integer added to the current small first aid count

     Output: none
    **************************************************************************/
    public void AddToSmallFirstAidCount(int count = 1)
    {
        smallFirstAidCount += count;
    }

    /**************************************************************************
   Function: AddToSupportSmallAidCount

Description: Given a count, this function adds that count to the current
             suport small aid count.

      Input: count - integer added to the current support small aid count

     Output: none
    **************************************************************************/
    public void AddToSuportSmallAidCount(int count = 1)
    {
        supportSmallAidCount += count;
    }

    /**************************************************************************
   Function: AddToLargeFirstAidCount

Description: Given a count, this function adds that count to the current large
             first aid count.

      Input: count - integer added to the current large first aid count

     Output: none
    **************************************************************************/
    public void AddToLargeFirstAidCount(int count = 1)
    {
        largeFirstAidCount += count;
    }

    /**************************************************************************
   Function: AddToSupportLargeAidCount

Description: Given a count, this function adds that count to the current 
             support large aid count.

      Input: count - integer added to the current support large aid count

     Output: none
    **************************************************************************/
    public void AddToSupportLargeAidCount(int count = 1)
    {
        supportLargeAidCount += count;
    }

    /**************************************************************************
   Function: AddToPistolAmmoCount

Description: Given a count, this function adds that count to the current pistol
             ammo count.

      Input: count - integer added to the current pistol ammo count

     Output: none
    **************************************************************************/
    public void AddToPistolAmmoCount(int count)
    {
        pistolAmmoCount += count;
    }

    /**************************************************************************
   Function: AddToSupportPistolAmmoCount

Description: Given a count, this function adds that count to the current
             support pistol ammo count.

      Input: count - integer added to the current support pistol ammo count

     Output: none
    **************************************************************************/
    public void AddToSupportPistolAmmoCount(int count)
    {
        supportPistolAmmoCount += count;
    }

    /**************************************************************************
   Function: AddToShotgunAmmoCount

Description: Given a count, this function adds that count to the current 
             shotgun ammo count.

      Input: count - integer added to the current shotgun ammo count

     Output: none
    **************************************************************************/
    public void AddToShotgunAmmoCount(int count)
    {
        shotgunAmmoCount += count;
    }

    /**************************************************************************
   Function: AddToSupportShotgunAmmoCount

Description: Given a count, this function adds that count to the current 
             support shotgun ammo count.

      Input: count - integer added to the current support shotgun ammo count

     Output: none
    **************************************************************************/
    public void AddToSupportShotgunAmmoCount(int count)
    {
        supportShotgunAmmoCount += count;
    }

    /**************************************************************************
   Function: AddToRifleAmmoCount

Description: Given a count, this function adds that count to the current rifle
             ammo count.

      Input: count - integer added to the current rifle ammo count

     Output: none
    **************************************************************************/
    public void AddToRifleAmmoCount(int count)
    {
        rifleAmmoCount += count;
    }

    /**************************************************************************
   Function: AddToSupportRifleAmmoCount

Description: Given a count, this function adds that count to the current 
             support rifle ammo count.

      Input: count - integer added to the current support rifle ammo count

     Output: none
    **************************************************************************/
    public void AddToSupportRifleAmmoCount(int count)
    {
        supportRifleAmmoCount += count;
    }

    /**************************************************************************
   Function: AddToFuelCount

Description: Given a count, this function adds that count to the current fuel
             count.

      Input: count - integer added to the current fuel count

     Output: none
    **************************************************************************/
    public void AddToFuelCount(int count = 1)
    {
        fuelCount += count;
    }

    /**************************************************************************
   Function: AddToEssenceCount

Description: Given a count, this function adds that count to the current 
             essence count.

      Input: count - integer added to the current essence count

     Output: none
    **************************************************************************/
    public void AddToEssenceCount(int count)
    {
        essenceCount += count;
    }

    /**************************************************************************
   Function: UsePistolAmmo

Description: Given an integer, this function deducts it from current ammo
             count.

      Input: none

     Output: none
    **************************************************************************/
    public void UsePistolAmmo(int amount)
    {
        pistolAmmoCount -= amount;
    }

    /**************************************************************************
   Function: UseSupportPistolAmmo

Description: Given an integer, this function deducts it from current ammo
             count.

      Input: none

     Output: none
    **************************************************************************/
    public void UseSupportPistolAmmo(int amount)
    {
        supportPistolAmmoCount -= amount;
    }

    /**************************************************************************
   Function: UseShotgunAmmo

Description: Given an integer, this function deducts it from current ammo
             count.

      Input: none

     Output: none
    **************************************************************************/
    public void UseShotgunAmmo(int amount)
    {
        shotgunAmmoCount -= amount;
    }

    /**************************************************************************
   Function: UseSupportShotgunAmmo

Description: Given an integer, this function deducts it from current ammo
             count.

      Input: none

     Output: none
    **************************************************************************/
    public void UseSupportShotgunAmmo(int amount)
    {
        supportShotgunAmmoCount -= amount;
    }

    /**************************************************************************
   Function: UseRifleAmmo

Description: Given an integer, this function deducts it from current ammo
             count.

      Input: amount - number of rounds to use

     Output: none
    **************************************************************************/
    public void UseRifleAmmo(int amount)
    {
        rifleAmmoCount -= amount;
    }

    /**************************************************************************
   Function: UseSupportRifleAmmo

Description: Given an integer, this function deducts it from current ammo
             count.

      Input: amount - number of rounds to use

     Output: none
    **************************************************************************/
    public void UseSupportRifleAmmo(int amount)
    {
        supportRifleAmmoCount -= amount;
    }

    /**************************************************************************
   Function: GetCurrentItemCount

Description: Given a string, this function returns the current item count
             associated with the name of the item given.

      Input: itemName - string used to return the desired item count

     Output: Returns the current count of the specified item.
    **************************************************************************/
    public int GetCurrentInventoryItemCount(string itemName)
    {
        switch (itemName)
        {
            case "LargeFirstAidKit":
                return largeFirstAidCount;
            case "SmallFirstAidKit":
                return smallFirstAidCount;
            case "PistolAmmo":
                return pistolAmmoCount;
            case "ShotgunAmmo":
                return shotgunAmmoCount;
            case "RifleAmmo":
                return rifleAmmoCount;
            case "Fuel":
                return fuelCount;
            case "SupportLargeAidKit":
                return supportLargeAidCount;
            case "SupportSmallAidKit":
                return supportSmallAidCount;
            case "SupportPistolAmmo":
                return supportPistolAmmoCount;
            case "SupportShotgunAmmo":
                return supportShotgunAmmoCount;
            case "SupportRifleAmmo":
                return supportRifleAmmoCount;
            case "BlueEssenceS":
            case "BlueEssenceM":
            case "BlueEssenceL":
            case "YellowEssenceS":
            case "YellowEssenceM":
            case "YellowEssenceL":
            case "RedEssenceS":
            case "RedEssenceM":
            case "RedEssenceL":
                return essenceCount;
            default:
                //Debug.Log("This shouldn't happen!");
                return -1;
        }
    }

    /**************************************************************************
   Function: GetPickupItemCount

Description: Given an integer, this function stores that integer in a variable
             which will be compared to partially filled item slots to determine
             where to store the pickup.

      Input: itemCount - number of items in the pickup

     Output: none
    **************************************************************************/
    public void GetPickupItemCount(int itemCount)
    {
        pickupItemCount = itemCount;
    }

    /**************************************************************************
   Function: CheckForOccupiedPanel

Description: Given a string, this function searches for occupied panels that
             contain the specified item name. If an item slot is found, that
             index is stored in itemSlotNumber.

      Input: itemName - string of the item to check for

     Output: Returns true if an item slot was found, otherwise, returns false.
    **************************************************************************/
    public bool CheckForOccupiedPanel(string itemName)
    {
          //resets the number variable
        itemSlotNumber = -1;
          //each function changes itemSlotNumber if partially filled item slot is found
        switch (itemName)
        {
            case "LargeFirstAidKit":
                CheckForLargeAidKits(); 
                break;
            case "SmallFirstAidKit":
                CheckForSmallAidKits();
                break;
            case "PistolAmmo":
                CheckForPistolAmmo();
                break;
            case "ShotgunAmmo":
                CheckForShotgunAmmo();
                break;
            case "RifleAmmo":
                CheckForRifleAmmo();
                break;
            case "Fuel":
                CheckForFuel();
                break;
            case "SupportSmallAidKit":
                CheckForSupportSmallAidKits();
                break;
            case "SupportLargeAidKit":
                CheckForSupportLargeAidKits();
                break;
            case "SupportPistolAmmo":
                CheckForSupportPistolAmmo();
                break;
            case "SupportShotgunAmmo":
                CheckForSupportShotgunAmmo();
                break;
            case "SupportRifleAmmo":
                CheckForSupportRifleAmmo();
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        return (itemSlotNumber != -1); //true if an itemslot was found
    }


    /**************************************************************************
   Function: CheckForOccupiedPanel

Description: Given a string, this function searches for occupied panels that
             contain the specified item name. If an item slot is found, that
             index is stored in itemSlotNumber.

      Input: itemName - string of the item to check for

     Output: Returns true if an item slot was found, otherwise, returns false.
    **************************************************************************/
    public bool ReverseCheckForOccupiedPanel(string itemName)
    {
          //resets the number variable
        itemSlotNumber = -1;
          //each function changes itemSlotNumber if partially filled item slot is found
        switch (itemName)
        {
            case "SupportPistolAmmo":
                ReverseCheckForSupportPistolAmmo();
                break;
            case "SupportShotgunAmmo":
                ReverseCheckForSupportShotgunAmmo();
                break;
            case "SupportRifleAmmo":
                ReverseCheckForSupportRifleAmmo();
                break;
            case "PistolAmmo":
                ReverseCheckForPistolAmmo();
                break;
            case "ShotgunAmmo":
                ReverseCheckForShotgunAmmo();
                break;
            case "RifleAmmo":
                ReverseCheckForRifleAmmo();
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        return (itemSlotNumber != -1); //true if an itemslot was found
    }

    /**************************************************************************
   Function: GetMaxPanelPistolAmmo

Description: This function returns the max number of pistol bullets that fit
             in a single item slot.

      Input: none

     Output: Returns the max pistol panel ammo count.
    **************************************************************************/
    public int GetMaxPanelPistolAmmo()
    {
        return maxPistolAmmoPanelCount;
    }

    /**************************************************************************
   Function: GetMaxSupportPanelPistolAmmo

Description: This function returns the max number of support pistol bullets 
             that fit in a single item slot.

      Input: none

     Output: Returns the max pistol panel ammo count.
    **************************************************************************/
    public int GetMaxSupportPanelPistolAmmo()
    {
        return maxPistolAmmoPanelCount;
    }

    /**************************************************************************
   Function: GetMaxPanelShotgunAmmo

Description: This function returns the max number of shotgun shells that fit
             in a single item slot.

      Input: none

     Output: Returns the max shotgun panel ammo count.
    **************************************************************************/
    public int GetMaxPanelShotgunAmmo()
    {
        return maxShotgunAmmoPanelCount;
    }

    /**************************************************************************
   Function: GetMaxSupportPanelShotgunAmmo

Description: This function returns the max number of support shotgun shells
             that fit in a single item slot.

      Input: none

     Output: Returns the max support shotgun panel ammo count.
    **************************************************************************/
    public int GetMaxSupportPanelShotgunAmmo()
    {
        return maxShotgunAmmoPanelCount;
    }

    /**************************************************************************
   Function: GetMaxPanelRifleAmmo

Description: This function returns the max number of rifle bullets that fit
             in a single item slot.

      Input: none

     Output: Returns the max rifle panel ammo count.
    **************************************************************************/
    public int GetMaxPanelRifleAmmo()
    {
        return maxRifleAmmoPanelCount;
    }

    /**************************************************************************
   Function: GetMaxSupportPanelRifleAmmo

Description: This function returns the max number of support rifle bullets 
             that fit in a single item slot.

      Input: none

     Output: Returns the max support rifle panel ammo count.
    **************************************************************************/
    public int GetMaxSupportPanelRifleAmmo()
    {
        return maxRifleAmmoPanelCount;
    }

    /**************************************************************************
   Function: GetMaxPanelLargeAidKit

Description: This function returns the max number of large aid kits that fit
             in a single item slot.

      Input: none

     Output: Returns the max large aid kit panel count.
    **************************************************************************/
    public int GetMaxPanelLargeAidKit()
    {
        return maxLargeFirstAidPanelCount;
    }

    /**************************************************************************
   Function: GetMaxSupportPanelLargeAidKit

Description: This function returns the max number of support large aid kits 
             that fit in a single item slot.

      Input: none

     Output: Returns the max support large aid kit panel count.
    **************************************************************************/
    public int GetMaxSupportPanelLargeAidKit()
    {
        return maxLargeFirstAidPanelCount;
    }

    /**************************************************************************
   Function: GetMaxPanelSmallAidKit

Description: This function returns the max number of small aid kits that fit
             in a single item slot.

      Input: none

     Output: Returns the max small aid kit panel count.
    **************************************************************************/
    public int GetMaxPanelSmallAidKit()
    {
        return maxSmallFirstAidPanelCount;
    }

    /**************************************************************************
   Function: GetMaxSupportPanelSmallAidKit

Description: This function returns the max number of support small aid kits 
             that fit in a single item slot.

      Input: none

     Output: Returns the max support small aid kit panel count.
    **************************************************************************/
    public int GetMaxSupportPanelSmallAidKit()
    {
        return maxSmallFirstAidPanelCount;
    }

    /**************************************************************************
   Function: GetMaxPanelFuel

Description: This function returns the max number of fuel cans that fit in a 
             single item slot.

      Input: none

     Output: Returns the max fuel panel count.
    **************************************************************************/
    public int GetMaxPanelFuel()
    {
        return maxFuelPanelCount;
    }

    /**************************************************************************
   Function: UseItem

Description: This function gets the current item slot's name to determine which
             use function to call. If the item can't be used, text is displayed
             to inform the player.

      Input: none

     Output: none
    **************************************************************************/
    public void UseItem()
    {
        string itemName = itemNameTexts[itemSlotNumber].text;

        switch(itemName)
        {
            case "Large First Aid Kit":
                if(hudManager.GetCurrentHealth() < hudManager.GetMaxHealth())
                {
                    UseLargeFirstAidKit(); //use the large first aid kit
                }
                else
                {
                    DisplayFullHealthText(); //tell player they can't use it
                }
                break;
            case "Small First Aid Kit":
                if (hudManager.GetCurrentHealth() < hudManager.GetMaxHealth())
                {
                    UseSmallFirstAidKit(); //use the small first aid kit
                }
                else
                {
                    DisplayFullHealthText(); //tell player they can't use it
                }
                break;
            case "Pistol Ammo":
                DisplayCantUseAmmoText();
                break;
            case "Shotgun Ammo":
                DisplayCantUseAmmoText();
                break;
            case "Rifle Ammo":
                DisplayCantUseAmmoText();
                break;
            case "Fuel":
                  //checks if the raycast hit something before trying to access it
                if(firstPersonController.RaycastHitSomething())
                {
                    if (hit.collider.tag == "Generator")
                    {
                        UseFuelFromInventory();
                    }
                    else
                    {
                        DisplayCantUseFuelText();
                    }
                }
                else
                {
                    DisplayCantUseFuelText();
                }
                break;
            case "Support Small First Aid Kit":
                if (hudManager.GetCurrentHealth() < hudManager.GetMaxHealth())
                {
                    UseSupportSmallAidKit(); //use the support small aid kit
                }
                else
                {
                    DisplayFullHealthText(); //tell player they can't use it
                }
                break;
            case "Support Large First Aid Kit":
                if (hudManager.GetCurrentHealth() < hudManager.GetMaxHealth())
                {
                    UseSupportLargeAidKit(); //use the support large aid kit
                }
                else
                {
                    DisplayFullHealthText(); //tell player they can't use it
                }
                break;
            case "Support Pistol Ammo":
                DisplayCantUseAmmoText();
                break;
            case "Support Shotgun Ammo":
                DisplayCantUseAmmoText();
                break;
            case "Support Rifle Ammo":
                DisplayCantUseAmmoText();
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        uiManager.DeselectItemSlot();
        RemoveDetails();
    }

    /**************************************************************************
   Function: DisplayDetails

Description: This function gets the item name of the currently selected item
             slot and displays its image and text in the detail panel.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayDetails()
    {
        string itemName = itemNameTexts[itemSlotNumber].text;

        switch (itemName)
        {
            case "Large First Aid Kit":
                invDetailImage.color = visible;
                invDetailImage.sprite = largeFirstAidKitImage;
                invDetailText.text = largeAidKitDetails;
                invDetailNameText.text = itemName;
                break;
            case "Small First Aid Kit":
                invDetailImage.color = visible;
                invDetailImage.sprite = smallFirstAidKitImage;
                invDetailText.text = smallAidKitDetails;
                invDetailNameText.text = itemName;
                break;
            case "Pistol Ammo":
                invDetailImage.color = visible;
                invDetailImage.sprite = pistolAmmoImage;
                invDetailText.text = pistolAmmoDetails;
                invDetailNameText.text = itemName;
                break;
            case "Shotgun Ammo":
                invDetailImage.color = visible;
                invDetailImage.sprite = shotgunAmmoImage;
                invDetailText.text = shotgunAmmoDetails;
                invDetailNameText.text = itemName;
                break;
            case "Rifle Ammo":
                invDetailImage.color = visible;
                invDetailImage.sprite = rifleAmmoImage;
                invDetailText.text = rifleAmmoDetails;
                invDetailNameText.text = itemName;
                break;
            case "Fuel":
                invDetailImage.color = visible;
                invDetailImage.sprite = fuelImage;
                invDetailText.text = fuelDetails;
                invDetailNameText.text = itemName;
                break;
            case "Support Small First Aid Kit":
                invDetailImage.color = visible;
                invDetailImage.sprite = smallFirstAidKitImage;
                invDetailText.text = supportSmallAidKitDetails;
                invDetailNameText.text = itemName;
                break;
            case "Support Large First Aid Kit":
                invDetailImage.color = visible;
                invDetailImage.sprite = largeFirstAidKitImage;
                invDetailText.text = supportLargeAidKitDetails;
                invDetailNameText.text = itemName;
                break;
            case "Support Pistol Ammo":
                invDetailImage.color = visible;
                invDetailImage.sprite = pistolAmmoImage;
                invDetailText.text = supportPistolAmmoDetails;
                invDetailNameText.text = itemName;
                break;
            case "Support Shotgun Ammo":
                invDetailImage.color = visible;
                invDetailImage.sprite = shotgunAmmoImage;
                invDetailText.text = supportShotgunAmmoDetails;
                invDetailNameText.text = itemName;
                break;
            case "Support Rifle Ammo":
                invDetailImage.color = visible;
                invDetailImage.sprite = rifleAmmoImage;
                invDetailText.text = supportRifleAmmoDetails;
                invDetailNameText.text = itemName;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
    }

    /**************************************************************************
   Function: RemoveDetails

Description: This function makes the detail image invisible, sets its sprite
             to null, and sets the text to an empty string.

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
   Function: DisplayFullHealthText

Description: This function tells the player their health is full.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayFullHealthText()
    {
        invTextMessage.text = "Your health is full";
        ResetCantUsePromptCounter();
    }

    /**************************************************************************
   Function: DisplayCantUseAmmoText

Description: This function tells the player they can't use ammo with the use
             button.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayCantUseAmmoText()
    {
        invTextMessage.text = "You can't use ammo like this";
        ResetCantUsePromptCounter();
    }

    /**************************************************************************
   Function: DisplayCantUseFuelText

Description: This function tells the player they can't use their fuel.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayCantUseFuelText()
    {
        invTextMessage.text = "You can't use fuel here";
        ResetCantUsePromptCounter();
    }

    /**************************************************************************
   Function: DecrementCantUsePromptCounter

Description: This function decrements the cantUseLength variable by delta
             every time it's called.

      Input: none

     Output: none
    **************************************************************************/
    private void DecrementCantUsePromptCounter()
    {
        cantUseLength -= delta;
    }

    /**************************************************************************
   Function: ResetCantUsePromptCounter

Description: This function sets the cantUseLength variable to its default
             value.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetCantUsePromptCounter()
    {
        cantUseLength = defaultCantUseLength;
    }

    /**************************************************************************
   Function: CheckForLargeAidKits

Description: This function checks each item panel in order to see if it
             contains large first aid kits. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForLargeAidKits()
    {
          //needs to match what's shown in inventory
        string itemName = "Large First Aid Kit";

        for (int i = 0; i < panelCount; i++)
        {
            if(itemNameTexts[i].text == itemName)
            {
                int itemCount = int.Parse(itemCountTexts[i].text);
                  //checks if item about to be picked up and current count in this panel
                  //all fits in this panel
                if (itemCount < maxLargeFirstAidPanelCount)
                {
                    itemSlotNumber = i;
                    return;
                }
            }
        }
    }

    /**************************************************************************
   Function: CheckForSupportLargeAidKits

Description: This function checks each item panel in order to see if it
             contains support large aid kits. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForSupportLargeAidKits()
    {
          //needs to match what's shown in inventory
        string itemName = "Support Large First Aid Kit";

        for (int i = 0; i < panelCount; i++)
        {
            if (itemNameTexts[i].text == itemName)
            {
                int itemCount = int.Parse(itemCountTexts[i].text);
                  //checks if item about to be picked up and current count in this panel
                  //all fits in this panel
                if (itemCount < maxLargeFirstAidPanelCount)
                {
                    itemSlotNumber = i;
                    return;
                }
            }
        }
    }

    /**************************************************************************
   Function: CheckForSmallAidKits

Description: This function checks each item panel in order to see if it
             contains small first aid kits. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForSmallAidKits()
    {
        string itemName = "Small First Aid Kit";

        for (int i = 0; i < panelCount; i++)
        {
            if (itemNameTexts[i].text == itemName)
            {
                int itemCount = int.Parse(itemCountTexts[i].text);

                if (itemCount < maxSmallFirstAidPanelCount)
                {
                    itemSlotNumber = i;
                    return;
                }
            }
        }
    }

    /**************************************************************************
   Function: CheckForSupportSmallAidKits

Description: This function checks each item panel in order to see if it
             contains support small aid kits. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForSupportSmallAidKits()
    {
        string itemName = "Support Small First Aid Kit";

        for (int i = 0; i < panelCount; i++)
        {
            if (itemNameTexts[i].text == itemName)
            {
                int itemCount = int.Parse(itemCountTexts[i].text);

                if (itemCount < maxSmallFirstAidPanelCount)
                {
                    itemSlotNumber = i;
                    return;
                }
            }
        }
    }

    /**************************************************************************
   Function: CheckForPistolAmmo

Description: This function checks each item panel in order to see if it
             contains pistol ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForPistolAmmo()
    {
        string itemName = "Pistol Ammo";

        for (int i = 0; i < panelCount; i++)
        {
            if (itemNameTexts[i].text == itemName)
            {
                int itemCount = int.Parse(itemCountTexts[i].text);

                  //checks if ammo in current slot is less than full capacity
                if (itemCount < maxPistolAmmoPanelCount)
                {
                    itemSlotNumber = i;
                    return;
                }
            }
        }
    }

    /**************************************************************************
   Function: ReverseCheckForPistolAmmo

Description: This function checks each item panel in reverse order to see if it
             contains pistol ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void ReverseCheckForPistolAmmo()
    {
        string itemName = "Pistol Ammo";

        for (int i = (panelCount - 1); i > -1; i--)
        {
              //if this item slot has a name, ammo count is greater than 0
            if (itemNameTexts[i].text == itemName)
            {
                itemSlotNumber = i;
                return;
            }
        }
    }

    /**************************************************************************
   Function: CheckForSupportPistolAmmo

Description: This function checks each item panel in order to see if it
             contains support pistol ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForSupportPistolAmmo()
    {
        string itemName = "Support Pistol Ammo";

        for (int i = 0; i < panelCount; i++)
        {
            if (itemNameTexts[i].text == itemName)
            {
                int itemCount = int.Parse(itemCountTexts[i].text);

                  //checks if ammo in current slot is less than full capacity
                if (itemCount < maxPistolAmmoPanelCount)
                {
                    itemSlotNumber = i;
                    return;
                }
            }
        }
    }

    /**************************************************************************
   Function: ReverseCheckForSupportPistolAmmo

Description: This function checks each item panel in order to see if it
             contains support pistol ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void ReverseCheckForSupportPistolAmmo()
    {
        string itemName = "Support Pistol Ammo";

          //remember, arrays are 0 indexed. length - 1 to access last element
        for (int i = (panelCount - 1); i > -1; i--)
        {
            if (itemNameTexts[i].text == itemName)
            {
                itemSlotNumber = i;
                return;
            }
        }
    }

    /**************************************************************************
   Function: CheckForShotgunAmmo

Description: This function checks each item panel in order to see if it
             contains shotgun ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForShotgunAmmo()
    {
        string itemName = "Shotgun Ammo";

        for (int i = 0; i < panelCount; i++)
        {
            if (itemNameTexts[i].text == itemName)
            {
                int itemCount = int.Parse(itemCountTexts[i].text);

                if (itemCount < maxShotgunAmmoPanelCount)
                {
                    itemSlotNumber = i;
                    return;
                }
            }
        }
    }

    /**************************************************************************
   Function: ReverseCheckForShotgunAmmo

Description: This function checks each item panel in reverse order to see if it
             contains shotgun ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void ReverseCheckForShotgunAmmo()
    {
        string itemName = "Shotgun Ammo";

        for (int i = (panelCount - 1); i > -1; i--)
        {
              //if this item slot has a name, ammo count is greater than 0
            if (itemNameTexts[i].text == itemName)
            {
                itemSlotNumber = i;
                return;
            }
        }
    }

    /**************************************************************************
   Function: CheckForSupportShotgunAmmo

Description: This function checks each item panel in order to see if it
             contains support shotgun ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForSupportShotgunAmmo()
    {
        string itemName = "Support Shotgun Ammo";

        for (int i = 0; i < panelCount; i++)
        {
            if (itemNameTexts[i].text == itemName)
            {
                int itemCount = int.Parse(itemCountTexts[i].text);

                if (itemCount < maxShotgunAmmoPanelCount)
                {
                    itemSlotNumber = i;
                    return;
                }
            }
        }
    }

    /**************************************************************************
   Function: ReverseCheckForSupportShotgunAmmo

Description: This function checks each item panel in order to see if it
             contains support shotgun ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void ReverseCheckForSupportShotgunAmmo()
    {
        string itemName = "Support Shotgun Ammo";

          //remember, arrays are 0 indexed. length - 1 to access last element
        for (int i = (panelCount - 1); i > -1; i--)
        {
            if (itemNameTexts[i].text == itemName)
            {
                itemSlotNumber = i;
                return;
            }
        }
    }

    /**************************************************************************
   Function: CheckForRifleAmmo

Description: This function checks each item panel in order to see if it
             contains rifle ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForRifleAmmo()
    {
        string itemName = "Rifle Ammo";

        for (int i = 0; i < panelCount; i++)
        {
            if (itemNameTexts[i].text == itemName)
            {
                int itemCount = int.Parse(itemCountTexts[i].text);

                if (itemCount < maxRifleAmmoPanelCount)
                {
                    itemSlotNumber = i;
                    return;
                }
            }
        }
    }

    /**************************************************************************
   Function: ReverseCheckForRifleAmmo

Description: This function checks each item panel in reverse order to see if it
             contains rifle ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void ReverseCheckForRifleAmmo()
    {
        string itemName = "Rifle Ammo";

        for (int i = (panelCount - 1); i > -1; i--)
        {
              //if this item slot has a name, ammo count is greater than 0
            if (itemNameTexts[i].text == itemName)
            {
                itemSlotNumber = i;
                return;
            }
        }
    }

    /**************************************************************************
   Function: CheckForSupportRifleAmmo

Description: This function checks each item panel in order to see if it
             contains support rifle ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForSupportRifleAmmo()
    {
        string itemName = "Support Rifle Ammo";

        for (int i = 0; i < panelCount; i++)
        {
            if (itemNameTexts[i].text == itemName)
            {
                int itemCount = int.Parse(itemCountTexts[i].text);

                if (itemCount < maxRifleAmmoPanelCount)
                {
                    itemSlotNumber = i;
                    return;
                }
            }
        }
    }

    /**************************************************************************
   Function: ReverseCheckForSupportRifleAmmo

Description: This function checks each item panel in order to see if it
             contains support rifle ammo. If it does and the count is less
             than the max count allowed in the panel, that number is assigned 
             to itemSlotNumber. If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void ReverseCheckForSupportRifleAmmo()
    {
        string itemName = "Support Rifle Ammo";

          //remember, arrays are 0 indexed. length - 1 to access last element
        for (int i = (panelCount - 1); i > -1; i--)
        {
            if (itemNameTexts[i].text == itemName)
            {
                itemSlotNumber = i;
                return;
            }
        }
    }

    /**************************************************************************
   Function: CheckForFuel

Description: This function checks each item panel in order to see if it
             contains fuel. If it does and the count is less than the max count 
             allowed in the panel, that number is assigned to itemSlotNumber. 
             If not, it keeps searching.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForFuel()
    {
        string itemName = "Fuel";

        for (int i = 0; i < panelCount; i++)
        {
            if (itemNameTexts[i].text == itemName)
            {
                int itemCount = int.Parse(itemCountTexts[i].text);

                if (itemCount < maxFuelPanelCount)
                {
                    itemSlotNumber = i; //assigns index to itemSlotNumber
                    return; //exits the function
                }
            }
        }
    }

    /**************************************************************************
   Function: GetPanelItemCount

Description: Given an integer, this function converts the count text of the
             specified item slot to an integer before returning it.

      Input: slotNumber - integer of specific item slot to get count from

     Output: Returns the number of items in the specified slot
    **************************************************************************/
    public int GetPanelItemCount()
    {
        return int.Parse(itemCountTexts[itemSlotNumber].text);
    }

    /**************************************************************************
   Function: CheckForEmptyPanel

Description: This function checks each item panel in order and sets the
             itemSlotNumber to the first empty slot found.

      Input: none

     Output: Returns true if an empty item panel was found, otherwise, returns
             false.
    **************************************************************************/
    public bool CheckForEmptyPanel()
    {
        itemSlotNumber = -1; //resets the variable

        for (int i = 0; i < panelCount; i++) //loops through all 18 panels
        {
            if(itemImages[i].sprite == null)
            {
                itemSlotNumber = i;
                break;
            }
        }

        return (itemSlotNumber != -1); //true if an empty item slot was found
    }

    /**************************************************************************
   Function: AddToEmptyPanel

Description: Given a string, this function adds the specified item to an empty
             item slot.

      Input: itemName - string of the item to add to an item slot

     Output: none
    **************************************************************************/
    public void AddToEmptyPanel(string itemName)
    {
        switch(itemName)
        {
            case "LargeFirstAidKit":
                  //image of large first aid kit
                itemImages[itemSlotNumber].sprite = largeFirstAidKitImage;
                  //makes image visible
                itemImages[itemSlotNumber].color = visible;
                  //name of large first aid kit
                itemNameTexts[itemSlotNumber].text = "Large First Aid Kit";
                  //number of large first aid kits
                itemCountTexts[itemSlotNumber].text = pickupItemCount.ToString();
                break;
            case "SmallFirstAidKit":
                itemImages[itemSlotNumber].sprite = smallFirstAidKitImage;
                itemImages[itemSlotNumber].color = visible;
                itemNameTexts[itemSlotNumber].text = "Small First Aid Kit";
                itemCountTexts[itemSlotNumber].text = pickupItemCount.ToString();
                break;
            case "PistolAmmo":
                itemImages[itemSlotNumber].sprite = pistolAmmoImage;
                itemImages[itemSlotNumber].color = visible;
                itemNameTexts[itemSlotNumber].text = "Pistol Ammo";
                itemCountTexts[itemSlotNumber].text = pickupItemCount.ToString();
                break;
            case "ShotgunAmmo":
                itemImages[itemSlotNumber].sprite = shotgunAmmoImage;
                itemImages[itemSlotNumber].color = visible;
                itemNameTexts[itemSlotNumber].text = "Shotgun Ammo";
                itemCountTexts[itemSlotNumber].text = pickupItemCount.ToString();
                break;
            case "RifleAmmo":
                itemImages[itemSlotNumber].sprite = rifleAmmoImage;
                itemImages[itemSlotNumber].color = visible;
                itemNameTexts[itemSlotNumber].text = "Rifle Ammo";
                itemCountTexts[itemSlotNumber].text = pickupItemCount.ToString();
                break;
            case "Fuel":
                itemImages[itemSlotNumber].sprite = fuelImage;
                itemImages[itemSlotNumber].color = visible;
                itemNameTexts[itemSlotNumber].text = "Fuel";
                itemCountTexts[itemSlotNumber].text = pickupItemCount.ToString();
                break;
            case "SupportLargeAidKit":
                itemImages[itemSlotNumber].sprite = largeFirstAidKitImage;
                itemImages[itemSlotNumber].color = visible;
                itemNameTexts[itemSlotNumber].text = "Support Large First Aid Kit";
                itemCountTexts[itemSlotNumber].text = pickupItemCount.ToString();
                break;
            case "SupportSmallAidKit":
                itemImages[itemSlotNumber].sprite = smallFirstAidKitImage;
                itemImages[itemSlotNumber].color = visible;
                itemNameTexts[itemSlotNumber].text = "Support Small First Aid Kit";
                itemCountTexts[itemSlotNumber].text = pickupItemCount.ToString();
                break;
            case "SupportPistolAmmo":
                itemImages[itemSlotNumber].sprite = pistolAmmoImage;
                itemImages[itemSlotNumber].color = visible;
                itemNameTexts[itemSlotNumber].text = "Support Pistol Ammo";
                itemCountTexts[itemSlotNumber].text = pickupItemCount.ToString();
                break;
            case "SupportShotgunAmmo":
                itemImages[itemSlotNumber].sprite = shotgunAmmoImage;
                itemImages[itemSlotNumber].color = visible;
                itemNameTexts[itemSlotNumber].text = "Support Shotgun Ammo";
                itemCountTexts[itemSlotNumber].text = pickupItemCount.ToString();
                break;
            case "SupportRifleAmmo":
                itemImages[itemSlotNumber].sprite = rifleAmmoImage;
                itemImages[itemSlotNumber].color = visible;
                itemNameTexts[itemSlotNumber].text = "Support Rifle Ammo";
                itemCountTexts[itemSlotNumber].text = pickupItemCount.ToString();
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        CheckForFullPanel(itemName); //checks if item slot is full
    }

    /**************************************************************************
   Function: AddToItemPanel

Description: Given a string, this function adds the specified item to a
             partially filled item slot.

      Input: itemName - string of the item to add to an item slot

     Output: none
    **************************************************************************/
    public void AddToItemPanel(string itemName)
    {
        switch(itemName)
        {
            case "SmallFirstAidKit":
                  //number of large first aid kits
                itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) + pickupItemCount).ToString();
                break;
            case "LargeFirstAidKit":
                  //number of large first aid kits
                itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) + pickupItemCount).ToString();
                break;
            case "PistolAmmo":
                  //number of pistol bullets
                itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) + pickupItemCount).ToString();
                break;
            case "ShotgunAmmo":
                  //number of shotgun shells
                itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) + pickupItemCount).ToString();
                break;
            case "RifleAmmo":
                  //number of rifle bullets
                itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) + pickupItemCount).ToString();
                break;
            case "Fuel":
                  //number of fuel cans
                itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) + pickupItemCount).ToString();
                break;
            case "SupportSmallAidKit":
                  //number of large first aid kits
                itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) + pickupItemCount).ToString();
                break;
            case "SupportLargeAidKit":
                  //number of large first aid kits
                itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) + pickupItemCount).ToString();
                break;
            case "SupportPistolAmmo":
                  //number of support pistol bullets
                itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) + pickupItemCount).ToString();
                break;
            case "SupportShotgunAmmo":
                  //number of shotgun shells
                itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) + pickupItemCount).ToString();
                break;
            case "SupportRifleAmmo":
                  //number of rifle bullets
                itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) + pickupItemCount).ToString();
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        CheckForFullPanel(itemName); //checks if item slot is full
    }

    /**************************************************************************
   Function: AddToItemPanel

Description: Given a string, this function uses the specified ammo from the
             current item slot before decrementing the amount used from that
             slot. If the slot is empty, it is cleared of all information.

      Input: itemName - string of the type of ammo to use

     Output: none
    **************************************************************************/
    public void UseAmmoFromItemPanel(string ammoName)
    {
        switch (ammoName)
        {
            case "SupportPistolAmmo":
                  //checks if ammo currently in magazine + all of ammo in item slot fit in gun
                if (pistol.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text) <= pistol.GetMaxMagazineAmmoCount())
                {
                      //stores amount to add to pistol magazine and to deduct from item panel
                    int partialAmount = pistol.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text);
                      //puts calculated amount into pistol
                    pistol.SetMagazineAmmoCount(partialAmount);
                    UseSupportPistolAmmo(int.Parse(itemCountTexts[itemSlotNumber].text)); //deducts amount in item slot from ammo count
                    itemCountTexts[itemSlotNumber].text = "0"; //item slot is now empty
                    partialAmount = 0; //clears this variable
                }
                else
                {
                      //amount to add to magazine
                    int partialAmount = (pistol.GetMaxMagazineAmmoCount() - pistol.GetMagazineAmmoCount());
                      //fills up magazine
                    pistol.SetMagazineAmmoCount(pistol.GetMagazineAmmoCount() + partialAmount);
                      //only reduces panel by the amount used
                    itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) - partialAmount).ToString();
                    UseSupportPistolAmmo(partialAmount); //deducts partial amount form total count
                    partialAmount = 0; //clears this variable
                }
                break;
            case "SupportShotgunAmmo":
                  //checks if ammo currently in magazine + all of ammo in item slot fit in gun
                if (shotgun.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text) <= shotgun.GetMaxMagazineAmmoCount())
                {
                      //stores amount to add to shotgun magazine and to deduct from item panel
                    int partialAmount = shotgun.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text);
                      //puts calculated amount into shotgun
                    shotgun.SetMagazineAmmoCount(partialAmount);
                    UseSupportShotgunAmmo(int.Parse(itemCountTexts[itemSlotNumber].text)); //deducts amount in item slot from ammo count
                    itemCountTexts[itemSlotNumber].text = "0"; //item slot is now empty
                    partialAmount = 0; //clears this variable
                }
                else
                {
                      //amount to add to magazine
                    int partialAmount = (shotgun.GetMaxMagazineAmmoCount() - shotgun.GetMagazineAmmoCount());
                      //fills up magazine
                    shotgun.SetMagazineAmmoCount(shotgun.GetMagazineAmmoCount() + partialAmount);
                      //only reduces panel by the amount used
                    itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) - partialAmount).ToString();
                    UseSupportShotgunAmmo(partialAmount); //deducts partial amount form total count
                    partialAmount = 0; //clears this variable
                }
                break;
            case "SupportRifleAmmo":
                  //checks if ammo currently in magazine + all of ammo in item slot fit in gun
                if (rifle.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text) <= rifle.GetMaxMagazineAmmoCount())
                {
                      //stores amount to add to rifle magazine and to deduct from item panel
                    int partialAmount = rifle.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text);
                      //puts calculated amount into rifle
                    rifle.SetMagazineAmmoCount(partialAmount);
                    UseSupportRifleAmmo(int.Parse(itemCountTexts[itemSlotNumber].text)); //deducts amount in item slot from ammo count
                    itemCountTexts[itemSlotNumber].text = "0"; //item slot is now empty
                    partialAmount = 0; //clears this variable
                }
                else
                {
                      //amount to add to magazine
                    int partialAmount = (rifle.GetMaxMagazineAmmoCount() - rifle.GetMagazineAmmoCount());
                      //fills up magazine
                    rifle.SetMagazineAmmoCount(rifle.GetMagazineAmmoCount() + partialAmount);
                      //only reduces panel by the amount used
                    itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) - partialAmount).ToString();
                    UseSupportRifleAmmo(partialAmount); //deducts partial amount form total count
                    partialAmount = 0; //clears this variable
                }
                break;
            case "PistolAmmo":
                  //checks if ammo currently in magazine + all of ammo in item slot fit in gun
                if (pistol.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text) <= pistol.GetMaxMagazineAmmoCount())
                {
                      //stores amount to add to pistol magazine and to deduct from item panel
                    int partialAmount = pistol.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text);
                      //puts calculated amount into pistol
                    pistol.SetMagazineAmmoCount(partialAmount);
                    UsePistolAmmo(int.Parse(itemCountTexts[itemSlotNumber].text)); //deducts partial amount from total count
                    itemCountTexts[itemSlotNumber].text = "0"; //item slot is now empty
                    partialAmount = 0; //clears this variable
                }
                else
                {
                      //amount to add to magazine
                    int partialAmount = (pistol.GetMaxMagazineAmmoCount() - pistol.GetMagazineAmmoCount());
                      //fills up magazine
                    pistol.SetMagazineAmmoCount(pistol.GetMagazineAmmoCount() + partialAmount);
                      //only reduces panel by the amount used
                    itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) - partialAmount).ToString();
                    UsePistolAmmo(partialAmount); //deducts partial amount from total count
                    partialAmount = 0; //clears this variable
                }
                break;
            case "ShotgunAmmo":
                  //checks if ammo currently in magazine + all of ammo in item slot fit in gun
                if (shotgun.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text) <= shotgun.GetMaxMagazineAmmoCount())
                {
                      //stores amount to add to shotgun magazine and to deduct from item panel
                    int partialAmount = shotgun.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text);
                      //puts calculated amount into shotgun
                    shotgun.SetMagazineAmmoCount(partialAmount);
                    UseShotgunAmmo(int.Parse(itemCountTexts[itemSlotNumber].text)); //deducts amount in item slot from ammo count             
                    itemCountTexts[itemSlotNumber].text = "0"; //item slot is now empty
                    partialAmount = 0; //clears this variable
                }
                else
                {
                      //amount to add to magazine
                    int partialAmount = (shotgun.GetMaxMagazineAmmoCount() - shotgun.GetMagazineAmmoCount());
                      //fills up magazine
                    shotgun.SetMagazineAmmoCount(shotgun.GetMagazineAmmoCount() + partialAmount);
                      //only reduces panel by the amount used
                    itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) - partialAmount).ToString();
                    UseShotgunAmmo(partialAmount); //deducts partial amount from total count
                    partialAmount = 0; //clears this variable
                }
                break;
            case "RifleAmmo":
                  //checks if ammo currently in magazine + all of ammo in item slot fit in gun
                if (rifle.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text) <= rifle.GetMaxMagazineAmmoCount())
                {
                      //stores amount to add to rifle magazine and to deduct from item panel
                    int partialAmount = rifle.GetMagazineAmmoCount() + int.Parse(itemCountTexts[itemSlotNumber].text);
                      //puts calculated amount into rifle
                    rifle.SetMagazineAmmoCount(partialAmount);
                    UseRifleAmmo(int.Parse(itemCountTexts[itemSlotNumber].text)); //deducts amount in item slot from ammo count
                    itemCountTexts[itemSlotNumber].text = "0"; //item slot is now empty
                    partialAmount = 0; //clears this variable
                }
                else
                {
                      //amount to add to magazine
                    int partialAmount = (rifle.GetMaxMagazineAmmoCount() - rifle.GetMagazineAmmoCount());
                      //fills up magazine
                    rifle.SetMagazineAmmoCount(rifle.GetMagazineAmmoCount() + partialAmount);
                      //only reduces panel by the amount used
                    itemCountTexts[itemSlotNumber].text = (int.Parse(itemCountTexts[itemSlotNumber].text) - partialAmount).ToString();
                    UseRifleAmmo(partialAmount); //deducts partial amount form total count
                    partialAmount = 0; //clears this variable
                }
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

          //checks if current slot has all ammo used up
        if(int.Parse(itemCountTexts[itemSlotNumber].text) == 0)
        {
            ClearItemSlot(); //empty item slot so other items can fit here
        }
        else
        {
              //item slot is no longer full, so change text to white
            itemCountTexts[itemSlotNumber].color = visible;
        }
    }

    /**************************************************************************
   Function: SetEssenceSlot

Description: This function reveals the currency panel, image, name, and count
             of the player's current essence.

      Input: none

     Output: none
    **************************************************************************/
    public void SetEssenceSlot()
    {        
        EnableCurrencyPanel(true); //displays currency panel and all child objects
        currencyImage.sprite = essenceImage; //sets the sprite image
        currencyImage.color = visible;  //makes it visible
        currencyNameText.text = "Essence"; //adds essence name
        currencyCountText.text = essenceCount.ToString(); //displays current value
        essenceSlotSet = true; //now currency slot will be visible when accessing inventory
    }

    /**************************************************************************
   Function: UpdateEssenceText

Description: This function updates the count text for the essence currency.

      Input: none

     Output: none
    **************************************************************************/
    public void UpdateEssenceText()
    {
        currencyCountText.text = essenceCount.ToString();
    }

    /**************************************************************************
   Function: DiscardItem

Description: This function deducts the current item's count from total count
             then calls the clear item slot function.

      Input: none

     Output: none
    **************************************************************************/
    public void DiscardItem()
    {
        switch(itemNameTexts[itemSlotNumber].text)
        {
            case "Large First Aid Kit":
                largeFirstAidCount -= int.Parse(itemCountTexts[itemSlotNumber].text);
                break;
            case "Small First Aid Kit":
                smallFirstAidCount -= int.Parse(itemCountTexts[itemSlotNumber].text);
                break;
            case "Pistol Ammo":
                pistolAmmoCount -= int.Parse(itemCountTexts[itemSlotNumber].text);
                break;
            case "Shotgun Ammo":
                shotgunAmmoCount -= int.Parse(itemCountTexts[itemSlotNumber].text);
                break;
            case "Rifle Ammo":
                rifleAmmoCount -= int.Parse(itemCountTexts[itemSlotNumber].text);
                break;
            case "Fuel":
                fuelCount -= int.Parse(itemCountTexts[itemSlotNumber].text);
                break;
            case "Support Large First Aid Kit":
                supportLargeAidCount -= int.Parse(itemCountTexts[itemSlotNumber].text);
                break;
            case "Support Small First Aid Kit":
                supportSmallAidCount -= int.Parse(itemCountTexts[itemSlotNumber].text);
                break;
            case "Support Pistol Ammo":
                supportPistolAmmoCount -= int.Parse(itemCountTexts[itemSlotNumber].text);
                break;
            case "Support Shotgun Ammo":
                supportShotgunAmmoCount -= int.Parse(itemCountTexts[itemSlotNumber].text);
                break;
            case "Support Rifle Ammo":
                supportRifleAmmoCount -= int.Parse(itemCountTexts[itemSlotNumber].text);
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        ClearItemSlot();
        RemoveDetails();

        uiManager.DeselectItemSlot();
    }

    /**************************************************************************
   Function: ClearItemSlot

Description: This function clears the specified item slot of all text and
             its image.

      Input: none

     Output: none
    **************************************************************************/
    public void ClearItemSlot()
    {         
        itemImages[itemSlotNumber].sprite = null; //removes the item image       
        itemImages[itemSlotNumber].color = invisible; //makes the item slot black         
        itemNameTexts[itemSlotNumber].text = ""; //removes the name text        
        itemCountTexts[itemSlotNumber].text = ""; //removes the count text
    }

    /**************************************************************************
   Function: CheckForFullPanel

Description: Given a string, this function checks the index at the current
             itemSlotNumber to check if the item slot is full. If it is, the
             number is changed to gree, otherwise it remains white.

      Input: itemName - string of the item whose count will be checked

     Output: none
    **************************************************************************/
    private void CheckForFullPanel(string itemName)
    {
          //stores number of items in current item slot
        int tempCount = int.Parse(itemCountTexts[itemSlotNumber].text);

        switch (itemName)
        {
            case "SmallFirstAidKit":
                if (tempCount == maxSmallFirstAidPanelCount)
                {
                    itemCountTexts[itemSlotNumber].color = fullColor; //green number
                }
                else
                {
                    itemCountTexts[itemSlotNumber].color = visible; //white number
                }
                break;
            case "LargeFirstAidKit":
                if (tempCount == maxLargeFirstAidPanelCount)
                {
                    itemCountTexts[itemSlotNumber].color = fullColor; //green number
                }
                else
                {
                    itemCountTexts[itemSlotNumber].color = visible; //white number
                }
                break;
            case "PistolAmmo":
                if (tempCount == maxPistolAmmoPanelCount)
                {
                    itemCountTexts[itemSlotNumber].color = fullColor; //green number
                }
                else
                {
                    itemCountTexts[itemSlotNumber].color = visible; //white number
                }
                break;
            case "ShotgunAmmo":
                if (tempCount == maxShotgunAmmoPanelCount)
                {
                    itemCountTexts[itemSlotNumber].color = fullColor; //green number
                }
                else
                {
                    itemCountTexts[itemSlotNumber].color = visible; //white number
                }
                break;
            case "RifleAmmo":
                if (tempCount == maxRifleAmmoPanelCount)
                {
                    itemCountTexts[itemSlotNumber].color = fullColor; //green number
                }
                else
                {
                    itemCountTexts[itemSlotNumber].color = visible; //white number
                }
                break;
            case "Fuel":
                if (tempCount == maxFuelPanelCount)
                {
                    itemCountTexts[itemSlotNumber].color = fullColor; //green number
                }
                else
                {
                    itemCountTexts[itemSlotNumber].color = visible; //white number
                }
                break;
            case "SupportSmallAidKit":
                if (tempCount == maxSmallFirstAidPanelCount)
                {
                    itemCountTexts[itemSlotNumber].color = fullColor; //green number
                }
                else
                {
                    itemCountTexts[itemSlotNumber].color = visible; //white number
                }
                break;
            case "SupportLargeAidKit":
                if (tempCount == maxLargeFirstAidPanelCount)
                {
                    itemCountTexts[itemSlotNumber].color = fullColor; //green number
                }
                else
                {
                    itemCountTexts[itemSlotNumber].color = visible; //white number
                }
                break;
            case "SupportPistolAmmo":
                if (tempCount == maxPistolAmmoPanelCount)
                {
                    itemCountTexts[itemSlotNumber].color = fullColor; //green number
                }
                else
                {
                    itemCountTexts[itemSlotNumber].color = visible; //white number
                }
                break;
            case "SupportShotgunAmmo":
                if (tempCount == maxShotgunAmmoPanelCount)
                {
                    itemCountTexts[itemSlotNumber].color = fullColor; //green number
                }
                else
                {
                    itemCountTexts[itemSlotNumber].color = visible; //white number
                }
                break;
            case "SupportRifleAmmo":
                if (tempCount == maxRifleAmmoPanelCount)
                {
                    itemCountTexts[itemSlotNumber].color = fullColor; //green number
                }
                else
                {
                    itemCountTexts[itemSlotNumber].color = visible; //white number
                }
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
    }

    /**************************************************************************
   Function: StoreItemPanels

Description: This function stores all item slot buttons into a single array for
             easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreItemPanels()
    {
        itemPanels[0] =  itemPanel1;
        itemPanels[1] =  itemPanel2;
        itemPanels[2] =  itemPanel3;
        itemPanels[3] =  itemPanel4;
        itemPanels[4] =  itemPanel5;
        itemPanels[5] =  itemPanel6;
        itemPanels[6] =  itemPanel7;
        itemPanels[7] =  itemPanel8;
        itemPanels[8] =  itemPanel9;
        itemPanels[9] =  itemPanel10;
        itemPanels[10] = itemPanel11;
        itemPanels[11] = itemPanel12;
        itemPanels[12] = itemPanel13;
        itemPanels[13] = itemPanel14;
        itemPanels[14] = itemPanel15;
        itemPanels[15] = itemPanel16;
        itemPanels[16] = itemPanel17;
        itemPanels[17] = itemPanel18;
    }

    /**************************************************************************
   Function: StoreItemImages

Description: This function stores all item slot images into a single array for 
             easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreItemImages()
    {
        itemImages[0] = itemImage1;
        itemImages[1] = itemImage2;
        itemImages[2] = itemImage3;
        itemImages[3] = itemImage4;
        itemImages[4] = itemImage5;
        itemImages[5] = itemImage6;
        itemImages[6] = itemImage7;
        itemImages[7] = itemImage8;
        itemImages[8] = itemImage9;
        itemImages[9] = itemImage10;
        itemImages[10] = itemImage11;
        itemImages[11] = itemImage12;
        itemImages[12] = itemImage13;
        itemImages[13] = itemImage14;
        itemImages[14] = itemImage15;
        itemImages[15] = itemImage16;
        itemImages[16] = itemImage17;
        itemImages[17] = itemImage18;
    }

    /**************************************************************************
   Function: StoreItemNameTexts

Description: This function stores all item slot name text objects into a single 
             array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreItemNameTexts()
    {
        itemNameTexts[0] = itemNameText1;
        itemNameTexts[1] = itemNameText2;
        itemNameTexts[2] = itemNameText3;
        itemNameTexts[3] = itemNameText4;
        itemNameTexts[4] = itemNameText5;
        itemNameTexts[5] = itemNameText6;
        itemNameTexts[6] = itemNameText7;
        itemNameTexts[7] = itemNameText8;
        itemNameTexts[8] = itemNameText9;
        itemNameTexts[9] = itemNameText10;
        itemNameTexts[10] = itemNameText11;
        itemNameTexts[11] = itemNameText12;
        itemNameTexts[12] = itemNameText13;
        itemNameTexts[13] = itemNameText14;
        itemNameTexts[14] = itemNameText15;
        itemNameTexts[15] = itemNameText16;
        itemNameTexts[16] = itemNameText17;
        itemNameTexts[17] = itemNameText18;
    }

    /**************************************************************************
   Function: StoreItemCountTexts

Description: This function stores all item slot count text objects into a
             single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreItemCountTexts()
    {
        itemCountTexts[0] =  itemCountText1;
        itemCountTexts[1] =  itemCountText2;
        itemCountTexts[2] =  itemCountText3;
        itemCountTexts[3] =  itemCountText4;
        itemCountTexts[4] =  itemCountText5;
        itemCountTexts[5] =  itemCountText6;
        itemCountTexts[6] =  itemCountText7;
        itemCountTexts[7] =  itemCountText8;
        itemCountTexts[8] =  itemCountText9;
        itemCountTexts[9] =  itemCountText10;
        itemCountTexts[10] = itemCountText11;
        itemCountTexts[11] = itemCountText12;
        itemCountTexts[12] = itemCountText13;
        itemCountTexts[13] = itemCountText14;
        itemCountTexts[14] = itemCountText15;
        itemCountTexts[15] = itemCountText16;
        itemCountTexts[16] = itemCountText17;
        itemCountTexts[17] = itemCountText18;
    }
}
