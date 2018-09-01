/******************************************************************************
  File Name: WeaponManager.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that manage the player's weapons.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
      //used to deselect weapon slots after equipping/secondary setting
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private Pipe pipe = null;
    [SerializeField] private Pickaxe pickaxe = null;
    [SerializeField] private Pistol pistol = null;
    [SerializeField] private Shotgun shotgun = null;
    [SerializeField] private Rifle rifle = null;
      //images to put in weapon slots and equipped/secondary slots
    [SerializeField] private Sprite pipeSprite = null;
    [SerializeField] private Sprite pickaxeSprite = null;
    [SerializeField] private Sprite pistolSprite = null;
    [SerializeField] private Sprite shotgunSprite = null;
    [SerializeField] private Sprite rifleSprite = null;
      //these make up the key item button in inventory sub tab
    [SerializeField] private Image weaponButtonImage = null;
    [SerializeField] private Text weaponButtonText = null;
      //displays weapon and information about it
    [SerializeField] private Image invDetailImage = null;
    [SerializeField] private Text invDetailText = null;
    [SerializeField] private Text invDetailNameText = null;
      //panels that contain weapon images
    [SerializeField] private GameObject primaryWeaponPanel = null;
    [SerializeField] private GameObject secondaryWeaponPanel = null;
      //images that show the current equipped and secondary weapons
    [SerializeField] private Image primaryWeaponImage = null;
    [SerializeField] private Image secondaryWeaponImage = null;
      //contains sliders to display a weapon's stats
    [SerializeField] private GameObject weaponStatPanel = null;
      //displays the name of the currently equipped weapon in the stat panel
    [SerializeField] private Text weaponNameText = null;
      //these display the weapon's numeric values in stat panel
    [SerializeField] private Text weaponDamageText = null;
    [SerializeField] private Text weaponRangeText = null;
    [SerializeField] private Text weaponReloadSpeedText = null;
    [SerializeField] private Text weaponRateOfFireText = null;
      //visually represents the weapon's damage
    [SerializeField] private Slider weaponDamageSlider = null;
      //shows upgraded weapon damage
    [SerializeField] private Slider upgradedWeaponDamageSlider = null;
      //the handle of the upgraded slider
    [SerializeField] private GameObject upgradeDamageHandle = null;
      //visually represents the weapon's range
    [SerializeField] private Slider weaponRangeSlider = null;
      //shows upgraded weapon range
    [SerializeField] private Slider upgradedWeaponRangeSlider = null;
      //the handle of the upgraded slider
    [SerializeField] private GameObject upgradeRangeHandle = null;
      //visually represents the weapon's reload speed
    [SerializeField] private Slider weaponReloadSpeedSlider = null;
      //shows upgraded weapon reload speed
    [SerializeField] private Slider upgradedWeaponReloadSpeedSlider = null;
      //the handle of the upgraded reload slider
    [SerializeField] private GameObject upgradedReloadHandle = null;
      //visually represents the weapon's rate of fire
    [SerializeField] private Slider weaponRateOfFireSlider = null;
      //shows upgraded weapon rate of fire
    [SerializeField] private Slider upgradedWeaponRateOfFireSlider = null;
      //the handle of the upgraded rate of fire slider
    [SerializeField] private GameObject upgradedRateOfFireHandle = null;
      //used to tell player if they can't set secondary weapon
    [SerializeField] private Text invMessageText = null;
      //these are the panels the weapon slots are children of
    [SerializeField] private GameObject weaponPanel1 = null;
    [SerializeField] private GameObject weaponPanel2 = null;
    [SerializeField] private GameObject weaponPanel3 = null;
    [SerializeField] private GameObject weaponPanel4 = null;
    [SerializeField] private GameObject weaponPanel5 = null;
    [SerializeField] private GameObject weaponPanel6 = null;
      //these are the weapon slots
    [SerializeField] private Button weaponPanelButton1 = null;
    [SerializeField] private Button weaponPanelButton2 = null;
    [SerializeField] private Button weaponPanelButton3 = null;
    [SerializeField] private Button weaponPanelButton4 = null;
    [SerializeField] private Button weaponPanelButton5 = null;
    [SerializeField] private Button weaponPanelButton6 = null;
      //these are the weapon images
    [SerializeField] private Image weaponImage1 = null;
    [SerializeField] private Image weaponImage2 = null;
    [SerializeField] private Image weaponImage3 = null;
    [SerializeField] private Image weaponImage4 = null;
    [SerializeField] private Image weaponImage5 = null;
    [SerializeField] private Image weaponImage6 = null;
      //these are the weapon names
    [SerializeField] private Text weaponNameText1 = null;
    [SerializeField] private Text weaponNameText2 = null;
    [SerializeField] private Text weaponNameText3 = null;
    [SerializeField] private Text weaponNameText4 = null;
    [SerializeField] private Text weaponNameText5 = null;
    [SerializeField] private Text weaponNameText6 = null;
      //these are the current number of rounds in each gun
    //NOTE: there shouldn't be any numbers displayed on melee weapons
    [SerializeField] private Text magazineAmmoCountText1 = null;
    [SerializeField] private Text magazineAmmoCountText2 = null;
    [SerializeField] private Text magazineAmmoCountText3 = null;
    [SerializeField] private Text magazineAmmoCountText4 = null;
    [SerializeField] private Text magazineAmmoCountText5 = null;
    [SerializeField] private Text magazineAmmoCountText6 = null;
      //these are the max rounds allowed in each gun at once
    //NOTE: there shouldn't be any numbers displayed on melee weapons
    [SerializeField] private Text maxMagazineAmmoCountText1 = null;
    [SerializeField] private Text maxMagazineAmmoCountText2 = null;
    [SerializeField] private Text maxMagazineAmmoCountText3 = null;
    [SerializeField] private Text maxMagazineAmmoCountText4 = null;
    [SerializeField] private Text maxMagazineAmmoCountText5 = null;
    [SerializeField] private Text maxMagazineAmmoCountText6 = null;
      //these are enabled when a weapon in this item slot is equipped
    [SerializeField] private Image weaponEquippedImage1 = null;
    [SerializeField] private Image weaponEquippedImage2 = null;
    [SerializeField] private Image weaponEquippedImage3 = null;
    [SerializeField] private Image weaponEquippedImage4 = null;
    [SerializeField] private Image weaponEquippedImage5 = null;
    [SerializeField] private Image weaponEquippedImage6 = null;

    private GameObject[] weaponPanels = new GameObject[6];
    private Button[] weaponPanelButtons = new Button[6];
    private Image[] weaponImages = new Image[6];
    private Text[] weaponNames = new Text[6];
    private Text[] magazineCounts = new Text[6];
    private Text[] maxMagazineCounts = new Text[6];
    private Image[] weaponEquippedImages = new Image[6];

    private Color visible = new Color(1.0f, 1.0f, 1.0f, 1.0f); //display image
    private Color invisible = new Color(0.0f, 0.0f, 0.0f, 0.0f); //hide image

    private Color fullColor = Color.green; //full magazine is green 
    private Color emptyColor = Color.red; //empty magazine is empty

    private const string pipeDetails = "A pipe used in construction. It's a decent makeshift weapon.";
    private const string pickaxeDetails = "An old pickaxe used for mining. It takes longer to swing than the pipe, but it deals much more damage.";
    private const string pistolDetails = "A reliable handgun with a high rate of fire.";
    private const string shotgunDetails = "A pump-action shotgun that deals large amounts of damage at close range but isn't effective at long range.";
    private const string rifleDetails = "An old rifle that has been used for hunting. It's slow to fire but deals massive damage per shot.";

    private string equippedWeaponName; //name of current equipped weapon
    private string secondaryWeaponName; //name of current secondary weapon
      //how long a message appears if the player tries to set secondary weapon
      //without an equipped weapon
    private const float defaultCantSetLength = 1.2f;
    private const float delta = 0.019f; //used instead of delta time when time is paused
    private float cantSetLength;
    //these are the max values of the upgrade sliders
    private const int maxDamage = 150;
    private const int maxRange = 250;
    private const int maxReloadSpeed = 10;
    private const int maxRateOfFire = 10;
    //most rounds that fit in gun before they need to be reloaded
    private const int maxPistolMagazineCount = 10;
    private const int maxShotgunMagazineCount = 8;
    private const int maxRifleMagazineCount = 5;
    private const int panelCount = 6; //number of weapon slots

    private int weaponSlotNumber; //number of specific slot
    private int ammoInMagazine; //number of rounds currently in magazine

    private bool secondaryWeaponEquipped; //must be true to quick swap weapons
    private bool primaryWeaponEquipped; //is true if any weapon is equipped


    void Start()
    {
          //stores all weapon slots, images, and texts into arrays
        StoreWeaponPanels();
        StoreWeaponPanelButtons();
        StoreWeaponImages();
        StoreWeaponNames();
        StoreWeaponMagazineAmmoCounts();
        StoreWeaponMaxMagazineAmmoCounts();
        StoreWeaponEquippedImages();

        SetUpgradeDamageSliderMax();
        SetUpgradeRangeSliderMax();
        SetUpgradeRateOfFireSliderMax();
        SetUpgradeReloadSpeedSliderMax();
    }

    void Update()
    {
        if (cantSetLength > 0.0f) //checks if counter is over zero
        {
            DecrementCantSetPromptCounter(); //keep reducing the counter
        }
        else
        {
            invMessageText.text = ""; //removes message
        }
    }

    /**************************************************************************
   Function: EnableWeaponStatPanel

Description: Given a bool, this function either displays or hides the weapon
             stat panel based on the bool.

      Input: enable - bool used to display or hide the panel

     Output: none
    **************************************************************************/
    public void EnableWeaponStatPanel(bool enable)
    {
        if (enable)
        {
            weaponStatPanel.SetActive(true);
        }
        else
        {
            weaponStatPanel.SetActive(false);
        }
    }

    /**************************************************************************
   Function: EnableDamageUpgradeHandle

Description: Given a bool, this function either displays or hides the damage 
             upgrade handle based on the bool.

      Input: enable - bool used to display or hide the handle

     Output: none
    **************************************************************************/
    public void EnableDamageUpgradeHandle(bool enable)
    {
        if (enable)
        {
            upgradeDamageHandle.SetActive(true);
        }
        else
        {
            upgradeDamageHandle.SetActive(false);
        }
    }

    /**************************************************************************
   Function: EnableRangeUpgradeHandle

Description: Given a bool, this function either displays or hides the range 
             upgrade handle based on the bool.

      Input: enable - bool used to display or hide the handle

     Output: none
    **************************************************************************/
    public void EnableRangeUpgradeSlider(bool enable)
    {
        if (enable)
        {
            upgradeRangeHandle.SetActive(true);
        }
        else
        {
            upgradeRangeHandle.SetActive(false);
        }
    }

    /**************************************************************************
   Function: EnableRateOfFireUpgradeHandle

Description: Given a bool, this function either displays or hides the rate of
             fire upgrade handle based on the bool.

      Input: enable - bool used to display or hide the handle

     Output: none
    **************************************************************************/
    public void EnableRateOfFireUpgradeSlider(bool enable)
    {
        if (enable)
        {
            upgradedRateOfFireHandle.SetActive(true);
        }
        else
        {
            upgradedRateOfFireHandle.SetActive(false);
        }
    }

    /**************************************************************************
   Function: EnableReloadSpeedUpgradeHandle

Description: Given a bool, this function either displays or hides the reload
             speed upgrade handle based on the bool.

      Input: enable - bool used to display or hide the handle

     Output: none
    **************************************************************************/
    public void EnableReloadSpeedUpgradeSlider(bool enable)
    {
        if (enable)
        {
            upgradedReloadHandle.SetActive(true);
        }
        else
        {
            upgradedReloadHandle.SetActive(false);
        }
    }

    /**************************************************************************
   Function: EquippedImageIsEmpty

Description: This function returns an expression which checks of the primary
             weapon slot image is empty.

      Input: none

     Output: Returns true if primary weapon slot image is empty, otherwise,
             returns false.
    **************************************************************************/
    public bool EquippedImageIsEmpty()
    {
        return primaryWeaponImage.sprite == null;
    }

    /**************************************************************************
   Function: SetPrimaryWeaponEquipped

Description: This function sets the primaryWeaponEquipped bool to true.

      Input: none

     Output: none
    **************************************************************************/
    public void SetPrimaryWeaponEquipped()
    {
        primaryWeaponEquipped = true;
    }

    /**************************************************************************
   Function: GetPrimaryWeaponEquipped

Description: This function returns the bool that represents whether or not
             the primary weapon is equipped.

      Input: none

     Output: Returns true if the primary weapon is equipped, otherwise, returns
             false.
    **************************************************************************/
    public bool GetPrimaryWeaponEquipped()
    {
        return primaryWeaponEquipped;
    }

    /**************************************************************************
   Function: EquippedImageIsEnabled

Description: This function checks if the "equipped" image is enabled and
             returns a bool based on this check.

      Input: none

     Output: Returns true if the "equipped" weapon image is active, otherwise,
             returns false.
    **************************************************************************/
    public bool EquippedImageIsEnabled()
    {
        return primaryWeaponImage.IsActive();
    }

    /**************************************************************************
   Function: EnableEquippedImage

Description: This function enables the "equipped" image on the weapon tab's
             screen.

      Input: enable - bool used to display or hide the panel

     Output: none
    **************************************************************************/
    public void EnableEquippedPanel(bool enable)
    {
        if (enable)
        {
            primaryWeaponPanel.SetActive(true);
        }
        else
        {
            primaryWeaponPanel.SetActive(false);
        }
    }

    /**************************************************************************
   Function: EnableEquippedImage

Description: This function enables the "secondary" image on the weapon tab's
             screen.

      Input: enable - bool used to display or hide the panel

     Output: none
    **************************************************************************/
    public void EnableSecondaryPanel(bool enable)
    {
        if (enable)
        {
            secondaryWeaponPanel.SetActive(true);
        }
        else
        {
            secondaryWeaponPanel.SetActive(false);
        }
    }

    /**************************************************************************
   Function: RemoveDetails

Description: This function clears the image and text from the detail panel.

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

Description: This function retrieves the name of the weapon at the current
             weapon slot then adds that specified weapon's image and details
             to the detail panel.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayDetails()
    {
        string weaponName = weaponNames[weaponSlotNumber].text;

        switch (weaponName)
        {
            case "Pipe":
                invDetailImage.color = visible;
                invDetailImage.sprite = pipeSprite;
                invDetailText.text = pipeDetails;
                invDetailNameText.text = weaponName;
                break;
            case "Pickaxe":
                invDetailImage.color = visible;
                invDetailImage.sprite = pickaxeSprite;
                invDetailText.text = pickaxeDetails;
                invDetailNameText.text = weaponName;
                break;
            case "Pistol":
                invDetailImage.color = visible;
                invDetailImage.sprite = pistolSprite;
                invDetailText.text = pistolDetails;
                invDetailNameText.text = weaponName;
                break;
            case "Shotgun":
                invDetailImage.color = visible;
                invDetailImage.sprite = shotgunSprite;
                invDetailText.text = shotgunDetails;
                invDetailNameText.text = weaponName;
                break;
            case "Rifle":
                invDetailImage.color = visible;
                invDetailImage.sprite = rifleSprite;
                invDetailText.text = rifleDetails;
                invDetailNameText.text = weaponName;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
    }

    /**************************************************************************
   Function: GetWeaponPanelButtons

Description: This function returns an array containing all the weapon slot
             buttons.

      Input: none

     Output: Returns the array of weapon slot buttons.
    **************************************************************************/
    public Button[] GetWeaponPanelButtons()
    {
        return weaponPanelButtons;
    }

    /**************************************************************************
   Function: SetEquippedWeaponName

Description: Given a string, this function sets the equippedWeapon string to
             the given string.

      Input: weaponName - string of weapon to store in equippedWeapon

     Output: none
    **************************************************************************/
    public void SetEquippedWeaponName(string weaponName)
    {
        equippedWeaponName = weaponName;
    }

    /**************************************************************************
   Function: SetSecondaryWeaponName

Description: Given a string, this function sets the secondaryWeapon string to
             the given string.

      Input: weaponName - string of weapon to store in secondaryWeapon

     Output: none
    **************************************************************************/
    public void SetSecondaryWeaponName(string weaponName)
    {
        secondaryWeaponName = weaponName;
    }

    /**************************************************************************
   Function: EquipWeapon

Description: This function gets the name of the weapon in the current weapon
             slot and equips it after unequipping the currently equipped
             weapon. The equipped weapon image in the inventory is also
             updated.

      Input: none

     Output: none
    **************************************************************************/
    public void EquipWeapon()
    {
        UnequipWeapon(); //unequips current weapon
          //retrieves current slot's weapon name
        string weaponName = weaponNames[weaponSlotNumber].text;

          //checks if weapon to be equipped is the secondary weapon
        if (weaponName == secondaryWeaponName)
        {
            SetSecondaryWeapon(equippedWeaponName);
        }

        switch (weaponName)
        {
            case "Pipe":
                pipe.EquipPipe();
                break;
            case "Pickaxe":
                pickaxe.EquipPickaxe();
                break;
            case "Pistol":
                pistol.EquipPistol();
                break;
            case "Shotgun":
                shotgun.EquipShotgun();
                break;
            case "Rifle":
                rifle.EquipRifle();
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        SetEquippedImage(weaponName); //puts sprite in equipped image
        UpdateWeaponStatPanel(); //updates stat panel with equipped weapon's stats

        uiManager.DeselectWeaponSlot();
        RemoveDetails();
    }

    /**************************************************************************
   Function: SetEquippedImage

Description: Given a string, this function puts the specified weapon in the
             "equipped" weapon image. This function also sets the text below
             the equipped weapon image to the name of the weapon.

      Input: weaponName - string of weapon to put in the image

     Output: none
    **************************************************************************/
    public void SetEquippedImage(string weaponName)
    {
        switch (weaponName)
        {
            case "Pipe":
                primaryWeaponImage.sprite = pipeSprite;
                primaryWeaponImage.color = visible;
                break;
            case "Pickaxe":
                primaryWeaponImage.sprite = pickaxeSprite;
                primaryWeaponImage.color = visible;
                break;
            case "Pistol":
                primaryWeaponImage.sprite = pistolSprite;
                primaryWeaponImage.color = visible;
                break;
            case "Shotgun":
                primaryWeaponImage.sprite = shotgunSprite;
                primaryWeaponImage.color = visible;
                break;
            case "Rifle":
                primaryWeaponImage.sprite = rifleSprite;
                primaryWeaponImage.color = visible;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        SetEquippedSlotImage();
    }

    /**************************************************************************
   Function: SetEquippedSlotImage

Description: Given a string, this function searches for the specified weapon 
             in the names array. If it's present, the red border is enabled
             to signify it is equipped. If the weapon is a secondary, its
             red border is also enabled.

      Input: weaponName - string of weapon to search for

     Output: none
    **************************************************************************/
    public void SetEquippedSlotImage()
    {
        for (int i = 0; i < panelCount; i++)
        {
            if(weaponNames[i].text == equippedWeaponName || weaponNames[i].text == secondaryWeaponName)
            {
                weaponEquippedImages[i].gameObject.SetActive(true);
            }

            if(weaponNames[i].text != equippedWeaponName && weaponNames[i].text != secondaryWeaponName)
            {
                weaponEquippedImages[i].gameObject.SetActive(false);
            }
        }
    }

    /**************************************************************************
   Function: EquipWeapon

Description: Given a string, this weapon equips the specified weapon and puts
             its image in the equipped slot.

      Input: weaponName - string of the weapon to equip

     Output: none
    **************************************************************************/
    public void EquipWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "Pipe":
                pipe.EquipPipe();
                primaryWeaponImage.sprite = pipeSprite;
                primaryWeaponImage.color = visible;
                break;
            case "Pickaxe":
                pickaxe.EquipPickaxe();
                primaryWeaponImage.sprite = pickaxeSprite;
                primaryWeaponImage.color = visible;
                break;
            case "Pistol":
                pistol.EquipPistol();
                primaryWeaponImage.sprite = pistolSprite;
                primaryWeaponImage.color = visible;
                break;
            case "Shotgun":
                shotgun.EquipShotgun();
                primaryWeaponImage.sprite = shotgunSprite;
                primaryWeaponImage.color = visible;
                break;
            case "Rifle":
                rifle.EquipRifle();
                primaryWeaponImage.sprite = rifleSprite;
                primaryWeaponImage.color = visible;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        SetEquippedSlotImage();
    }

    /**************************************************************************
   Function: UnequipWeapon

Description: This function checks which weapon is currently equipped and
             unequips it.

      Input: none

     Output: none
    **************************************************************************/
    public void UnequipWeapon()
    {
        if (pipe.GetEquippedStatus())
        {
            pipe.UnequipPipe();
        }
        else if (pickaxe.GetEquippedStatus())
        {
            pickaxe.UnequipPickaxe();
        }
        else if (pistol.GetEquippedStatus())
        {
            pistol.UnequipPistol();
        }
        else if (shotgun.GetEquippedStatus())
        {
            shotgun.UnequipShotgun();
        }
        else if (rifle.GetEquippedStatus())
        {
            rifle.UnequipRifle();
        }

        primaryWeaponImage.sprite = null; //removes current weapon image
        primaryWeaponImage.color = invisible; //makes image object invisible
          //disables equipped red border image
        weaponEquippedImages[weaponSlotNumber].gameObject.SetActive(false);
    }

    /**************************************************************************
   Function: SetSecondaryWeapon

Description: This function checks if the player already has a weapon equipped.
             Then it checks if a secondary weapon hasn't been equipped yet. If
             the player attempts to set their only weapon to secondary, they
             get a message informing them they need to keep a weapon equipped.
             Otherwise, the chosen weapon is put in the secondary weapon slot
             and can be quick swapped outside of the inventory.

      Input: none

     Output: none
    **************************************************************************/
    public void SetSecondaryWeapon()
    {
          //checks if there's no secondary weapon yet
        if (!secondaryWeaponEquipped)
        {
              //checks if pipe is equipped, and player is trying to set pipe to secondary
            if (pipe.GetEquippedStatus() && weaponNames[weaponSlotNumber].text == "Pipe")
            {
                invMessageText.text = "You need to keep a weapon equipped";
                ResetCantSetPromptCounter();
                return; //exit function early
            }
            else if (pickaxe.GetEquippedStatus() && weaponNames[weaponSlotNumber].text == "Pickaxe")
            {
                invMessageText.text = "You need to keep a weapon equipped";
                ResetCantSetPromptCounter();
                return; //exit function early
            }
            else if (pistol.GetEquippedStatus() && weaponNames[weaponSlotNumber].text == "Pistol")
            {
                invMessageText.text = "You need to keep a weapon equipped";
                ResetCantSetPromptCounter();
                return; //exit function early
            }
            else if (shotgun.GetEquippedStatus() && weaponNames[weaponSlotNumber].text == "Shotgun")
            {
                invMessageText.text = "You need to keep a weapon equipped";
                ResetCantSetPromptCounter();
                return; //exit function early
            }
            else if (rifle.GetEquippedStatus() && weaponNames[weaponSlotNumber].text == "Rifle")
            {
                invMessageText.text = "You need to keep a weapon equipped";
                ResetCantSetPromptCounter();
                return; //exit function early
            }
        }
          //checks if weapon to make secondary is currently equipped weapon
        if (weaponNames[weaponSlotNumber].text == equippedWeaponName)
        {
            string tempName = equippedWeaponName; //get name of weapon to make secondary

            UnequipWeapon(); //unequip current weapon
            EquipWeapon(secondaryWeaponName); //equip secondary weapon
            UpdateWeaponStatPanel(); //updates stat panel to show new equipped weapon's stats
            SetSecondaryWeapon(tempName); //set former equipped weapon to secondary
        }
        else
        {
            SetSecondaryWeapon(weaponNames[weaponSlotNumber].text);
        }

        secondaryWeaponImage.gameObject.SetActive(true);
        secondaryWeaponPanel.SetActive(true);

        SetEquippedSlotImage();

        uiManager.DeselectWeaponSlot();
        RemoveDetails();
    }

    /**************************************************************************
   Function: SetSecondaryWeapon

Description: Given a string, this function sets the specified weapon as the
             secondary weapon.

      Input: weaponName - string of weapon to set as secondary weapon

     Output: none
    **************************************************************************/
    public void SetSecondaryWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "Pipe":
                SetPipeAsSecondary();
                secondaryWeaponImage.sprite = pipeSprite;
                secondaryWeaponImage.color = visible; //makes image in weapon tab visible
                break;
            case "Pickaxe":
                SetPickaxeAsSecondary();
                secondaryWeaponImage.sprite = pickaxeSprite;
                secondaryWeaponImage.color = visible;
                break;
            case "Pistol":
                SetPistolAsSecondary();
                secondaryWeaponImage.sprite = pistolSprite;
                secondaryWeaponImage.color = visible;
                break;
            case "Shotgun":
                SetShotgunAsSecondary();
                secondaryWeaponImage.sprite = shotgunSprite;
                secondaryWeaponImage.color = visible;
                break;
            case "Rifle":
                SetRifleAsSecondary();
                secondaryWeaponImage.sprite = rifleSprite;
                secondaryWeaponImage.color = visible;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        secondaryWeaponName = weaponName; //stores name of secondary weapon for future comparison
        SetEquippedSlotImage(); //updates red border on the correct weapon slots
    }

    /**************************************************************************
   Function: SwapWeapons

Description: This function unequips the current weapon, sets it as the
             secondary weapon, and equips the previous secondary weapon.

      Input: none

     Output: none
    **************************************************************************/
    public void SwapWeapons()
    {
        string equippedWeaponName = "";
        secondaryWeaponName = ""; //empties secondary weapon string

        if (pipe.GetEquippedStatus()) //gets the current equipped weapon
        {
            equippedWeaponName = "Pipe";
        }
        else if (pickaxe.GetEquippedStatus())
        {
            equippedWeaponName = "Pickaxe";
        }
        else if (pistol.GetEquippedStatus())
        {
            equippedWeaponName = "Pistol";
        }
        else if (shotgun.GetEquippedStatus())
        {
            equippedWeaponName = "Shotgun";
        }
        else if (rifle.GetEquippedStatus())
        {
            equippedWeaponName = "Rifle";
        }

        if (pipe.GetSecondaryStatus()) //gets the current secondary weapon
        {
            secondaryWeaponName = "Pipe";
        }
        else if (pickaxe.GetSecondaryStatus())
        {
            secondaryWeaponName = "Pickaxe";
        }
        else if (pistol.GetSecondaryStatus())
        {
            secondaryWeaponName = "Pistol";
        }
        else if (shotgun.GetSecondaryStatus())
        {
            secondaryWeaponName = "Shotgun";
        }
        else if (rifle.GetSecondaryStatus())
        {
            secondaryWeaponName = "Rifle";
        }

        UnequipWeapon();
        EquipWeapon(secondaryWeaponName); //equip secondary weapon
        UpdateWeaponStatPanel(); //updates the weapon stat panel
        SetSecondaryWeapon(equippedWeaponName); //makes equipped weapon secondary
    }

    /**************************************************************************
   Function: SetPipeAsSecondary

Description: This function sets the pipe's secondary bool to true and tells
             the weapon manager that a secondary weapon has been equipped.

      Input: none

     Output: none
    **************************************************************************/
    private void SetPipeAsSecondary()
    {
        pipe.SetSecondaryStatus(true);

        pickaxe.SetSecondaryStatus(false);
        pistol.SetSecondaryStatus(false);
        shotgun.SetSecondaryStatus(false);
        rifle.SetSecondaryStatus(false);

        secondaryWeaponEquipped = true;
    }

    /**************************************************************************
   Function: SetPickaxeAsSecondary

Description: This function sets the pickaxe's secondary bool to true and tells
             the weapon manager that a secondary weapon has been equipped.

      Input: none

     Output: none
    **************************************************************************/
    private void SetPickaxeAsSecondary()
    {
        pickaxe.SetSecondaryStatus(true);

        pipe.SetSecondaryStatus(false);
        pistol.SetSecondaryStatus(false);
        shotgun.SetSecondaryStatus(false);
        rifle.SetSecondaryStatus(false);

        secondaryWeaponEquipped = true;
    }

    /**************************************************************************
   Function: SetPistolAsSecondary

Description: This function sets the pistol's secondary bool to true and tells
             the weapon manager that a secondary weapon has been equipped.

      Input: none

     Output: none
    **************************************************************************/
    private void SetPistolAsSecondary()
    {
        pistol.SetSecondaryStatus(true);

        pipe.SetSecondaryStatus(false);
        pickaxe.SetSecondaryStatus(false);
        shotgun.SetSecondaryStatus(false);
        rifle.SetSecondaryStatus(false);

        secondaryWeaponEquipped = true;
    }

    /**************************************************************************
   Function: SetShotgunAsSecondary

Description: This function sets the shotgun's secondary bool to true and tells
             the weapon manager that a secondary weapon has been equipped.

      Input: none

     Output: none
    **************************************************************************/
    private void SetShotgunAsSecondary()
    {
        shotgun.SetSecondaryStatus(true);

        pipe.SetSecondaryStatus(false);
        pickaxe.SetSecondaryStatus(false);
        pistol.SetSecondaryStatus(false);
        rifle.SetSecondaryStatus(false);

        secondaryWeaponEquipped = true;
    }

    /**************************************************************************
   Function: SetRifleAsSecondary

Description: This function sets the rifle's secondary bool to true and tells
             the weapon manager that a secondary weapon has been equipped.

      Input: none

     Output: none
    **************************************************************************/
    private void SetRifleAsSecondary()
    {
        rifle.SetSecondaryStatus(true);

        pipe.SetSecondaryStatus(false);
        pickaxe.SetSecondaryStatus(false);
        pistol.SetSecondaryStatus(false);
        shotgun.SetSecondaryStatus(false);

        secondaryWeaponEquipped = true;
    }

    /**************************************************************************
   Function: GetSecondaryStatus

Description: This function returns the bool that represents whether or not
             a secondary weapon is equipped.

      Input: none

     Output: Returns true if a secondary weapon is set, otherwise, returns
             false.
    **************************************************************************/
    public bool GetSecondaryStatus()
    {
        return secondaryWeaponEquipped;
    }

    /**************************************************************************
   Function: SetWeaponSlotNumber

Description: Given an integer, this function sets the current weapon slot
             number to the given integer.

      Input: weaponNumber - integer that's assigned to weaponSlotNumber

     Output: none
    **************************************************************************/
    public void SetWeaponSlotNumber(int weaponNumber)
    {
        weaponSlotNumber = weaponNumber;
    }

    /**************************************************************************
   Function: CheckForEmptyPanel

Description: This function checks each weapon panel in order and sets the
             weaponSlotNumber to the first empty slot found.

      Input: none

     Output: Returns true if an empty weapon panel was found, otherwise, 
             returns false.
    **************************************************************************/
    public bool CheckForEmptyPanel()
    {
        weaponSlotNumber = -1; //resets the variable
          //loops through all weapon panels
        for (int i = 0; i < panelCount; i++)
        {
            if (weaponImages[i].sprite == null)
            {
                weaponSlotNumber = i;
                break;
            }
        }

        return (weaponSlotNumber != -1); //true if an empty item slot was found
    }

    /**************************************************************************
   Function: SetCurrentMagazineCount

Description: Given an integer, this function sets the current ammo in the gun's
             magazine to the given integer.

      Input: ammoCount - integer assigned to ammoInMagazine

     Output: none
    **************************************************************************/
    public void SetCurrentMagazineCount(int ammoCount)
    {
        ammoInMagazine = ammoCount;

        if (pistol.GetEquippedStatus())
        {
            UpdateCurrentMagazineAmmo("Pistol");
            CheckForFullMagazine("Pistol");
        }
        else if (shotgun.GetEquippedStatus())
        {
            UpdateCurrentMagazineAmmo("Shotgun");
            CheckForFullMagazine("Shotgun");
        }
        else if (rifle.GetEquippedStatus())
        {
            UpdateCurrentMagazineAmmo("Rifle");
            CheckForFullMagazine("Rifle");
        }
    }

    /**************************************************************************
   Function: UpdateWeaponStatPanel

Description: This function retrieves the name of the currently equipped weapon
             and sets the sliders in the weapon stat panel to display its
             stats. Then it calls the UpdateWeaponSliders function.

      Input: none

     Output: none
    **************************************************************************/
    public void UpdateWeaponStatPanel()
    {
        switch (GetEquippedWeapon())
        {
            case "Pipe":
                weaponNameText.text = "Pipe";
                weaponDamageSlider.value = pipe.GetDamageForStatPanel();
                weaponRangeSlider.value = pipe.GetRangeForStatPanel();
                weaponReloadSpeedSlider.value = pipe.GetReloadSpeedForStatPanel();
                weaponRateOfFireSlider.value = pipe.GetRateOfFireForStatPanel();
                break;
            case "Pickaxe":
                weaponNameText.text = "Pickaxe";
                weaponDamageSlider.value = pickaxe.GetDamageForStatPanel();
                weaponRangeSlider.value = pickaxe.GetRangeForStatPanel();
                weaponReloadSpeedSlider.value = pickaxe.GetReloadSpeedForStatPanel();
                weaponRateOfFireSlider.value = pickaxe.GetRateOfFireForStatPanel();
                break;
            case "Pistol":
                weaponNameText.text = "Pistol";
                weaponDamageSlider.value = pistol.GetDamageForStatPanel();
                weaponRangeSlider.value = pistol.GetRangeForStatPanel();
                weaponReloadSpeedSlider.value = pistol.GetReloadSpeedForStatPanel();
                weaponRateOfFireSlider.value = pistol.GetRateOfFireForStatPanel();
                break;
            case "Shotgun":
                weaponNameText.text = "Shotgun";
                weaponDamageSlider.value = shotgun.GetDamageForStatPanel();
                weaponRangeSlider.value = shotgun.GetRangeForStatPanel();
                weaponReloadSpeedSlider.value = shotgun.GetReloadSpeedForStatPanel();
                weaponRateOfFireSlider.value = shotgun.GetRateOfFireForStatPanel();
                break;
            case "Rifle":
                weaponNameText.text = "Rifle";
                weaponDamageSlider.value = rifle.GetDamageForStatPanel();
                weaponRangeSlider.value = rifle.GetRangeForStatPanel();
                weaponReloadSpeedSlider.value = rifle.GetReloadSpeedForStatPanel();
                weaponRateOfFireSlider.value = rifle.GetRateOfFireForStatPanel();
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        UpdateWeaponUpgradeSliders();
        UpdateWeaponStatPanelTextValues();
    }

    /**************************************************************************
   Function: UpdateWeaponStatPanelTextValues

Description: This function checks which weapon is equipped and assigns its
             current values to the text objects at the end of each stat slider
             on the weapon stat panel.

      Input: none

     Output: none
    **************************************************************************/
    public void UpdateWeaponStatPanelTextValues()
    {
        switch(GetEquippedWeapon())
        {
            case "Pipe":
                weaponDamageText.text = pipe.GetUpgradedDamageForStatPanel().ToString();
                weaponRangeText.text = pipe.GetUpgradedRangeForStatPanel().ToString();
                weaponReloadSpeedText.text = pipe.GetUpgradedReloadSpeedForStatPanel().ToString();
                weaponRateOfFireText.text = pipe.GetUpgradedRateOfFireForStatPanel().ToString();
                break;
            case "Pickaxe":
                weaponDamageText.text = pickaxe.GetUpgradedDamageForStatPanel().ToString();
                weaponRangeText.text = pickaxe.GetUpgradedRangeForStatPanel().ToString();
                weaponReloadSpeedText.text = pickaxe.GetUpgradedReloadSpeedForStatPanel().ToString();
                weaponRateOfFireText.text = pickaxe.GetUpgradedRateOfFireForStatPanel().ToString();
                break;
            case "Pistol":
                weaponDamageText.text = pistol.GetUpgradedDamageForStatPanel().ToString();
                weaponRangeText.text = pistol.GetUpgradedRangeForStatPanel().ToString();
                weaponReloadSpeedText.text = pistol.GetUpgradedReloadSpeedForStatPanel().ToString();
                weaponRateOfFireText.text = pistol.GetUpgradedRateOfFireForStatPanel().ToString();
                break;
            case "Shotgun":
                weaponDamageText.text = shotgun.GetUpgradedDamageForStatPanel().ToString();
                weaponRangeText.text = shotgun.GetUpgradedRangeForStatPanel().ToString();
                weaponReloadSpeedText.text = shotgun.GetUpgradedReloadSpeedForStatPanel().ToString();
                weaponRateOfFireText.text = shotgun.GetUpgradedRateOfFireForStatPanel().ToString();
                break;
            case "Rifle":
                weaponDamageText.text = rifle.GetUpgradedDamageForStatPanel().ToString();
                weaponRangeText.text = rifle.GetUpgradedRangeForStatPanel().ToString();
                weaponReloadSpeedText.text = rifle.GetUpgradedReloadSpeedForStatPanel().ToString();
                weaponRateOfFireText.text = rifle.GetUpgradedRateOfFireForStatPanel().ToString();
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
    }

    /**************************************************************************
   Function: UpdateWeaponUpgradeSliders

Description: This function checks which weapon is equipped and assigns its
             current values to the upgrade sliders.

      Input: none

     Output: none
    **************************************************************************/
    public void UpdateWeaponUpgradeSliders()
    {
        switch (equippedWeaponName)
        {
            case "Pipe":
                upgradedWeaponDamageSlider.value = pipe.GetUpgradedDamageForStatPanel();
                upgradedWeaponRangeSlider.value = pipe.GetUpgradedRangeForStatPanel();
                upgradedWeaponReloadSpeedSlider.value = pipe.GetUpgradedReloadSpeedForStatPanel();
                upgradedWeaponRateOfFireSlider.value = pipe.GetUpgradedRateOfFireForStatPanel();
                break;
            case "Pickaxe":
                upgradedWeaponDamageSlider.value = pickaxe.GetUpgradedDamageForStatPanel();
                upgradedWeaponRangeSlider.value = pickaxe.GetUpgradedRangeForStatPanel();
                upgradedWeaponReloadSpeedSlider.value = pickaxe.GetUpgradedReloadSpeedForStatPanel();
                upgradedWeaponRateOfFireSlider.value = pickaxe.GetUpgradedRateOfFireForStatPanel();
                break;
            case "Pistol":
                upgradedWeaponDamageSlider.value = pistol.GetUpgradedDamageForStatPanel();
                upgradedWeaponRangeSlider.value = pistol.GetUpgradedRangeForStatPanel();
                upgradedWeaponReloadSpeedSlider.value = pistol.GetUpgradedReloadSpeedForStatPanel();
                upgradedWeaponRateOfFireSlider.value = pistol.GetUpgradedRateOfFireForStatPanel();
                break;
            case "Shotgun":
                upgradedWeaponDamageSlider.value = shotgun.GetUpgradedDamageForStatPanel();
                upgradedWeaponRangeSlider.value = shotgun.GetUpgradedRangeForStatPanel();
                upgradedWeaponReloadSpeedSlider.value = shotgun.GetUpgradedReloadSpeedForStatPanel();
                upgradedWeaponRateOfFireSlider.value = shotgun.GetUpgradedRateOfFireForStatPanel();
                break;
            case "Rifle":
                upgradedWeaponDamageSlider.value = rifle.GetUpgradedDamageForStatPanel();
                upgradedWeaponRangeSlider.value = rifle.GetUpgradedRangeForStatPanel();
                upgradedWeaponReloadSpeedSlider.value = rifle.GetUpgradedReloadSpeedForStatPanel();
                upgradedWeaponRateOfFireSlider.value = rifle.GetUpgradedRateOfFireForStatPanel();
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
    }

    /**************************************************************************
   Function: GetEquippedWeapon

Description: This function checks which weapon is equipped and returns the
             name of that weapon. If nothing is equipped, which shouldn't
             happen when the weapon tab is accessible, error is returned.

      Input: none

     Output: Returns a string of the currently equipped weapon. If something
             goes wrong, error is returned instead.
    **************************************************************************/
    private string GetEquippedWeapon()
    {
        if (pipe.GetEquippedStatus())
        {
            return "Pipe";
        }
        else if (pickaxe.GetEquippedStatus())
        {
            return "Pickaxe";
        }
        else if (pistol.GetEquippedStatus())
        {
            return "Pistol";
        }
        else if (shotgun.GetEquippedStatus())
        {
            return "Shotgun";
        }
        else if (rifle.GetEquippedStatus())
        {
            return "Rifle";
        }

        return "Error"; //this should never happen
    }

    /**************************************************************************
   Function: DecrementCantSetPromptCounter

Description: This function decrements the cantSetLength variable every frame.

      Input: none

     Output: none
    **************************************************************************/
    private void DecrementCantSetPromptCounter()
    {
        cantSetLength -= delta;
    }

    /**************************************************************************
   Function: ResetCantSetPromptCounter

Description: This function sets the cantSetLength variable to its default
             value.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetCantSetPromptCounter()
    {
        cantSetLength = defaultCantSetLength;
    }

    /**************************************************************************
   Function: UpdateCurrentMagazineAmmo

Description: Given a string, this function searches for the current equipped
             gun and updates its magazine ammo count in its weapon slot.

      Input: gunName - string used to update the specific gun's magazine count

     Output: none
    **************************************************************************/
    private void UpdateCurrentMagazineAmmo(string gunName)
    {
        for (int i = 0; i < panelCount; i++)
        {
            if (weaponNames[i].text == gunName)
            {
                magazineCounts[i].text = ammoInMagazine.ToString();
                return;
            }
        }

        //Debug.Log("Couldn't find gun");
    }

    /**************************************************************************
   Function: AddToEmptyPanel

Description: Given a string, this function adds the specified weapon to an 
             empty weapon slot.

      Input: weaponName - string of the weapon to add to an weapon slot

     Output: none
    **************************************************************************/
    public void AddToEmptyPanel(string weaponName)
    {
          //checks if weapon button hasn't been revealed yet
        if(!WeaponButtonIsVisible())
        {
            RevealWeaponButton();
        }

        switch (weaponName)
        {
            case "Pipe":
                  //makes weapon slot visible
                weaponPanels[weaponSlotNumber].SetActive(true);
                  //image of pipe
                weaponImages[weaponSlotNumber].sprite = pipeSprite;
                  //makes image visible
                weaponImages[weaponSlotNumber].color = visible;
                  //name of pipe
                weaponNames[weaponSlotNumber].text = "Pipe";
                break;
            case "Pickaxe":
                weaponPanels[weaponSlotNumber].SetActive(true);
                weaponImages[weaponSlotNumber].sprite = pickaxeSprite;
                weaponImages[weaponSlotNumber].color = visible;
                weaponNames[weaponSlotNumber].text = "Pickaxe";
                break;
            case "Pistol":
                weaponPanels[weaponSlotNumber].SetActive(true);
                weaponImages[weaponSlotNumber].sprite = pistolSprite;
                weaponImages[weaponSlotNumber].color = visible;
                weaponNames[weaponSlotNumber].text = "Pistol";
                  //number of bullets currently in pistol
                magazineCounts[weaponSlotNumber].text = pistol.GetMagazineAmmoCount().ToString();
                  //max number of bullets that fit in pistol at once
                maxMagazineCounts[weaponSlotNumber].text = "  / " + maxPistolMagazineCount.ToString();
                CheckForFullMagazine("Pistol");
                break;
            case "Shotgun":
                weaponPanels[weaponSlotNumber].SetActive(true);
                weaponImages[weaponSlotNumber].sprite = shotgunSprite;
                weaponImages[weaponSlotNumber].color = visible;
                weaponNames[weaponSlotNumber].text = "Shotgun";
                magazineCounts[weaponSlotNumber].text = shotgun.GetMagazineAmmoCount().ToString();
                maxMagazineCounts[weaponSlotNumber].text = "/ " + maxShotgunMagazineCount.ToString();
                CheckForFullMagazine("Shotgun");
                break;
            case "Rifle":
                weaponPanels[weaponSlotNumber].SetActive(true);
                weaponImages[weaponSlotNumber].sprite = rifleSprite;
                weaponImages[weaponSlotNumber].color = visible;
                weaponNames[weaponSlotNumber].text = "Rifle";
                magazineCounts[weaponSlotNumber].text = rifle.GetMagazineAmmoCount().ToString();
                maxMagazineCounts[weaponSlotNumber].text = "/ " + maxRifleMagazineCount.ToString();
                CheckForFullMagazine("Rifle");
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        SetEquippedSlotImage();
    }

    /**************************************************************************
   Function: ClearWeaponSlot

Description: This function clears the specified weapon slot of all text and
             its image.

      Input: none

     Output: none
    **************************************************************************/
    public void ClearWeaponSlot()
    {
        weaponImages[weaponSlotNumber].sprite = null; //removes the item image       
        weaponImages[weaponSlotNumber].color = invisible; //makes the item slot black         
        weaponNames[weaponSlotNumber].text = ""; //removes the name text        
        magazineCounts[weaponSlotNumber].text = ""; //removes the count text
        maxMagazineCounts[weaponSlotNumber].text = ""; //removes max count text
    }

    /**************************************************************************
   Function: CheckForFullMagazine

Description: Given a string, this function checks the index at the current
             weaponSlotNumber to check if the gun's magazine is full. If it 
             is, the number is changed to gree, otherwise it remains white.

      Input: itemName - string of the gun whose count will be checked

     Output: none
    **************************************************************************/
    public void CheckForFullMagazine(string gunName)
    {
          //loops through weapon slots to find current gun's weapon slot
        for (int i = 0; i < panelCount; i++)
        {
            if (weaponNames[i].text == gunName)
            {
                weaponSlotNumber = i;
            }
        }

          //stores number of rounds in current weapon slot
        int tempCount = int.Parse(magazineCounts[weaponSlotNumber].text);

        switch (gunName)
        {
            case "Pistol":
                if (tempCount == maxPistolMagazineCount)
                {
                    magazineCounts[weaponSlotNumber].color = fullColor; //green number
                }
                else if (tempCount > 0)
                {
                    magazineCounts[weaponSlotNumber].color = visible; //white number
                }
                else
                {
                    magazineCounts[weaponSlotNumber].color = emptyColor; //white number
                }
                break;
            case "Shotgun":
                if (tempCount == maxShotgunMagazineCount)
                {
                    magazineCounts[weaponSlotNumber].color = fullColor; //green number
                }
                else if (tempCount > 0)
                {
                    magazineCounts[weaponSlotNumber].color = visible; //white number
                }
                else
                {
                    magazineCounts[weaponSlotNumber].color = emptyColor; //white number
                }
                break;
            case "Rifle":
                if (tempCount == maxRifleMagazineCount)
                {
                    magazineCounts[weaponSlotNumber].color = fullColor; //green number
                }
                else if (tempCount > 0)
                {
                    magazineCounts[weaponSlotNumber].color = visible; //white number
                }
                else
                {
                    magazineCounts[weaponSlotNumber].color = emptyColor; //white number
                }
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
    }

    /**************************************************************************
   Function: RevealWeaponButton

Description: This function enables the weapon button image and sets its text
             to "Weapons".

      Input: none

     Output: none
    **************************************************************************/
    public void RevealWeaponButton()
    {
        weaponButtonImage.enabled = true;
        weaponButtonText.text = "Weapons";
    }

    /**************************************************************************
   Function: WeaponButtonIsVisible

Description: This function returns a bool that represents whether or not the
             weapon button image is enabled.

      Input: none

     Output: Returns true if the weapon button image is enabled, otherwise,
             returns false.
    **************************************************************************/
    private bool WeaponButtonIsVisible()
    {
        return weaponButtonImage.enabled;
    }

    /**************************************************************************
   Function: StoreWeaponPanelButtons

Description: This function stores all weapon slot buttons into a single array
             for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreWeaponPanelButtons()
    {
        weaponPanelButtons[0] = weaponPanelButton1;
        weaponPanelButtons[1] = weaponPanelButton2;
        weaponPanelButtons[2] = weaponPanelButton3;
        weaponPanelButtons[3] = weaponPanelButton4;
        weaponPanelButtons[4] = weaponPanelButton5;
        weaponPanelButtons[5] = weaponPanelButton6;
    }

    /**************************************************************************
   Function: StoreWeaponImages

Description: This function stores all weapon slot images into a single array
             for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreWeaponImages()
    {
        weaponImages[0] = weaponImage1;
        weaponImages[1] = weaponImage2;
        weaponImages[2] = weaponImage3;
        weaponImages[3] = weaponImage4;
        weaponImages[4] = weaponImage5;
        weaponImages[5] = weaponImage6;
    }

    /**************************************************************************
   Function: StoreWeaponNames

Description: This function stores all weapon slot name text objects into a 
             single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreWeaponNames()
    {
        weaponNames[0] = weaponNameText1;
        weaponNames[1] = weaponNameText2;
        weaponNames[2] = weaponNameText3;
        weaponNames[3] = weaponNameText4;
        weaponNames[4] = weaponNameText5;
        weaponNames[5] = weaponNameText6;
    }

    /**************************************************************************
   Function: StoreWeaponMagazineAmmoCounts

Description: This function stores all weapon slot current magazine ammo counts 
             into a single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreWeaponMagazineAmmoCounts()
    {
        magazineCounts[0] = magazineAmmoCountText1;
        magazineCounts[1] = magazineAmmoCountText2;
        magazineCounts[2] = magazineAmmoCountText3;
        magazineCounts[3] = magazineAmmoCountText4;
        magazineCounts[4] = magazineAmmoCountText5;
        magazineCounts[5] = magazineAmmoCountText6;
    }

    /**************************************************************************
   Function: StoreWeaponMaxMagazineAmmoCounts

Description: This function stores all weapon slot max magazine ammo counts 
             into a single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreWeaponMaxMagazineAmmoCounts()
    {
        maxMagazineCounts[0] = maxMagazineAmmoCountText1;
        maxMagazineCounts[1] = maxMagazineAmmoCountText2;
        maxMagazineCounts[2] = maxMagazineAmmoCountText3;
        maxMagazineCounts[3] = maxMagazineAmmoCountText4;
        maxMagazineCounts[4] = maxMagazineAmmoCountText5;
        maxMagazineCounts[5] = maxMagazineAmmoCountText6;
    }

    /**************************************************************************
   Function: SetUpgradeDamageSliderMax

Description: This function sets the upgraded weapon damage slider's max value
             to the maxDamage variable.

      Input: none

     Output: none
    **************************************************************************/
    private void SetUpgradeDamageSliderMax()
    {
        upgradedWeaponDamageSlider.maxValue = maxDamage;
    }

    /**************************************************************************
   Function: SetUpgradeRangeSliderMax

Description: This function sets the upgraded weapon range slider's max value
             to the maxRange variable.

      Input: none

     Output: none
    **************************************************************************/
    private void SetUpgradeRangeSliderMax()
    {
        upgradedWeaponRangeSlider.maxValue = maxRange;
    }

    /**************************************************************************
   Function: SetUpgradeReloadSpeedSliderMax

Description: This function sets the upgraded weapon reload speed slider's max 
             value to the maxReloadSpeed variable.

      Input: none

     Output: none
    **************************************************************************/
    private void SetUpgradeReloadSpeedSliderMax()
    {
        upgradedWeaponReloadSpeedSlider.maxValue = maxReloadSpeed;
    }

    /**************************************************************************
   Function: SetUpgradeRateOfFireSliderMax

Description: This function sets the upgraded weapon rate of fire slider's max 
             value to the maxRateOfFire variable.

      Input: none

     Output: none
    **************************************************************************/
    private void SetUpgradeRateOfFireSliderMax()
    {
        upgradedWeaponRateOfFireSlider.maxValue = maxRateOfFire;
    }

    /**************************************************************************
   Function: StoreWeaponEquippedImages

Description: This function stores all weapon slot equipped images into a 
             single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreWeaponEquippedImages()
    {
        weaponEquippedImages[0] = weaponEquippedImage1;
        weaponEquippedImages[1] = weaponEquippedImage2;
        weaponEquippedImages[2] = weaponEquippedImage3;
        weaponEquippedImages[3] = weaponEquippedImage4;
        weaponEquippedImages[4] = weaponEquippedImage5;
        weaponEquippedImages[5] = weaponEquippedImage6;
    }

    /**************************************************************************
   Function: StoreWeaponPanels

Description: This function stores all panels the weapon buttons are children of
             into a single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreWeaponPanels()
    {
        weaponPanels[0] = weaponPanel1;
        weaponPanels[1] = weaponPanel2;
        weaponPanels[2] = weaponPanel3;
        weaponPanels[3] = weaponPanel4;
        weaponPanels[4] = weaponPanel5;
        weaponPanels[5] = weaponPanel6;
    }
}
