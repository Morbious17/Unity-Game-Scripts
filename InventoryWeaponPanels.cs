using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWeaponPanels : MonoBehaviour
{
    //This script activates the weapon submenu panel, parents it, and moves it to the weapon button within the weapon panel
    //that is pressed. 

    //TODO: Edit script when actual weapons are created so weapons can be equipped and examined.

    [SerializeField] private GameObject invWeaponsPanel;
    [SerializeField] private GameObject weaponPanel;
    [SerializeField] private GameObject weaponSubMenuPanel;

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void EnableSubMenu()
    {
        weaponSubMenuPanel.SetActive(true);
        weaponSubMenuPanel.transform.parent = weaponPanel.transform;
        weaponSubMenuPanel.transform.localPosition = new Vector3(55, -45);
        weaponSubMenuPanel.transform.parent = invWeaponsPanel.transform;
    }
}
