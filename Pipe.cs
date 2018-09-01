/******************************************************************************
  File Name: Pipe.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains a function for manipulating the pipe melee
             weapon, its damage, stamina use, and animations.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Pipe : MonoBehaviour
{
      //used to communicate with player object
    [SerializeField] private FirstPersonController firstPersonController = null;
      //used to communicate with HUD
    [SerializeField] private HUDManager hudManager = null;
    [SerializeField] private SurvivalNoteManager survivalNoteManager = null;
    [SerializeField] private WeaponManager weaponManager = null;
    [SerializeField] private Pipe pipe = null;
    [SerializeField] private Animator pipeAnim = null; //contains all animations
    private MeshRenderer meshRenderer = null; //used to make visible/invisible
    private BoxCollider boxCollider = null; //used to detect collisions
    private const float firstSwingUse = 10.0f; //stamina used with first attack
    private const float secondSwingUse = 7.0f; //stamina used with second attack
    private const float thirdSwingUse = 20.0f; //stamina used with third attack
    private const float defaultFirstSwingDamage = 20.0f; //damage dealt with first attack
    private const float defaultSecondSwingDamage = 10.0f; //damage dealt with second attack
    private const float defaultThirdSwingDamage = 30.0f; //damage dealt with third attack
    private const float defaultAverageDamage = 20.0f; //average damage of all attacks
    private const float defaultRange = 0.0f; //default range of the pipe
    private const float defaultReloadSpeed = 0.0f; //default reload speed of the pipe
    private const float defaultRateOfFire = 5.0f; //default rate of fire of the pipe
    private const float firstDamageBonus = 5.0f; //value of 1st damage upgrade
    private const float secondDamageBonus = 10.0f; //value of 2nd damage upgrade
    private const float firstRangeBonus = 10.0f; //value of final upgrade's range bonus
    private float currentAverageDamage; //the current average damage of the pipe
    private float currentFirstSwingDamage; 
    private float currentSecondSwingDamage;
    private float currentThirdSwingDamage; //TODO: write functions that set these if upgraded
    private float currentRange; //the current range of the pipe
    private float currentRateOfFire; //the current rate of fire of the pipe
    private float currentReloadSpeed; //the current reload speed of the pipe
    private bool swinging; //used to deal damage during collision check
    private bool firstSwingReady; //used to play first swing animation
    private bool secondSwingReady; //used to play second swing animation
    private bool thirdSwingReady; //used to play third swing animation
    private bool equipped;
    private bool secondary; //is true if this weapon is in secondary slot
    private bool weaknessHit; //used to ensure weakness is hit up to once per swing
    private bool damageUpgrade1; //increases weapon damage once
    private bool damageUpgrade2; //increases weapon damage a second time
    private bool rangeUpgrade1; //increases weapon range once

    void Start()
    {
        SetMeshRenderer(ref meshRenderer); //retrieves meshRenderer component
        SetBoxCollider(ref boxCollider); //retrieves collider component

        SetDamage(); //sets damage done to enemies
          //these setters are for the weapon stat panel
        SetAverageDamage(defaultAverageDamage);
        SetRange(defaultRange);
        SetRateOfFire(defaultRateOfFire);
        SetReloadSpeed(defaultReloadSpeed);

        UpgradePipe(); //apply any pipe upgrades
    }

    void Update ()
    {
          //TODO: remove this when finished testing
		if(Input.GetKeyDown(KeyCode.V)) //test code to equip/unequip pipe
        {
            if(equipped)
            {
                UnequipPipe();
            }
            else
            {
                EquipPipe();
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            UnequipPipe();
        }
    }

    /**************************************************************************
   Function: SetUpgrade

Description: Given a string, this function sets the upgrade of the given name
             to true. Then it calls the UpgradePipe function.

      Input: upgradeName - string of the upgrade to set

     Output: none
    **************************************************************************/
    public void SetUpgrade(string upgradeName)
    {
        switch (upgradeName)
        {
            case "PipeDamageUpgrade1":
                damageUpgrade1 = true;
                break;
            case "PipeDamageUpgrade2":
                damageUpgrade2 = true;
                break;
            case "PipeRangeUpgrade1":
                rangeUpgrade1 = true;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        UpgradePipe(); //NOTE: this must be called once for each upgrade
    }

    /**************************************************************************
   Function: UpgradePipe

Description: This function checks each upgrade bool. If any are true, the
             pipe's current stat related to that bool is updated.

      Input: none

     Output: none
    **************************************************************************/
    public void UpgradePipe()
    {
        if (damageUpgrade2) //if player has 2nd damage upgrade
        {
              //10.0f added to each attack
            SetAverageDamage(defaultAverageDamage + (secondDamageBonus * 3)); 

            currentFirstSwingDamage = defaultFirstSwingDamage + secondDamageBonus;
            currentSecondSwingDamage = defaultSecondSwingDamage + secondDamageBonus;
            currentThirdSwingDamage = defaultThirdSwingDamage + secondDamageBonus;
        }
        else if (damageUpgrade1) //if the player only has 1st damage upgrade
        {
              //5.0f added to each attack
            SetAverageDamage(defaultAverageDamage + (firstDamageBonus * 3));

            currentFirstSwingDamage = defaultFirstSwingDamage + firstDamageBonus;
            currentSecondSwingDamage = defaultSecondSwingDamage + firstDamageBonus;
            currentThirdSwingDamage = defaultThirdSwingDamage + firstDamageBonus;
        }

        if (rangeUpgrade1) //if player has first range upgrade
        {
            currentRange = defaultRange + firstRangeBonus; //TODO: extended box collider of pipe. this will be part of final upgrade's bonus
            IncreaseColliderRange(); //stretches and repositions pipe's collider
        }
    }

    /**************************************************************************
   Function: IncreaseColliderRange

Description: This function increases the collider of the pipe and repositions
             it to be at the end of the pipe. The larger collider can now hit
             enemies from afar.

      Input: none

     Output: none
    **************************************************************************/
    public void IncreaseColliderRange()
    {
          //increases the pipe's reach in the shape of a 3D rectangle
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

Description: This function calculates the current average damage per swing of
             the pipe and returns that value.

      Input: none

     Output: Returns average damage per swing of the pipe.
    **************************************************************************/
    public float GetDamageForStatPanel()
    {
        return defaultAverageDamage;
    }

    /**************************************************************************
   Function: GetRangeForStatPanel

Description: This function returns the current range of the pipe.

      Input: none

     Output: Returns the range of the pipe.
    **************************************************************************/
    public float GetRangeForStatPanel()
    {
        return defaultRange;
    }

    /**************************************************************************
   Function: GetReloadSpeedForStatPanel

Description: This function returns the current reload speed of the pipe.
             Because the pipe doesn't reload anything, this value is set to 0.

      Input: none

     Output: Returns the reload speed of the pipe.
    **************************************************************************/
    public float GetReloadSpeedForStatPanel()
    {
        return defaultReloadSpeed;
    }

    /**************************************************************************
   Function: GetRateOfFireForStatPanel

Description: This function returns current attack speed of the pipe.

      Input: none

     Output: Returns the attack speed of the pipe.
    **************************************************************************/
    public float GetRateOfFireForStatPanel()
    {
        return defaultRateOfFire;
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

Description: Given a bool, this function either enables or disables the
             "MovingForward" animation based on the bool.

      Input: forward - bool used to enable/disable animation

     Output: none
    **************************************************************************/
    public void MoveForward(bool forward)
    {
        if(forward)
        {
            pipeAnim.SetTrigger("MovingForward");
        }
        else
        {
            pipeAnim.ResetTrigger("MovingForward");
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
        pipeAnim.SetBool("isCharging", true);
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
        pipeAnim.SetBool("isCharging", false);
    }

    /**************************************************************************
   Function: CheckForPipeCharge

Description: This function checks of the player is holding down the right
             mouse button. If they are, the charging animation continues to
             play. Otherwise, the charging animation is exited.

      Input: none

     Output: none
    **************************************************************************/
    public void CheckForPipeCharge()
    {
        if(Input.GetMouseButton(1))
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
        return pipeAnim.GetCurrentAnimatorStateInfo(0).IsName("PipeCharge");
    }

    /**************************************************************************
   Function: Running

Description: Given a bool, this function doubles the animation speed or returns
             it to normal based on the bool.

      Input: isRunning - bool used to set the animation speed

     Output: none
    **************************************************************************/
    public void Running(bool isRunning)
    {
        if(isRunning)
        {
            pipeAnim.SetBool("isSprinting", true);
            pipeAnim.speed = 2.0f; //doubles speed to look like the player is running
        }
        else
        {
            pipeAnim.SetBool("isSprinting", false);
            pipeAnim.speed = 1.0f; //return speed to normal
        }
    }

    /**************************************************************************
   Function: UseFirstSwing

Description: This function sets the trigger which plays the "PipeSwing1"
             animation.

      Input: none

     Output: none
    **************************************************************************/
    public void UseFirstSwing()
    {
        pipeAnim.SetTrigger("usingFirstSwing");
    }

    /**************************************************************************
   Function: UseSecondSwing

Description: This function sets the trigger which plays the "PipeSwing2"
             animation while disabling the trigger for the first swing
             animation.

      Input: none

     Output: none
    **************************************************************************/
    public void UseSecondSwing()
    {
        pipeAnim.SetTrigger("usingSecondSwing"); //plays second swing attack
          //allows first swing attack to be played again
        pipeAnim.ResetTrigger("usingFirstSwing"); 
    }

    /**************************************************************************
   Function: UseThirdSwing

Description: This function sets the trigger which plays the "PipeSwing3"
             animation while disabling the trigger for the second swing
             animation.

      Input: none

     Output: none
    **************************************************************************/
    public void UseThirdSwing()
    {
        pipeAnim.SetTrigger("usingThirdSwing"); //plays third swing attack
          //allows second swing attack to be played again
        pipeAnim.ResetTrigger("usingSecondSwing");
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
   Function: GetThirdSwingStatus

Description: This function returns that status of whether or not the player
             is ready for the third swing animation.

      Input: none

     Output: Returns true if player is ready for third swing animation,
             otherwise, returns false.
    **************************************************************************/
    public bool GetThirdSwingStatus()
    {
        return thirdSwingReady;
    }

    /**************************************************************************
   Function: GetSwingingStatus

Description: This function returns the status of whether or not the player is
             swinging the pipe.

      Input: none

     Output: Returns true if the player is swinging the pipe, otherwise, 
             returns false.
    **************************************************************************/
    public bool GetSwingingStatus()
    {
        return swinging;
    }

    /**************************************************************************
   Function: PipeIsInAttackAnimation

Description: This function checks if any of the three pipe animations are
             playing and returns a bool based on that check.

      Input: none

     Output: Returns true if any pipe attack animation is playing, otherwise,
             returns false.
    **************************************************************************/
    public bool PipeIsInAttackAnimation()
    {
        return (pipeAnim.GetCurrentAnimatorStateInfo(0).IsName("PipeSwing1") ||
                pipeAnim.GetCurrentAnimatorStateInfo(0).IsName("PipeSwing2") ||
                pipeAnim.GetCurrentAnimatorStateInfo(0).IsName("PipeSwing3"));
    }

    /**************************************************************************
   Function: GetPipeDamage

Description: This function checks which attack animation is playing and returns
             the damage value that corresponds with that attack.

      Input: none

     Output: Returns the damage based on which attack the player is executing.
    **************************************************************************/
    public float GetPipeDamage()
    {
        if(pipeAnim.GetCurrentAnimatorStateInfo(0).IsName("PipeSwing1"))
        {
            return currentFirstSwingDamage;
        }
        else if(pipeAnim.GetCurrentAnimatorStateInfo(0).IsName("PipeSwing2"))
        {
            return currentSecondSwingDamage;
        }
        else
        {
            return currentThirdSwingDamage;
        }
    }

    /**************************************************************************
   Function: GetEquippedStatus

Description: This function returns the status of whether or not the pipe
             is equipped.

      Input: none

     Output: Returns true if the pipe is equipped, otherwise, returns false.
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

Description: This function returns the status of whether or not the pipe
             is the secondary weapon.

      Input: none

     Output: Returns true if the pipe is the secondary weapon, otherwise, 
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
   Function: ResetFirstSwing

Description: This function resets the trigger that activates the "PipeSwing1"
             animation.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetFirstSwing()
    {
        pipeAnim.ResetTrigger("usingFirstSwing");
    }

    /**************************************************************************
   Function: ResetSecondSwing

Description: This function resets the trigger that activates the "PipeSwing2"
             animation.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetSecondSwing()
    {
        pipeAnim.ResetTrigger("usingSecondSwing");
    }

    /**************************************************************************
   Function: ResetThirdSwing

Description: This function resets the trigger that activates the "PipeSwing3"
             animation.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetThirdSwing()
    {
        pipeAnim.ResetTrigger("usingThirdSwing");
    }

    /**************************************************************************
   Function: ResetAllSwings

Description: This function resets all triggers for the pipe's attack animations.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetAllSwings()
    {
        pipeAnim.ResetTrigger("usingFirstSwing");
        pipeAnim.ResetTrigger("usingSecondSwing");
        pipeAnim.ResetTrigger("usingThirdSwing");
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
   Function: ThirdSwingStaminaUse

Description: This function consumes stamina after using the third swing then
             checks the remaining stamina to determine how long the 
             regeneration cooldown will be before setting it.

      Input: none

     Output: none
    **************************************************************************/
    private void ThirdSwingStaminaUse()
    {
        hudManager.DecrementStamina(thirdSwingUse);
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
        pipeAnim.speed = 1.0f;
    }

    /**************************************************************************
   Function: SetMeshRenderer

Description: Given a reference to a meshRenderer variable, this function 
             retrieves this gameObject's MeshRenderer component and stores it
             in the given variable.

      Input: meshRenderer - a reference to a meshRenderer variable

     Output: none
    **************************************************************************/
    private void SetMeshRenderer(ref MeshRenderer meshRenderer)
    {
        meshRenderer = firstPersonController.GetWeapon("Pipe").GetComponent<MeshRenderer>();
    }

    /**************************************************************************
   Function: SetBoxCOllider

Description: Given a reference to a boxCollider variable, this function
             retrieves this gameObject's BoxCollider component and stores it
             in the given variable.

      Input: boxCollider - a reference to a boxCollider variable

     Output: none
    **************************************************************************/
    private void SetBoxCollider(ref BoxCollider boxCollider)
    {
        boxCollider = firstPersonController.GetWeapon("Pipe").GetComponent<BoxCollider>();
    }

    /**************************************************************************
   Function: EquipPipe

Description: This function plays the equipping animations and enables the 
             meshRender making the pipe visible and enables the meshCollider 
             for collision detection.

      Input: none

     Output: none
    **************************************************************************/
    public void EquipPipe()
    {
        pipeAnim.SetTrigger("isEquipping"); //plays equip animation

        meshRenderer.enabled = true;
        boxCollider.enabled = true;

        equipped = true;

        weaponManager.SetEquippedWeaponName(gameObject.tag);

        hudManager.SetWeaponNameText("Pipe");
        hudManager.SetMaxLoadedAmmoText(""); //melee weapon has no ammo
        hudManager.DisplayWeaponInfo(true);
        hudManager.UpdateWeaponInfo();
    }

    /**************************************************************************
   Function: UnequipPipe

Description: This function disables the meshRenderer making the pipe invisible
             and disables the meshCollider to prevent any collisions.

      Input: none

     Output: none
    **************************************************************************/
    public void UnequipPipe()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;

        equipped = false;
    }

    /**************************************************************************
   Function: PipeIsInEquippingAnimation
   
Description: This function checks if the equipping animation is playing and
             returns a bool based on this check.

      Input: none

     Output: Returns true if the equipping animation is playing, otherwise,
             returns false.
    **************************************************************************/
    public bool PipeIsInEquippingAnimation()
    {
        return pipeAnim.GetCurrentAnimatorStateInfo(0).IsName("PipeEquip");
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
        pipeAnim.ResetTrigger("isEquipping");
    }

    /**************************************************************************
   Function: HitByRay

Description: This function is called when the player's raycast hits this
             gameObject. The HUD displays a message to pick up the pipe. If the
             player presses the E key, the pipe is equipped.

      Input: none

     Output: none
    **************************************************************************/
    private void HitByRay()
    {
        hudManager.DisplayPrompt();

        if (hudManager.GetPickupCooldown() <= 0.0f && Input.GetKeyDown(KeyCode.E) && !hudManager.GetPauseStatus())
        {
              //checks if player doesn't have an equipped weapon yet
            if(!weaponManager.GetPrimaryWeaponEquipped())
            {
                  //sets equipped bool to true on the pipe attached to the player
                pipe.EquipPipe();
                weaponManager.UpdateWeaponStatPanel(); //displays pipe's stats
                  //prevents other weapons from being automatically equipped
                weaponManager.SetPrimaryWeaponEquipped();
                weaponManager.SetEquippedImage(gameObject.tag);
            }
              //checks if there is no secondary weapon yet
            else if(!weaponManager.GetSecondaryStatus())
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

            hudManager.DisplayPickUpText(gameObject.tag); //tells player they found a pipe
            hudManager.DisplayImage(gameObject.tag); //shows image of the pipe
        }
    }

    /**************************************************************************
   Function: SwingingPipe

Description: This function sets the swinging bool to true.

      Input: none

     Output: none
    **************************************************************************/
    private void SwingingPipe()
    {
        swinging = true;
    }

    /**************************************************************************
   Function: NotSwingingPipe

Description: This function sets the swinging bool to false.

      Input: none

     Output: none
    **************************************************************************/
    private void NotSwingingPipe()
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
   Function: ReadyForThirdSwing

Description: This function sets the thirdSwingReady bool to true.

      Input: none

     Output: none
    **************************************************************************/
    private void ReadyForThirdSwing()
    {
        thirdSwingReady = true;
    }

    /**************************************************************************
   Function: NotReadyForThirdSwing

Description: This function sets the thirdSwingReady bool to false.

      Input: none

     Output: none
    **************************************************************************/
    private void NotReadyForThirdSwing()
    {
        thirdSwingReady = false;
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
   Function: OnCollisionStay

Description: This function is called when the pipe collides with another
             object. If swinging is true, the pipe is in a part of its
             animation when it can damage enemies. Deal damage to those enemies
             once per swing.

      Input: collision - information about the collision

     Output: none
    **************************************************************************/
    private void OnCollisionStay(Collision collision)
    {
        if(swinging)
        {
            //Debug.Log("Collision");
            //Debug.Log(collision.gameObject.name);
        }
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
        if(swinging)
        {
            //Debug.Log("Trigger");
            //Debug.Log(other.tag);

            if(!weaknessHit && other.tag == "Weakness")
            { 
                other.transform.SendMessageUpwards("Kill");
                weaknessHit = true;
            }
        }
    }

    /**************************************************************************
   Function: SetDamage

Description: This function sets the damage the pipe's attacks do to their
             default damage values.

      Input: none

     Output: none
    **************************************************************************/
    private void SetDamage()
    {
        currentFirstSwingDamage = defaultFirstSwingDamage;
        currentSecondSwingDamage = defaultSecondSwingDamage;
        currentThirdSwingDamage = defaultThirdSwingDamage;
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
