using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject Pipe;
    [SerializeField] private GameObject Pickaxe;
    [SerializeField] private WeaponPickup weaponPickup;

    void Start ()
    {

	}
	
    //TODO: When all weapons are create, made child objects of FPS object, and functionality in the menu
    //is done, return to this script to ensure only one weapon can be SetActive(true) at a time.

	void Update ()
    {
        //Debug.Log(weaponPickup.IsPickedUp);

        if (weaponPickup.IsPickedUp)
        {
            switch(weaponPickup.weaponName)
            {
                case "PipeTrigger":
                    Pipe.SetActive(true);
                    weaponPickup.IsPickedUp = false;
                    break;
                case "PickaxeTrigger":
                    Pickaxe.SetActive(true);
                    weaponPickup.IsPickedUp = false;
                    break;
            }          
        }
    }
}
