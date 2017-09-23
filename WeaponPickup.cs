using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

//This script makes it possible to pick up weapons.
public class WeaponPickup : MonoBehaviour
{

    public bool IsPickedUp = false;
    public string weaponName;

	void Start ()
    {

	}

	void Update ()
    {

    }

    public void PickupWeapon()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            weaponName = FirstPersonController.gameObjectName;
            IsPickedUp = true;
            Destroy(FirstPersonController.targetedObject);
        }
    }
}
