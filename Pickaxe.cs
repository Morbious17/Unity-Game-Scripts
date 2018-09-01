/******************************************************************************
  File Name: Pickaxe.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains a function for manipulating the pickaxe melee
             weapon, its damage, stamina use, and animations.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Pickaxe : MonoBehaviour
{
    [SerializeField] private SurvivalNoteManager survivalNoteManager = null;
      //used to communicate with player object
    [SerializeField] private FirstPersonController firstPersonController = null;
      //used to communicate with the HUD
    [SerializeField] private HUDManager hudManager = null;
    [SerializeField] private WeaponManager weaponManager = null;
    [SerializeField] private Pickaxe pickaxe = null;
    [SerializeField] private Animator pickaxeAnim = null; //contains all animations
    private MeshRenderer meshRenderer = null; //used to make visible/invisible
    private BoxCollider boxCollider = null; //used for collision detection
    private const float firstSwingUse = 20.0f; //stamina used with first attack
    private const float secondSwingUse = 30.0f; //stamina used with second attack
    private const float defaultAverageDamage = 55.0f;
    private const float defaultFirstSwingDamage = 40.0f; //damage dealt with first attack
    private const float defaultSecondSwingDamage = 70.0f; //damage dealt with second attack
    private const float defaultRange = 0.0f; //default range of the pipe
    private const float defaultReloadSpeed = 0.0f; //default reload speed of the pipe
    private const float defaultRateOfFire = 2.0f; //default rate of fire of the pipe
    private const float firstDamageBonus = 10.0f; //value of 1st damage upgrade
    private const float secondDamageBonus = 20.0f; //value of 2nd damage upgrade
    private const float firstRangeBonus = 10.0f; //value of final upgrade's range bonus
    private float currentAverageDamage; //the current average damage of the pipe
    private float currentFirstSwingDamage;
    private float currentSecondSwingDamage;
    private float currentRange; //the current range of the pipe
    private float currentRateOfFire; //the current rate of fire of the pipe
    private float currentReloadSpeed; //the current reload speed of the pipe
    private bool swinging; //used to deal damage during collision check
    private bool firstSwingReady; //used to play first swing animation
    private bool secondSwingReady; //used to play second swing animation
    private bool equipped; //used to check if weapon is equipped
    private bool secondary; //is true if this weapon is in secondary slot
    private bool weaknessHit; //used to ensure weakness is hit up to once per swing
    private bool damageUpgrade1; //increases weapon damage once
    private bool damageUpgrade2; //increases weapon damage a second time
    private bool rangeUpgrade1; //increases weapon range once
    //TODO: when merchant is implemented, return to this file to create and
    //implement damage and "stamina" upgrades.

    void Start ()
    {
        SetMeshRenderer(ref meshRenderer); //retrieves meshRenderer component
        SetBoxCollider(ref boxCollider); //retrieves collider component

        SetDamage(); //sets damage done to enemies
          //these setters are for the weapon stat panel
        SetAverageDamage(defaultAverageDamage);
        SetRange(defaultRange);
        SetRateOfFire(defaultRateOfFire);
        SetReloadSpeed(defaultReloadSpeed);

        UpgradePickaxe(); //apply any pickaxe upgrades
    }
	
	void Update ()
    {
          //TODO: remove this when done testing
        if (Input.GetKeyDown(KeyCode.V)) //test code to enable/disable pickaxe
        {
            if (equipped)
            {
                UnequipPickaxe();
            }
            else
            {
                EquipPickaxe();
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            UnequipPickaxe();
        }
    }

    /**************************************************************************
   Function: SetUpgrade

Description: Given a string, this function sets the upgrade of the given name
             to true. Then it calls the UpgradePickaxe function.

      Input: upgradeName - string of the upgrade to set

     Output: none
    **************************************************************************/
    public void SetUpgrade(string upgradeName)
    {
        switch (upgradeName)
        {
            case "PickaxeDamageUpgrade1":
                damageUpgrade1 = true;
                break;
            case "PickaxeDamageUpgrade2":
                damageUpgrade2 = true;
                break;
            case "PickaxeRangeUpgrade1":
                rangeUpgrade1 = true;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        UpgradePickaxe(); //NOTE: this must be called once for each upgrade
    }

    /**************************************************************************
   Function: UpgradePickaxe

Description: This function checks each upgrade bool. If any are true, the
             pickaxe's current stat related to that bool is updated.

      Input: none

     Output: none
    **************************************************************************/
    public void UpgradePickaxe()
    {
        if (damageUpgrade2) //if player has 2nd damage upgrade
        {
              //10.0f added to each attack
            SetAverageDamage(defaultAverageDamage + (secondDamageBonus * 2));

            currentFirstSwingDamage = defaultFirstSwingDamage + secondDamageBonus;
            currentSecondSwingDamage = defaultSecondSwingDamage + secondDamageBonus;
        }
        else if (damageUpgrade1) //if the player only has 1st damage upgrade
        {
              //5.0f added to each attack
            SetAverageDamage(defaultAverageDamage + (firstDamageBonus * 2));

            currentFirstSwingDamage = defaultFirstSwingDamage + firstDamageBonus;
            currentSecondSwingDamage = defaultSecondSwingDamage + firstDamageBonus;
        }

        if (rangeUpgrade1) //if player has first range upgrade
        {
            currentRange = defaultRange + firstRangeBonus; //TODO: extended box collider of pickaxe. this will be part of final upgrade's bonus
            IncreaseColliderRange();
        }
    }


    //TODO: change this to AoE later for final upgrade shockwave attack
    public void IncreaseColliderRange()
    {
          //increases the pipe's reach
        boxCollider.size = new Vector3(boxCollider.size.x + (firstRangeBonus / 2.0f),
                                       boxCollider.size.y + firstRangeBonus,
                                       boxCollider.size.z + (firstRangeBonus / 2.0f));
          //repositions the pipe's box collider
        boxCollider.center = new Vector3(boxCollider.center.x,
                                         boxCollider.center.y + 4.9f,
                                         boxCollider.center.z);
    }

    /**************************************************************************
   Function: GetDamageUpgrade1

Description: This function returns the bool representing whether or not the
             player has the first damage upgrade.

      Input: none

     Output: Returns true if player has the first pipe damage upgrade,
             otherwise, returns false.
    **************************************************************************/
    public bool GetDamageUpgrade1()
    {
        return damageUpgrade1;
    }

    /**************************************************************************
   Function: GetDamageUpgrade2

Description: This function returns the bool representing whether or not the
             player has the second damage upgrade.

      Input: none

     Output: Returns true if player has the second pipe damage upgrade,
             otherwise, returns false.
    **************************************************************************/
    public bool GetDamageUpgrade2()
    {
        return damageUpgrade2;
    }

    /**************************************************************************
   Function: GetRangeUpgrade1

Description: This function returns the bool representing whether or not the
             player has the first range upgrade.

      Input: none

     Output: Returns true if player has the first pipe range upgrade,
             otherwise, returns false.
    **************************************************************************/
    public bool GetRangeUpgrade1()
    {
        return rangeUpgrade1;
    }

    /**************************************************************************
   Function: GetDamageForStatPanel

Description: This function returns the default average damage of the pickaxe.

      Input: none

     Output: Returns average damage per swing of the pickaxe.
    **************************************************************************/
    public float GetDamageForStatPanel()
    {
        return defaultAverageDamage;
    }

    /**************************************************************************
   Function: GetRangeForStatPanel

Description: This function returns the current range of the pickaxe.

      Input: none

     Output: Returns the range of the pickaxe.
    **************************************************************************/
    public float GetRangeForStatPanel()
    {
        return defaultRange;
    }

    /**************************************************************************
   Function: GetReloadSpeedForStatPanel

Description: This function returns the current reload speed of the pickaxe.
             Because the pickaxe doesn't reload anything, this value is set to
             0.

      Input: none

     Output: Returns the reload speed of the pickaxe.
    **************************************************************************/
    public float GetReloadSpeedForStatPanel()
    {
        return defaultReloadSpeed;
    }

    /**************************************************************************
   Function: GetRateOfFireForStatPanel

Description: This function returns current attack speed of the pickaxe.

      Input: none

     Output: Returns the attack speed of the pickaxe.
    **************************************************************************/
    public float GetRateOfFireForStatPanel()
    {
        return defaultRateOfFire;
    }

    /**************************************************************************
   Function: EquipPickaxe

Description: This function plays the equipping animation and enables the 
             pickaxe's meshRenderer and collider, making it visible and able 
             to collide with enemies.

      Input: none

     Output: none
    **************************************************************************/
    public void EquipPickaxe()
    {
        pickaxeAnim.SetTrigger("isEquipping"); //plays equip animation

        meshRenderer.enabled = true;
        boxCollider.enabled = true;

        equipped = true;

        weaponManager.SetEquippedWeaponName(gameObject.tag);

        hudManager.SetWeaponNameText("Pickaxe");
        hudManager.SetMaxLoadedAmmoText(""); //melee weapon has no ammo
        hudManager.DisplayWeaponInfo(true);
        hudManager.UpdateWeaponInfo();
    }

    /**************************************************************************
   Function: UnequipPickaxe

Description: This function disables the pickaxes' meshRenderer and collider,
             making it invisible and unable to collide with enemies.

      Input: none

     Output: none
    **************************************************************************/
    public void UnequipPickaxe()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;

        equipped = false;
    }

    /**************************************************************************
   Function: GetUpgradedDamageForStatPanel

Description: This function returns the current damage per pipe attack. If the
             player has any damage upgrade, current damage is modified
             beforehand.

      Input: none

     Output: Returns the current damage per pipe attack.
    **************************************************************************/
    public float GetUpgradedDamageForStatPanel()
    {
        return currentAverageDamage;
    }

    /**************************************************************************
   Function: GetUpgradedRangeForStatPanel

Description: This function returns the current range of the pipe. If the
             player has any range upgrade, current range is modified
             beforehand.

      Input: none

     Output: Returns the current range of the pipe.
    **************************************************************************/
    public float GetUpgradedRangeForStatPanel()
    {
        return currentRange;
    }

    /**************************************************************************
   Function: GetUpgradedReloadSpeedForStatPanel

Description: This function returns the current reload speed of the pipe. If 
             the player has any reload upgrade, current reload speed is 
             modified beforehand.

      Input: none

     Output: Returns the current reload speed of the pipe.
    **************************************************************************/
    public float GetUpgradedReloadSpeedForStatPanel()
    {
        return currentReloadSpeed;
    }

    /**************************************************************************
   Function: GetUpgradedRateOfFireForStatPanel

Description: This function returns the current rate of fire of the pipe. If 
             the player has any rate of fire upgrade, current rate of fire is 
             modified beforehand.

      Input: none

     Output: Returns the current rate of fire of the pipe.
    **************************************************************************/
    public float GetUpgradedRateOfFireForStatPanel()
    {
        return currentRateOfFire;
    }

    /**************************************************************************
   Function: MoveForward

Description: Given a bool, this function either activates or deactivates the
             "Movingforward" animation.

      Input: forward - bool used to set or reset the moving animation

     Output: none
    **************************************************************************/
    public void MoveForward(bool forward)
    {
        if (forward)
        {
            pickaxeAnim.SetTrigger("MovingForward");
        }
        else
        {
            pickaxeAnim.ResetTrigger("MovingForward");
        }
    }

    /**************************************************************************
   Function: ChargeSwing

Description: This function sets the isCharging bool which causes the charge
             swing animation to play.

      Input: none

     Output: none
    **************************************************************************/
    public void ChargeSwing()
    {
        pickaxeAnim.SetBool("isCharging", true);
    }

    /**************************************************************************
   Function: ResetChargeSwing

Description: This function sets the isCharging bool to false which exits the
             charge swing animation and plays the third swing animation.

      Input: none

     Output: none
    **************************************************************************/
    public void ResetChargeSwing()
    {
        pickaxeAnim.SetBool("isCharging", false);
    }

    /**************************************************************************
   Function: CheckForPickaxeCharge

Description: This function checks of the player is holding down the right
             mouse button. If they are, the charging animation continues to
             play. Otherwise, the charging animation is exited.

      Input: none

     Output: none
    **************************************************************************/
    public void CheckForPickaxeCharge()
    {
        if (Input.GetMouseButton(1))
        {
            ChargeSwing();
        }
        else
        {
            ResetChargeSwing();
        }
    }

    /**************************************************************************
   Function: GetChargingStatus

Description: This function returns the status of whether or not the charge
             swing animation is playing.

      Input: none

     Output: none
    **************************************************************************/
    public bool GetChargingStatus()
    {
        return pickaxeAnim.GetCurrentAnimatorStateInfo(0).IsName("PipeCharge");
    }

    /**************************************************************************
   Function: Running

Description: Given a bool, this function either doubles the animation speed
             or return its speed to normal based on the bool.

      Input: isRunning - bool used to set animation speed

     Output: none
    **************************************************************************/
    public void Running(bool isRunning)
    {
        if (isRunning)
        {
            pickaxeAnim.SetBool("isSprinting", true);
            pickaxeAnim.speed = 2.0f;
        }
        else
        {
            pickaxeAnim.SetBool("isSprinting", false);
            pickaxeAnim.speed = 1.0f;
        }
    }

    /**************************************************************************
   Function: GetSwingingStatus

Description: This function returns the status of whether or not the player is
             swinging the pickaxe.

      Input: none

     Output: Returns true if the player is swinging the pickaxe, otherwise, 
             returns false.
    **************************************************************************/
    public bool GetSwingingStatus()
    {
        return swinging;
    }

    /**************************************************************************
   Function: UseFirstSwing

Description: This function sets the trigger which plays the "PickaxeSwing1"
             animation.

      Input: none

     Output: none
    **************************************************************************/
    public void UseFirstSwing()
    {
        pickaxeAnim.SetTrigger("usingFirstSwing");
    }

    /**************************************************************************
   Function: UseSecondSwing

Description: This function sets the trigger which plays the "PickaxeSwing2"
             animation while disabling the trigger for the first swing
             animation.

      Input: none

     Output: none
    **************************************************************************/
    public void UseSecondSwing()
    {
        pickaxeAnim.SetTrigger("usingSecondSwing"); //plays second swing attack
          //allows first swing attack to be played again
        pickaxeAnim.ResetTrigger("usingFirstSwing");
    }

    /**************************************************************************
   Function: GetFirstSwingStatus

Description: This function returns that status of whether or not the player
             is ready for the first swing animation.

      Input: none

     Output: Returns true if player is ready for first swing animation,
             otherwise, returns false.
    **************************************************************************/
    public bool GetFirstSwingStatus()
    {
        return firstSwingReady;
    }

    /**************************************************************************
   Function: GetSecondSwingStatus

Description: This function returns that status of whether or not the player
             is ready for the second swing animation.

      Input: none

     Output: Returns true if player is ready for second swing animation,
             otherwise, returns false.
    **************************************************************************/
    public bool GetSecondSwingStatus()
    {
        return secondSwingReady;
    }

    /**************************************************************************
   Function: PickaxeIsInAttackAnimation

Description: This function checks if either of the pickaxe animations are
             playing and returns a bool based on that check.

      Input: none

     Output: Returns true if any pickaxe attack animation is playing, otherwise,
             returns false.
    **************************************************************************/
    public bool PickaxeIsInAttackAnimation()
    {
        return (pickaxeAnim.GetCurrentAnimatorStateInfo(0).IsName("PickaxeSwing1") ||
                pickaxeAnim.GetCurrentAnimatorStateInfo(0).IsName("PickaxeSwing2"));
    }

    /**************************************************************************
   Function: GetPickaxeDamage

Description: This function checks which attack animation is playing and returns
             the damage value that corresponds with that attack.

      Input: none

     Output: Returns the damage based on which attack the player is executing.
    **************************************************************************/
    public float GetPickaxeDamage()
    {
        if (pickaxeAnim.GetCurrentAnimatorStateInfo(0).IsName("PickaxeSwing1"))
        {
            return currentFirstSwingDamage;
        }
        else
        {
            return currentSecondSwingDamage;
        }
    }

    /**************************************************************************
   Function: GetEquippedStatus

Description: This function returns the status of whether or not the pickaxe
             is equipped.

      Input: none

     Output: Returns true if the pickaxe is equipped, otherwise, returns false.
    **************************************************************************/
    public bool GetEquippedStatus()
    {
        return equipped;
    }

    /**************************************************************************
   Function: SetEquippedStatus

Description: Given a bool, this function sets the equipped variable to the 
             given bool.

      Input: status - bool equipped is assigned to

     Output: none
    **************************************************************************/
    public void SetEquippedStatus(bool status)
    {
        equipped = status;
    }

    /**************************************************************************
   Function: GetSecondaryStatus

Description: This function returns the status of whether or not the pickaxe
             is the secondary weapon.

      Input: none

     Output: Returns true if the pickaxe is the secondary weapon, otherwise, 
             returns false.
    **************************************************************************/
    public bool GetSecondaryStatus()
    {
        return secondary;
    }

    /**************************************************************************
   Function: SetSecondaryStatus

Description: Given a bool, this function assigns it to the secondary variable.

      Input: status - bool secondary is set to

     Output: none
    **************************************************************************/
    public void SetSecondaryStatus(bool status)
    {
        secondary = status;
    }

    /**************************************************************************
   Function: PickaxeIsInEquippingAnimation

Description: This function checks if the equipping animation is playing and
             returns a bool based on this check.

      Input: none

     Output: Returns true if the equipping animation is playing, otherwise,
             returns false.
    **************************************************************************/
    public bool PickaxeIsInEquippingAnimation()
    {
        return pickaxeAnim.GetCurrentAnimatorStateInfo(0).IsName("PickaxeEquip");
    }

    /**************************************************************************
   Function: ResetFirstSwing

Description: This function resets the trigger that activates the "PickaxeSwing1"
             animation.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetFirstSwing()
    {
        pickaxeAnim.ResetTrigger("usingFirstSwing");
    }

    /**************************************************************************
   Function: ResetSecondSwing

Description: This function resets the trigger that activates the "PickaxeSwing2"
             animation.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetSecondSwing()
    {
        pickaxeAnim.ResetTrigger("usingSecondSwing");
    }

    /**************************************************************************
   Function: ResetAllSwings

Description: This function resets all triggers for the pickaxe's attack animations.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetAllSwings()
    {
        pickaxeAnim.ResetTrigger("usingFirstSwing");
        pickaxeAnim.ResetTrigger("usingSecondSwing");
    }

    /**************************************************************************
   Function: FirstSwingStaminaUse

Description: This function consumes stamina after using the first swing then
             checks the remaining stamina to determine how long the 
             regeneration cooldown will be before setting it.

      Input: none

     Output: none
    **************************************************************************/
    private void FirstSwingStaminaUse()
    {
        hudManager.DecrementStamina(firstSwingUse);
        hudManager.CheckRemainingStamina();
    }

    /**************************************************************************
   Function: SecondSwingStaminaUse

Description: This function consumes stamina after using the second swing then
             checks the remaining stamina to determine how long the 
             regeneration cooldown will be before setting it.

      Input: none

     Output: none
    **************************************************************************/
    private void SecondSwingStaminaUse()
    {
        hudManager.DecrementStamina(secondSwingUse);
        hudManager.CheckRemainingStamina();
    }

    /**************************************************************************
   Function: ResetAnimSpeed

Description: This function resets the animation speed of the weapon's animator.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetAnimSpeed()
    {
        pickaxeAnim.speed = 1.0f;
    }

    /**************************************************************************
   Function: SetMeshRenderer

Description: Given a reference to a meshRenderer, this function retrieves the
             pickaxe's meshRenderer and stores it in the referenced variable.

      Input: meshRenderer - reference to a MeshRenderer

     Output: none
    **************************************************************************/
    private void SetMeshRenderer(ref MeshRenderer meshRenderer)
    {
        meshRenderer = firstPersonController.GetWeapon("Pickaxe").GetComponent<MeshRenderer>();
    }

    /**************************************************************************
   Function: SetBoxCollider

Description: Given a reference to a boxCollider, this function retrieves the
             pickaxe's boxCollider and stores it in the referenced variable.

      Input: boxCollider - reference to a BoxCollider

     Output: none
    **************************************************************************/
    private void SetBoxCollider(ref BoxCollider boxCollider)
    {
        boxCollider = firstPersonController.GetWeapon("Pickaxe").GetComponent<BoxCollider>();
    }

    /**************************************************************************
   Function: ResetIsEquippingTrigger

Description: This function resets the trigger that activates the equipping
             animation to prevent it from playing multiple times in a row.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetIsEquippingTrigger()
    {
        pickaxeAnim.ResetTrigger("isEquipping");
    }

    /**************************************************************************
   Function: HitByRay

Description: This function is called when the player's raycast hits the 
             pickaxe object. If the E key is pressed while looking at the
             pickaxe, it is picked up and equipped.

      Input: none

     Output: none
    **************************************************************************/
    private void HitByRay()
    {
        hudManager.DisplayPrompt();
        //TODO: need to add this weapon to weaponManager and replace the currently
        //equipped weapon
        if (hudManager.GetPickupCooldown() <= 0.0f && Input.GetKeyDown(KeyCode.E) && !hudManager.GetPauseStatus())
        {
              //checks if player doesn't have an equipped weapon yet
            if (!weaponManager.GetPrimaryWeaponEquipped())
            {
                  //sets equipped bool to true on the pickaxe attached to the player
                pickaxe.EquipPickaxe();
                weaponManager.UpdateWeaponStatPanel(); //displays pickaxe's stats
                  //prevents other weapons from being automatically equipped
                weaponManager.SetPrimaryWeaponEquipped();
                weaponManager.SetEquippedImage(gameObject.tag);
            }
              //checks if there is no secondary weapon yet
            else if (!weaponManager.GetSecondaryStatus())
            {
                weaponManager.SetSecondaryWeapon(gameObject.tag);
                weaponManager.SetEquippedSlotImage();

                survivalNoteManager.RevealNote(0); //reveals 1st note
                  //hud text should tell player a new note was revealed
                hudManager.SetDisplayNoteMessage(true);
            }

            weaponManager.CheckForEmptyPanel();
            weaponManager.AddToEmptyPanel(gameObject.tag);

            Destroy(gameObject);

            hudManager.DisplayPickUpText(gameObject.tag); //tells player they found a pickaxe
            hudManager.DisplayImage(gameObject.tag); //shows image of pickaxe
        }
    }

    /**************************************************************************
   Function: SwingingPickaxe

Description: This function sets the swinging bool to true.

      Input: none

     Output: none
    **************************************************************************/
    private void SwingingPickaxe()
    {
        swinging = true;
    }

    /**************************************************************************
   Function: NotSwingingPickaxe

Description: This function sets the swinging bool to false.

      Input: none

     Output: none
    **************************************************************************/
    private void NotSwingingPickaxe()
    {
        swinging = false;
    }

    /**************************************************************************
   Function: ReadyForFirstSwing

Description: This function sets the firstSwingReady bool to true.

      Input: none

     Output: none
    **************************************************************************/
    private void ReadyForFirstSwing()
    {
        firstSwingReady = true;
    }

    /**************************************************************************
   Function: NotReadyForFirstSwing

Description: This function sets the firstSwingReady bool to false.

      Input: none

     Output: none
    **************************************************************************/
    private void NotReadyForFirstSwing()
    {
        firstSwingReady = false;
    }

    /**************************************************************************
   Function: ReadyForSecondSwing

Description: This function sets the secondSwingReady bool to true.

      Input: none

     Output: none
    **************************************************************************/
    private void ReadyForSecondSwing()
    {
        secondSwingReady = true;
    }

    /**************************************************************************
   Function: NotReadyForSecondSwing

Description: This function sets the secondSwingReady bool to false.

      Input: none

     Output: none
    **************************************************************************/
    private void NotReadyForSecondSwing()
    {
        secondSwingReady = false;
    }

    /**************************************************************************
   Function: ResetWeaknessHit

Description: This function resets the weaknessHit bool back to false so the
             enemy weaknesses can be hit again.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetWeaknessHit()
    {
        weaknessHit = false;
    }

    /**************************************************************************
   Function: OnTriggerStay

Description: This function is called when the pipe collides with another
             collider. If swinging is true, the pipe is in a part of its
             animation when it can damage enemies. Deal damage to those enemies
             once per swing.

      Input: other - collider of the other gameObject

     Output: none
    **************************************************************************/
    private void OnTriggerStay(Collider other)
    {
        //TODO: in the enemy script, if damage is done use a bool to prevent 
        //further damage on the same attack
        if (swinging)
        {
            //Debug.Log("Trigger");
            //Debug.Log(other.tag);

            if (!weaknessHit && other.tag == "Weakness")
            {
                other.transform.SendMessageUpwards("Kill");
                weaknessHit = true;
            }
        }
    }

    /**************************************************************************
   Function: SetDamage

Description: This function sets the damage the pickaxe's attacks do to their
             default damage values.

      Input: none

     Output: none
    **************************************************************************/
    private void SetDamage()
    {
        currentFirstSwingDamage = defaultFirstSwingDamage;
        currentSecondSwingDamage = defaultSecondSwingDamage;
    }

    /**************************************************************************
   Function: SetAverageDamage

Description: Given a float, this function sets currentAverageDamage to the given 
             float.

      Input: damage - float that's assigned to currentAverageDamage

     Output: none
    **************************************************************************/
    private void SetAverageDamage(float damage)
    {
        currentAverageDamage = damage;
    }

    /**************************************************************************
   Function: SetRange

Description: Given a float, this function sets currentRange to the given 
             float.

      Input: range - float that's assigned to currentRange

     Output: none
    **************************************************************************/
    private void SetRange(float range)
    {
        currentRange = range;
    }

    /**************************************************************************
   Function: SetReloadSpeed

Description: Given a float, this function sets currentReloadSpeed to the given 
             float.

      Input: reloadSpeed - float that's assigned to currentReloadSpeed

     Output: none
    **************************************************************************/
    private void SetReloadSpeed(float reloadSpeed)
    {
        currentReloadSpeed = reloadSpeed;
    }

    /**************************************************************************
   Function: SetRateOfFire

Description: Given a float, this function sets currentrateOfFire to the given 
             float.

      Input: rateOfFire - float that's assigned to currentRateOfFire

     Output: none
    **************************************************************************/
    private void SetRateOfFire(float rateOfFire)
    {
        currentRateOfFire = rateOfFire;
    }
}
