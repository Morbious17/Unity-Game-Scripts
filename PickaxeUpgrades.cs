/******************************************************************************
  File Name: PickaxeUpgrades.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that upgrade the pickaxe if the
             object this script is attached to is picked up.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeUpgrades : MonoBehaviour
{
    [SerializeField] private Pickaxe pickaxe = null;
    [SerializeField] private WeaponManager weaponManager = null;

    private void HitByRay() //if player is looking at this object
    {
        if (Input.GetKeyDown(KeyCode.E)) //if player presses E
        {
              //get name of this object to apply specific upgrade
            pickaxe.SetUpgrade(gameObject.name);
            weaponManager.UpdateWeaponUpgradeSliders();
            Destroy(gameObject);
        }
    }
}
