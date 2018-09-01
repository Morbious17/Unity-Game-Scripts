/******************************************************************************
  File Name: UIManager.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that manages the user interface
             which includes navigating between different sections and 
             sub-sections within them.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
      //These will communicate with the UI, with functions being called on
      //certain button presses.
    [SerializeField] private HUDManager hudManager = null;
    [SerializeField] private ItemManager itemManager = null;
    [SerializeField] private WeaponManager weaponManager = null;
    [SerializeField] private KeyItemManager keyItemManager = null;
    [SerializeField] private UpgradeManager upgradeManager = null;
    [SerializeField] private SurvivalNoteManager survivalNoteManager = null;
    [SerializeField] private DocumentManager documentManager = null;
    [SerializeField] private MapManager mapManager = null;
    [SerializeField] private OptionsManager optionsManager = null;
      //is disabled when game is unpaused
    [SerializeField] private Canvas inventoryCanvas = null;
      //These are the five main tabs, each of which has their own sub tabs.
    [SerializeField] private Button inventoryButton = null;
    [SerializeField] private Button survivalButton = null;
    [SerializeField] private Button documentsButton = null;
    [SerializeField] private Button mapButton = null;
    [SerializeField] private Button optionsButton = null;
    [SerializeField] private Button closeButton = null;
      //These have all the children of their sub tabs, and hiding these
      //will hide all the children.
    [SerializeField] private GameObject inventoryBackPanel = null;
    [SerializeField] private GameObject survivalBackPanel = null;
    [SerializeField] private GameObject documentsBackPanel = null;
    [SerializeField] private GameObject mapBackPanel = null;
    [SerializeField] private GameObject optionsBackPanel = null;
      //These are the four sub tabs in the inventory panel.
    [SerializeField] private Button inventoryItemButton = null;
    [SerializeField] private Button inventoryWeaponButton = null;
    [SerializeField] private Button inventoryKeyItemButton = null;
    [SerializeField] private Button inventoryUpgradeButton = null;
      //These are the panels of each sub tab in the inventory panel.
      //Disabling one hides all the children for that sub tab.
    [SerializeField] private GameObject invItemsPanel = null;
    [SerializeField] private GameObject invWeaponPanel = null;
    [SerializeField] private GameObject invKeyItemPanel = null;
    [SerializeField] private GameObject invUpgradePanel = null;
      //These are the sub menus that are displayed when clicked.
      //Ideally, the buttons on the sub menus will call functions
      //on the specific item slot they are attached to.
    [SerializeField] private GameObject itemSubMenuPanel = null;
    [SerializeField] private GameObject weaponSubMenuPanel = null;
    [SerializeField] private GameObject keyItemSubMenuPanel = null;
    [SerializeField] private GameObject upgradeSubMenuPanel = null;
      //displays name, image, and description of items, key items, and weapons
    [SerializeField] private GameObject invDetailPanel = null;
      //default position of sub menu panel when it's not active
    private Vector3 defaultSubMenuPosition = new Vector3(-350, -350, 0);
      //position the subpanel should be placed for each item slot clicked
    [SerializeField] private GameObject itemSubMenuPosition1 = null;
    [SerializeField] private GameObject itemSubMenuPosition2 = null;
    [SerializeField] private GameObject itemSubMenuPosition3 = null;
    [SerializeField] private GameObject itemSubMenuPosition4 = null;
    [SerializeField] private GameObject itemSubMenuPosition5 = null;
    [SerializeField] private GameObject itemSubMenuPosition6 = null;
    [SerializeField] private GameObject itemSubMenuPosition7 = null;
    [SerializeField] private GameObject itemSubMenuPosition8 = null;
    [SerializeField] private GameObject itemSubMenuPosition9 = null;
    [SerializeField] private GameObject itemSubMenuPosition10 = null;
    [SerializeField] private GameObject itemSubMenuPosition11 = null;
    [SerializeField] private GameObject itemSubMenuPosition12 = null;
    [SerializeField] private GameObject itemSubMenuPosition13 = null;
    [SerializeField] private GameObject itemSubMenuPosition14 = null;
    [SerializeField] private GameObject itemSubMenuPosition15 = null;
    [SerializeField] private GameObject itemSubMenuPosition16 = null;
    [SerializeField] private GameObject itemSubMenuPosition17 = null;
    [SerializeField] private GameObject itemSubMenuPosition18 = null;
      //contains all the above game objects
    private GameObject[] itemSubMenuPositions = new GameObject[18];

      //position the subpanel should be placed for each weapon slot clicked
    [SerializeField] private GameObject weaponSubMenuPosition1 = null;
    [SerializeField] private GameObject weaponSubMenuPosition2 = null;
    [SerializeField] private GameObject weaponSubMenuPosition3 = null;
    [SerializeField] private GameObject weaponSubMenuPosition4 = null;
    [SerializeField] private GameObject weaponSubMenuPosition5 = null;
    [SerializeField] private GameObject weaponSubMenuPosition6 = null;
      //contains all the above game objects
    private GameObject[] weaponSubMenuPositions = new GameObject[6];

      //position the subpanel should be placed for each key item slot clicked
    [SerializeField] private GameObject keyItemSubMenuPosition1 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition2 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition3 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition4 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition5 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition6 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition7 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition8 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition9 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition10 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition11 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition12 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition13 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition14 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition15 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition16 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition17 = null;
    [SerializeField] private GameObject keyItemSubMenuPosition18 = null;
      //contains all the above game objects
    private GameObject[] keyItemSubMenuPositions = new GameObject[18];

      //position the subpanel should be placed for each item slot clicked
    [SerializeField] private GameObject upgradeSubMenuPosition1 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition2 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition3 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition4 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition5 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition6 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition7 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition8 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition9 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition10 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition11 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition12 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition13 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition14 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition15 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition16 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition17 = null;
    [SerializeField] private GameObject upgradeSubMenuPosition18 = null;
      //contains all the above game objects
    private GameObject[] upgradeSubMenuPositions = new GameObject[18];

    //These are buttons that will display a brief description of a hint when
    //it's revealed. Clicking on the revealed button will display a
    //screenshot and detailed description to aid the player.
    //TODO: add more notes when game is near completiong
    [SerializeField] private Button survivalNote1 = null;
    [SerializeField] private Button survivalNote2 = null;
    [SerializeField] private Button survivalNote3 = null;
    [SerializeField] private Button survivalNote4 = null;
    [SerializeField] private Button survivalNote5 = null;
    [SerializeField] private Button survivalNote6 = null;
    [SerializeField] private Button survivalNote7 = null;
    [SerializeField] private Button survivalNote8 = null;
    [SerializeField] private Button survivalNote9 = null;
    [SerializeField] private Button survivalNote10 = null;

    //This text object will contain all the text in the clicked document.
    [SerializeField] private Text detailedDocumentText;
    //This text object will display the title of the selected document.
    [SerializeField] private Text currentDocumentTitleText;
      //These buttons will be revealed as soon as the player enters that
      //specific location. Ex: Location2 could be revealed as hospital as soon
      //as the player enters the hospital.
    [SerializeField] private Button documentLocation1 = null;
    [SerializeField] private Button documentLocation2 = null;
    [SerializeField] private Button documentLocation3 = null;
    [SerializeField] private Button documentLocation4 = null;

      //These are simply empty spaces that contain question marks until the
      //player enters a location to reveal all the possible documents in that
      //location
    [SerializeField] private Button documentEmptySpace1 = null;
    [SerializeField] private Button documentEmptySpace2 = null;
    [SerializeField] private Button documentEmptySpace3 = null;
    [SerializeField] private Button documentEmptySpace4 = null;

      //These are the actual documents. They are revealed and can be viewed
      //again as soon as they are collected for the first time.
    [SerializeField] private Button document1 = null;
    [SerializeField] private Button document2 = null;
    [SerializeField] private Button document3 = null;
    [SerializeField] private Button document4 = null;
    [SerializeField] private Button document5 = null;
    [SerializeField] private Button document6 = null;
    [SerializeField] private Button document7 = null;
    [SerializeField] private Button document8 = null;
    [SerializeField] private Button document9 = null;
    [SerializeField] private Button document10 = null;
    [SerializeField] private Button document11 = null;
    [SerializeField] private Button document12 = null;
    [SerializeField] private Button document13 = null;
    [SerializeField] private Button document14 = null;
    [SerializeField] private Button document15 = null;
    [SerializeField] private Button document16 = null;
    [SerializeField] private Button document17 = null;
    [SerializeField] private Button document18 = null;
    [SerializeField] private Button document19 = null;
    [SerializeField] private Button document20 = null;
    [SerializeField] private Button document21 = null;

      //These buttons display the outskirts where the player starts.
    [SerializeField] private Button OutskirtsMapButton;
    [SerializeField] private Button Outskirts1FButton;
      //These buttons display the different levels of the hospital.
    [SerializeField] private Button HospitalMapButton;
    [SerializeField] private Button HospitalB2Button;
    [SerializeField] private Button HospitalB1Button;
    [SerializeField] private Button Hospital1FButton;
    [SerializeField] private Button Hospital2FButton;
    [SerializeField] private Button Hospital3FButton;
      //These buttons display the name of the town the game takes place in.
      //TODO: Change these and the buttons ingame to Lacier Nil.
    [SerializeField] private Button YearnAmharuMapButton;
    [SerializeField] private Button YearnAmharu1FButton;
      //These buttons display the different levels of the apartment.
    [SerializeField] private Button ApartmentsMapButton;
    [SerializeField] private Button ApartmentsB1Button;
    [SerializeField] private Button Apartments1FButton;
    [SerializeField] private Button Apartments2FButton;
    [SerializeField] private Button Apartments3FButton;
    [SerializeField] private Button Apartments4FButton;
      //These buttons display the different levels of the police station.
    [SerializeField] private Button PoliceStationMapButton;
    [SerializeField] private Button PoliceStation1FButton;
    [SerializeField] private Button PoliceStation2FButton;
      //These buttons display the different levels of the mine.
    [SerializeField] private Button AbandonedMinesMapButton;
    [SerializeField] private Button AbandonedMines1FButton;
    [SerializeField] private Button AbandonedMinesB1Button;
    [SerializeField] private Button AbandonedMinesB2Button;
    [SerializeField] private Button AbandonedMinesB3Button;

      //These buttons are in the options tab and display panels that contain
      //different settings based on the button that was clicked.
    [SerializeField] private Button AudioSettingsButton;
    [SerializeField] private Button VideoSettingsButton;
    [SerializeField] private Button ControlSettingsButton;
    [SerializeField] private Button GameplaySettingsButton;
    [SerializeField] private Button QuitGameButton;
      //These are the panels that are displayed when the above buttons are
      //clicked.
    [SerializeField] private GameObject AudioSettingsPanel;
    [SerializeField] private GameObject VideoSettingsPanel;
    [SerializeField] private GameObject ControlSettingsPanel;
    [SerializeField] private GameObject GameplaySettingsPanel;
      //These buttons are displayed when the "Controls Settings" button is
      //clicked.
    [SerializeField] private Button KeyboardControlsButton;
    [SerializeField] private GameObject KeyboardControlsPanel = null;
    [SerializeField] private Button KeyboardControlsCloseButton;
      //This slider will allow the player to zoom in and out of the map.
      //TODO: Try allowing the mouse wheel to do the same thing.
    [SerializeField] private Slider MapZoomSlider;

    private Button[] survivalNotes = new Button[10];
    //private Button[] documentLocations = new Button[4];
    //private Button[] documentEmptySpaces = new Button[4];
    private Button[] documents = new Button[21];
      //buttons that appear to be documents but are removed later in the game
    private Button[] documentDummyButtons = new Button[8];

      //arrays to contain all the item slots for each inventory sub tab
    private Button[] itemPanelButtons;
    private Button[] weaponPanelButtons;
    private Button[] keyItemPanelButtons;
    private Button[] upgradePanelButtons;

      //index of specific buttons to make active
    private int survivalButtonToActivate = 0;
    private int documentButtonToActivate = 0;
    private int dummyButtonToActivate = 0;
    private int itemSlotButtonToActivate = 0;
    private int weaponSlotButtonToActivate = 0;
    private int keyItemSlotButtonToActivate = 0;
    private int upgradeSlotButtonToActivate = 0;

    void Start()
    {
        itemPanelButtons = itemManager.GetItemPanels();
        weaponPanelButtons = weaponManager.GetWeaponPanelButtons();
        keyItemPanelButtons = keyItemManager.GetKeyItemPanelButtons();
        upgradePanelButtons = upgradeManager.GetUpgradePanelButtons();
        SetSurvivalNotes(); //stores notes in an array
        SetDocumentDummyButtons(); //stores document dummy buttons in an array
        SetDocuments(); //stores documents in an array
        SetItemSubMenuPositions(); //stores item sub menu positions in an array
        SetWeaponSubMenuPositions(); //stores weapon sub menu positions in an array
        SetKeyItemSubMenuPositions(); //stores key item sub menu positions in an array
        SetUpgradeSubMenuPositions(); //stores upgrade sub menu positions in an array
    }

    void Update()
    {
        //NOTE: this correctly retrieves the 18 item panels
        //foreach (Button buttons in itemPanels)
        //{
        //    Debug.Log("Button name is " + buttons.name);
        //}
    }



    /**************************************************************************
   Function: PressInventorybutton

Description: This function disables all other tabs, making the other main
             buttons clickable while making the inventory button activated and
             unclickable. The inventory tab is displayed and the four sub tabs
             are available to be clicked.

      Input: none

     Output: none
    **************************************************************************/
    public void PressInventoryButton()
    {
          //prevents button from being clickable because it's currently active
        inventoryButton.interactable = false;
        inventoryBackPanel.SetActive(true);  //activates the inventory panel

        PressInvItemButton(); //item sub tab is selected by default
          //hides all children and makes survival notes button clickable
        DeactivateSurvivalButton();
          //hides all children and makes documents button clickable
        DeactivateDocumentsButton();
          //hides all children and makes map button clickable
        DeactivateMapButton();
          //hides all children and makes options button clickable
        DeactivateOptionsButton();
          //hides all sub menus that are displayed when an item slot is clicked
        DeactivateSubPanels();
          //hides panel that shows keyboard controls
        DeactivateKeyboardContPanel();

        DeselectActiveDocument(); //deselects last clicked document
        DeselectActiveSurvivalNote(); //deselects last clicked survival note

          //unclicks last button clicked in options menu
        optionsManager.DeselectAllButtons();
        mapManager.DeselectAllButtons();
    }

    /**************************************************************************
   Function: PressSurvivalNotesbutton

Description: This function disables all other tabs, making the other main
             buttons clickable while making the survival notes button activated
             and unclickable. The list of collected and possible survival notes
             are now displayed for clicking.

      Input: none

     Output: none
    **************************************************************************/
    //TODO: these function salso had the active button as deselect(null)
    public void PressSurvivalNotesButton()
    {
        survivalButton.interactable = false;
        survivalBackPanel.SetActive(true);

        DeactivateInventoryButton();
        DeactivateDocumentsButton();
        DeactivateMapButton();
        DeactivateOptionsButton();
        DeactivateSubPanels();
        DeactivateKeyboardContPanel();

        DeselectActiveItemPanel(); //deselects last clicked item slot
        DeselectActiveWeapon(); //deselects last clicked weapon slot
        DeselectActiveKeyItem(); //deselects last clicked key item
        DeselectActiveUpgrade(); //deselects last clicked upgrade

        DeselectActiveDocument(); //deselects last clicked document

        optionsManager.DeselectAllButtons();
        mapManager.DeselectAllButtons();
    }

    /**************************************************************************
   Function: PressDocumentsButton

Description: This function disables all other tabs, making the other main
             buttons clickable while making the documents button activated and
             unclickable. The list of collected and possible documents are now
             available for clicking.

      Input: none

     Output: none
    **************************************************************************/
    public void PressDocumentButton()
    {
        documentsButton.interactable = false;
        documentsBackPanel.SetActive(true);

        DeactivateInventoryButton();
        DeactivateSurvivalButton();
        DeactivateMapButton();
        DeactivateOptionsButton();
        DeactivateSubPanels();
        DeactivateKeyboardContPanel();

        DeselectActiveItemPanel();
        DeselectActiveWeapon();
        DeselectActiveKeyItem();
        DeselectActiveUpgrade();

        DeselectActiveSurvivalNote();

        optionsManager.DeselectAllButtons();
        mapManager.DeselectAllButtons();
    }

    /**************************************************************************
   Function: PressMapbutton

Description: This function disables all other tabs, making the other main
             buttons clickable while making the map button activated and
             unclickable. The maps and floors of each collected map or explored
             area are available for clicking.

      Input: none

     Output: none
    **************************************************************************/
    public void PressMapButton()
    {
        mapButton.interactable = false;
        mapBackPanel.SetActive(true);

        DeactivateInventoryButton();
        DeactivateSurvivalButton();
        DeactivateDocumentsButton();
        DeactivateOptionsButton();
        DeactivateSubPanels();
        DeactivateKeyboardContPanel();

        DeselectActiveItemPanel();
        DeselectActiveWeapon();
        DeselectActiveKeyItem();
        DeselectActiveUpgrade();

        DeselectActiveSurvivalNote();
        DeselectActiveDocument();

        optionsManager.DeselectAllButtons();
        mapManager.UpdateContentScrollBars();
    }

    /**************************************************************************
   Function: PressOptionsbutton

Description: This function disables all other tabs, making the other main
             buttons clickable while making the options button activated and
             unclickable. The different settings are now available for
             clicking.

      Input: none

     Output: none
    **************************************************************************/
    public void PressOptionsButton()
    {
        optionsButton.interactable = false;
        optionsBackPanel.SetActive(true);

        DeactivateInventoryButton();
        DeactivateSurvivalButton();
        DeactivateDocumentsButton();
        DeactivateMapButton();
        DeactivateSubPanels();
        DeactivateKeyboardContPanel();

        DeselectActiveItemPanel();
        DeselectActiveWeapon();
        DeselectActiveKeyItem();
        DeselectActiveUpgrade();

        DeselectActiveSurvivalNote();
        DeselectActiveDocument();

        mapManager.DeselectAllButtons();
    }

    /**************************************************************************
   Function: PressClosebutton

Description: This function deselects all clicked buttons on every tab,
             locks the cursor and hides it, and unpauses the game.

      Input: none

     Output: none
    **************************************************************************/
    public void PressCloseButton()
    {

        inventoryCanvas.enabled = false; //hides user interface
          //deselects the last clicked item slot, document, survival note
          //regardless of which one the player clicked before clicking close
        DeselectActiveItemPanel();
        DeselectActiveWeapon();
        DeselectActiveKeyItem();
        DeselectActiveUpgrade();
        DeselectActiveSurvivalNote();
        DeselectActiveDocument();
        DeactivateSubPanels();

        optionsManager.DeselectAllButtons();
        mapManager.DeselectAllButtons();

        Cursor.lockState = CursorLockMode.Locked; //locks mouse cursor
        Cursor.visible = false; //makes cursor invisible

        closeButton.interactable = true;
        closeButton.OnDeselect(null);

        EnableInventorySubTabs(); //enables all inventory subtabs for pickups to detect scripts
        ReactivateMainButtons();

        hudManager.PauseGameForMenus(false); //unpauses game
    }

    /**************************************************************************
   Function: PressInvItemButton

Description: This function deselects all other sub tab buttons and hides their
             panels and item slots while displaying the item button's panel
             and item slots.

      Input: none

     Output: none
    **************************************************************************/
    public void PressInvItemButton()
    {
        inventoryItemButton.interactable = false;
        invItemsPanel.SetActive(true);

        if(itemManager.EssenceSlotEnabled()) //checks if any essence has been acquired
        {
            itemManager.EnableCurrencyPanel(true);
        }

        DeactivateInventoryWeaponButton();
        DeactivateInventoryKeyItemButton();
        DeactivateInventoryUpgradeButton();

        DeactivateSubPanels();

        DeselectActiveItemPanel();
        DeselectActiveWeapon();
        DeselectActiveKeyItem();
        DeselectActiveUpgrade();

        EnableInvDetailPanel();
    }

    /**************************************************************************
   Function: PressInvWeaponButton

Description: This function deselects all other sub tab buttons and hides their
             panels and item slots while displaying the weapon button's panel
             and weapon slots.

      Input: none

     Output: none
    **************************************************************************/
    public void PressInvWeaponButton()
    {
        inventoryWeaponButton.interactable = false;
        invWeaponPanel.SetActive(true);

        if(!weaponManager.EquippedImageIsEmpty())
        {
            weaponManager.EnableWeaponStatPanel(true);
            weaponManager.EnableEquippedPanel(true);
        }

        if(weaponManager.GetSecondaryStatus())
        {
            weaponManager.EnableSecondaryPanel(true);
        }

        DeactivateInventoryItemButton();
        DeactivateInventoryKeyItemButton();
        DeactivateInventoryUpgradeButton();

        DeactivateSubPanels();

        DeselectActiveItemPanel();
        DeselectActiveWeapon();
        DeselectActiveKeyItem();
        DeselectActiveUpgrade();

        EnableInvDetailPanel();
    }

    /**************************************************************************
   Function: PressInvKeyItemButton

Description: This function deselects all other sub tab buttons and hides their
             panels and item slots while displaying the key item button's panel
             and key item slots.

      Input: none

     Output: none
    **************************************************************************/
    public void PressInvKeyItemButton()
    {
        inventoryKeyItemButton.interactable = false;
        invKeyItemPanel.SetActive(true);

        DeactivateInventoryItemButton();
        DeactivateInventoryWeaponButton();
        DeactivateInventoryUpgradeButton();

        DeactivateSubPanels();

        DeselectActiveItemPanel();
        DeselectActiveWeapon();
        DeselectActiveKeyItem();
        DeselectActiveUpgrade();

        EnableInvDetailPanel();
    }

    /**************************************************************************
   Function: PressInvUpgradeButton

Description: This function deselects all other sub tab buttons and hides their
             panels and item slots while displaying the upgrade button's panel
             and upgrade slots.

      Input: none

     Output: none
    **************************************************************************/
    public void PressInvUpgradeButton()
    {
        inventoryUpgradeButton.interactable = false;
        invUpgradePanel.SetActive(true);

        DeactivateInventoryItemButton();
        DeactivateInventoryWeaponButton();
        DeactivateInventoryKeyItemButton();

        DeactivateSubPanels();

        DeselectActiveItemPanel(); //deactivates last chosen item slot
        DeselectActiveWeapon();  //deactivates last chosen weapon slot
        DeselectActiveKeyItem(); //deactivates last chosen key item slot
        DeselectActiveUpgrade(); //deactivates last chosen upgrade slot

        DeactivateInvDetailPanel(); //hides panel for upgrade sub tab
    }

    /**************************************************************************
   Function: DeselectActiveItemPanel

Description: This function deselects the last item slot the player clicked and 
             removes any details if an item was examined.

      Input: none

     Output: none
    **************************************************************************/
    public void DeselectActiveItemPanel()
    {
        itemPanelButtons[itemSlotButtonToActivate].interactable = true;
        itemPanelButtons[itemSlotButtonToActivate].OnDeselect(null);
        itemManager.RemoveDetails(); //removes any details if there are some
    }

    /**************************************************************************
   Function: DeactivateInvDetailPanel

Description: This function disables the inventory detail panel.

      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateInvDetailPanel()
    {
        invDetailPanel.SetActive(false);
    }

    /**************************************************************************
   Function: EnableInvDetailPanel

Description: This function enables the inventory detail panel.

      Input: none

     Output: none
    **************************************************************************/
    private void EnableInvDetailPanel()
    {
        invDetailPanel.SetActive(true);
    }

    /**************************************************************************
   Function: DeselectActiveDocument

Description: This function deselects the last document the player clicked.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectActiveDocument()
    {
        documents[documentButtonToActivate].interactable = true;
        documents[documentButtonToActivate].OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectActiveSurvivalNote

Description: This function deselects the last survival note the player clicked.

      Input: none

     Output: none
    **************************************************************************/
    public void DeselectActiveSurvivalNote()
    {
        survivalNotes[survivalButtonToActivate].interactable = true;
        survivalNotes[survivalButtonToActivate].OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectActiveWeapon

Description: This function deselects the last weapon the player clicked.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectActiveWeapon()
    {
        weaponPanelButtons[weaponSlotButtonToActivate].interactable = true;
        weaponPanelButtons[weaponSlotButtonToActivate].OnDeselect(null);
        weaponManager.RemoveDetails();
    }

    /**************************************************************************
   Function: DeselectActiveKeyItem

Description: This function deselects the last key item the player clicked.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectActiveKeyItem()
    {
        keyItemPanelButtons[keyItemSlotButtonToActivate].interactable = true;
        keyItemPanelButtons[keyItemSlotButtonToActivate].OnDeselect(null);
        itemManager.RemoveDetails();
    }

    /**************************************************************************
   Function: DeselectActiveUpgrade

Description: This function deselects the last upgrade the player clicked.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectActiveUpgrade()
    {
        upgradePanelButtons[upgradeSlotButtonToActivate].interactable = true;
        upgradePanelButtons[upgradeSlotButtonToActivate].OnDeselect(null);
        upgradeManager.RemoveUpgradeStatPanelDetails();
    }

    /**************************************************************************
   Function: PressSurvivalNote

Description: This function sets the specified button as activate while making
             all other survival note buttons as not active and clickable. Then
             it calls the ReadNote function.

      Input: none

     Output: none
    **************************************************************************/
    public void PressSurvivalNote()
    {
        for (int i = 0; i < survivalNotes.Length; i++)
        {
            if (i == survivalButtonToActivate)
            {
                survivalNotes[i].interactable = false;

                if(survivalNoteManager.GetRevealedNoteStatus())
                {
                    survivalNoteManager.ReadNote();
                }
            }
            else
            {
                survivalNotes[i].interactable = true;
                survivalNotes[i].OnDeselect(null);
            }
        }
    }

    /**************************************************************************
   Function: SelectSurvivalNote#

Description: These function set the index of which survival button to make
             active.

      Input: none

     Output: none
    **************************************************************************/
    public void SelectSurvivalNote1()
    {
        survivalButtonToActivate = 0;
        survivalNoteManager.SetSurvivalNoteSlotNumber(0);
    }
    public void SelectSurvivalNote2()
    {
        survivalButtonToActivate = 1;
        survivalNoteManager.SetSurvivalNoteSlotNumber(1);
    }
    public void SelectSurvivalNote3()
    {
        survivalButtonToActivate = 2;
        survivalNoteManager.SetSurvivalNoteSlotNumber(2);
    }
    public void SelectSurvivalNote4()
    {
        survivalButtonToActivate = 3;
        survivalNoteManager.SetSurvivalNoteSlotNumber(3);
    }
    public void SelectSurvivalNote5()
    {
        survivalButtonToActivate = 4;
        survivalNoteManager.SetSurvivalNoteSlotNumber(4);
    }
    public void SelectSurvivalNote6()
    {
        survivalButtonToActivate = 5;
        survivalNoteManager.SetSurvivalNoteSlotNumber(5);
    }
    public void SelectSurvivalNote7()
    {
        survivalButtonToActivate = 6;
        survivalNoteManager.SetSurvivalNoteSlotNumber(6);
    }
    public void SelectSurvivalNote8()
    {
        survivalButtonToActivate = 7;
        survivalNoteManager.SetSurvivalNoteSlotNumber(7);
    }
    public void SelectSurvivalNote9()
    {
        survivalButtonToActivate = 8;
        survivalNoteManager.SetSurvivalNoteSlotNumber(8);
    }
    public void SelectSurvivalNote10()
    {
        survivalButtonToActivate = 9;
        survivalNoteManager.SetSurvivalNoteSlotNumber(9);
    }

    /**************************************************************************
   Function: PressDocument

Description: This function sets the specified button as activate while making
             all other document buttons not active and clickable.

      Input: none

     Output: none
    **************************************************************************/
    public void PressDocument()
    {
          //bounds checking
        if(documentButtonToActivate < 0 || documentButtonToActivate > documents.Length)
        {
            return;
        }

        for (int i = 0; i < documents.Length; i++)
        {
            if (i == documentButtonToActivate)
            {
                documents[i].interactable = false;
            }
            else
            {
                documents[i].interactable = true;
                documents[i].OnDeselect(null);
            }
        }

        DeselectAllDummyDocuments();
    }

    /**************************************************************************
   Function: SelectDocument#

Description: These function set the index of which document button to make
             active while clearing document detail panel before displaying
             new details.

      Input: none

     Output: none
    **************************************************************************/
    public void SelectDocument1()
    {
        documentButtonToActivate = 0;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(0);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument2()
    {
        documentButtonToActivate = 1;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(1);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument3()
    {
        documentButtonToActivate = 2;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(2);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument4()
    {
        documentButtonToActivate = 3;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(3);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument5()
    {
        documentButtonToActivate = 4;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(4);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument6()
    {
        documentButtonToActivate = 5;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(5);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument7()
    {
        documentButtonToActivate = 6;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(6);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument8()
    {
        documentButtonToActivate = 7;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(7);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument9()
    {
        documentButtonToActivate = 8;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(8);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument10()
    {
        documentButtonToActivate = 9;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(9);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument11()
    {
        documentButtonToActivate = 10;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(10);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument12()
    {
        documentButtonToActivate = 11;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(11);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument13()
    {
        documentButtonToActivate = 12;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(12);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument14()
    {
        documentButtonToActivate = 13;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(13);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument15()
    {
        documentButtonToActivate = 14;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(14);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument16()
    {
        documentButtonToActivate = 15;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(15);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument17()
    {
        documentButtonToActivate = 16;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(16);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument18()
    {
        documentButtonToActivate = 17;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(17);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument19()
    {
        documentButtonToActivate = 18;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(18);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument20()
    {
        documentButtonToActivate = 19;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(19);
        documentManager.CheckIfDocumentIsCollected();
    }
    public void SelectDocument21()
    {
        documentButtonToActivate = 20;
        documentManager.RemoveDetails();
        documentManager.SetDocumentSlotNumber(20);
        documentManager.CheckIfDocumentIsCollected();
    }

    /**************************************************************************
   Function: PressEmptyDocumentButton

Description: This function sets the specified button as activate while making
             all other dummy document buttons not active and clickable.

      Input: none

     Output: none
    **************************************************************************/
    public void PressEmptyDocumentButton()
    {
          //bounds checking
        if(dummyButtonToActivate < 0 || dummyButtonToActivate > documentDummyButtons.Length)
        {
            return;
        }

        for (int i = 0; i < documentDummyButtons.Length; i++)
        {
            if (i == dummyButtonToActivate)
            {
                documentDummyButtons[i].interactable = false;
            }
            else
            {
                documentDummyButtons[i].interactable = true;
                documentDummyButtons[i].OnDeselect(null);
            }
        }

        DeselectAllDocuments();
    }

    /**************************************************************************
   Function: SelectDummyDocument#

Description: These functions set the index of which dummy document button to
             make active.

      Input: none

     Output: none
    **************************************************************************/
    public void SelectDummyDocument1()
    {
        dummyButtonToActivate = 4; //documentLocation1
    }
    public void SelectDummyDocument2()
    {
        dummyButtonToActivate = 0; //documentEmptySpace1
    }
    public void SelectDummyDocument3()
    {
        dummyButtonToActivate = 5; //documentLocation2
    }
    public void SelectDummyDocument4()
    {
        dummyButtonToActivate = 1; //documentEmptySpace2
    }
    public void SelectDummyDocument5()
    {
        dummyButtonToActivate = 6; //documentLocation3
    }
    public void SelectDummyDocument6()
    {
        dummyButtonToActivate = 2; //documentEmptySpace3
    }
    public void SelectDummyDocument7()
    {
        dummyButtonToActivate = 7; //documentLocation4
    }
    public void SelectDummyDocument8()
    {
        dummyButtonToActivate = 3; //documentEmptySpace4
    }

    /**************************************************************************
   Function: PressItemSlot

Description: This function sets the specified button as activate while making
             all other item slot buttons as not active and clickable.

      Input: none

     Output: none
    **************************************************************************/
    public void PressItemSlot()
    {
        for (int i = 0; i < itemPanelButtons.Length; i++)
        {
            if (i == itemSlotButtonToActivate)
            {
                itemPanelButtons[i].interactable = false;
            }
            else
            {
                itemPanelButtons[i].interactable = true;
                itemPanelButtons[i].OnDeselect(null);
            }
        }
    }

    /**************************************************************************
   Function: DeselectItemSlot

Description: This function sets the current item slot as clickable and hides
             the item sub panel.

      Input: none

     Output: none
    **************************************************************************/
    public void DeselectItemSlot()
    {
        for (int i = 0; i < itemPanelButtons.Length; i++)
        {
            if (i == itemSlotButtonToActivate)
            {
                itemPanelButtons[i].interactable = true;
                HideItemSubMenu();
            }
        }
    }

    /**************************************************************************
   Function: SelectItem#

Description: These function set the index of which item slot button to make
             active as well as which item slot for the item manager to perform
             actions on if the player uses the item sub menu.

      Input: none

     Output: none
    **************************************************************************/
    public void SelectItem1()
    {
        itemSlotButtonToActivate = 0;
        itemManager.SetItemSlotNumber(0);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(0);
    }
    public void SelectItem2()
    {
        itemSlotButtonToActivate = 1;
        itemManager.SetItemSlotNumber(1);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(1);
    }
    public void SelectItem3()
    {
        itemSlotButtonToActivate = 2;
        itemManager.SetItemSlotNumber(2);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(2);
    }
    public void SelectItem4()
    {
        itemSlotButtonToActivate = 3;
        itemManager.SetItemSlotNumber(3);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(3);
    }
    public void SelectItem5()
    {
        itemSlotButtonToActivate = 4;
        itemManager.SetItemSlotNumber(4);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(4);
    }
    public void SelectItem6()
    {
        itemSlotButtonToActivate = 5;
        itemManager.SetItemSlotNumber(5);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(5);
    }
    public void SelectItem7()
    {
        itemSlotButtonToActivate = 6;
        itemManager.SetItemSlotNumber(6);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(6);
    }
    public void SelectItem8()
    {
        itemSlotButtonToActivate = 7;
        itemManager.SetItemSlotNumber(7);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(7);
    }
    public void SelectItem9()
    {
        itemSlotButtonToActivate = 8;
        itemManager.SetItemSlotNumber(8);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(8);
    }
    public void SelectItem10()
    {
        itemSlotButtonToActivate = 9;
        itemManager.SetItemSlotNumber(9);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(9);
    }
    public void SelectItem11()
    {
        itemSlotButtonToActivate = 10;
        itemManager.SetItemSlotNumber(10);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(10);
    }
    public void SelectItem12()
    {
        itemSlotButtonToActivate = 11;
        itemManager.SetItemSlotNumber(11);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(11);
    }
    public void SelectItem13()
    {
        itemSlotButtonToActivate = 12;
        itemManager.SetItemSlotNumber(12);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(12);
    }
    public void SelectItem14()
    {
        itemSlotButtonToActivate = 13;
        itemManager.SetItemSlotNumber(13);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(13);
    }
    public void SelectItem15()
    {
        itemSlotButtonToActivate = 14;
        itemManager.SetItemSlotNumber(14);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(14);
    }
    public void SelectItem16()
    {
        itemSlotButtonToActivate = 15;
        itemManager.SetItemSlotNumber(15);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(15);
    }
    public void SelectItem17()
    {
        itemSlotButtonToActivate = 16;
        itemManager.SetItemSlotNumber(16);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(16);
    }
    public void SelectItem18()
    {
        itemSlotButtonToActivate = 17;
        itemManager.SetItemSlotNumber(17);
        itemManager.RemoveDetails();

        MoveItemSubMenuPosition(17);
    }

    /**************************************************************************
   Function: HideItemSubMenu

Description: This function deactivates the item sub menu and moves it to its
             default position.

      Input: none

     Output: none
    **************************************************************************/
    public void HideItemSubMenu()
    {
          //deactivates sub menu
        itemSubMenuPanel.SetActive(false);
          //returns it to its default position
        itemSubMenuPanel.transform.position = defaultSubMenuPosition;
    }

    /**************************************************************************
   Function: PressWeaponSlot

Description: This function sets the specified button as activate while making
             all other weapon slot buttons as not active and clickable.

      Input: none

     Output: none
    **************************************************************************/
    public void PressWeaponSlot()
    {
        for (int i = 0; i < weaponPanelButtons.Length; i++)
        {
            if (i == weaponSlotButtonToActivate)
            {
                weaponPanelButtons[i].interactable = false;
            }
            else
            {
                weaponPanelButtons[i].interactable = true;
                weaponPanelButtons[i].OnDeselect(null);
            }
        }
    }

    /**************************************************************************
   Function: DeselectWeaponSlot

Description: This function sets the current weapon slot as clickable and hides
             the weapon sub panel.

      Input: none

     Output: none
    **************************************************************************/
    public void DeselectWeaponSlot()
    {
        for (int i = 0; i < weaponPanelButtons.Length; i++)
        {
            if (i == weaponSlotButtonToActivate)
            {
                weaponPanelButtons[i].interactable = true;
                HideWeaponSubMenu();
            }
        }
    }

    /**************************************************************************
   Function: SelectWeapon#

Description: These function set the index of which weapon slot button to make
             active as well as which weapon slot for the weapon manager to 
             perform actions on if the player uses the weapon sub menu.

      Input: none

     Output: none
    **************************************************************************/
    public void SelectWeapon1()
    {
        weaponSlotButtonToActivate = 0;
        weaponManager.SetWeaponSlotNumber(0);
        weaponManager.RemoveDetails();

        MoveWeaponSubMenuPosition(0);
    }
    public void SelectWeapon2()
    {
        weaponSlotButtonToActivate = 1;
        weaponManager.SetWeaponSlotNumber(1);
        weaponManager.RemoveDetails();

        MoveWeaponSubMenuPosition(1);
    }
    public void SelectWeapon3()
    {
        weaponSlotButtonToActivate = 2;
        weaponManager.SetWeaponSlotNumber(2);
        weaponManager.RemoveDetails();

        MoveWeaponSubMenuPosition(2);
    }
    public void SelectWeapon4()
    {
        weaponSlotButtonToActivate = 3;
        weaponManager.SetWeaponSlotNumber(3);
        weaponManager.RemoveDetails();

        MoveWeaponSubMenuPosition(3);
    }
    public void SelectWeapon5()
    {
        weaponSlotButtonToActivate = 4;
        weaponManager.SetWeaponSlotNumber(4);
        weaponManager.RemoveDetails();

        MoveWeaponSubMenuPosition(4);
    }
    public void SelectWeapon6()
    {
        weaponSlotButtonToActivate = 5;
        weaponManager.SetWeaponSlotNumber(5);
        weaponManager.RemoveDetails();

        MoveWeaponSubMenuPosition(5);
    }

    /**************************************************************************
   Function: HideWeaponSubMenu

Description: This function deactivates the weapon sub menu and moves it to its
             default position.

      Input: none

     Output: none
    **************************************************************************/
    public void HideWeaponSubMenu()
    {
          //deactivates sub menu
        weaponSubMenuPanel.SetActive(false);
          //returns it to its default position
        weaponSubMenuPanel.transform.position = defaultSubMenuPosition;
    }

    /**************************************************************************
   Function: PressKeyItemSlot

Description: This function sets the specified button as activate while making
             all other key item slot buttons as not active and clickable.

      Input: none

     Output: none
    **************************************************************************/
    public void PressKeyItemSlot()
    {
        for (int i = 0; i < keyItemPanelButtons.Length; i++)
        {
            if (i == keyItemSlotButtonToActivate)
            {
                keyItemPanelButtons[i].interactable = false;
            }
            else
            {
                keyItemPanelButtons[i].interactable = true;
                keyItemPanelButtons[i].OnDeselect(null);
            }
        }
    }

    /**************************************************************************
   Function: DeselectKeyItemSlot

Description: This function sets the current key item slot as clickable and 
             hides the key item sub panel.

      Input: none

     Output: none
    **************************************************************************/
    public void DeselectKeyItemSlot()
    {
        for (int i = 0; i < keyItemPanelButtons.Length; i++)
        {
            if (i == keyItemSlotButtonToActivate)
            {
                keyItemPanelButtons[i].interactable = true;
                HideKeyItemSubMenu();
            }
        }
    }

    /**************************************************************************
   Function: SelectKeyItem#

Description: These function set the index of which key item slot button to make
             active as well as which key item slot for the key item manager to 
             perform actions on if the player uses the key item sub menu.

      Input: none

     Output: none
    **************************************************************************/
    public void SelectKeyItem1()
    {
        keyItemSlotButtonToActivate = 0;
        keyItemManager.SetKeyItemSlotNumber(0);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(0);
    }
    public void SelectKeyItem2()
    {
        keyItemSlotButtonToActivate = 1;
        keyItemManager.SetKeyItemSlotNumber(1);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(1);
    }
    public void SelectKeyItem3()
    {
        keyItemSlotButtonToActivate = 2;
        keyItemManager.SetKeyItemSlotNumber(2);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(2);
    }
    public void SelectKeyItem4()
    {
        keyItemSlotButtonToActivate = 3;
        keyItemManager.SetKeyItemSlotNumber(3);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(3);
    }
    public void SelectKeyItem5()
    {
        keyItemSlotButtonToActivate = 4;
        keyItemManager.SetKeyItemSlotNumber(4);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(4);
    }
    public void SelectKeyItem6()
    {
        keyItemSlotButtonToActivate = 5;
        keyItemManager.SetKeyItemSlotNumber(5);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(5);
    }
    public void SelectKeyItem7()
    {
        keyItemSlotButtonToActivate = 6;
        keyItemManager.SetKeyItemSlotNumber(6);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(6);
    }
    public void SelectKeyItem8()
    {
        keyItemSlotButtonToActivate = 7;
        keyItemManager.SetKeyItemSlotNumber(7);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(7);
    }
    public void SelectKeyItem9()
    {
        keyItemSlotButtonToActivate = 8;
        keyItemManager.SetKeyItemSlotNumber(8);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(8);
    }
    public void SelectKeyItem10()
    {
        keyItemSlotButtonToActivate = 9;
        keyItemManager.SetKeyItemSlotNumber(9);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(9);
    }
    public void SelectKeyItem11()
    {
        keyItemSlotButtonToActivate = 10;
        keyItemManager.SetKeyItemSlotNumber(10);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(10);
    }
    public void SelectKeyItem12()
    {
        keyItemSlotButtonToActivate = 11;
        keyItemManager.SetKeyItemSlotNumber(11);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(11);
    }
    public void SelectKeyItem13()
    {
        keyItemSlotButtonToActivate = 12;
        keyItemManager.SetKeyItemSlotNumber(12);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(12);
    }
    public void SelectKeyItem14()
    {
        keyItemSlotButtonToActivate = 13;
        keyItemManager.SetKeyItemSlotNumber(13);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(13);
    }
    public void SelectKeyItem15()
    {
        keyItemSlotButtonToActivate = 14;
        keyItemManager.SetKeyItemSlotNumber(14);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(14);
    }
    public void SelectKeyItem16()
    {
        keyItemSlotButtonToActivate = 15;
        keyItemManager.SetKeyItemSlotNumber(15);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(15);
    }
    public void SelectKeyItem17()
    {
        keyItemSlotButtonToActivate = 16;
        keyItemManager.SetKeyItemSlotNumber(16);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(16);
    }
    public void SelectKeyItem18()
    {
        keyItemSlotButtonToActivate = 17;
        keyItemManager.SetKeyItemSlotNumber(17);
        keyItemManager.RemoveDetails();

        MoveKeyItemSubMenuPosition(17);
    }

    /**************************************************************************
   Function: HideKeyItemSubMenu

Description: This function deactivates the key item sub menu and moves it to 
             its default position.

      Input: none

     Output: none
    **************************************************************************/
    public void HideKeyItemSubMenu()
    {
          //deactivates sub menu
        keyItemSubMenuPanel.SetActive(false);
          //returns it to its default position
        keyItemSubMenuPanel.transform.position = defaultSubMenuPosition;
    }

    /**************************************************************************
   Function: PressUpgradeSlot

Description: This function sets the specified button as activate while making
             all other upgrade slot buttons as not active and clickable.

      Input: none

     Output: none
    **************************************************************************/
    public void PressUpgradeSlot()
    {
        for (int i = 0; i < upgradePanelButtons.Length; i++)
        {
            if (i == upgradeSlotButtonToActivate)
            {
                upgradePanelButtons[i].interactable = false;
            }
            else
            {
                upgradePanelButtons[i].interactable = true;
                upgradePanelButtons[i].OnDeselect(null);
            }
        }
    }

    /**************************************************************************
   Function: DeselectUpgradeSlot

Description: This function sets the current upgrade slot as clickable and hides
             the upgrade sub panel.

      Input: none

     Output: none
    **************************************************************************/
    public void DeselectUpgradeSlot()
    {
        for (int i = 0; i < upgradePanelButtons.Length; i++)
        {
            if (i == upgradeSlotButtonToActivate)
            {
                upgradePanelButtons[i].interactable = true;
                HideUpgradeSubMenu();
            }
        }
    }

    /**************************************************************************
   Function: SelectUpgrade#

Description: These function set the index of which upgrade slot button to make
             active as well as which upgrade slot for the upgrade manager to 
             perform actions on if the player uses the upgrade sub menu that
             is moved to each clicked upgrade slot.

      Input: none

     Output: none
    **************************************************************************/
    public void SelectUpgrade1()
    {
        upgradeSlotButtonToActivate = 0;
        upgradeManager.SetUpgradeSlotNumber(0);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(0);
    }
    public void SelectUpgrade2()
    {
        upgradeSlotButtonToActivate = 1;
        upgradeManager.SetUpgradeSlotNumber(1);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(1);
    }
    public void SelectUpgrade3()
    {
        upgradeSlotButtonToActivate = 2;
        upgradeManager.SetUpgradeSlotNumber(2);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(2);
    }
    public void SelectUpgrade4()
    {
        upgradeSlotButtonToActivate = 3;
        upgradeManager.SetUpgradeSlotNumber(3);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(3);
    }
    public void SelectUpgrade5()
    {
        upgradeSlotButtonToActivate = 4;
        upgradeManager.SetUpgradeSlotNumber(4);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(4);
    }
    public void SelectUpgrade6()
    {
        upgradeSlotButtonToActivate = 5;
        upgradeManager.SetUpgradeSlotNumber(5);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(5);
    }
    public void SelectUpgrade7()
    {
        upgradeSlotButtonToActivate = 6;
        upgradeManager.SetUpgradeSlotNumber(6);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(6);
    }
    public void SelectUpgrade8()
    {
        upgradeSlotButtonToActivate = 7;
        upgradeManager.SetUpgradeSlotNumber(7);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(7);
    }
    public void SelectUpgrade9()
    {
        upgradeSlotButtonToActivate = 8;
        upgradeManager.SetUpgradeSlotNumber(8);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(8);
    }
    public void SelectUpgrade10()
    {
        upgradeSlotButtonToActivate = 9;
        upgradeManager.SetUpgradeSlotNumber(9);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(9);
    }
    public void SelectUpgrade11()
    {
        upgradeSlotButtonToActivate = 10;
        upgradeManager.SetUpgradeSlotNumber(10);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(10);
    }
    public void SelectUpgrade12()
    {
        upgradeSlotButtonToActivate = 11;
        upgradeManager.SetUpgradeSlotNumber(11);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(11);
    }
    public void SelectUpgrade13()
    {
        upgradeSlotButtonToActivate = 12;
        upgradeManager.SetUpgradeSlotNumber(12);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(12);
    }
    public void SelectUpgrade14()
    {
        upgradeSlotButtonToActivate = 13;
        upgradeManager.SetUpgradeSlotNumber(13);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(13);
    }
    public void SelectUpgrade15()
    {
        upgradeSlotButtonToActivate = 14;
        upgradeManager.SetUpgradeSlotNumber(14);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(14);
    }
    public void SelectUpgrade16()
    {
        upgradeSlotButtonToActivate = 15;
        upgradeManager.SetUpgradeSlotNumber(15);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(15);
    }
    public void SelectUpgrade17()
    {
        upgradeSlotButtonToActivate = 16;
        upgradeManager.SetUpgradeSlotNumber(16);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(16);
    }
    public void SelectUpgrade18()
    {
        upgradeSlotButtonToActivate = 17;
        upgradeManager.SetUpgradeSlotNumber(17);
        upgradeManager.RemoveUpgradeStatPanelDetails();

        MoveUpgradeSubMenuPosition(17);
    }

    /**************************************************************************
   Function: HideUpgradeSubMenu

Description: This function deactivates the upgrade sub menu and moves it to its
             default position.

      Input: none

     Output: none
    **************************************************************************/
    public void HideUpgradeSubMenu()
    {
          //deactivates sub menu
        upgradeSubMenuPanel.SetActive(false);
          //returns it to its default position
        upgradeSubMenuPanel.transform.position = defaultSubMenuPosition;
    }

    /**************************************************************************
   Function: EnableInventorySubTabs

Description: This function enables each button in the inventory tab, allowing
             item drops, weapons, key items, and upgrades to find the scripts
             attached to those buttons. NOTE: If any of these buttons are
             disabled, their scripts can't be found by drops.

      Input: none

     Output: none
    **************************************************************************/
    public void EnableInventorySubTabs()
    {
        inventoryItemButton.interactable = true;
        invItemsPanel.SetActive(true);

        inventoryWeaponButton.interactable = true;
        invWeaponPanel.SetActive(true);

        inventoryKeyItemButton.interactable = true;
        invKeyItemPanel.SetActive(true);

        inventoryUpgradeButton.interactable = true;
        invUpgradePanel.SetActive(true);
    }

    /**************************************************************************
   Function: DeselectAllDocuments

Description: This function removes the current document's details and deselects
             all document buttons.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectAllDocuments()
    {
        documentManager.RemoveDetails();

        for (int i = 0; i < documents.Length; i++)
        {
            documents[i].interactable = true;
            documents[i].OnDeselect(null);
        }
    }

    /**************************************************************************
   Function: DeselectAllDummyDocuments

Description: This function deselects all dummy document buttons.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectAllDummyDocuments()
    {
        for (int i = 0; i < documentDummyButtons.Length; i++)
        {
            documentDummyButtons[i].interactable = true;
            documentDummyButtons[i].OnDeselect(null);
        }
    }

    /**************************************************************************
   Function: MoveItemSubMenuPosition

Description: Given an integer, this function moves the item sub menu to be 
             next to the clicked item slot based on the integer.

      Input: index - index of the item slot to put sub menu next to

     Output: none
    **************************************************************************/
    private void MoveItemSubMenuPosition(int index)
    {
          //activates sub menu
        itemSubMenuPanel.SetActive(true);
          //moves sub menu to be next to item slot of the specified index
        itemSubMenuPanel.transform.SetParent(itemSubMenuPositions[index].transform);
          //centers the sub menu onto the parent's position
        itemSubMenuPanel.transform.localPosition = Vector3.zero;
    }

    /**************************************************************************
   Function: MoveWeaponSubMenuPosition

Description: Given an integer, this function moves the weapon sub menu to be 
             next to the clicked upgrade slot based on the integer.

      Input: index - index of the weapon slot to put sub menu next to

     Output: none
    **************************************************************************/
    private void MoveWeaponSubMenuPosition(int index)
    {
          //activates sub menu
        weaponSubMenuPanel.SetActive(true);
          //moves sub menu to be next to weapon slot of the specified index
        weaponSubMenuPanel.transform.SetParent(weaponSubMenuPositions[index].transform);
          //centers the sub menu onto the parent's position
        weaponSubMenuPanel.transform.localPosition = Vector3.zero;
    }

    /**************************************************************************
   Function: MoveKeyItemSubMenuPosition

Description: Given an integer, this function moves the key item sub menu to be 
             next to the clicked key item slot based on the integer.

      Input: index - index of the key item slot to put sub menu next to

     Output: none
    **************************************************************************/
    private void MoveKeyItemSubMenuPosition(int index)
    {
          //activates sub menu
        keyItemSubMenuPanel.SetActive(true);
          //moves sub menu to be next to key item slot of the specified index
        keyItemSubMenuPanel.transform.SetParent(keyItemSubMenuPositions[index].transform);
          //centers the sub menu onto the parent's position
        keyItemSubMenuPanel.transform.localPosition = Vector3.zero;
    }

    /**************************************************************************
   Function: MoveUpgradeSubMenuPosition

Description: Given an integer, this function moves the upgrade sub menu to be 
             next to the clicked upgrade slot based on the integer.

      Input: index - index of the upgrade slot to put sub menu next to

     Output: none
    **************************************************************************/
    private void MoveUpgradeSubMenuPosition(int index)
    {
          //activates sub menu
        upgradeSubMenuPanel.SetActive(true);
          //moves sub menu to be next to upgrade slot of the specified index
        upgradeSubMenuPanel.transform.SetParent(upgradeSubMenuPositions[index].transform);
          //centers the sub menu onto the parent's position
        upgradeSubMenuPanel.transform.localPosition = Vector3.zero;
    }

    /**************************************************************************
   Function: ReactivateMainButtons

Description: This function enables each backpanel as the UI is closed so loot
             and other items can find the scripts in the inventory sub tabs.

      Input: none

     Output: none
    **************************************************************************/
    private void ReactivateMainButtons()
    {
        inventoryBackPanel.SetActive(true);
        survivalBackPanel.SetActive(true);
        documentsBackPanel.SetActive(true);
        mapBackPanel.SetActive(true);
        optionsBackPanel.SetActive(true);
    }


    /**************************************************************************
   Function: DeactivateInventoryButton

Description: This function makes the inventory button clickable, hides its
             panel which hides all item slots, sub buttons, etc., and signifies
             that the button is no longer the clicked button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateInventoryButton()
    {

        inventoryButton.interactable = true; //button can now be clicked        
        inventoryBackPanel.SetActive(false); //hides the back panel and its children
        inventoryButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeactivateSurvivalButton

Description: This function makes the survival button clickable, hides its
             panel which hides all item slots, sub buttons, etc., and signifies
             that the button is no longer the clicked button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateSurvivalButton()
    {
        survivalButton.interactable = true; //survival notes button can be clicked
        survivalBackPanel.SetActive(false); //deactivates the survival back panel
        survivalButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeactivateDocumentsButton

Description: This function makes the documents button clickable, hides its
             panel which hides all item slots, sub buttons, etc., and signifies
             that the button is no longer the clicked button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateDocumentsButton()
    {
        documentsButton.interactable = true; //documents button can be clicked
        documentsBackPanel.SetActive(false); //deactivates the documents back panel
        documentsButton.OnDeselect(null);
        documentManager.RemoveDetails(); //removes text of last clicked document
    }

    /**************************************************************************
   Function: DeactivateMapButton

Description: This function makes the map button clickable, hides its panel 
             which hides all item slots, sub buttons, etc., and signifies that 
             the button is no longer the clicked button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateMapButton()
    {
        mapButton.interactable = true; //map button can be clicked
        mapBackPanel.SetActive(false); //deactivates the map back panel
        mapButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeactivateOptionsButton

Description: This function makes the options button clickable, hides its
             panel which hides all item slots, sub buttons, etc., and signifies
             that the button is no longer the clicked button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateOptionsButton()
    {
        optionsButton.interactable = true; //options button can be clicked
        optionsBackPanel.SetActive(false); //deactivates the options back panel
        optionsButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeactivateSubPanels

Description: This function hides all the sub menu panels that are displays when
             an item slot within one of the inventory sub tabs is clicked.

      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateSubPanels()
    {
        HideItemSubMenu();
        weaponSubMenuPanel.SetActive(false);
        keyItemSubMenuPanel.SetActive(false);
        upgradeSubMenuPanel.SetActive(false);
    }

    /**************************************************************************
   Function: DeactivateKeyboardContPanel

Description: This function hides the panel that displays the game's PC
             controls.

      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateKeyboardContPanel()
    {
          //keyboard controls panel is deactivated
        KeyboardControlsPanel.SetActive(false);
    }

    /**************************************************************************
   Function: DeactivateInventoryItemButton

Description: This function deselects the item button and signifies that it can
             now be clicked. The item slots attached to the item button are
             also hidden.

      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateInventoryItemButton()
    {
        inventoryItemButton.interactable = true;
        invItemsPanel.SetActive(false);
        itemManager.EnableCurrencyPanel(false);
        inventoryItemButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeactivateInventoryWeaponButton

Description: This function deselects the weapon button and signifies that it 
             can now be clicked. The weapon slots attached to the weapon button 
             are also hidden.

      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateInventoryWeaponButton()
    {
        inventoryWeaponButton.interactable = true;
        inventoryWeaponButton.OnDeselect(null);
        weaponManager.EnableWeaponStatPanel(false);
        weaponManager.EnableEquippedPanel(false);
        weaponManager.EnableSecondaryPanel(false);
        invWeaponPanel.SetActive(false);
    }

    /**************************************************************************
   Function: DeactivateInventoryKeyItemButton

Description: This function deselects the key item button and signifies that it 
             can now be clicked. The key item slots attached to the key item
             button are also hidden.

      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateInventoryKeyItemButton()
    {
        inventoryKeyItemButton.interactable = true;
        inventoryKeyItemButton.OnDeselect(null);
        invKeyItemPanel.SetActive(false);
    }

    /**************************************************************************
   Function: DeactivateInventoryUpgradeButton

Description: This function deselects the upgrade button and signifies that it 
             can now be clicked. The upgrade slots attached to the upgrade
             button are also hidden.
             
      Input: none

     Output: none
    **************************************************************************/
    private void DeactivateInventoryUpgradeButton()
    {
        inventoryUpgradeButton.interactable = true;
        inventoryUpgradeButton.OnDeselect(null);
        invUpgradePanel.SetActive(false);
    }

    /**************************************************************************
   Function: SetSurvivalNotes

Description: This function stores all serialized survival notes buttons into 
             an array for easy access in all functions that need these objects.
             
      Input: none

     Output: none
    **************************************************************************/
    private void SetSurvivalNotes()
    {
        survivalNotes[0] = survivalNote1;
        survivalNotes[1] = survivalNote2;
        survivalNotes[2] = survivalNote3;
        survivalNotes[3] = survivalNote4;
        survivalNotes[4] = survivalNote5;
        survivalNotes[5] = survivalNote6;
        survivalNotes[6] = survivalNote7;
        survivalNotes[7] = survivalNote8;
        survivalNotes[8] = survivalNote9;
        survivalNotes[9] = survivalNote10;
    }

    /**************************************************************************
   Function: SetDocumentDummyButtons

Description: This function stores all serialized empty space and location
             document buttons in a single array for easy access in other
             functions.
             
      Input: none

     Output: none
    **************************************************************************/
    private void SetDocumentDummyButtons()
    {
        documentDummyButtons[0] = documentEmptySpace1;
        documentDummyButtons[1] = documentEmptySpace2;
        documentDummyButtons[2] = documentEmptySpace3;
        documentDummyButtons[3] = documentEmptySpace4;
        documentDummyButtons[4] = documentLocation1;
        documentDummyButtons[5] = documentLocation2;
        documentDummyButtons[6] = documentLocation3;
        documentDummyButtons[7] = documentLocation4;
    }

    /**************************************************************************
   Function: SetDocuments

Description: This function stores all serialized document buttons into an 
             array for easy access in all functions that need these objects.
             
      Input: none

     Output: none
    **************************************************************************/
    private void SetDocuments()
    {
        documents[0] = document1;
        documents[1] = document2;
        documents[2] = document3;
        documents[3] = document4;
        documents[4] = document5;
        documents[5] = document6;
        documents[6] = document7;
        documents[7] = document8;
        documents[8] = document9;
        documents[9] = document10;
        documents[10] = document11;
        documents[11] = document12;
        documents[12] = document13;
        documents[13] = document14;
        documents[14] = document15;
        documents[15] = document16;
        documents[16] = document17;
        documents[17] = document18;
        documents[18] = document19;
        documents[19] = document20;
        documents[20] = document21;
    }

    /**************************************************************************
   Function: SetItemSubMenuPositions

Description: This function stores all serialized item sub menu position objects 
             in an array for easy access in all functions that need these 
             objects.
             
      Input: none

     Output: none
    **************************************************************************/
    private void SetItemSubMenuPositions()
    {
        itemSubMenuPositions[0] = itemSubMenuPosition1;
        itemSubMenuPositions[1] = itemSubMenuPosition2;
        itemSubMenuPositions[2] = itemSubMenuPosition3; 
        itemSubMenuPositions[3] = itemSubMenuPosition4; 
        itemSubMenuPositions[4] = itemSubMenuPosition5; 
        itemSubMenuPositions[5] = itemSubMenuPosition6; 
        itemSubMenuPositions[6] = itemSubMenuPosition7; 
        itemSubMenuPositions[7] = itemSubMenuPosition8; 
        itemSubMenuPositions[8] = itemSubMenuPosition9; 
        itemSubMenuPositions[9] = itemSubMenuPosition10;
        itemSubMenuPositions[10] = itemSubMenuPosition11;
        itemSubMenuPositions[11] = itemSubMenuPosition12;
        itemSubMenuPositions[12] = itemSubMenuPosition13;
        itemSubMenuPositions[13] = itemSubMenuPosition14;
        itemSubMenuPositions[14] = itemSubMenuPosition15;
        itemSubMenuPositions[15] = itemSubMenuPosition16;
        itemSubMenuPositions[16] = itemSubMenuPosition17;
        itemSubMenuPositions[17] = itemSubMenuPosition18;
    }

    /**************************************************************************
   Function: SetWeaponSubMenuPositions

Description: This function stores all serialized weapon sub menu position 
             objects in an array for easy access in all functions that need 
             these objects.
             
      Input: none

     Output: none
    **************************************************************************/
    private void SetWeaponSubMenuPositions()
    {
        weaponSubMenuPositions[0] = weaponSubMenuPosition1;
        weaponSubMenuPositions[1] = weaponSubMenuPosition2;
        weaponSubMenuPositions[2] = weaponSubMenuPosition3;
        weaponSubMenuPositions[3] = weaponSubMenuPosition4;
        weaponSubMenuPositions[4] = weaponSubMenuPosition5;
        weaponSubMenuPositions[5] = weaponSubMenuPosition6;
    }

    /**************************************************************************
   Function: SetKeyItemSubMenuPositions

Description: This function stores all serialized key item sub menu position 
             objects in an array for easy access in all functions that need 
             these objects.

      Input: none

     Output: none
    **************************************************************************/
    private void SetKeyItemSubMenuPositions()
    {
        keyItemSubMenuPositions[0] = keyItemSubMenuPosition1;
        keyItemSubMenuPositions[1] = keyItemSubMenuPosition2;
        keyItemSubMenuPositions[2] = keyItemSubMenuPosition3;
        keyItemSubMenuPositions[3] = keyItemSubMenuPosition4;
        keyItemSubMenuPositions[4] = keyItemSubMenuPosition5;
        keyItemSubMenuPositions[5] = keyItemSubMenuPosition6;
        keyItemSubMenuPositions[6] = keyItemSubMenuPosition7;
        keyItemSubMenuPositions[7] = keyItemSubMenuPosition8;
        keyItemSubMenuPositions[8] = keyItemSubMenuPosition9;
        keyItemSubMenuPositions[9] = keyItemSubMenuPosition10;
        keyItemSubMenuPositions[10] = keyItemSubMenuPosition11;
        keyItemSubMenuPositions[11] = keyItemSubMenuPosition12;
        keyItemSubMenuPositions[12] = keyItemSubMenuPosition13;
        keyItemSubMenuPositions[13] = keyItemSubMenuPosition14;
        keyItemSubMenuPositions[14] = keyItemSubMenuPosition15;
        keyItemSubMenuPositions[15] = keyItemSubMenuPosition16;
        keyItemSubMenuPositions[16] = keyItemSubMenuPosition17;
        keyItemSubMenuPositions[17] = keyItemSubMenuPosition18;
    }

    /**************************************************************************
   Function: SetUpgradeSubMenuPositions

Description: This function stores all serialized upgrade sub menu position 
             objects in an array for easy access in all functions that need 
             these objects.

      Input: none

     Output: none
    **************************************************************************/
    private void SetUpgradeSubMenuPositions()
    {
        upgradeSubMenuPositions[0] = upgradeSubMenuPosition1;
        upgradeSubMenuPositions[1] = upgradeSubMenuPosition2;
        upgradeSubMenuPositions[2] = upgradeSubMenuPosition3;
        upgradeSubMenuPositions[3] = upgradeSubMenuPosition4;
        upgradeSubMenuPositions[4] = upgradeSubMenuPosition5;
        upgradeSubMenuPositions[5] = upgradeSubMenuPosition6;
        upgradeSubMenuPositions[6] = upgradeSubMenuPosition7;
        upgradeSubMenuPositions[7] = upgradeSubMenuPosition8;
        upgradeSubMenuPositions[8] = upgradeSubMenuPosition9;
        upgradeSubMenuPositions[9] = upgradeSubMenuPosition10;
        upgradeSubMenuPositions[10] = upgradeSubMenuPosition11;
        upgradeSubMenuPositions[11] = upgradeSubMenuPosition12;
        upgradeSubMenuPositions[12] = upgradeSubMenuPosition13;
        upgradeSubMenuPositions[13] = upgradeSubMenuPosition14;
        upgradeSubMenuPositions[14] = upgradeSubMenuPosition15;
        upgradeSubMenuPositions[15] = upgradeSubMenuPosition16;
        upgradeSubMenuPositions[16] = upgradeSubMenuPosition17;
        upgradeSubMenuPositions[17] = upgradeSubMenuPosition18;
    }
}