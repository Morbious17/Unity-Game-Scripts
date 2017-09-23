using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //This script controls the menus the player is able to interact with. The player can enable/disable them,
    //press buttons, examine items, use items, discard items, read collectibles, read survival notes,
    //view maps, and change some game settings.

    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private Canvas invCanvas;

    [SerializeField] private Button invButton;
    [SerializeField] private Button surButton;
    [SerializeField] private Button colButton;
    [SerializeField] private Button mapButton;
    [SerializeField] private Button optButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject invPanel;
    [SerializeField] private GameObject surPanel;
    [SerializeField] private GameObject colPanel;
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private GameObject optPanel;

    //TODO: Create scripts that contain info for all items before creating functions that pull that info
    //and put it in the panels and text objects.

    //This allows this script to access itemInfo to check item counts so they can be added to current item panels
    //or moved to another item panel if space is available.
    [SerializeField] private ItemInfo itemInfo;

    [SerializeField] private Button invItemButton;
    [SerializeField] private Button invWeaponButton;
    [SerializeField] private Button invKeyItemButton;
    [SerializeField] private Button invUpgradeButton;

    [SerializeField] private GameObject invItemsPanel;
    [SerializeField] private GameObject invWeaponPanel;
    [SerializeField] private GameObject invKeyItemPanel;
    [SerializeField] private GameObject invUpgradePanel;

    [SerializeField] private GameObject itemSubMenuPanel;
    [SerializeField] private GameObject weaponSubMenuPanel;
    [SerializeField] private GameObject keyItemSubMenuPanel;
    [SerializeField] private GameObject upgradeSubMenuPanel;

    [SerializeField] private Button SurvivalNote1;
    [SerializeField] private Button SurvivalNote2;
    [SerializeField] private Button SurvivalNote3;
    [SerializeField] private Button SurvivalNote4;
    [SerializeField] private Button SurvivalNote5;
    [SerializeField] private Button SurvivalNote6;
    [SerializeField] private Button SurvivalNote7;
    [SerializeField] private Button SurvivalNote8;
    [SerializeField] private Button SurvivalNote9;
    [SerializeField] private Button SurvivalNote10;

    //TODO: Create a collectible info script before creating functions that pull that info
    //and put it in the text objects.

    [SerializeField] private Text DetailedCollectibleText;
    [SerializeField] private Text CurrentColNoteText;

    [SerializeField] private Button CollectibleLocation1;
    [SerializeField] private Button CollectibleEmptySpace1;
    [SerializeField] private Button CollectibleLocation2;
    [SerializeField] private Button CollectibleEmptySpace2;
    [SerializeField] private Button CollectibleLocation3;
    [SerializeField] private Button CollectibleEmptySpace3;
    [SerializeField] private Button CollectibleLocation4;
    [SerializeField] private Button CollectibleEmptySpace4;
    [SerializeField] private Button Collectible1;
    [SerializeField] private Button Collectible2;
    [SerializeField] private Button Collectible3;
    [SerializeField] private Button Collectible4;
    [SerializeField] private Button Collectible5;
    [SerializeField] private Button Collectible6;
    [SerializeField] private Button Collectible7;
    [SerializeField] private Button Collectible8;
    [SerializeField] private Button Collectible9;
    [SerializeField] private Button Collectible10;
    [SerializeField] private Button Collectible11;
    [SerializeField] private Button Collectible12;
    [SerializeField] private Button Collectible13;
    [SerializeField] private Button Collectible14;
    [SerializeField] private Button Collectible15;
    [SerializeField] private Button Collectible16;
    [SerializeField] private Button Collectible17;
    [SerializeField] private Button Collectible18;
    [SerializeField] private Button Collectible19;
    [SerializeField] private Button Collectible20;
    [SerializeField] private Button Collectible21;

    [SerializeField] private Button OutskirtsMapButton;
    [SerializeField] private Button Outskirts1FButton;

    [SerializeField] private Button HospitalMapButton;
    [SerializeField] private Button HospitalB2Button;
    [SerializeField] private Button HospitalB1Button;
    [SerializeField] private Button Hospital1FButton;
    [SerializeField] private Button Hospital2FButton;
    [SerializeField] private Button Hospital3FButton;

    [SerializeField] private Button YearnAmharuMapButton;
    [SerializeField] private Button YearnAmharu1FButton;

    [SerializeField] private Button ApartmentsMapButton;
    [SerializeField] private Button ApartmentsB1Button;
    [SerializeField] private Button Apartments1FButton;
    [SerializeField] private Button Apartments2FButton;
    [SerializeField] private Button Apartments3FButton;
    [SerializeField] private Button Apartments4FButton;

    [SerializeField] private Button PoliceStationMapButton;
    [SerializeField] private Button PoliceStation1FButton;
    [SerializeField] private Button PoliceStation2FButton;

    [SerializeField] private Button AbandonedMinesMapButton;
    [SerializeField] private Button AbandonedMines1FButton;
    [SerializeField] private Button AbandonedMinesB1Button;
    [SerializeField] private Button AbandonedMinesB2Button;
    [SerializeField] private Button AbandonedMinesB3Button;

    [SerializeField] private Button AudioSettingsButton;
    [SerializeField] private Button VideoSettingsButton;
    [SerializeField] private Button ControlSettingsButton;
    [SerializeField] private Button GameplaySettingsButton;
    [SerializeField] private Button QuitGameButton;

    [SerializeField] private GameObject AudioSettingsPanel;
    [SerializeField] private GameObject VideoSettingsPanel;
    [SerializeField] private GameObject ControlSettingsPanel;
    [SerializeField] private GameObject GameplaySettingsPanel;

    [SerializeField] private Button KeyboardControlsButton;
    [SerializeField] private GameObject KeyboardControlsPanel;
    [SerializeField] private Button KeyboardContCloseButton;

    [SerializeField] private Slider MapZoomSlider;

    [SerializeField] private Button ItemPanelButton1;
    [SerializeField] private Button ItemPanelButton2;
    [SerializeField] private Button ItemPanelButton3;
    [SerializeField] private Button ItemPanelButton4;
    [SerializeField] private Button ItemPanelButton5;
    [SerializeField] private Button ItemPanelButton6;
    [SerializeField] private Button ItemPanelButton7;
    [SerializeField] private Button ItemPanelButton8;
    [SerializeField] private Button ItemPanelButton9;
    [SerializeField] private Button ItemPanelButton10;
    [SerializeField] private Button ItemPanelButton11;
    [SerializeField] private Button ItemPanelButton12;
    [SerializeField] private Button ItemPanelButton13;
    [SerializeField] private Button ItemPanelButton14;
    [SerializeField] private Button ItemPanelButton15;
    [SerializeField] private Button ItemPanelButton16;
    [SerializeField] private Button ItemPanelButton17;
    [SerializeField] private Button ItemPanelButton18;

    //This int will be read by another script to determine if the player has room to pick up items.
    public static int NumOfEmptyItemPanels;

    [SerializeField] private Button WeaponPanelButton1;
    [SerializeField] private Button WeaponPanelButton2;
    [SerializeField] private Button WeaponPanelButton3;
    [SerializeField] private Button WeaponPanelButton4;
    [SerializeField] private Button WeaponPanelButton5;
    [SerializeField] private Button WeaponPanelButton6;

    [SerializeField] private Button KeyItemPanelButton1;
    [SerializeField] private Button KeyItemPanelButton2;
    [SerializeField] private Button KeyItemPanelButton3;
    [SerializeField] private Button KeyItemPanelButton4;
    [SerializeField] private Button KeyItemPanelButton5;
    [SerializeField] private Button KeyItemPanelButton6;
    [SerializeField] private Button KeyItemPanelButton7;
    [SerializeField] private Button KeyItemPanelButton8;
    [SerializeField] private Button KeyItemPanelButton9;
    [SerializeField] private Button KeyItemPanelButton10;
    [SerializeField] private Button KeyItemPanelButton11;
    [SerializeField] private Button KeyItemPanelButton12;
    [SerializeField] private Button KeyItemPanelButton13;
    [SerializeField] private Button KeyItemPanelButton14;
    [SerializeField] private Button KeyItemPanelButton15;
    [SerializeField] private Button KeyItemPanelButton16;
    [SerializeField] private Button KeyItemPanelButton17;
    [SerializeField] private Button KeyItemPanelButton18;

    [SerializeField] private Button UpgradePanelButton1;
    [SerializeField] private Button UpgradePanelButton2;
    [SerializeField] private Button UpgradePanelButton3;
    [SerializeField] private Button UpgradePanelButton4;
    [SerializeField] private Button UpgradePanelButton5;
    [SerializeField] private Button UpgradePanelButton6;
    [SerializeField] private Button UpgradePanelButton7;
    [SerializeField] private Button UpgradePanelButton8;
    [SerializeField] private Button UpgradePanelButton9;
    [SerializeField] private Button UpgradePanelButton10;
    [SerializeField] private Button UpgradePanelButton11;
    [SerializeField] private Button UpgradePanelButton12;
    [SerializeField] private Button UpgradePanelButton13;
    [SerializeField] private Button UpgradePanelButton14;
    [SerializeField] private Button UpgradePanelButton15;
    [SerializeField] private Button UpgradePanelButton16;
    [SerializeField] private Button UpgradePanelButton17;
    [SerializeField] private Button UpgradePanelButton18;

    //TODO: Add the current objective text object when a script containing all objectives is made
    //so they can be passed into the text object.

    void Start ()
    {

	}

	void Update ()
    {
        Debug.Log("Empty Panels: " + NumOfEmptyItemPanels);
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (invCanvas.enabled == false)
            {
                Time.timeScale = 0;               

                invCanvas.enabled = true;

                PressInvButton();

                //invPanel.SetActive(true);
                //invButton.interactable = false;

                //subMenuPanel.SetActive(false);
                //invItemButton.interactable = false;
                //invItemsPanel.SetActive(true);

                PressInvItemButton();             

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1;

                invCanvas.enabled = false;
                invPanel.SetActive(false);
                itemSubMenuPanel.SetActive(false);
                invButton.interactable = true;
                invItemButton.interactable = true;
                invItemsPanel.SetActive(false);
                MapZoomSlider.enabled = false;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                DeactivateItemPanelButtons();
                DeactivateWeaponPanelButtons();
                DeactivateKeyItemPanelButtons();
                DeactivateUpgradePanelButtons();
            }
        }

        //TODO: Add if statement that checks to see if map key is pressed. If so, open map panel and change time scale.
        //TODO: Add if statement that checks to see if option key(pause button) is pressed. If so, open option panel and
        //change time scale.
        //TODO: When item, weapon, key item, and upgrade scripts are created, add if statements here to check when the 
        //first weapon, key item, and upgrade is found. When they are, reveal their buttons permanently. Before then,
        //make sure they are hidden.
    } 

    public void PressInvButton()
    {
        invButton.interactable = false;
        invPanel.SetActive(true);

        surButton.interactable = true;
        surPanel.SetActive(false);
        surButton.OnDeselect(null);

        colButton.interactable = true;
        colPanel.SetActive(false);
        colButton.OnDeselect(null);

        mapButton.interactable = true;
        mapPanel.SetActive(false);
        mapButton.OnDeselect(null);

        optButton.interactable = true;
        optPanel.SetActive(false);
        optButton.OnDeselect(null);

        itemSubMenuPanel.SetActive(false);
        weaponSubMenuPanel.SetActive(false);
        keyItemSubMenuPanel.SetActive(false);
        upgradeSubMenuPanel.SetActive(false);

        KeyboardControlsPanel.SetActive(false);
    }

    public void PressSurButton()
    {
        invButton.interactable = true;
        invPanel.SetActive(false);
        invButton.OnDeselect(null);

        surButton.interactable = false;
        surPanel.SetActive(true);

        colButton.interactable = true;
        colPanel.SetActive(false);
        colButton.OnDeselect(null);

        mapButton.interactable = true;
        mapPanel.SetActive(false);
        mapButton.OnDeselect(null);

        optButton.interactable = true;
        optPanel.SetActive(false);
        optButton.OnDeselect(null);

        itemSubMenuPanel.SetActive(false);
        weaponSubMenuPanel.SetActive(false);
        keyItemSubMenuPanel.SetActive(false);
        upgradeSubMenuPanel.SetActive(false);

        KeyboardControlsPanel.SetActive(false);
    }

    public void PressColButton()
    {
        invButton.interactable = true;
        invPanel.SetActive(false);
        invButton.OnDeselect(null);

        surButton.interactable = true;
        surPanel.SetActive(false);
        surButton.OnDeselect(null);

        colButton.interactable = false;
        colPanel.SetActive(true);
        colButton.OnDeselect(null);

        mapButton.interactable = true;
        mapPanel.SetActive(false);
        mapButton.OnDeselect(null);

        optButton.interactable = true;
        optPanel.SetActive(false);
        optButton.OnDeselect(null);

        itemSubMenuPanel.SetActive(false);
        weaponSubMenuPanel.SetActive(false);
        keyItemSubMenuPanel.SetActive(false);
        upgradeSubMenuPanel.SetActive(false);

        KeyboardControlsPanel.SetActive(false);
    }

    public void PressMapButton()
    {
        invButton.interactable = true;
        invPanel.SetActive(false);
        invButton.OnDeselect(null);

        surButton.interactable = true;
        surPanel.SetActive(false);
        surButton.OnDeselect(null);

        colButton.interactable = true;
        colPanel.SetActive(false);
        colButton.OnDeselect(null);

        mapButton.interactable = false;
        mapPanel.SetActive(true);
        mapButton.OnDeselect(null);

        optButton.interactable = true;
        optPanel.SetActive(false);
        optButton.OnDeselect(null);

        itemSubMenuPanel.SetActive(false);
        weaponSubMenuPanel.SetActive(false);
        keyItemSubMenuPanel.SetActive(false);
        upgradeSubMenuPanel.SetActive(false);

        KeyboardControlsPanel.SetActive(false);

        MapZoomSlider.enabled = true;
    }

    public void PressOptButton()
    {
        invButton.interactable = true;
        invPanel.SetActive(false);
        invButton.OnDeselect(null);

        surButton.interactable = true;
        surPanel.SetActive(false);
        surButton.OnDeselect(null);

        colButton.interactable = true;
        colPanel.SetActive(false);
        colButton.OnDeselect(null);

        mapButton.interactable = true;
        mapPanel.SetActive(false);
        mapButton.OnDeselect(null);

        optButton.interactable = false;
        optPanel.SetActive(true);
        optButton.OnDeselect(null);

        itemSubMenuPanel.SetActive(false);
        weaponSubMenuPanel.SetActive(false);
        keyItemSubMenuPanel.SetActive(false);
        upgradeSubMenuPanel.SetActive(false);

        KeyboardControlsPanel.SetActive(false);
    }

    public void PressInvItemButton()
    {
        invItemButton.interactable = false;
        invItemsPanel.SetActive(true);

        invWeaponButton.interactable = true;
        invWeaponButton.OnDeselect(null);
        invWeaponPanel.SetActive(false);

        invKeyItemButton.interactable = true;
        invKeyItemButton.OnDeselect(null);
        invKeyItemPanel.SetActive(false);

        invUpgradeButton.interactable = true;
        invUpgradeButton.OnDeselect(null);
        invUpgradePanel.SetActive(false);

        itemSubMenuPanel.SetActive(false);
        weaponSubMenuPanel.SetActive(false);
        keyItemSubMenuPanel.SetActive(false);
        upgradeSubMenuPanel.SetActive(false);
    }

    public void PressInvWeaponButton()
    {
        invItemButton.interactable = true;
        invItemButton.OnDeselect(null);
        invItemsPanel.SetActive(false);

        invWeaponButton.interactable = false;
        invWeaponPanel.SetActive(true);

        invKeyItemButton.interactable = true;
        invKeyItemButton.OnDeselect(null);
        invKeyItemPanel.SetActive(false);

        invUpgradeButton.interactable = true;
        invUpgradeButton.OnDeselect(null);
        invUpgradePanel.SetActive(false);

        itemSubMenuPanel.SetActive(false);
        weaponSubMenuPanel.SetActive(false);
        keyItemSubMenuPanel.SetActive(false);
        upgradeSubMenuPanel.SetActive(false);
    }

    public void PressInvKeyItemButton()
    {
        invItemButton.interactable = true;
        invItemButton.OnDeselect(null);
        invItemsPanel.SetActive(false);

        invWeaponButton.interactable = true;
        invWeaponButton.OnDeselect(null);
        invWeaponPanel.SetActive(false);

        invKeyItemButton.interactable = false;
        invKeyItemPanel.SetActive(true);

        invUpgradeButton.interactable = true;
        invUpgradeButton.OnDeselect(null);
        invUpgradePanel.SetActive(false);

        itemSubMenuPanel.SetActive(false);
        weaponSubMenuPanel.SetActive(false);
        keyItemSubMenuPanel.SetActive(false);
        upgradeSubMenuPanel.SetActive(false);
    }

    public void PressInvUpgradeButton()
    {
        invItemButton.interactable = true;
        invItemButton.OnDeselect(null);
        invItemsPanel.SetActive(false);

        invWeaponButton.interactable = true;
        invWeaponButton.OnDeselect(null);
        invWeaponPanel.SetActive(false);

        invKeyItemButton.interactable = true;
        invKeyItemButton.OnDeselect(null);
        invKeyItemPanel.SetActive(false);

        invUpgradeButton.interactable = false;
        invUpgradePanel.SetActive(true);

        itemSubMenuPanel.SetActive(false);
        weaponSubMenuPanel.SetActive(false);
        keyItemSubMenuPanel.SetActive(false);
        upgradeSubMenuPanel.SetActive(false);
    }

    //TODO: Create scripts that contain all survival note info so those these functions can be edited to
    //include funcations that take that info and display it in the correct panels along with changing
    //the survival note buttons and text to reveal the notes.

    public void PressSurNote1Button()
    {
        SurvivalNote1.interactable = false;

        SurvivalNote2.interactable = true;
        SurvivalNote2.OnDeselect(null);

        SurvivalNote3.interactable = true;
        SurvivalNote3.OnDeselect(null);

        SurvivalNote4.interactable = true;
        SurvivalNote4.OnDeselect(null);

        SurvivalNote5.interactable = true;
        SurvivalNote5.OnDeselect(null);

        SurvivalNote6.interactable = true;
        SurvivalNote6.OnDeselect(null);

        SurvivalNote7.interactable = true;
        SurvivalNote7.OnDeselect(null);

        SurvivalNote8.interactable = true;
        SurvivalNote8.OnDeselect(null);

        SurvivalNote9.interactable = true;
        SurvivalNote9.OnDeselect(null);

        SurvivalNote10.interactable = true;
        SurvivalNote10.OnDeselect(null);
    }

    public void PressSurNote2Button()
    {
        SurvivalNote1.interactable = true;
        SurvivalNote1.OnDeselect(null);

        SurvivalNote2.interactable = false;

        SurvivalNote3.interactable = true;
        SurvivalNote3.OnDeselect(null);

        SurvivalNote4.interactable = true;
        SurvivalNote4.OnDeselect(null);

        SurvivalNote5.interactable = true;
        SurvivalNote5.OnDeselect(null);

        SurvivalNote6.interactable = true;
        SurvivalNote6.OnDeselect(null);

        SurvivalNote7.interactable = true;
        SurvivalNote7.OnDeselect(null);

        SurvivalNote8.interactable = true;
        SurvivalNote8.OnDeselect(null);

        SurvivalNote9.interactable = true;
        SurvivalNote9.OnDeselect(null);

        SurvivalNote10.interactable = true;
        SurvivalNote10.OnDeselect(null);
    }

    public void PressSurNote3Button()
    {
        SurvivalNote1.interactable = true;
        SurvivalNote1.OnDeselect(null);

        SurvivalNote2.interactable = true;
        SurvivalNote2.OnDeselect(null);

        SurvivalNote3.interactable = false;

        SurvivalNote4.interactable = true;
        SurvivalNote4.OnDeselect(null);

        SurvivalNote5.interactable = true;
        SurvivalNote5.OnDeselect(null);

        SurvivalNote6.interactable = true;
        SurvivalNote6.OnDeselect(null);

        SurvivalNote7.interactable = true;
        SurvivalNote7.OnDeselect(null);

        SurvivalNote8.interactable = true;
        SurvivalNote8.OnDeselect(null);

        SurvivalNote9.interactable = true;
        SurvivalNote9.OnDeselect(null);

        SurvivalNote10.interactable = true;
        SurvivalNote10.OnDeselect(null);
    }

    public void PressSurNote4Button()
    {
        SurvivalNote1.interactable = true;
        SurvivalNote1.OnDeselect(null);

        SurvivalNote2.interactable = true;
        SurvivalNote2.OnDeselect(null);

        SurvivalNote3.interactable = true;
        SurvivalNote3.OnDeselect(null);

        SurvivalNote4.interactable = false;

        SurvivalNote5.interactable = true;
        SurvivalNote5.OnDeselect(null);

        SurvivalNote6.interactable = true;
        SurvivalNote6.OnDeselect(null);

        SurvivalNote7.interactable = true;
        SurvivalNote7.OnDeselect(null);

        SurvivalNote8.interactable = true;
        SurvivalNote8.OnDeselect(null);

        SurvivalNote9.interactable = true;
        SurvivalNote9.OnDeselect(null);

        SurvivalNote10.interactable = true;
        SurvivalNote10.OnDeselect(null);
    }

    public void PressSurNote5Button()
    {
        SurvivalNote1.interactable = true;
        SurvivalNote1.OnDeselect(null);

        SurvivalNote2.interactable = true;
        SurvivalNote2.OnDeselect(null);

        SurvivalNote3.interactable = true;
        SurvivalNote3.OnDeselect(null);

        SurvivalNote4.interactable = true;
        SurvivalNote4.OnDeselect(null);

        SurvivalNote5.interactable = false;

        SurvivalNote6.interactable = true;
        SurvivalNote6.OnDeselect(null);

        SurvivalNote7.interactable = true;
        SurvivalNote7.OnDeselect(null);

        SurvivalNote8.interactable = true;
        SurvivalNote8.OnDeselect(null);

        SurvivalNote9.interactable = true;
        SurvivalNote9.OnDeselect(null);

        SurvivalNote10.interactable = true;
        SurvivalNote10.OnDeselect(null);
    }

    public void PressSurNote6Button()
    {
        SurvivalNote1.interactable = true;
        SurvivalNote1.OnDeselect(null);

        SurvivalNote2.interactable = true;
        SurvivalNote2.OnDeselect(null);

        SurvivalNote3.interactable = true;
        SurvivalNote3.OnDeselect(null);

        SurvivalNote4.interactable = true;
        SurvivalNote4.OnDeselect(null);

        SurvivalNote5.interactable = true;
        SurvivalNote5.OnDeselect(null);

        SurvivalNote6.interactable = false;

        SurvivalNote7.interactable = true;
        SurvivalNote7.OnDeselect(null);

        SurvivalNote8.interactable = true;
        SurvivalNote8.OnDeselect(null);

        SurvivalNote9.interactable = true;
        SurvivalNote9.OnDeselect(null);

        SurvivalNote10.interactable = true;
        SurvivalNote10.OnDeselect(null);
    }

    public void PressSurNote7Button()
    {
        SurvivalNote1.interactable = true;
        SurvivalNote1.OnDeselect(null);

        SurvivalNote2.interactable = true;
        SurvivalNote2.OnDeselect(null);

        SurvivalNote3.interactable = true;
        SurvivalNote3.OnDeselect(null);

        SurvivalNote4.interactable = true;
        SurvivalNote4.OnDeselect(null);

        SurvivalNote5.interactable = true;
        SurvivalNote5.OnDeselect(null);

        SurvivalNote6.interactable = true;
        SurvivalNote6.OnDeselect(null);

        SurvivalNote7.interactable = false;

        SurvivalNote8.interactable = true;
        SurvivalNote8.OnDeselect(null);

        SurvivalNote9.interactable = true;
        SurvivalNote9.OnDeselect(null);

        SurvivalNote10.interactable = true;
        SurvivalNote10.OnDeselect(null);
    }

    public void PressSurNote8Button()
    {
        SurvivalNote1.interactable = true;
        SurvivalNote1.OnDeselect(null);

        SurvivalNote2.interactable = true;
        SurvivalNote2.OnDeselect(null);

        SurvivalNote3.interactable = true;
        SurvivalNote3.OnDeselect(null);

        SurvivalNote4.interactable = true;
        SurvivalNote4.OnDeselect(null);

        SurvivalNote5.interactable = true;
        SurvivalNote5.OnDeselect(null);

        SurvivalNote6.interactable = true;
        SurvivalNote6.OnDeselect(null);

        SurvivalNote7.interactable = true;
        SurvivalNote7.OnDeselect(null);

        SurvivalNote8.interactable = false;

        SurvivalNote9.interactable = true;
        SurvivalNote9.OnDeselect(null);

        SurvivalNote10.interactable = true;
        SurvivalNote10.OnDeselect(null);
    }

    public void PressSurNote9Button()
    {
        SurvivalNote1.interactable = true;
        SurvivalNote1.OnDeselect(null);

        SurvivalNote2.interactable = true;
        SurvivalNote2.OnDeselect(null);

        SurvivalNote3.interactable = true;
        SurvivalNote3.OnDeselect(null);

        SurvivalNote4.interactable = true;
        SurvivalNote4.OnDeselect(null);

        SurvivalNote5.interactable = true;
        SurvivalNote5.OnDeselect(null);

        SurvivalNote6.interactable = true;
        SurvivalNote6.OnDeselect(null);

        SurvivalNote7.interactable = true;
        SurvivalNote7.OnDeselect(null);

        SurvivalNote8.interactable = true;
        SurvivalNote8.OnDeselect(null);

        SurvivalNote9.interactable = false;

        SurvivalNote10.interactable = true;
        SurvivalNote10.OnDeselect(null);
    }

    public void PressSurNote10Button()
    {
        SurvivalNote1.interactable = true;
        SurvivalNote1.OnDeselect(null);

        SurvivalNote2.interactable = true;
        SurvivalNote2.OnDeselect(null);

        SurvivalNote3.interactable = true;
        SurvivalNote3.OnDeselect(null);

        SurvivalNote4.interactable = true;
        SurvivalNote4.OnDeselect(null);

        SurvivalNote5.interactable = true;
        SurvivalNote5.OnDeselect(null);

        SurvivalNote6.interactable = true;
        SurvivalNote6.OnDeselect(null);

        SurvivalNote7.interactable = true;
        SurvivalNote7.OnDeselect(null);

        SurvivalNote8.interactable = true;
        SurvivalNote8.OnDeselect(null);

        SurvivalNote9.interactable = true;
        SurvivalNote9.OnDeselect(null);

        SurvivalNote10.interactable = false;
    }

    //TODO: Create scripts that contain all collectible info so these functions can be edited to include
    //functions that take that info and display it in the correct panels along with changing collectible
    //buttons and text to reveal the collectibles.

    public void PressCollectible1Button()
    {
        Collectible1.interactable = false;

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible2Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = false;

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible3Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = false;

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible4Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = false;

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible5Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = false;

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible6Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = false;

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible7Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = false;

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible8Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = false;

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible9Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = false;

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible10Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = false;

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible11Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = false;

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible12Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = false;

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible13Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = false;

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible14Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = false;

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible15Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = false;

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible16Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = false;

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible17Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = false;

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible18Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = false;

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible19Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = false;

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible20Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = false;

        Collectible21.interactable = true;
        Collectible21.OnDeselect(null);
    }

    public void PressCollectible21Button()
    {
        Collectible1.interactable = true;
        Collectible1.OnDeselect(null);

        Collectible2.interactable = true;
        Collectible2.OnDeselect(null);

        Collectible3.interactable = true;
        Collectible3.OnDeselect(null);

        Collectible4.interactable = true;
        Collectible4.OnDeselect(null);

        Collectible5.interactable = true;
        Collectible5.OnDeselect(null);

        Collectible6.interactable = true;
        Collectible6.OnDeselect(null);

        Collectible7.interactable = true;
        Collectible7.OnDeselect(null);

        Collectible8.interactable = true;
        Collectible8.OnDeselect(null);

        Collectible9.interactable = true;
        Collectible9.OnDeselect(null);

        Collectible10.interactable = true;
        Collectible10.OnDeselect(null);

        Collectible11.interactable = true;
        Collectible11.OnDeselect(null);

        Collectible12.interactable = true;
        Collectible12.OnDeselect(null);

        Collectible13.interactable = true;
        Collectible13.OnDeselect(null);

        Collectible14.interactable = true;
        Collectible14.OnDeselect(null);

        Collectible15.interactable = true;
        Collectible15.OnDeselect(null);

        Collectible16.interactable = true;
        Collectible16.OnDeselect(null);

        Collectible17.interactable = true;
        Collectible17.OnDeselect(null);

        Collectible18.interactable = true;
        Collectible18.OnDeselect(null);

        Collectible19.interactable = true;
        Collectible19.OnDeselect(null);

        Collectible20.interactable = true;
        Collectible20.OnDeselect(null);

        Collectible21.interactable = false;
    }

    //TODO: Add trigger zones at the entrance of each spot on a map where the player moves to a new floor
    //so conditional statements can be written to determine which floor to focus on when the map is first opened.
    //TODO: After trigger zones are added, consider referencing them using booleans and using those booleans in
    //the map shortcut key.

    public void PressOutskirtsMapButton()
    {
        OutskirtsMapButton.interactable = false;
        Outskirts1FButton.gameObject.SetActive(true);
        Outskirts1FButton.interactable = false;

        HospitalMapButton.interactable = true;
        HospitalMapButton.OnDeselect(null);
        HospitalB2Button.gameObject.SetActive(false);
        HospitalB1Button.gameObject.SetActive(false);
        Hospital1FButton.gameObject.SetActive(false);
        Hospital2FButton.gameObject.SetActive(false);
        Hospital3FButton.gameObject.SetActive(false);

        YearnAmharuMapButton.interactable = true;
        YearnAmharuMapButton.OnDeselect(null);
        YearnAmharu1FButton.gameObject.SetActive(false);

        ApartmentsMapButton.interactable = true;
        ApartmentsMapButton.OnDeselect(null);
        ApartmentsB1Button.gameObject.SetActive(false);
        Apartments1FButton.gameObject.SetActive(false);
        Apartments2FButton.gameObject.SetActive(false);
        Apartments3FButton.gameObject.SetActive(false);
        Apartments4FButton.gameObject.SetActive(false);

        PoliceStationMapButton.interactable = true;
        PoliceStationMapButton.OnDeselect(null);
        PoliceStation1FButton.gameObject.SetActive(false);
        PoliceStation2FButton.gameObject.SetActive(false);

        AbandonedMinesMapButton.interactable = true;
        AbandonedMinesMapButton.OnDeselect(null);
        AbandonedMines1FButton.gameObject.SetActive(false);
        AbandonedMinesB1Button.gameObject.SetActive(false);
        AbandonedMinesB2Button.gameObject.SetActive(false);
        AbandonedMinesB3Button.gameObject.SetActive(false);
    }

    public void PressHospitalMapButton()
    {
        OutskirtsMapButton.interactable = true;
        OutskirtsMapButton.OnDeselect(null);
        Outskirts1FButton.gameObject.SetActive(false);

        HospitalMapButton.interactable = false;
        HospitalB2Button.gameObject.SetActive(true);
        HospitalB2Button.interactable = true;
        HospitalB2Button.OnDeselect(null);
        HospitalB1Button.gameObject.SetActive(true);
        HospitalB1Button.interactable = true;
        HospitalB1Button.OnDeselect(null);
        Hospital1FButton.gameObject.SetActive(true);
        Hospital1FButton.interactable = false;
        Hospital2FButton.gameObject.SetActive(true);
        Hospital2FButton.interactable = true;
        Hospital2FButton.OnDeselect(null);
        Hospital3FButton.gameObject.SetActive(true);
        Hospital3FButton.interactable = true;
        Hospital3FButton.OnDeselect(null);

        YearnAmharuMapButton.interactable = true;
        YearnAmharuMapButton.OnDeselect(null);
        YearnAmharu1FButton.gameObject.SetActive(false);

        ApartmentsMapButton.interactable = true;
        ApartmentsMapButton.OnDeselect(null);
        ApartmentsB1Button.gameObject.SetActive(false);
        Apartments1FButton.gameObject.SetActive(false);
        Apartments2FButton.gameObject.SetActive(false);
        Apartments3FButton.gameObject.SetActive(false);
        Apartments4FButton.gameObject.SetActive(false);

        PoliceStationMapButton.interactable = true;
        PoliceStationMapButton.OnDeselect(null);
        PoliceStation1FButton.gameObject.SetActive(false);
        PoliceStation2FButton.gameObject.SetActive(false);

        AbandonedMinesMapButton.interactable = true;
        AbandonedMinesMapButton.OnDeselect(null);
        AbandonedMines1FButton.gameObject.SetActive(false);
        AbandonedMinesB1Button.gameObject.SetActive(false);
        AbandonedMinesB2Button.gameObject.SetActive(false);
        AbandonedMinesB3Button.gameObject.SetActive(false);
    }

    public void PressYearnAmharuButton()
    {
        OutskirtsMapButton.interactable = true;
        OutskirtsMapButton.OnDeselect(null);
        Outskirts1FButton.gameObject.SetActive(false);

        HospitalMapButton.interactable = true;
        HospitalMapButton.OnDeselect(null);
        HospitalB2Button.gameObject.SetActive(false);
        HospitalB1Button.gameObject.SetActive(false);
        Hospital1FButton.gameObject.SetActive(false);
        Hospital2FButton.gameObject.SetActive(false);
        Hospital3FButton.gameObject.SetActive(false);

        YearnAmharuMapButton.interactable = false;
        YearnAmharu1FButton.gameObject.SetActive(true);
        YearnAmharu1FButton.interactable = false;

        ApartmentsMapButton.interactable = true;
        ApartmentsMapButton.OnDeselect(null);
        ApartmentsB1Button.gameObject.SetActive(false);
        Apartments1FButton.gameObject.SetActive(false);
        Apartments2FButton.gameObject.SetActive(false);
        Apartments3FButton.gameObject.SetActive(false);
        Apartments4FButton.gameObject.SetActive(false);

        PoliceStationMapButton.interactable = true;
        PoliceStationMapButton.OnDeselect(null);
        PoliceStation1FButton.gameObject.SetActive(false);
        PoliceStation2FButton.gameObject.SetActive(false);

        AbandonedMinesMapButton.interactable = true;
        AbandonedMinesMapButton.OnDeselect(null);
        AbandonedMines1FButton.gameObject.SetActive(false);
        AbandonedMinesB1Button.gameObject.SetActive(false);
        AbandonedMinesB2Button.gameObject.SetActive(false);
        AbandonedMinesB3Button.gameObject.SetActive(false);
    }

    public void PressApartmentsMapButton()
    {
        OutskirtsMapButton.interactable = true;
        OutskirtsMapButton.OnDeselect(null);
        Outskirts1FButton.gameObject.SetActive(false);

        HospitalMapButton.interactable = true;
        HospitalMapButton.OnDeselect(null);
        HospitalB2Button.gameObject.SetActive(false);
        HospitalB1Button.gameObject.SetActive(false);
        Hospital1FButton.gameObject.SetActive(false);
        Hospital2FButton.gameObject.SetActive(false);
        Hospital3FButton.gameObject.SetActive(false);

        YearnAmharuMapButton.interactable = true;
        YearnAmharuMapButton.OnDeselect(null);
        YearnAmharu1FButton.gameObject.SetActive(false);

        ApartmentsMapButton.interactable = false;
        ApartmentsB1Button.gameObject.SetActive(true);
        ApartmentsB1Button.interactable = true;
        ApartmentsB1Button.OnDeselect(null);
        Apartments1FButton.gameObject.SetActive(true);
        Apartments1FButton.interactable = false;
        Apartments2FButton.gameObject.SetActive(true);
        Apartments2FButton.interactable = true;
        Apartments2FButton.OnDeselect(null);
        Apartments3FButton.gameObject.SetActive(true);
        Apartments3FButton.interactable = true;
        Apartments3FButton.OnDeselect(null);
        Apartments4FButton.gameObject.SetActive(true);
        Apartments4FButton.interactable = true;
        Apartments4FButton.OnDeselect(null);

        PoliceStationMapButton.interactable = true;
        PoliceStationMapButton.OnDeselect(null);
        PoliceStation1FButton.gameObject.SetActive(false);
        PoliceStation2FButton.gameObject.SetActive(false);

        AbandonedMinesMapButton.interactable = true;
        AbandonedMinesMapButton.OnDeselect(null);
        AbandonedMines1FButton.gameObject.SetActive(false);
        AbandonedMinesB1Button.gameObject.SetActive(false);
        AbandonedMinesB2Button.gameObject.SetActive(false);
        AbandonedMinesB3Button.gameObject.SetActive(false);
    }

    public void PressPoliceStationMapButton()
    {
        OutskirtsMapButton.interactable = true;
        OutskirtsMapButton.OnDeselect(null);
        Outskirts1FButton.gameObject.SetActive(false);

        HospitalMapButton.interactable = true;
        HospitalMapButton.OnDeselect(null);
        HospitalB2Button.gameObject.SetActive(false);
        HospitalB1Button.gameObject.SetActive(false);
        Hospital1FButton.gameObject.SetActive(false);
        Hospital2FButton.gameObject.SetActive(false);
        Hospital3FButton.gameObject.SetActive(false);

        YearnAmharuMapButton.interactable = true;
        YearnAmharuMapButton.OnDeselect(null);
        YearnAmharu1FButton.gameObject.SetActive(false);

        ApartmentsMapButton.interactable = true;
        ApartmentsMapButton.OnDeselect(null);
        ApartmentsB1Button.gameObject.SetActive(false);
        Apartments1FButton.gameObject.SetActive(false);
        Apartments2FButton.gameObject.SetActive(false);
        Apartments3FButton.gameObject.SetActive(false);
        Apartments4FButton.gameObject.SetActive(false);

        PoliceStationMapButton.interactable = false;
        PoliceStation1FButton.gameObject.SetActive(true);
        PoliceStation1FButton.interactable = false;
        PoliceStation2FButton.gameObject.SetActive(true);
        PoliceStation2FButton.interactable = true;
        PoliceStation1FButton.OnDeselect(null);

        AbandonedMinesMapButton.interactable = true;
        AbandonedMinesMapButton.OnDeselect(null);
        AbandonedMines1FButton.gameObject.SetActive(false);
        AbandonedMinesB1Button.gameObject.SetActive(false);
        AbandonedMinesB2Button.gameObject.SetActive(false);
        AbandonedMinesB3Button.gameObject.SetActive(false);
    }

    public void PressAbandonedMinesMapButton()
    {
        OutskirtsMapButton.interactable = true;
        OutskirtsMapButton.OnDeselect(null);
        Outskirts1FButton.gameObject.SetActive(false);

        HospitalMapButton.interactable = true;
        HospitalMapButton.OnDeselect(null);
        HospitalB2Button.gameObject.SetActive(false);
        HospitalB1Button.gameObject.SetActive(false);
        Hospital1FButton.gameObject.SetActive(false);
        Hospital2FButton.gameObject.SetActive(false);
        Hospital3FButton.gameObject.SetActive(false);

        YearnAmharuMapButton.interactable = true;
        YearnAmharuMapButton.OnDeselect(null);
        YearnAmharu1FButton.gameObject.SetActive(false);

        ApartmentsMapButton.interactable = true;
        ApartmentsMapButton.OnDeselect(null);
        ApartmentsB1Button.gameObject.SetActive(false);
        Apartments1FButton.gameObject.SetActive(false);
        Apartments2FButton.gameObject.SetActive(false);
        Apartments3FButton.gameObject.SetActive(false);
        Apartments4FButton.gameObject.SetActive(false);

        PoliceStationMapButton.interactable = true;
        PoliceStationMapButton.OnDeselect(null);
        PoliceStation1FButton.gameObject.SetActive(false);
        PoliceStation2FButton.gameObject.SetActive(false);


        AbandonedMinesMapButton.interactable = false;
        AbandonedMines1FButton.gameObject.SetActive(true);
        AbandonedMines1FButton.interactable = false;
        AbandonedMinesB1Button.gameObject.SetActive(true);
        AbandonedMinesB1Button.interactable = true;
        AbandonedMinesB1Button.OnDeselect(null);
        AbandonedMinesB2Button.gameObject.SetActive(true);
        AbandonedMinesB2Button.interactable = true;
        AbandonedMinesB2Button.OnDeselect(null);
        AbandonedMinesB3Button.gameObject.SetActive(true);
        AbandonedMinesB3Button.interactable = true;
        AbandonedMinesB3Button.OnDeselect(null);
    }

    public void PressAudioSettingsButton()
    {
        AudioSettingsButton.interactable = false;
        AudioSettingsPanel.SetActive(true);

        VideoSettingsButton.interactable = true;
        VideoSettingsButton.OnDeselect(null);
        VideoSettingsPanel.SetActive(false);

        ControlSettingsButton.interactable = true;
        ControlSettingsButton.OnDeselect(null);
        ControlSettingsPanel.SetActive(false);
        KeyboardControlsPanel.SetActive(false);

        GameplaySettingsButton.interactable = true;
        GameplaySettingsButton.OnDeselect(null);
        GameplaySettingsPanel.SetActive(false);
    }

    public void PressVideoSettingsButton()
    {
        AudioSettingsButton.interactable = true;
        AudioSettingsButton.OnDeselect(null);
        AudioSettingsPanel.SetActive(false);

        VideoSettingsButton.interactable = false;
        VideoSettingsPanel.SetActive(true);

        ControlSettingsButton.interactable = true;
        ControlSettingsButton.OnDeselect(null);
        ControlSettingsPanel.SetActive(false);
        KeyboardControlsPanel.SetActive(false);

        GameplaySettingsButton.interactable = true;
        GameplaySettingsButton.OnDeselect(null);
        GameplaySettingsPanel.SetActive(false);
    }

    public void PressControlSettingsButton()
    {
        AudioSettingsButton.interactable = true;
        AudioSettingsButton.OnDeselect(null);
        AudioSettingsPanel.SetActive(false);

        VideoSettingsButton.interactable = true;
        VideoSettingsButton.OnDeselect(null);
        VideoSettingsPanel.SetActive(false);

        ControlSettingsButton.interactable = false;
        ControlSettingsPanel.SetActive(true);
        KeyboardControlsPanel.SetActive(false);

        GameplaySettingsButton.interactable = true;
        GameplaySettingsButton.OnDeselect(null);
        GameplaySettingsPanel.SetActive(false);
    }

    public void PressGameplaySettingsButton()
    {
        AudioSettingsButton.interactable = true;
        AudioSettingsButton.OnDeselect(null);
        AudioSettingsPanel.SetActive(false);

        VideoSettingsButton.interactable = true;
        VideoSettingsButton.OnDeselect(null);
        VideoSettingsPanel.SetActive(false);

        ControlSettingsButton.interactable = true;
        ControlSettingsButton.OnDeselect(null);
        ControlSettingsPanel.SetActive(false);
        KeyboardControlsPanel.SetActive(false);

        GameplaySettingsButton.interactable = false;
        GameplaySettingsPanel.SetActive(true);
    }

    public void PressQuitGameButton()
    {
        Application.Quit();
    }

    public void PressKeyboardControlsButton()
    {
        KeyboardControlsPanel.SetActive(true);
    }

    public void PressKeyboardContCloseButton()
    {
        KeyboardControlsPanel.SetActive(false);
    }

    //TODO: Program functionality for the Save Settings, Reset to Default Settings, and other
    //sliders and toggles on the options panel.

    public void PressCloseButton()
    {
        Time.timeScale = 1;

        invCanvas.enabled = false;
        invPanel.SetActive(false);
        itemSubMenuPanel.SetActive(false);
        weaponSubMenuPanel.SetActive(false);
        keyItemSubMenuPanel.SetActive(false);
        upgradeSubMenuPanel.SetActive(false);
        invButton.interactable = true;
        invItemButton.interactable = true;
        invItemsPanel.SetActive(false);
        MapZoomSlider.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PressItemPanelButton1()
    {
        ItemPanelButton1.interactable = false;

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton2()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = false;

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton3()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = false;

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton4()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = false;

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton5()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = false;

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton6()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = false;

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton7()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = false;

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton8()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = false;

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton9()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = false;

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton10()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = false;

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton11()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = false;

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton12()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = false;

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton13()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = false;

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton14()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = false;

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton15()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = false;

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton16()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = false;

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton17()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = false;

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    public void PressItemPanelButton18()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = false;
    }

    public void DeactivateItemPanelButtons()
    {
        ItemPanelButton1.interactable = true;
        ItemPanelButton1.OnDeselect(null);

        ItemPanelButton2.interactable = true;
        ItemPanelButton2.OnDeselect(null);

        ItemPanelButton3.interactable = true;
        ItemPanelButton3.OnDeselect(null);

        ItemPanelButton4.interactable = true;
        ItemPanelButton4.OnDeselect(null);

        ItemPanelButton5.interactable = true;
        ItemPanelButton5.OnDeselect(null);

        ItemPanelButton6.interactable = true;
        ItemPanelButton6.OnDeselect(null);

        ItemPanelButton7.interactable = true;
        ItemPanelButton7.OnDeselect(null);

        ItemPanelButton8.interactable = true;
        ItemPanelButton8.OnDeselect(null);

        ItemPanelButton9.interactable = true;
        ItemPanelButton9.OnDeselect(null);

        ItemPanelButton10.interactable = true;
        ItemPanelButton10.OnDeselect(null);

        ItemPanelButton11.interactable = true;
        ItemPanelButton11.OnDeselect(null);

        ItemPanelButton12.interactable = true;
        ItemPanelButton12.OnDeselect(null);

        ItemPanelButton13.interactable = true;
        ItemPanelButton13.OnDeselect(null);

        ItemPanelButton14.interactable = true;
        ItemPanelButton14.OnDeselect(null);

        ItemPanelButton15.interactable = true;
        ItemPanelButton15.OnDeselect(null);

        ItemPanelButton16.interactable = true;
        ItemPanelButton16.OnDeselect(null);

        ItemPanelButton17.interactable = true;
        ItemPanelButton17.OnDeselect(null);

        ItemPanelButton18.interactable = true;
        ItemPanelButton18.OnDeselect(null);
    }

    //TODO: Add functionality to edit item panel text to receive an item's name, edit item panel count text
    //to receive the item's count, and edit item panel image to receive the item's image.

    public void PressWeaponPanelButton1()
    {
        WeaponPanelButton1.interactable = false;

        WeaponPanelButton2.interactable = true;
        WeaponPanelButton2.OnDeselect(null);

        WeaponPanelButton3.interactable = true;
        WeaponPanelButton3.OnDeselect(null);

        WeaponPanelButton4.interactable = true;
        WeaponPanelButton4.OnDeselect(null);

        WeaponPanelButton5.interactable = true;
        WeaponPanelButton5.OnDeselect(null);

        WeaponPanelButton6.interactable = true;
        WeaponPanelButton6.OnDeselect(null);
    }

    public void PressWeaponPanelButton2()
    {
        WeaponPanelButton1.interactable = true;
        WeaponPanelButton1.OnDeselect(null);

        WeaponPanelButton2.interactable = false;

        WeaponPanelButton3.interactable = true;
        WeaponPanelButton3.OnDeselect(null);

        WeaponPanelButton4.interactable = true;
        WeaponPanelButton4.OnDeselect(null);

        WeaponPanelButton5.interactable = true;
        WeaponPanelButton5.OnDeselect(null);

        WeaponPanelButton6.interactable = true;
        WeaponPanelButton6.OnDeselect(null);
    }

    public void PressWeaponPanelButton3()
    {
        WeaponPanelButton1.interactable = true;
        WeaponPanelButton1.OnDeselect(null);

        WeaponPanelButton2.interactable = true;
        WeaponPanelButton2.OnDeselect(null);

        WeaponPanelButton3.interactable = false;

        WeaponPanelButton4.interactable = true;
        WeaponPanelButton4.OnDeselect(null);

        WeaponPanelButton5.interactable = true;
        WeaponPanelButton5.OnDeselect(null);

        WeaponPanelButton6.interactable = true;
        WeaponPanelButton6.OnDeselect(null);
    }

    public void PressWeaponPanelButton4()
    {
        WeaponPanelButton1.interactable = true;
        WeaponPanelButton1.OnDeselect(null);

        WeaponPanelButton2.interactable = true;
        WeaponPanelButton2.OnDeselect(null);

        WeaponPanelButton3.interactable = true;
        WeaponPanelButton3.OnDeselect(null);

        WeaponPanelButton4.interactable = false;

        WeaponPanelButton5.interactable = true;
        WeaponPanelButton5.OnDeselect(null);

        WeaponPanelButton6.interactable = true;
        WeaponPanelButton6.OnDeselect(null);
    }

    public void PressWeaponPanelButton5()
    {
        WeaponPanelButton1.interactable = true;
        WeaponPanelButton1.OnDeselect(null);

        WeaponPanelButton2.interactable = true;
        WeaponPanelButton2.OnDeselect(null);

        WeaponPanelButton3.interactable = true;
        WeaponPanelButton3.OnDeselect(null);

        WeaponPanelButton4.interactable = true;
        WeaponPanelButton4.OnDeselect(null);

        WeaponPanelButton5.interactable = false;

        WeaponPanelButton6.interactable = true;
        WeaponPanelButton6.OnDeselect(null);
    }

    public void PressWeaponPanelButton6()
    {
        WeaponPanelButton1.interactable = true;
        WeaponPanelButton1.OnDeselect(null);

        WeaponPanelButton2.interactable = true;
        WeaponPanelButton2.OnDeselect(null);

        WeaponPanelButton3.interactable = true;
        WeaponPanelButton3.OnDeselect(null);

        WeaponPanelButton4.interactable = true;
        WeaponPanelButton4.OnDeselect(null);

        WeaponPanelButton5.interactable = true;
        WeaponPanelButton5.OnDeselect(null);

        WeaponPanelButton6.interactable = false;
    }

    public void DeactivateWeaponPanelButtons()
    {
        WeaponPanelButton1.interactable = true;
        WeaponPanelButton1.OnDeselect(null);

        WeaponPanelButton2.interactable = true;
        WeaponPanelButton2.OnDeselect(null);

        WeaponPanelButton3.interactable = true;
        WeaponPanelButton3.OnDeselect(null);

        WeaponPanelButton4.interactable = true;
        WeaponPanelButton4.OnDeselect(null);

        WeaponPanelButton5.interactable = true;
        WeaponPanelButton5.OnDeselect(null);

        WeaponPanelButton6.interactable = true;
        WeaponPanelButton6.OnDeselect(null);
    }

    //TODO: Add functionality to edit weapon panel text to receive a weapon's name, edit weapon panel ammo count text
    //to receive the weapon's ammo count, and edit weapon panel image to receive the weapon's image.

    public void PressKeyItemPanelButton1()
    {
        KeyItemPanelButton1.interactable = false;

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton2()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = false;

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton3()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = false;

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton4()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = false;

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton5()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = false;

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton6()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = false;

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton7()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = false;

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton8()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = false;

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton9()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = false;

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton10()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = false;

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton11()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = false;

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton12()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = false;

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton13()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = false;

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton14()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = false;

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton15()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = false;

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton16()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = false;

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton17()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = false;

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    public void PressKeyItemPanelButton18()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = false;
    }

    public void DeactivateKeyItemPanelButtons()
    {
        KeyItemPanelButton1.interactable = true;
        KeyItemPanelButton1.OnDeselect(null);

        KeyItemPanelButton2.interactable = true;
        KeyItemPanelButton2.OnDeselect(null);

        KeyItemPanelButton3.interactable = true;
        KeyItemPanelButton3.OnDeselect(null);

        KeyItemPanelButton4.interactable = true;
        KeyItemPanelButton4.OnDeselect(null);

        KeyItemPanelButton5.interactable = true;
        KeyItemPanelButton5.OnDeselect(null);

        KeyItemPanelButton6.interactable = true;
        KeyItemPanelButton6.OnDeselect(null);

        KeyItemPanelButton7.interactable = true;
        KeyItemPanelButton7.OnDeselect(null);

        KeyItemPanelButton8.interactable = true;
        KeyItemPanelButton8.OnDeselect(null);

        KeyItemPanelButton9.interactable = true;
        KeyItemPanelButton9.OnDeselect(null);

        KeyItemPanelButton10.interactable = true;
        KeyItemPanelButton10.OnDeselect(null);

        KeyItemPanelButton11.interactable = true;
        KeyItemPanelButton11.OnDeselect(null);

        KeyItemPanelButton12.interactable = true;
        KeyItemPanelButton12.OnDeselect(null);

        KeyItemPanelButton13.interactable = true;
        KeyItemPanelButton13.OnDeselect(null);

        KeyItemPanelButton14.interactable = true;
        KeyItemPanelButton14.OnDeselect(null);

        KeyItemPanelButton15.interactable = true;
        KeyItemPanelButton15.OnDeselect(null);

        KeyItemPanelButton16.interactable = true;
        KeyItemPanelButton16.OnDeselect(null);

        KeyItemPanelButton17.interactable = true;
        KeyItemPanelButton17.OnDeselect(null);

        KeyItemPanelButton18.interactable = true;
        KeyItemPanelButton18.OnDeselect(null);
    }

    //TODO: Add functionality to edit upgrade panel text to receive a upgrade's name, edit upgrade panel mastered image
    //to receive the upgrade star image when mastered, and edit upgrade panel image to receive the upgrade's image (if
    //there is one).

    public void PressUpgradePanelButton1()
    {
        UpgradePanelButton1.interactable = false;

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton2()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = false;

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton3()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = false;

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton4()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = false;

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton5()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = false;

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton6()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = false;

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton7()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = false;

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton8()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = false;

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton9()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = false;

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton10()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = false;

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton11()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = false;

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton12()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = false;

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton13()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = false;

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton14()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = false;

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton15()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = false;

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton16()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = false;

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton17()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = false;

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }

    public void PressUpgradePanelButton18()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = false;
    }

    public void DeactivateUpgradePanelButtons()
    {
        UpgradePanelButton1.interactable = true;
        UpgradePanelButton1.OnDeselect(null);

        UpgradePanelButton2.interactable = true;
        UpgradePanelButton2.OnDeselect(null);

        UpgradePanelButton3.interactable = true;
        UpgradePanelButton3.OnDeselect(null);

        UpgradePanelButton4.interactable = true;
        UpgradePanelButton4.OnDeselect(null);

        UpgradePanelButton5.interactable = true;
        UpgradePanelButton5.OnDeselect(null);

        UpgradePanelButton6.interactable = true;
        UpgradePanelButton6.OnDeselect(null);

        UpgradePanelButton7.interactable = true;
        UpgradePanelButton7.OnDeselect(null);

        UpgradePanelButton8.interactable = true;
        UpgradePanelButton8.OnDeselect(null);

        UpgradePanelButton9.interactable = true;
        UpgradePanelButton9.OnDeselect(null);

        UpgradePanelButton10.interactable = true;
        UpgradePanelButton10.OnDeselect(null);

        UpgradePanelButton11.interactable = true;
        UpgradePanelButton11.OnDeselect(null);

        UpgradePanelButton12.interactable = true;
        UpgradePanelButton12.OnDeselect(null);

        UpgradePanelButton13.interactable = true;
        UpgradePanelButton13.OnDeselect(null);

        UpgradePanelButton14.interactable = true;
        UpgradePanelButton14.OnDeselect(null);

        UpgradePanelButton15.interactable = true;
        UpgradePanelButton15.OnDeselect(null);

        UpgradePanelButton16.interactable = true;
        UpgradePanelButton16.OnDeselect(null);

        UpgradePanelButton17.interactable = true;
        UpgradePanelButton17.OnDeselect(null);

        UpgradePanelButton18.interactable = true;
        UpgradePanelButton18.OnDeselect(null);
    }
}
