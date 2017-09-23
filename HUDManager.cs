using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class HUDManager : MonoBehaviour
{
    //This script will manage everything that is displayed on the HUD. It will get data from other
    //scripts to determine what to display and when to display it. Some elements will always
    //be displayed.

    [SerializeField] private EnvironmentInfo environmentInfo;
    [SerializeField] private InteractPrompts interactPrompts;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private SurvivalNoteInfo survivalNoteInfo;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;

    //[SerializeField] private Text popupPrompt;
    //[SerializeField] private Text popupText;

    //TODO: Need to add a crosshair that can change from four lines that look like a plus sign
    //with the center part cut out to a circle, depending on if the shotgun is equipped or not.
    //The plus sign lines should move in and out based on movement speed IF a pistol or rifle is equipped
    //along with the accuracy of those weapons. (If there is a hidden accuracy value for them.)

	void Start ()
    {

	}
	
	void Update ()
    {
        healthSlider.value = playerInfo.currentHealth;
        staminaSlider.value = playerInfo.currentStamina;

        //TODO: Return to this switch statement when all possible tags, or whatever is used, have been determined.
        switch (FirstPersonController.tag)
        {

            case "Small First Aid Kit":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Large First Aid Kit":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Pistol Ammo":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Shotgun Ammo":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Rifle Ammo":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Fuel":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Round Cut S":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Round Cut M":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Round Cut L":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Rose Cut S":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Rose Cut M":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Rose Cut L":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Pear Cut S":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Pear Cut M":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Pear Cut L":
                interactPrompts.ItemPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Pipe":
                interactPrompts.WeaponPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Pickaxe":
                interactPrompts.WeaponPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Pistol":
                interactPrompts.WeaponPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Shotgun":
                interactPrompts.WeaponPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Rifle":
                interactPrompts.WeaponPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Collectible":
                interactPrompts.CollectiblePrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Door":
                interactPrompts.DoorPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;

            case "Chest":
                interactPrompts.ChestPrompt();
                interactPrompts.DisplayPopupPrompt();
                break;
            default:
                interactPrompts.HidePopupPrompt();
                break;
        }
	}
}
