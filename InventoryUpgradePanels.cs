using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUpgradePanels : MonoBehaviour
{
    //This script activates the upgrade submenu panel, parents it, and moves it to the upgrade button within the upgrade panel
    //that is pressed. 

    //TODO: Edit script when actual upgrades are created so upgrades can be equipped and examined.

    [SerializeField] private GameObject invUpgradesPanel;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject upgradeSubMenuPanel;
    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void EnableSubMenu()
    {
        upgradeSubMenuPanel.SetActive(true);
        upgradeSubMenuPanel.transform.parent = upgradePanel.transform;
        upgradeSubMenuPanel.transform.localPosition = new Vector3(55, -45);
        upgradeSubMenuPanel.transform.parent = invUpgradesPanel.transform;
    }
}
