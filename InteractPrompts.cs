using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractPrompts : MonoBehaviour
{
    //This script determines what text pops up on the HUD and calls the appropriate functions from
    //other pick up scripts that get key presses to "pick up" the item.

   [SerializeField] private WeaponPickup weaponPickup;
   [SerializeField] private ItemPickup itemPickup;
   [SerializeField] private CollectiblePickup collectiblePickup;
   [SerializeField] private EnvironmentInfo environmentInfo;

    private string target;

    [SerializeField] private Text popupPrompt;
    [SerializeField] private Text popupText;

    private string doorState;
    private string chestState;
    private string containerState;
    private new string tag;
    private string objectName;

	void Start ()
    {
		
	}

	void Update ()
    {
        tag = FirstPersonController.tag;
        objectName = FirstPersonController.gameObjectName;
    }

    public void WeaponPrompt()
    {
        weaponPickup.PickupWeapon();
        popupPrompt.text = "Pick up " + tag;
        popupPrompt.color = new Color(0.588f, 0.588f, 0.588f);
        //popupPrompt.color = new Color(1f, 0.784f, 0f);
        popupPrompt.enabled = true;
    }

    public void ItemPrompt()
    {
        itemPickup.PickupItem();
        popupPrompt.text = "Pick up " + tag;
        popupPrompt.color = new Color(0.588f, 0.588f, 0.588f);
        popupPrompt.enabled = true;
    }

    public void CollectiblePrompt()
    {
        //Add code here to display text popup to inform player pressing E can pick up the collectible
        //Add function to pick up collectible
        //Add function to display collectible
    }

    public void DoorPrompt()
    {
        switch (doorState)
        {
            case "Unlocked":
                popupPrompt.text = "Open door.";
                popupPrompt.enabled = true;
                break;
            default:
                popupPrompt.text = "Examine door.";
                popupPrompt.enabled = true;
                break;
        }
    }

    public void ChestPrompt()
    {
        switch (chestState)
        {
            case "Unlocked":
                popupPrompt.text = "Open chest.";
                popupPrompt.enabled = true;
                break;
            default:
                popupPrompt.text = "Examine chest";
                popupPrompt.enabled = true;
                break;
        }
    }

    public void DisplayPopupPrompt()
    {
        popupPrompt.enabled = true;
    }

    public void HidePopupPrompt()
    {
        popupPrompt.enabled = false;
    }

    public void DisplayPopupText()
    {
        Time.timeScale = 0;
        popupText.enabled = true;
    }

    public void HidePopupText()
    {
        Time.timeScale = 1;
        popupText.enabled = false;
    }

    //This function should be applied to interact prompt text.
    //public void ItemTextColor()
    //{
    //    switch (itemType)
    //    {
    //        case "Healing":
    //            ItemNameText1.color = new Color(0, 100, 0);
    //            break;
    //        case "Ammo":
    //            ItemNameText1.color = new Color(255, 255, 0);
    //            break;
    //        case "Fuel":
    //            ItemNameText1.color = new Color(200, 100, 0);
    //            break;
    //        case "Key Item":
    //            ItemNameText1.color = new Color(0, 100, 255);
    //            break;
    //        case "Upgrade":
    //            ItemNameText1.color = new Color(143, 0, 255);
    //            break;
    //        //case "Weapon":
    //        //    ItemNameText1.color = new Color(200, 0, 0);
    //        //    break;
    //    }
    //}
}
