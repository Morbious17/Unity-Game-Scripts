using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemPanels : MonoBehaviour
{
    //This script assigns variables to each panel, such as item image, item count, etc. and 
    //activates the item submenu panel, parents it, and moves it to the item button within the item panel
    //that is pressed. 

    //TODO: Edit script when actual items and a health meter is created so items can be used, examined, and discarded.

    [SerializeField] private GameObject invItemsPanel;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject itemSubMenuPanel;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemCountText;

    //This bool ensures the num integer is only incremented or decremented once at a time when the panel is empty or not
    private bool stateChangedOnce = false;

    public bool panelIsEmpty = false;

	void Start ()
    {
        itemImage = null;
        itemNameText.text = "";
        itemCountText.text = "";
	}

	void Update ()
    {
        //If the panel does not contain an item, increment the num integer in ui controller script.
        if (itemImage == null && itemNameText.text == "" && itemCountText.text == "" && stateChangedOnce == false)
        {
            UIController.NumOfEmptyItemPanels++;
            panelIsEmpty = true;
            stateChangedOnce = true;
        }

        //If the panel does contain an item, decrement the num integer in ui controller script.
        if (itemImage != null && itemNameText.text != "" && itemCountText.text != "" && stateChangedOnce == true)
        {
            UIController.NumOfEmptyItemPanels--;
            panelIsEmpty = false;
            stateChangedOnce = false;
        }
	}

    public void EnableSubMenu()
    {
        itemSubMenuPanel.SetActive(true);
        itemSubMenuPanel.transform.parent = itemPanel.transform;
        itemSubMenuPanel.transform.localPosition = new Vector3(55, -45);
        itemSubMenuPanel.transform.parent = invItemsPanel.transform;
    }
}
