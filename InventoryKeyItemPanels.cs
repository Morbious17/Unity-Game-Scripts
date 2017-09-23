using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryKeyItemPanels : MonoBehaviour
{
    //This script activates the key item submenu panel, parents it, and moves it to the key item button within the key item panel
    //that is pressed. 

    //TODO: Edit script when actual key items are created so key items can be used and examined.

    [SerializeField] private GameObject invKeyItemPanel;
    [SerializeField] private GameObject keyItemPanel;
    [SerializeField] private GameObject keyItemSubMenuPanel;

    void Start ()
    {
		
	}

	void Update ()
    {
		
	}

    public void EnableSubMenu()
    {
        keyItemSubMenuPanel.SetActive(true);
        keyItemSubMenuPanel.transform.parent = keyItemPanel.transform;
        keyItemSubMenuPanel.transform.localPosition = new Vector3(55, -45);
        keyItemSubMenuPanel.transform.parent = invKeyItemPanel.transform;
    }
}
