/******************************************************************************
  File Name: HUDManager.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains a function for manipulating the HUD and the
             player's health and stamina.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class HUDManager : MonoBehaviour
{
      //used to check if fuel is in inventory to activate generators automatically
    [SerializeField] private ItemManager itemManager = null;
      //used to update stamina bar in upgrade menu
    [SerializeField] private UpgradeManager upgradeManager = null;
      //image of the flashlight
    [SerializeField] private Sprite flashlightImage = null;
      //image of the weakness1 upgrade
    [SerializeField] private Sprite weakness1Image = null;
      //images of weapons
    [SerializeField] private Sprite pipeImage = null;
    [SerializeField] private Sprite pickaxeImage = null;
    [SerializeField] private Sprite pistolImage = null;
    [SerializeField] private Sprite shotgunImage = null;
    [SerializeField] private Sprite rifleImage = null;
      //images of consumable items
    [SerializeField] private Sprite largeFirstAidKitImage = null;
    [SerializeField] private Sprite smallFirstAidKitImage = null;
    [SerializeField] private Sprite pistolAmmoImage = null;
    [SerializeField] private Sprite shotgunAmmoImage = null;
    [SerializeField] private Sprite rifleAmmoImage = null;
    [SerializeField] private Sprite fuelImage = null;
      //images of the different types of essence
    [SerializeField] private Sprite blueEssenceSImage = null;
    [SerializeField] private Sprite blueEssenceMImage = null;
    [SerializeField] private Sprite blueEssenceLImage = null;
    [SerializeField] private Sprite yellowEssenceSImage = null;
    [SerializeField] private Sprite yellowEssenceMImage = null;
    [SerializeField] private Sprite yellowEssenceLImage = null;
    [SerializeField] private Sprite redEssenceSImage = null;
    [SerializeField] private Sprite redEssenceMImage = null;
    [SerializeField] private Sprite redEssenceLImage = null;
      //used to manipulate health bar
    [SerializeField] private Slider healthSlider = null;
      //used to keep health bar in the correct position
    [SerializeField] private RectTransform healthRectTransform = null;
      //used to manipulate stamina bar
    [SerializeField] private Slider staminaSlider = null;
      //the color of the stamina bar
    [SerializeField] private Image staminaBarFill = null;
      //used to keep stamina bar in the correct position
    [SerializeField] private RectTransform staminaRectTransform = null;
      //used to show stamina cap on HUD when an upgrade is equipped
    [SerializeField] private Slider staminaCapMarker = null;
      //used to ensure marker's slider is the same size as stamina bar
    [SerializeField] private RectTransform staminaCapRectTransform = null;
      //used to signify where the stamina is capped
    [SerializeField] private GameObject staminaCapMarkerHandle = null;
      //used to signify a new survival note is available
    [SerializeField] private Image survivalBook = null;
      //used to display an image of the item that is picked up
    [SerializeField] private Image popupImage = null;
      //used to darken screen when player picks something up
    [SerializeField] private GameObject popupImageBackground = null;
      //used to signify to the player something can be interacted with
    [SerializeField] private Text popupPrompt = null;
      //used to display descriptions, lore, and other information
    [SerializeField] private Text popupText = null;
      //used to hide HUD during any cutscenes or other events
    [SerializeField] private CanvasGroup canvasGroup = null;
      //used to communicate with the player
    [SerializeField] private FirstPersonController firstPersonController = null;
      //displays the name of the gun when gun is equipped
    [SerializeField] private Text weaponName = null;
      //shows current amount of ammo loaded in gun 
    [SerializeField] private Text loadedAmmoText = null;
      //shows max amount of ammo the gun holds at one time
    [SerializeField] private Text maxLoadedAmmoText = null;
      //melee weapons that will have their names displays when equipped
    [SerializeField] private Pipe pipe = null;
    [SerializeField] private Pickaxe pickaxe = null;
      //retrieves ammo in gun and max gun can hold to update HUD
    [SerializeField] private Pistol pistol = null;
    [SerializeField] private Shotgun shotgun = null;
    [SerializeField] private Rifle rifle = null;

    private const string generatorDetails = "This generator is out of fuel but looks like it still works.";
    private const string upgradeLeveledUpText = "You've gotten used to an upgrade and can now draw out more of its power.";
      //the default colors of health and "stamina"
    private Color defaultStaminaColor = new Color(0f, 0.5f, 0.5f, 1.0f); //Teal color
    //TODO: switch between this and upgrade color if upgrading health is implemented
    //private Color defaultHealthColor = new Color(1.0f, 0.0f, 0.0f, 1.0f); //Red color
      //the color of the stamina bar as stamina is increased
    private Color UpgradingStaminaColor = new Color(1.0f, 1.0f, 1.0f, 1.0f); //White color
      //reveals and hides image of item being picked up
    private Color invisible = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    private Color visible = new Color(1.0f, 1.0f, 1.0f, 1.0f);
      //signifies how full gun is
    private Color fullColor = Color.green;
    private Color partialColor = Color.white;
    private Color emptyColor = Color.red;
      //this is the smallest amount of current max stamina the player can have at any point
    private const float smallestMaxStaminaAllowed = 30.0f;
      //health the player has at the beginning of the game
    private const float defaultMaxHealth = 100.0f; 
      //stamina the player has at the beginning of the game
    private const float defaultMaxStamina = 100.0f; 
      //if stamina is reduced to 0, it takes this long before it begins to regenerate
    private const float defaultNoStaminaCooldown = 3.0f;
      //if stamina was just used, it takes this long before it begins to regenerate
    private const float defaultStaminaCooldown = 1.0f;
      //This is width which is displayed in the Inspector
    private const float defaultBarSize = 160.0f;
      //How long the "you picked up" message is displayed before the player can close it
    private const float defaultPromptLength = 0.35f;
    private const float delta = 0.019f; //used instead of delta time when time is paused
    private const float defaultNeedFuelLength = 3.0f; //how long "need fuel" message appears
    private const float defaultNoteRevealedLength = 5.0f; //how long the "note revealed" message appears
    private const float defaultUpgradeLevelUpLength = 5.0f; //how long "upgrade level up" message appears
    private const float defaultPickupCooldown = 0.35f; //how long until player can pick up an item again
    private const float defaultUpgradingStaminaDuration = 2.0f; //how long stamina bar takes to upgrade
    private float upgradingStaminaDuration;
    private float pickupCooldown; //player can pick up items when this is 0.0f
    private float needFuelMessageCounter;
    private float upgradeLevelUpMessageCounter;
    private float noteRevealedMessageCounter;
    private float promptLength; //how long image of pickup is displayed before player can remove it
    private float staminaCooldown; //must be 0 before stamina recovers
    private float maxHealth; //the player's current max health
    private float currentHealth; //the player's current health
    private float maxStamina; //the player's current max stamina
    private float currentMaxStamina; //the player's max stamina after upgrade penalties are applied
    private float currentStamina; //the player's current stamina
    private float staminaSprintRate; //rate stamina is used when sprinting
    private float staminaRegenRate; //rate stamina recovers
    private bool displayNoteMessage; //bool used to tell hud to display message
    private bool textDisplayed; //is true if a description or dialogue is displayed
    private bool imageDisplayed; //is true if an image is being displayed
    private bool fuelMessageDisplayed; //used to clear text if true
    private bool upgradeLevelUpMessageDisplayed; //used to clear text if true
    private bool noteMessageDisplayed; //used to clear note text if true

    void Start ()
    {
        ShowSurvivalIcon(false); //disable icon by default
          //rate at which stamina decreases
        staminaSprintRate = Time.deltaTime * 15.0f;
          //rate at which stamina regenerates
        staminaRegenRate = Time.deltaTime * 10.0f;

        SetStamina(ref maxStamina, defaultMaxStamina); //sets default max stamina
        SetStamina(ref currentStamina, maxStamina); //sets starting stamina
        SetHealth(ref maxHealth, defaultMaxHealth); //sets default max health
        SetHealth(ref currentHealth, maxHealth); //sets starting health
        promptLength = defaultPromptLength; //sets prompt length
        pickupCooldown = 0.0f;
    }

    void Update()
    {
        UpdateStaminaBar(); //continuously updates stamina bar to match stamina
        UpdateHealthBar(); //continuously updates health bar to match health

        if (needFuelMessageCounter > 0.0f) 
        {
            DecrementNeedFuelCounter(); //reduce fuel message counter
        }
        else if(fuelMessageDisplayed) //if fuel message is still displayed
        {
            ClearText(); //remove the text
            fuelMessageDisplayed = false; //fuel message is no longer displayed
        }

        if (upgradeLevelUpMessageCounter > 0.0f)
        {
            DecrementUpgradeLevelUpCounter(); //reduce level up message counter
        }
        else if (upgradeLevelUpMessageDisplayed) //if level up message is still displayed
        {
            ClearText(); //remove the text
            upgradeLevelUpMessageDisplayed = false; //level up message is no longer displayed
        }

        if(noteRevealedMessageCounter > 0.0f)
        {
            DecrementNoteRevealedCounter();
        }
        else if (noteMessageDisplayed)
        {
            ClearText(); //remove the text
            noteMessageDisplayed = false; //note message is no longer displayed
        }

        if(upgradingStaminaDuration > 0.0f)
        {
            FlashStaminaBar(defaultUpgradingStaminaDuration);
            DecrementUpgradingStaminaDuration();
        }
        else
        {
            staminaBarFill.color = defaultStaminaColor;
        }

        if (pickupCooldown > 0.0f)
        {
            DecrementPickupCooldown();
        }
    }

    /**************************************************************************
   Function: GetCurrentHealth

Description: This function retrieves the player's current health.

      Input: none

     Output: Returns the player's current health.
    **************************************************************************/
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    /**************************************************************************
   Function: SetCurrentHealth

Description: Given an integer, this function adds it to the player's current
             health. If the new current health value is greater than the
             current max health, it is clamped to that max health value.

      Input: value - integer added to the player's current health

     Output: none
    **************************************************************************/
    public void SetCurrentHealth(int value)
    {
        currentHealth += value; //adds value to currentHealth

        if(currentHealth > maxHealth) //checks if current health > max health
        {
            currentHealth = maxHealth; //clamps current health
        }
    }

    /**************************************************************************
   Function: GetMaxHealth

Description: This function returns the player's current max health.

      Input: none

     Output: Returns maxHealth float.
    **************************************************************************/
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    /**************************************************************************
   Function: GetStaminaCooldown

Description: This function returns the time remaining until stamina begins to
             regenerate.

      Input: none

     Output: Returns staminaCooldown float.
    **************************************************************************/
    public float GetStaminaCooldown()
    {
        return staminaCooldown;
    }

    /**************************************************************************
   Function: SetStaminaCooldown

Description: Given a bool, this function sets the cooldown based on whether or
             not the player has any stamina remaining.

      Input: empty - bool used to choose from different cooldown times

     Output: none
    **************************************************************************/
    public void SetStaminaCooldown(bool empty)
    {
        if(empty) //if player ran out of stamina
        {
            staminaCooldown = defaultNoStaminaCooldown; //longer cooldown
        }
        else
        {
            staminaCooldown = defaultStaminaCooldown; //shorter cooldown
        }
    }

    /**************************************************************************
   Function: DecrementStaminaCooldown

Description: This function reduces the staminaCooldown timer every frame.

      Input: none

     Output: none
    **************************************************************************/
    public void DecrementStaminaCooldown()
    {
        staminaCooldown -= Time.deltaTime;
    }

    /**************************************************************************
   Function: GetMaxStamina

Description: This function returns the player's current max stamina.

      Input: none

     Output: Returns maxStmina float.
    **************************************************************************/
    public float GetMaxStamina()
    {
        return maxStamina;
    }

    /**************************************************************************
   Function: GetCurrentStamina

Description: This function returns the player's current stamina value.

      Input: none

     Output: Returns the player's current stamina.
    **************************************************************************/
    public float GetCurrentStamina()
    {
        return currentStamina;
    }

    /**************************************************************************
   Function: GetSmallestMaxStaminaAllowed

Description: This function returns the smallest value that currentMaxStamina
             can ever be.

      Input: none

     Output: Returns smallest current max stamina the player can have.
    **************************************************************************/
    public float GetSmallestMaxStaminaAllowed()
    {
        return smallestMaxStaminaAllowed;
    }

    /**************************************************************************
   Function: RegenerateStamina

Description: This function increases the current stamina by a regeneration
             rate. Stamina is clamped to prevent it from exceeding max stamina.

      Input: none

     Output: none
    **************************************************************************/
    public void RegenerateStamina()
    {
        currentStamina += staminaRegenRate;

        if(upgradeManager.AnyUpgradeIsEquipped()) //checks if player has any upgrade equipped
        {
            currentMaxStamina = maxStamina - upgradeManager.GetStaminaPenalty();
        }
        else
        {
            currentMaxStamina = maxStamina;
        }

          //clamps currentStamina so it doesn't exceed maxStamina
        if(currentStamina > currentMaxStamina)
        {
            currentStamina = currentMaxStamina;
        }
          //if current max and max are not the same, display cap marker
        EnableStaminaMarker(currentMaxStamina != maxStamina);
          //if current and max are not the same, display cap marker in upgrade menu
        upgradeManager.EnableUpgradeStaminaMarker(currentMaxStamina != maxStamina);

        SetStaminaMarker(); //positions marker in correct location
          //sets upgrade manager's marker to correct location
        upgradeManager.GetInventoryStaminaCapMarker().value = currentMaxStamina;
    }

    /**************************************************************************
   Function: Sprint

Description: This function checks if the player's remaining stamina is greater
             than zero. If so, stamina is reduced by the sprint rate. Then
             remaining stamina is checked to determine which cooldown to
             apply.

      Input: none

     Output: none
    **************************************************************************/
    public void Sprint()
    {
        if(currentStamina > 0)
        {
              //decrement stamina at sprint rate
            currentStamina -= staminaSprintRate; 
        }

        CheckRemainingStamina();
    }

    /**************************************************************************
   Function: DecrementStamina

Description: Given a value, this function subtracts it from the player's 
             current stamina and ensures current stamina doesn't remain below 
             zero.

      Input: value - amount to subtract from current stamina

     Output: none
    **************************************************************************/
    public void DecrementStamina(float value)
    {
        currentStamina -= value;

        if(currentStamina < 0) //clamps stamina if it goes below zero
        {
            currentStamina = 0;
        }
    }

    /**************************************************************************
   Function: UpgradeHealth

Description: Given a float, this function adds that value to maxHealth, 
             increasing the player's max health. The player is then fully
             healed and the health bar's size and position is updated.

      Input: amount - value to add to max Health

     Output: none
    **************************************************************************/
    public void UpgradeHealth(float amount)
    {
        maxHealth += amount; //increases max health
        currentHealth = maxHealth; //sets current health to new max health
        IncreaseHealthBar(); //updates health bar size and sets new position
    }

    /**************************************************************************
   Function: UpgradeStamina

Description: Given a float, this function adds that value to maxStamina, 
             increasing the player's max stamina. The player's stamina is then 
             fully restored and the stamina bar's size and position is updated.

      Input: amount - value to add to max Health

     Output: none
    **************************************************************************/
    public void UpgradeStamina(float amount)
    {
        maxStamina += amount;
        currentStamina = maxStamina; //TODO: consider adding old max and new max for gradually increasing the bar
        IncreaseStaminaBar();
        RegenerateStamina(); //updates stamina bar's current values
        ResetUpgradingStaminaDuration();
    }

    /**************************************************************************
   Function: TakeDamage

Description: Given a float, this function subtracts that value from the
             player's current health.

      Input: damage - value to subtract from currentHealth

     Output: none
    **************************************************************************/
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        //TODO: write/call function that checks if player is dead
    }

    /**************************************************************************
   Function: ShowSurvivalIcon

Description: Given a bool, this function enables or disables the survival note
             icon based on the bool.

      Input: display - bool used to display/hide icon

     Output: none
    **************************************************************************/
    public void ShowSurvivalIcon(bool display)
    {
        if(display)
        {
            survivalBook.enabled = true; //icon is visible
        }
        else
        {
            survivalBook.enabled = false; //icon is hidden
        }
    }

    /**************************************************************************
   Function: DisplayHUD

Description: Given a bool, this function displays or hides the HUD. It is still
             active while hidden, but it doesn't accept input.

      Input: display - bool used to enable/disable HUD

     Output: none
       NOTE: could also set scale to (0, 0, 0) to 'hide' HUD
    **************************************************************************/
    public void DisplayHUD(bool display)
    {
        if(display)
        {
            canvasGroup.alpha = 1.0f; //display HUD canvas
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.alpha = 0.0f; //hides HUD canvas
            canvasGroup.blocksRaycasts = true;
        }
    }

    /**************************************************************************
   Function: DisplayPrompt

Description: This function retrieves the raycastHit information from the
             player and displays an interact prompt based on the tag of the
             gameObject the raycast hit.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayPrompt()
    {
        RaycastHit hit = firstPersonController.GetRaycastHit();

        switch (hit.collider.tag)
        {
            case "Door":
                //TODO: Get different statuses from each door before displaying prompt.
                //ex: Message will be different if door is open, closed, locked, unlocked, or broken.
                Door door = hit.collider.GetComponent<Door>();
                if(door.GetClosedDoorStatus())
                {
                    popupPrompt.text = "Open door";
                }
                else
                {
                    popupPrompt.text = "Close door";
                }
                break;
            case "SmallFirstAidKit":
                popupPrompt.text = "Take Small First Aid Kit";
                break;
            case "LargeFirstAidKit":
                popupPrompt.text = "Take Large First Aid Kit";
                break;
            case "PistolAmmo":
                popupPrompt.text = "Take Pistol Ammo";
                break;
            case "ShotgunAmmo":
                popupPrompt.text = "Take Shotgun Ammo";
                break;
            case "RifleAmmo":
                popupPrompt.text = "Take Rifle Ammo";
                break;
            case "Fuel":
                popupPrompt.text = "Take Fuel";
                break;
            case "Essence":
                popupPrompt.text = "Take Essence";
                break;
            case "Flashlight":
                popupPrompt.text = "Take flashlight";
                break;
            case "Pipe":
                popupPrompt.text = "Take pipe";
                break;
            case "Pickaxe":
                popupPrompt.text = "Take pickaxe";
                break;
            case "Pistol":
                popupPrompt.text = "Take Pistol";
                break;
            case "Shotgun":
                popupPrompt.text = "Take Shotgun";
                break;
            case "Rifle":
                popupPrompt.text = "Take Rifle";
                break;
            case "SupportLargeAidKit":
                popupPrompt.text = "Take Support Large First Aid Kit";
                break;
            case "SupportSmallAidKit":
                popupPrompt.text = "Take Support Small First Aid Kit";
                break;
            case "SupportPistolAmmo":
                popupPrompt.text = "Take Support Pistol Ammo";
                break;
            case "SupportShotgunAmmo":
                popupPrompt.text = "Take Support Shotgun Ammo";
                break;
            case "SupportRifleAmmo":
                popupPrompt.text = "Take Support Rifle Ammo";
                break;
            case "Weakness1":
                popupPrompt.text = "Take Upgrade";
                break;
            case "Weakness2":
                popupPrompt.text = "Take Upgrade";
                break;
            case "BlueEssenceS":
            case "BlueEssenceM":
            case "BlueEssenceL":
            case "YellowEssenceS":
            case "YellowEssenceM":
            case "YellowEssenceL":
            case "RedEssenceS":
            case "RedEssenceM":
            case "RedEssenceL":
                popupPrompt.text = "Take Essence";
                break;
            case "Generator":
                if(itemManager.GetCurrentInventoryItemCount("Fuel") <= 0)
                {
                    popupPrompt.text = "Examine generator";
                }
                else
                {
                    popupPrompt.text = "Turn on generator";
                }
                break;
            case "Document1":
            case "Document2":
            case "Document3":
            case "Document4":
            case "Document5":
            case "Document6":
            case "Document7":
            case "Document8":
            case "Document9":
            case "Document10":
            case "Document11":
            case "Document12":
            case "Document13":
            case "Document14":
            case "Document15":
            case "Document16":
            case "Document17":
            case "Document18":
            case "Document19":
            case "Document20":
                popupPrompt.text = "Take Document";
                break;
            default:
                //Debug.Log("Invalid tag");
                break;
        }
    }

    /**************************************************************************
   Function: ExamineObject

Description: Given a string, this function sets the HUD's popup text to a
             const string of the object and pauses the game until the player
             clicks or presses a button to clear the text.

      Input: objectName - string of the object to interact with

     Output: none
    **************************************************************************/
    public void ExamineObject(string objectName)
    {
        switch(objectName)
        {
            case "Generator":
                popupText.text = generatorDetails;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        textDisplayed = true; //text is now displayed
        PauseGameToDisplayText(true); //game is paused to show information
    }

    /**************************************************************************
   Function: ClearPrompt

Description: This function removes the text from the popupPrompt.

      Input: none

     Output: none
    **************************************************************************/
    public void ClearPrompt()
    {
        popupPrompt.text = "";
    }

    /**************************************************************************
   Function: PauseGameToDisplayText

Description: Given a bool, this function pauses or unpauses the game to display
             information based on the bool.
    
      Input: pause - bool used to pause or unpause the game.
    
     Output: none
    **************************************************************************/
    public void PauseGameToDisplayText(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0; //time is stopped
        }
        else
        {
            Time.timeScale = 1; //time is at normal rate
        }
    }

    /**************************************************************************
   Function: PauseGameForMenus

Description: Given a bool, this function pauses or unpauses the game to access
             the UI menus based on the bool.

      Input: pause - bool used to pause or unpause the game.

     Output: none
    **************************************************************************/
    public void PauseGameForMenus(bool pause)
    {
        if(pause)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true; //makes cursor visible

            Time.timeScale = 0; //time is stopped

            DisplayHUD(false);
        }
        else
        {
              //locks cursor to the middle of game window
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; //hides cursor

            Time.timeScale = 1; //time is at normal rate

            DisplayHUD(true);
        }
    }

    /**************************************************************************
   Function: DisplayPickUpText

Description: This function sets the popupText object with text to convey
             information about what the player picked up.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayPickUpText(string objectName)
    {
        switch (objectName)
        {
            case "LargeFirstAidKit":
                popupText.text = "Acquired a Large First Aid Kit";
                break;
            case "SmallFirstAidKit":
                popupText.text = "Acquired a Small First Aid Kit";
                break;
            case "PistolAmmo":
                popupText.text = "Acquired some Pistol Ammo";
                break;
            case "ShotgunAmmo":
                popupText.text = "Acquired some Shotgun Ammo";
                break;
            case "RifleAmmo":
                popupText.text = "Acquired some Rifle Ammo";
                break;
            case "SupportLargeAidKit":
                popupText.text = "Acquired a Support Large First Aid Kit";
                break;
            case "SupportSmallAidKit":
                popupText.text = "Acquired a Support Small First Aid Kit";
                break;
            case "SupportPistolAmmo":
                popupText.text = "Acquired some Support Pistol Ammo";
                break;
            case "SupportShotgunAmmo":
                popupText.text = "Acquired some Support Shotgun Ammo";
                break;
            case "SupportRifleAmmo":
                popupText.text = "Acquired some Support Rifle Ammo";
                break;
            case "Flashlight":
                popupText.text = "Acquired a Flashlight";
                break;
            case "Pipe":
                popupText.text = "Acquired a Pipe";
                break;
            case "Pickaxe":
                popupText.text = "Acquired a Pickaxe";
                break;
            case "Pistol":
                popupText.text = "Acquired a Pistol";
                break;
            case "Shotgun":
                popupText.text = "Acquired a Shotgun";
                break;
            case "Rifle":
                popupText.text = "Acquired a Rifle";
                break;
            case "Fuel":
                popupText.text = "Acquired Fuel";
                break;
            case "BlueEssenceS":
                popupText.text = "Aqcquired some Small Blue Essence";
                break;
            case "BlueEssenceM":
                popupText.text = "Aqcquired some Medium Blue Essence";
                break;
            case "BlueEssenceL":
                popupText.text = "Aqcquired some Large Blue Essence";
                break;
            case "YellowEssenceS":
                popupText.text = "Aqcquired some Small Yellow Essence";
                break;
            case "YellowEssenceM":
                popupText.text = "Aqcquired some Medium Yellow Essence";
                break;
            case "YellowEssenceL":
                popupText.text = "Aqcquired some Large Yellow Essence";
                break;
            case "RedEssenceS":
                popupText.text = "Aqcquired some Small Red Essence";
                break;
            case "RedEssenceM":
                popupText.text = "Aqcquired some Medium Red Essence";
                break;
            case "RedEssenceL":
                popupText.text = "Aqcquired some Large Red Essence";
                break;
            case "Document1":
            case "Document2":
            case "Document3":
            case "Document4":
            case "Document5":
            case "Document6":
            case "Document7":
            case "Document8":
            case "Document9":
            case "Document10":
            case "Document11":
            case "Document12":
            case "Document13":
            case "Document14":
            case "Document15":
            case "Document16":
            case "Document17":
            case "Document18":
            case "Document19":
            case "Document20":
                popupText.text = "Acquired a document"; //TODO: come back when documents have different names
                break;
            case "Weakness1":
                popupText.text = "Acquired upgrade \"Reveal Weakness 1\"";
                break;
            case "Weakness2":
                popupText.text = "Acquired upgrade \"Reveal Weakness 2\"";
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        textDisplayed = true; //text is now displayed
        PauseGameToDisplayText(true); //game is paused to show information
    }

    /**************************************************************************
   Function: DisplaySurvivalNoteRevealedText

Description: This function displays the "note revealed" message when the player
             performs an action that results in a survival note being revealed.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplaySurvivalNoteRevealedText()
    {
        popupText.text = "A new survival note has been revealed.";
        noteMessageDisplayed = true; //note message has been displayed
        ResetNoteRevealedCounter(); //countdown to remove message
    }

    /**************************************************************************
   Function: DisplayNeedFuelText

Description: This function displays the "need fuel" message if the player
             interacts with a generator without any fuel.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayNeedFuelText()
    {
        popupText.text = "You need fuel to power this generator.";
        fuelMessageDisplayed = true; //fuel message has been displayed
        ResetNeedFuelCounter(); //countdown to remove message
    }

    /**************************************************************************
   Function: DisplayUpgradeLevelUpText

Description: This function displays the "upgrade level up" message when any of
             the player's upgrades level up.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayUpgradeLevelUpText()
    {
        popupText.text = upgradeLeveledUpText;
        upgradeLevelUpMessageDisplayed = true; //leveled up message has been displayed
        ResetUpgradeLevelUpCounter(); //countdown to remove message
    }

    /**************************************************************************
   Function: DisplayNoRoomText

Description: This function pauses the game and displays a popup text message
             to the player that remains until the player clicks or presses a
             key.

      Input: none

     Output: none
    **************************************************************************/
    public void DisplayNoRoomText()
    {
        popupText.text = "There's no room in your inventory";
        textDisplayed = true;
        PauseGameToDisplayText(true);

        //NOTE: Weapons, Key Items, and Upgrades don't have this check in their
        //pickup code because there's no implementation of selling or discarding
        //any of those types of items and every one added to the game should fit 
        //in a slot.
    }

    /**************************************************************************
   Function: GetPopupText

Description: This function retrieves the popupText gameObject.

      Input: none

     Output: Returns the popupText gameObject.
    **************************************************************************/
    public Text GetPopupText()
    {
        return popupText;
    }

    /**************************************************************************
   Function: GetDisplayNoteMessageStatus

Description: This function returns the bool representing whether or not the
             "note revealed" message needs to be displayed.

      Input: none

     Output: Returns true if the "note revealed" message should be displayed,
             otherwise, returns false.
    **************************************************************************/
    public bool GetDisplayNoteMessageStatus()
    {
        return displayNoteMessage;
    }

    /**************************************************************************
   Function: SetDisplayNoteMessage

Description: Given a bool, this function sets the displayNoteMessage variable
             to the given bool.

      Input: status - bool the displayNoteMessage is set to

     Output: none
    **************************************************************************/
    public void SetDisplayNoteMessage(bool status)
    {
        displayNoteMessage = status;
    }

    /**************************************************************************
   Function: GetNeedFuelCounter

Description: This function returns a counter representing how long until the
             "need fuel" message is removed.

      Input: none

     Output: Returns how long until the fuel message is removed.
    **************************************************************************/
    public float GetNeedFuelCounter()
    {
        return needFuelMessageCounter;
    }

    /**************************************************************************
   Function: DecrementPromptCounter

Description: This function decrements the promptLength until the player can
             close the popup prompt's image and text. The variable delta is
             used instead of timeDelta because time is 0 when the game is
             paused.

      Input: none

     Output: none
    **************************************************************************/
    public void DecrementPromptCounter()
    {
        promptLength -= delta; 
    }

    /**************************************************************************
   Function: DecrementNeedFuelCounter

Description: This function decrements the needFuelMessageCounter by deltaTime.

      Input: none

     Output: none
    **************************************************************************/
    public void DecrementNeedFuelCounter()
    {
        needFuelMessageCounter -= Time.deltaTime;
    }

    /**************************************************************************
   Function: DecrementUpgradeLevelUpCounter

Description: This function decrements the upgradeLevelUpMessageCounter by 
             deltaTime.

      Input: none

     Output: none
    **************************************************************************/
    public void DecrementUpgradeLevelUpCounter()
    {
        upgradeLevelUpMessageCounter -= Time.deltaTime;
    }

    /**************************************************************************
   Function: DecrementNoteRevealedCounter

Description: This function decrements the noteRevealedMessageCounter by 
             deltaTime.

      Input: none

     Output: none
    **************************************************************************/
    public void DecrementNoteRevealedCounter()
    {
        noteRevealedMessageCounter -= Time.deltaTime;
    }

    /**************************************************************************
   Function: DecrementPickupCooldown

Description: This function decrements the pickupCooldown by delta. Delta is
             used instead of timeDelta because time is 0 when the game is
             paused.

      Input: none

     Output: none
    **************************************************************************/
    public void DecrementPickupCooldown()
    {
        pickupCooldown -= delta;
    }

    /**************************************************************************
   Function: ResetPickupCooldown

Description: This function sets pickupCooldown to its default value.

      Input: none

     Output: none
    **************************************************************************/
    public void ResetPickupCooldown()
    {
        pickupCooldown = defaultPickupCooldown;
    }

    /**************************************************************************
   Function: ResetNeedFuelCounter

Description: This function sets needFuelMessageCounter to its default value.

      Input: none

     Output: none
    **************************************************************************/
    public void ResetNeedFuelCounter()
    {
        needFuelMessageCounter = defaultNeedFuelLength;
    }

    /**************************************************************************
   Function: ResetUpgradeLevelUpCounter

Description: This function sets upgradeLevelUpMessageCounter to its default 
             value.

      Input: none

     Output: none
    **************************************************************************/
    public void ResetUpgradeLevelUpCounter()
    {
        upgradeLevelUpMessageCounter = defaultUpgradeLevelUpLength;
    }

    /**************************************************************************
   Function: ResetNoteRevealedCounter

Description: This function sets noteRevealedMessageCounter to its default 
             value.

      Input: none

     Output: none
    **************************************************************************/
    public void ResetNoteRevealedCounter()
    {
        noteRevealedMessageCounter = defaultNoteRevealedLength;
    }

    /**************************************************************************
   Function: GetPickupCooldown

Description: This function returns a variable representing how long until the
             player can pick up an item again.

      Input: none

     Output: Returns length of time until an item can be picked up again.
    **************************************************************************/
    public float GetPickupCooldown()
    {
        return pickupCooldown;
    }

    /**************************************************************************
   Function: GetPromptCounter

Description: This function returns the float that represents how long until the
             popup prompt's text and image can be closed.

      Input: none

     Output: Return how long until text/image of prompt can be closed.
    **************************************************************************/
    public float GetPromptCounter()
    {
        return promptLength;
    }
    
    /**************************************************************************
   Function: ClearText

Description: This function removes the text from popupText.

      Input: none

     Output: none
    **************************************************************************/
    public void ClearText()
    {
        popupText.text = "";
        promptLength = defaultPromptLength; //reset prompt timer
        textDisplayed = false; //text is no longer displayed
          
        PauseGameToDisplayText(false); //unpauses game
        PauseGameForMenus(false);
    }

    /**************************************************************************
   Function: GetDisplayImageStatus

Description: This function returns the bool that is set to true when a popup
             image is displayed and set to false when it's removed.

      Input: none

     Output: Returns true if popup image is displayed, otherwise, returns
             false.
    **************************************************************************/
    public bool GetDisplayImageStatus()
    {
        return imageDisplayed;
    }

    /**************************************************************************
   Function: DisplayImage

Description: Given a string, this function sets the popup image to a specific
             sprite based on the given string.

      Input: imageName - string used to determine which sprite to use

     Output: none
    **************************************************************************/
    public void DisplayImage(string imageName)
    {
        switch(imageName)
        {
            case "LargeFirstAidKit":
            case "SupportLargeAidKit":
                popupImage.sprite = largeFirstAidKitImage;
                break;
            case "SmallFirstAidKit":
            case "SupportSmallAidKit":
                popupImage.sprite = smallFirstAidKitImage;
                break;
            case "PistolAmmo":
            case "SupportPistolAmmo":
                popupImage.sprite = pistolAmmoImage;
                break;
            case "ShotgunAmmo":
            case "SupportShotgunAmmo":
                popupImage.sprite = shotgunAmmoImage;
                break;
            case "RifleAmmo":
            case "SupportRifleAmmo":
                popupImage.sprite = rifleAmmoImage;
                break;
            case "Fuel":
                popupImage.sprite = fuelImage;
                break;
            case "Flashlight":
                popupImage.sprite = flashlightImage;
                break;
            case "Pipe":
                popupImage.sprite = pipeImage;
                break;
            case "Pickaxe":
                popupImage.sprite = pickaxeImage;
                break;
            case "Pistol":
                popupImage.sprite = pistolImage;
                break;
            case "Shotgun":
                popupImage.sprite = shotgunImage;
                break;
            case "Rifle":
                popupImage.sprite = rifleImage;
                break;
            case "BlueEssenceS":
                popupImage.sprite = blueEssenceSImage;
                break;
            case "BlueEssenceM":
                popupImage.sprite = blueEssenceMImage;
                break;
            case "BlueEssenceL":
                popupImage.sprite = blueEssenceLImage;
                break;
            case "YellowEssenceS":
                popupImage.sprite = yellowEssenceSImage;
                break;
            case "YellowEssenceM":
                popupImage.sprite = yellowEssenceMImage;
                break;
            case "YellowEssenceL":
                popupImage.sprite = yellowEssenceLImage;
                break;
            case "RedEssenceS":
                popupImage.sprite = redEssenceSImage;
                break;
            case "RedEssenceM":
                popupImage.sprite = redEssenceMImage;
                break;
            case "RedEssenceL":
                popupImage.sprite = redEssenceLImage;
                break;
            case "Document1":
            case "Document2":
            case "Document3":
            case "Document4":
            case "Document5":
            case "Document6":
            case "Document7":
            case "Document8":
            case "Document9":
            case "Document10":
            case "Document11":
            case "Document12":
            case "Document13":
            case "Document14":
            case "Document15":
            case "Document16":
            case "Document17":
            case "Document18":
            case "Document19":
            case "Document20":
                //TODO: get images to display here
                break;
            case "Weakness1":
                popupImage.sprite = weakness1Image;
                break;
            case "Weakness2":
                popupImage.sprite = weakness1Image; //TODO: replace with weakness2Image
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        popupImage.color = visible; //makes image visible
        imageDisplayed = true; 
        popupImageBackground.SetActive(true); //enables darkened background
    }

    /**************************************************************************
   Function: HideImage

Description: This function disables the darkened background, resets the prompt
             timer, unpauses the game, and calls the clear image function.

      Input: none

     Output: none
    **************************************************************************/
    public void HideImage()
    {
        popupImageBackground.SetActive(false); //hides darkened panel
        ClearImage(); //makes sprite invisible and sets it to null
    }

    /**************************************************************************
   Function: ClearImage

Description: This function sets the popup image to null, makes it invisible,
             and sets the displayed bool to false to inform other scripts.

      Input: none

     Output: none
    **************************************************************************/
    public void ClearImage()
    {
        popupImage.sprite = null;
        popupImage.color = invisible;
        imageDisplayed = false; //image is no longer displayed
    }

    /**************************************************************************
   Function: GetTextDisplayed

Description: This function returns the status of whether or not text is
             currently being displayed.

      Input: none

     Output: Returns true if text is being displayed, otherwise, returns false.
    **************************************************************************/
    public bool GetTextDisplayed()
    {
        return textDisplayed;
    }

    /**************************************************************************
   Function: SetTextDisplayed

Description: Given a bool, this function sets the textDisplayed bool to the
             given bool.

      Input: display - bool used to set textDisplayed

     Output: none
    **************************************************************************/
    public void SetTextDisplayed(bool display)
    {
        textDisplayed = display;
    }

    /**************************************************************************
   Function: GetPauseStatus

Description: This function returns the status of whether or not the game is
             currently paused.

      Input: none

     Output: Returns true if game is paused, otherwise, returns false.
    **************************************************************************/
    public bool GetPauseStatus()
    {
        return (Time.timeScale == 0); //true if timeScale is zero
    }

    /**************************************************************************
   Function: CheckRemainingStamina

Description: This function checks the player's current stamina. The stamina 
             cooldown is set to a value based on whether or not the current
             stamina is greater than zero.

      Input: none

     Output: none
    **************************************************************************/
    public void CheckRemainingStamina()
    {
        if (currentStamina > 0.0f)
        {
            SetStaminaCooldown(false); //set to smaller cooldown
        }
        else
        {
            SetStaminaCooldown(true); //set to larger cooldown
        }
    }

    /**************************************************************************
   Function: SetGuNameText

Description: Given a string, this function changes the weaponName text object's
             text field to the given string.

      Input: nameOfGun - string of the gun to be used

     Output: none
    **************************************************************************/
    public void SetWeaponNameText(string nameOfWeapon)
    {
        weaponName.text = nameOfWeapon;
    }

    /**************************************************************************
   Function: SetLoadedAmmoText

Description: Given a string, this function changes the loadedAmmoText object's
             text field to the given string.

      Input: loadedAmount - string of the current ammo count in gun to be used

     Output: none
    **************************************************************************/
    public void SetLoadedAmmoText(string loadedAmount)
    {
        loadedAmmoText.text = loadedAmount;
    }

    /**************************************************************************
   Function: SetMaxLoadedAmmoText

Description: Given a string, this function changes the maxLoadedAmmoText 
             object's text field to the given string.

      Input: maxAmount - string of the max amount of ammo to be used

     Output: none
    **************************************************************************/
    public void SetMaxLoadedAmmoText(string maxAmount)
    {
        maxLoadedAmmoText.text = maxAmount;
    }

    /**************************************************************************
   Function: DisplayWeaponInfo

Description: Given a bool, this function either displays the gun name and ammo
             in the gun or hides it based on the status of the bool.

      Input: display - bool used to make the text visible or invisible

     Output: none
    **************************************************************************/
    public void DisplayWeaponInfo(bool display)
    {
        if(display)
        {
            weaponName.enabled = true;
            loadedAmmoText.enabled = true;
            maxLoadedAmmoText.enabled = true;
        }
        else
        {
            weaponName.enabled = false;
            loadedAmmoText.enabled = false;
            maxLoadedAmmoText.enabled = false;
        }
    }

    /**************************************************************************
   Function: UpdateWeaponInfo

Description: This function checks which weapon is equipped. If it's a gun, it 
             retrieves that gun's current ammo count and max amount it will 
             hold. These numbers are assigned to the current and max text 
             objects on the HUD.

      Input: none

     Output: none
    **************************************************************************/
    public void UpdateWeaponInfo()
    {
        int currentAmmoCount = 0; //clear the variable from last time it was used

        if(pipe.GetEquippedStatus())
        {
            loadedAmmoText.text = ""; //no ammo to display
        }
        else if(pickaxe.GetEquippedStatus())
        {
            loadedAmmoText.text = ""; //no ammo to display
        }
        else if(pistol.GetEquippedStatus()) //if pistol is equipped
        {
              //retrieve the current ammo in the pistol
            currentAmmoCount = pistol.GetMagazineAmmoCount();
              //check if pistol is full
            if (currentAmmoCount == pistol.GetMaxMagazineAmmoCount())
            {
                loadedAmmoText.color = fullColor; //make font green
            }
            else if(currentAmmoCount == 0)  //check if pistol is empty
            {
                loadedAmmoText.color = emptyColor; //make font red
            }
            else
            {
                loadedAmmoText.color = partialColor; //make font white
            }

              //convert integer to string and assign it to text field
            loadedAmmoText.text = currentAmmoCount.ToString();
        }
        else if(shotgun.GetEquippedStatus()) //if shotgun is equipped
        {
              //retrieve the current ammo in the shotgun
            currentAmmoCount = shotgun.GetMagazineAmmoCount();

            if (currentAmmoCount == shotgun.GetMaxMagazineAmmoCount())
            {
                loadedAmmoText.color = fullColor; //make font green
            }
            else if (currentAmmoCount == 0)  //check if pistol is empty
            {
                loadedAmmoText.color = emptyColor; //make font red
            }
            else
            {
                loadedAmmoText.color = partialColor; //make font white
            }

              //convert integer to string and assign it to text field
            loadedAmmoText.text = currentAmmoCount.ToString();
        }
        else if(rifle.GetEquippedStatus()) //if rifle is equipped
        {
              //retrieve the current ammo in the rifle
            currentAmmoCount = rifle.GetMagazineAmmoCount();

            if (currentAmmoCount == rifle.GetMaxMagazineAmmoCount())
            {
                loadedAmmoText.color = fullColor; //make font green
            }
            else if (currentAmmoCount == 0)  //check if pistol is empty
            {
                loadedAmmoText.color = emptyColor; //make font red
            }
            else
            {
                loadedAmmoText.color = partialColor; //make font white
            }

              //convert integer to string and assign it to text field
            loadedAmmoText.text = currentAmmoCount.ToString();
        }
    }

    /**************************************************************************
   Function: FlashStaminaBar

Description: Given a float, this function smoothly changes the stamina bar
             from its default color to upgrading color and remains at the
             upgrading color for the duration of the given float.

      Input: colorBDuration - float that represents how long to remain at
                              2nd color each time

     Output: none
    **************************************************************************/
    private void FlashStaminaBar(float colorBDuration)
    {
        staminaBarFill.color = Color.Lerp(defaultStaminaColor, UpgradingStaminaColor, Mathf.PingPong(Time.time, colorBDuration));
    }

    /**************************************************************************
   Function: DecrementUpgradingStaminaDuration

Description: This function decrements upgrading stamina duration every frame.

      Input: none

     Output: none
    **************************************************************************/
    private void DecrementUpgradingStaminaDuration()
    {
        upgradingStaminaDuration -= Time.deltaTime;
    }

    /**************************************************************************
   Function: ResetUpgradingStaminaDuration

Description: This function sets upgrading stamina duration to its default
             value.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetUpgradingStaminaDuration()
    {
        upgradingStaminaDuration = defaultUpgradingStaminaDuration;
    }

    /**************************************************************************
   Function: EnableStaminaMarker

Description: Given a bool, this function either hides or enables the handle
             of the invisible slider placed on top of the stamina bar used to
             signify the current max stamina cap.

      Input: enable - bool used to hide or enable stamina cap slider's handle

     Output: none
    **************************************************************************/
    private void EnableStaminaMarker(bool enable)
    {
        staminaCapMarkerHandle.SetActive(enable);
    }

    /**************************************************************************
   Function: SetStaminaMarker

Description: This function set's the invisible slider's value to the current
             max stamina.

      Input: none

     Output: none
    **************************************************************************/
    private void SetStaminaMarker()
    {
        staminaCapMarker.value = currentMaxStamina;
    }

    /**************************************************************************
   Function: IncreaseHealthBar

Description: This function calculates the healthBarSize before using it to
             set the new size and position of the health bar. Then the health
             bar's max value is set to the player's new max health. This is
             also applied to the health bar displayed in the inventory.

      Input: none

     Output: none
    **************************************************************************/
    private void IncreaseHealthBar()
    {
          //calculates new health bar size
        float healthBarSize = defaultBarSize + (maxHealth - defaultMaxHealth);
          //changes size and position of health bar
        healthRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, healthBarSize);
        healthSlider.maxValue = maxHealth; //sets slider's new max value
          //matches item manager's health bar to match HUD
        itemManager.GetInventoryHealthBarRectTransform().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, healthBarSize);
        itemManager.GetInventoryHealthBar().maxValue = maxHealth;
    }

    /**************************************************************************
   Function: IncreaseStaminaBar

Description: This function calculates the staminaBarSize before using it to
             set the new size and position of the stamina bar. Then the stamina
             bar's max value is set to the player's new max stamina. This is
             also applied to the stamina bar in the upgrade menu.

      Input: none

     Output: none
    **************************************************************************/
    private void IncreaseStaminaBar()
    {
          //calculates new health bar size
        float staminaBarSize = defaultBarSize + (maxStamina - defaultMaxStamina);
          //changes size and position of stamina bar
        staminaRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, staminaBarSize);
        staminaSlider.maxValue = maxStamina; //sets slider's new max value
          //matches size and position of stamina bar
        staminaCapRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, staminaBarSize);
        staminaCapMarker.maxValue = maxStamina; //matches stamina slider
          //changes size and position of stamina bar in upgrade menu to match hud
        upgradeManager.GetInventoryStaminaBarRectTransform().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, staminaBarSize);
        upgradeManager.GetInventoryStaminaBar().maxValue = maxStamina;
          //changes marker's slider to match upgrade menu's stamina bar
        upgradeManager.GetInventoryStaminaBarCapRectTransform().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, staminaBarSize);
        upgradeManager.GetInventoryStaminaCapMarker().maxValue = maxStamina;
    }

    /**************************************************************************
   Function: SetStamina

Description: Given a reference to a float and a float, the reference is set
             to the given float.

      Input: stamina - reference to a float to be set
             value   - float to set the referenced variable to

     Output: none
    **************************************************************************/
    private void SetStamina(ref float stamina, float value)
    {
        stamina = value;
    }

    /**************************************************************************
   Function: SetHealth

Description: Given a reference to a float and a float, the reference is set
             to the given float.

      Input: health - reference to a float to be set
             value   - float to set the referenced variable to

     Output: none
    **************************************************************************/
    private void SetHealth(ref float health, float value)
    {
        health = value;
    }

    /**************************************************************************
   Function: UpdateHealthBar

Description: This function sets the HUD's health bar's value and inventory's 
             health bar's value to the player's current health.

      Input: none

     Output: none
    **************************************************************************/
    private void UpdateHealthBar()
    {
        healthSlider.value = GetCurrentHealth();
        itemManager.GetInventoryHealthBar().value = GetCurrentHealth();
    }

    /**************************************************************************
   Function: UpdateStaminaBar

Description: This function sets the health bar's value to the player's current
             stamina.

      Input: none

     Output: none
    **************************************************************************/
    private void UpdateStaminaBar()
    {
        staminaSlider.value = GetCurrentStamina();

        upgradeManager.GetInventoryStaminaBar().value = GetCurrentStamina();
    }

    /**************************************************************************
   Function: SetMaxStamina

Description: Given a float, this function adds it the maxStamina.

      Input: max - new value to add to max stamina

     Output: none
    **************************************************************************/
    //TODO: could remove this and just use SetStamina
    public void SetMaxStamina(float max)
    {
        maxStamina += max;
    }
}