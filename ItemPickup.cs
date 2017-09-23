using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemInfo itemInfo;
    [SerializeField] private ItemManager itemManager;
    //[SerializeField] private GameObject SmallFirstAidKit;
    //[SerializeField] private GameObject LargeFirstAidKit;
    //[SerializeField] private GameObject PistolAmmo;
    //[SerializeField] private GameObject ShotgunAmmo;
    //[SerializeField] private GameObject RifleAmmo;
    //[SerializeField] private GameObject Fuel;
    //[SerializeField] private GameObject RoundCutS;
    //[SerializeField] private GameObject RoundCutM;
    //[SerializeField] private GameObject RoundCutL;
    //[SerializeField] private GameObject RoseCutS;
    //[SerializeField] private GameObject RoseCutM;
    //[SerializeField] private GameObject RoseCutL;
    //[SerializeField] private GameObject PearCutS;
    //[SerializeField] private GameObject PearCutM;
    //[SerializeField] private GameObject PearCutL;


    void Start ()
    {

	}

	void Update ()
    {
		
	}

    public void PickupItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //TODO: Add assist items to switch statement.
            //TODO: Come up with a total max variable that multiplies the number of empty panels by max panel count,
            //then compare them to current counts and what the count that will be picked up will be.
            switch (FirstPersonController.tag)
            {
                case "Small First Aid Kit":
                    itemManager.AddItem();
                    break;
                case "Large First Aid Kit":
                    itemManager.AddItem();
                    break;
                case "Pistol Ammo":
                    itemManager.AddItem();
                    break;
                case "Shotgun Ammo":
                    itemManager.AddItem();
                    break;
                case "Rifle Ammo":
                    itemManager.AddItem();
                    break;
                case "Fuel":
                    itemManager.AddItem();
                    break;
                case "Round Cut S":
                    itemManager.AddCurrency();
                    break;
                case "Round Cut M":
                    itemManager.AddCurrency();
                    break;
                case "Round Cut L":
                    itemManager.AddCurrency();
                    break;
                case "Rose Cut S":
                    itemManager.AddCurrency();
                    break;
                case "Rose Cut M":
                    itemManager.AddCurrency();
                    break;
                case "Rose Cut L":
                    itemManager.AddCurrency();
                    break;
                case "Pear Cut S":
                    itemManager.AddCurrency();
                    break;
                case "Pear Cut M":
                    itemManager.AddCurrency();
                    break;
                case "Pear Cut L":
                    itemManager.AddCurrency();
                    break;
            }
        }
    }
}
