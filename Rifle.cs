/******************************************************************************
  File Name: Rifle.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that manage the rifle's stats, 
             behavior, and animations.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Rifle : MonoBehaviour
{
    [SerializeField] private SurvivalNoteManager survivalNoteManager = null;
      //used to communicate with player object
    [SerializeField] private FirstPersonController firstPersonController = null;
    [SerializeField] private WeaponManager weaponManager = null;
    [SerializeField] private ItemManager itemManager = null;
      //used to communicate with HUD
    [SerializeField] private HUDManager hudManager = null;
    [SerializeField] private Rifle rifle = null; //used by rifle pickup only
    [SerializeField] private Animator rifleAnim = null; //contains all animations
    [SerializeField] private GameObject barrelTip = null; //position the bullets originate from
    [SerializeField] private AudioSource audioSource = null; //plays rifle sound effects
    [SerializeField] private AudioClip rifleFire1Clip = null; //sound of rifle firing
    [SerializeField] private AudioClip rifleReloadClip = null; //sound of rifle reloading
    private MeshRenderer[] rifleMeshRenderers = null; //contains all child MeshRenderers
    private MeshRenderer meshRenderer = null; //the rifle body's meshRenderer
    private BoxCollider boxCollider = null; //used to detect collisions
    private Vector3 endOfBarrel; //position where rays for bullets are cast
    private RaycastHit hit; //contains info on what the gun hits
    private const float defaultRange = 200.0f; //default range of rifle bullets
    private const float defaultDamage = 80.0f; //this is the starting damage of the rifle
    private const float defaultReloadSpeed = 2.0f; //default reload speed of the rifle
    private const float defaultRateOfFire = 2.0f; //default rate of fire of the rifle
    private const float firstDamageBonus = 40.0f; //value of 1st damage upgrade
    private const float secondDamageBonus = 80.0f; //value of 2nd damage upgrade
    private const float firstRateOfFireBonus = 2.0f; //value of first RoF upgrade
    private const float firstRangeBonus = 50.0f; //value of first range upgrade
    private const float firstReloadSpeedBonus = 1.0f; //value of first reload upgrade
    private float currentDamage; //the current damage of each bullet
    private float currentRange; //the current range of the rifle
    private float currentRateOfFire; //the current rate of fire of the rifle
    private float currentReloadSpeed; //the current reload speed of the rifle
    private const int maxRifleMagazineCount = 5;
    private int currentRifleMagazineCount;
    private bool isRunning; //used to play the running animation
    private bool equipped; //used to check if the rifle is equipped
    private bool secondary; //is true if this weapon is in secondary slot
    private bool damageUpgrade1; //increases weapon damage once
    private bool damageUpgrade2; //increases weapon damage a second time
    private bool rangeUpgrade1; //increases weapon range once
    private bool rateOfFireUpgrade1; //increases rate of fire once
    private bool reloadSpeedUpgrade1; //increases reload speed once

    void Start()
    {
        SetMeshRenderer(ref meshRenderer, ref rifleMeshRenderers);
        SetBoxCollider(ref boxCollider);
        SetAudioSource(ref audioSource);
        //TODO: write function to set current magazine count incase I add upgrades to increase this
        currentRifleMagazineCount = maxRifleMagazineCount; //TODO: add code to make this only happen when rifle is equipped for first time

        SetDamage(defaultDamage);
        SetRange(defaultRange);
        SetRateOfFire(defaultRateOfFire);
        SetReloadSpeed(defaultReloadSpeed);

        UpgradeRifle();
    }


    void Update()
    {
        UpdateBulletOrigin(); //ensures origin of bullets follow the end of gun's barrel

        Debug.DrawRay(endOfBarrel, transform.forward * defaultRange, Color.blue);
        if (Input.GetKeyDown(KeyCode.G)) //test code to equip/unequip rifle
        {
            if (equipped)
            {
                UnequipRifle();
            }
            else
            {
                EquipRifle();
            }
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            UnequipRifle();
        }

        if(hudManager.GetPauseStatus()) //checks if game is paused
        {
            audioSource.Pause(); //pause current audio clip, if any is being played
        }
        else
        {
            audioSource.UnPause(); //resumes audio clip
        }
    }

    /**************************************************************************
   Function: SetUpgrade

Description: Given a string, this function sets the upgrade of the given name
             to true. Then it calls the UpgradeRifle function.

      Input: upgradeName - string of the upgrade to set

     Output: none
    **************************************************************************/
    public void SetUpgrade(string upgradeName)
    {
        switch (upgradeName)
        {
            case "RifleDamageUpgrade1":
                damageUpgrade1 = true;
                break;
            case "RifleDamageUpgrade2":
                damageUpgrade2 = true;
                break;
            case "RifleRateOfFireUpgrade1":
                rateOfFireUpgrade1 = true;
                break;
            case "RifleReloadSpeedUpgrade1":
                reloadSpeedUpgrade1 = true;
                break;
            case "RifleRangeUpgrade1":
                rangeUpgrade1 = true;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        UpgradeRifle(); //NOTE: this must be called once for each upgrade
    }

    /**************************************************************************
   Function: UpgradeRifle

Description: This function checks each upgrade bool. If any are true, the
             rifle's current stat related to that bool is updated.

      Input: none

     Output: none
    **************************************************************************/
    public void UpgradeRifle()
    {
        if (damageUpgrade2) //if player has 2nd damage upgrade
        {
            SetDamage(defaultDamage + secondDamageBonus);
        }
        else if (damageUpgrade1) //if the player only has 1st damage upgrade
        {
            SetDamage(defaultDamage + firstDamageBonus);
        }

        if (rateOfFireUpgrade1) //if player has 1st rate of fire upgrade
        {
            currentRateOfFire = defaultRateOfFire + firstRateOfFireBonus;
        }

        if (rangeUpgrade1) //if player has first range upgrade
        {
            currentRange = defaultRange + firstRangeBonus;
        }

        if (reloadSpeedUpgrade1) //if player has first reload speed upgrade
        {
            currentReloadSpeed = defaultReloadSpeed + firstReloadSpeedBonus;
        }
    }

    /**************************************************************************
   Function: GetDamageUpgrade1

Description: This function returns the bool representing whether or not the
             player has the first damage upgrade.

      Input: none

     Output: Returns true if player has the first rifle damage upgrade,
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

     Output: Returns true if player has the second rifle damage upgrade,
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

     Output: Returns true if player has the first rifle range upgrade,
             otherwise, returns false.
    **************************************************************************/
    public bool GetRangeUpgrade1()
    {
        return rangeUpgrade1;
    }

    /**************************************************************************
   Function: GetReloadUpgrade1

Description: This function returns the bool representing whether or not the
             player has the first reload upgrade.

      Input: none

     Output: Returns true if player has the first rifle reload upgrade,
             otherwise, returns false.
    **************************************************************************/
    public bool GetReloadUpgrade1()
    {
        return reloadSpeedUpgrade1;
    }

    /**************************************************************************
   Function: GetRateUpgrade1

Description: This function returns the bool representing whether or not the
             player has the first rate of fire upgrade.

      Input: none

     Output: Returns true if player has the first rifle rate of fire upgrade,
             otherwise, returns false.
    **************************************************************************/
    public bool GetRateUpgrade1()
    {
        return rateOfFireUpgrade1;
    }

    /**************************************************************************
   Function: GetDamageForStatPanel

Description: This function returns the current damage per rifle shot.

      Input: none

     Output: Returns the damage per rifle shot.
    **************************************************************************/
    public float GetDamageForStatPanel()
    {
        return defaultDamage;
    }

    /**************************************************************************
   Function: GetRangeForStatPanel

Description: This function returns the current range of the rifle.

      Input: none

     Output: Returns the range of the rifle.
    **************************************************************************/
    public float GetRangeForStatPanel()
    {
        return defaultRange;
    }

    /**************************************************************************
   Function: GetReloadSpeedForStatPanel

Description: This function returns the current reload speed of the rifle.

      Input: none

     Output: Returns the reload speed of the rifle.
    **************************************************************************/
    public float GetReloadSpeedForStatPanel()
    {
        return defaultReloadSpeed;
    }

    /**************************************************************************
   Function: GetRateOfFireForStatPanel

Description: This function returns current attack speed of the rifle.

      Input: none

     Output: Returns the current rate of fire of the rifle.
    **************************************************************************/
    public float GetRateOfFireForStatPanel()
    {
        return defaultRateOfFire;
    }

    /**************************************************************************
   Function: GetUpgradedDamageForStatPanel

Description: This function returns the current damage per rifle shot. If the
             player has any damage upgrade, current damage is modified
             beforehand.

      Input: none

     Output: Returns the current damage per rifle shot.
    **************************************************************************/
    public float GetUpgradedDamageForStatPanel()
    {
        return currentDamage;
    }

    /**************************************************************************
   Function: GetUpgradedRangeForStatPanel

Description: This function returns the current range of the rifle. If the
             player has any range upgrade, current range is modified
             beforehand.

      Input: none

     Output: Returns the current range per rifle shot.
    **************************************************************************/
    public float GetUpgradedRangeForStatPanel()
    {
        return currentRange;
    }

    /**************************************************************************
   Function: GetUpgradedReloadSpeedForStatPanel

Description: This function returns the current reload speed of the rifle. If 
             the player has any reload upgrade, current reload speed is 
             modified beforehand.

      Input: none

     Output: Returns the current reload speed of the rifle.
    **************************************************************************/
    public float GetUpgradedReloadSpeedForStatPanel()
    {
        return currentReloadSpeed;
    }

    /**************************************************************************
   Function: GetUpgradedRateOfFireForStatPanel

Description: This function returns the current rate of fire of the rifle. If 
             the player has any rate of fire upgrade, current rate of fire is 
             modified beforehand.

      Input: none

     Output: Returns the current rate of fire of the rifle.
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
        if (forward)
        {
            rifleAnim.SetBool("isMovingForward", true);
        }
        else
        {
            rifleAnim.SetBool("isMovingForward", false);
        }
    }

    /**************************************************************************
   Function: Aiming

Description: Given a bool, this function either plays or stops playing the
             aiming animation based on the bool.

      Input: aiming - bool used to play or stop playing the aiming animation

     Output: none
    **************************************************************************/
    public void Aiming(bool aiming)
    {
        if (aiming)
        {
            rifleAnim.SetBool("isAiming", true);
        }
        else
        {
            rifleAnim.SetBool("isAiming", false);
        }
    }

    /**************************************************************************
   Function: Reloading

Description: This function sets the trigger that plays the reloading animation.

      Input: none

     Output: none
    **************************************************************************/
    public void Reloading()
    {
        if (reloadSpeedUpgrade1)
        {
            rifleAnim.SetFloat("reloadSpeed", 1.5f);
        }

        rifleAnim.SetTrigger("Reloading");
        PlayRifleReloadAudio();
    }

    /**************************************************************************
   Function: ResetReloading

Description: This function resets the trigger that plays the reloading animation.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetReloading()
    {
        rifleAnim.ResetTrigger("Reloading");
    }


    private void PlayRifleFireAudio()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rifleFire1Clip);
        }
    }


    private void PlayRifleReloadAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rifleReloadClip);
        }
    }

    /**************************************************************************
   Function: Running

Description: Given a bool, this function checks if the bool is true and the
             rifle isn't shooting reloading or aiming. If these are all true,
             the sprinting bool is set to true which plays the running
             animation.

      Input: isRunning - bool used to set the animation speed

     Output: none
    **************************************************************************/
    public void Running(bool isRunning)
    {
        if (isRunning && !RifleIsShooting() && !RifleIsReloading() && !RifleIsInAimAnimation())
        {
            rifleAnim.SetBool("isSprinting", true);
            rifleAnim.speed = 2.0f;
        }
        else
        {
            rifleAnim.SetBool("isSprinting", false);
            rifleAnim.speed = 1.0f;
        }
    }

    /**************************************************************************
   Function: GetEquippedStatus

Description: This function returns the status of whether or not the rifle
             is equipped.

      Input: none

     Output: Returns true if the rifle is equipped, otherwise, returns false.
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

Description: This function returns the status of whether or not the rifle
             is the secondary weapon.

      Input: none

     Output: Returns true if the rifle is the secondary weapon, otherwise, 
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
   Function: RifleIsInAimAnimation

Description: This function checks if either of the two aim animations are
             playing and returns a bool based on that check.

      Input: none

     Output: Returns true if any rifle aim animation is playing, otherwise,
             returns false.
    **************************************************************************/
    public bool RifleIsInAimAnimation()
    {
        return (rifleAnim.GetCurrentAnimatorStateInfo(0).IsName("RifleAim") ||
                rifleAnim.GetCurrentAnimatorStateInfo(0).IsName("RifleAimShot"));
    }

    /**************************************************************************
   Function: RifleIsShooting

Description: This function checks if the shooting animation is playing and 
             returns a bool based on that check.

      Input: none

     Output: Returns true if the rifle shooting animation is playing, 
             otherwise, returns false.
    **************************************************************************/
    public bool RifleIsShooting()
    {
        return (rifleAnim.GetCurrentAnimatorStateInfo(0).IsName("RifleShoot") ||
                rifleAnim.GetCurrentAnimatorStateInfo(0).IsName("RifleAimShot"));
    }

    /**************************************************************************
   Function: RifleIsReloading

Description: This function the status of whether or not the reload animation is
             currently playing.

      Input: none

     Output: Returns true if the reload animation is playing, otherwise, 
             returns false.
    **************************************************************************/
    public bool RifleIsReloading()
    {
        return (rifleAnim.GetCurrentAnimatorStateInfo(0).IsName("RifleReload"));
    }

    /**************************************************************************
   Function: FireRifle

Description: This function sets the trigger for the shooting animation, then
             checks if a raycast out of the barrel hits anything. If so, the
             laserPoint's position is set to the point the ray hit. Lastly, 
             one bullet is deducted from the current magazine count.

      Input: none

     Output: none
    **************************************************************************/
    public void FireRifle()
    {
        if (currentRifleMagazineCount > 0)
        {
            if (rateOfFireUpgrade1)
            {
                rifleAnim.SetFloat("fireRate", 1.4f);
            }

            rifleAnim.SetTrigger("Shooting");
            PlayRifleFireAudio();

            UseMagazineAmmo(); //deducts from the magazine
            hudManager.UpdateWeaponInfo(); //updates HUD
            weaponManager.SetCurrentMagazineCount(currentRifleMagazineCount);

              //used to check if something was hit
            if (Physics.Raycast(endOfBarrel, transform.forward, out hit, currentRange))
            {
                //laserPoint.transform.position = hit.point;
            }
        }
    }

    /**************************************************************************
   Function: GetRifleDamage

Description: This function returns the rifle's current damage per bullet.

      Input: none

     Output: Returns the rifle's current damage.
    **************************************************************************/
    public float GetRifleDamage()
    {
        return currentDamage;
    }

    /**************************************************************************
   Function: GetMagazineAmmoCount

Description: This function returns the number of bullets currently loaded in
             the rifle.

      Input: none

     Output: Returns the rifle's current magazine count.
    **************************************************************************/
    public int GetMagazineAmmoCount()
    {
        return currentRifleMagazineCount;
    }

    /**************************************************************************
   Function: GetMaxMagazineAmmoCount

Description: This function returns the max number of bullets that can be
             loaded in the rifle.

      Input: none

     Output: Returns the rifle's max magazine count.
    **************************************************************************/
    public int GetMaxMagazineAmmoCount()
    {
        return maxRifleMagazineCount;
    }

    /**************************************************************************
   Function: SetMagazineAmmoCount

Description: Given an integer, this function sets the current number of bullets
             in the rifle to the given integer.

      Input: ammoCount - integer used to set magazine count

     Output: none
    **************************************************************************/
    public void SetMagazineAmmoCount(int ammoCount)
    {
        currentRifleMagazineCount = ammoCount;
    }

    /**************************************************************************
   Function: UseMagazineAmmo

Description: This function reduces the number of bullets currently in the
             rifle by one.

      Input: none

     Output: none
    **************************************************************************/
    private void UseMagazineAmmo()
    {
        --currentRifleMagazineCount;
    }

    /**************************************************************************
   Function: ReloadRifle

Description: This function searches the item slots in reverse order for ammo
             to load into the rifle. Support ammo is used before regular ammo.
             Then the HUD info is updated.

      Input: none

     Output: none
    **************************************************************************/
    public void ReloadRifle()
    {
          //check performed so reload function can be called 3 times by reload
          //animation. This is to account for an edge case of 3 item slots
          //being used to fill magazine in a single reload without interfering
          //with a panel's font color
        if(currentRifleMagazineCount < maxRifleMagazineCount)
        {
              //if support rifle ammo was found first
            if (itemManager.ReverseCheckForOccupiedPanel("SupportRifleAmmo"))
            {
                  //use the ammo from that item slot
                itemManager.UseAmmoFromItemPanel("SupportRifleAmmo");
            }
              //then if regular rifle ammo was found
            else if (itemManager.ReverseCheckForOccupiedPanel("RifleAmmo"))
            {
                  //use the ammo from that item slot
                itemManager.UseAmmoFromItemPanel("RifleAmmo");
            }
        }

        hudManager.UpdateWeaponInfo(); //updated current magazine count on HUD
        weaponManager.SetCurrentMagazineCount(currentRifleMagazineCount);
    }

    /**************************************************************************
   Function: RifleIsInEquippingAnimation

Description: This function checks if the equipping animation is playing and
             returns a bool based on this check.

      Input: none

     Output: Returns true if the equipping animation is playing, otherwise,
             returns false.
    **************************************************************************/
    public bool RifleIsInEquippingAnimation()
    {
        return rifleAnim.GetCurrentAnimatorStateInfo(0).IsName("RifleEquip");
    }

    /**************************************************************************
   Function: ResetAnimSpeed

Description: This function resets the animation speed of the weapon's animator.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetAnimSpeed()
    {
        rifleAnim.speed = 1.0f;
    }

    /**************************************************************************
   Function: SetMeshRenderer

Description: Given a reference to a meshRenderer variable and an array, this 
             function retrieves this gameObject's MeshRenderer component as 
             well as all children's MeshRenderers and stores them in the given 
             variables.

      Input: meshRenderer  - a reference to a meshRenderer variable
             meshRenderers - a reference to a meshRenderer array variable

     Output: none
    **************************************************************************/
    private void SetMeshRenderer(ref MeshRenderer meshRenderer, ref MeshRenderer[] meshRenderers)
    {
        meshRenderer = firstPersonController.GetWeapon("Rifle").GetComponent<MeshRenderer>();
        meshRenderers = firstPersonController.GetWeapon("Rifle").GetComponentsInChildren<MeshRenderer>();
    }

    /**************************************************************************
   Function: SetAudioSource

Description: Given a reference to an aurdioSource variable, this function
             retrieves this gameObject's AudioSource component and stores it
             in the given variable.

      Input: audioSource - a reference to an AudioSource variable

     Output: none
    **************************************************************************/
    private void SetAudioSource(ref AudioSource audioSource)
    {
        audioSource = GetComponent<AudioSource>();
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
        boxCollider = firstPersonController.GetWeapon("Rifle").GetComponentInChildren<BoxCollider>();
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
        rifleAnim.ResetTrigger("isEquipping");
    }

    /**************************************************************************
   Function: HitByRay

Description: This function is called when the player's raycast hits this
             gameObject. The HUD displays a message to pick up the rifle. 
             If the player presses the E key, the rifle is equipped.

      Input: none

     Output: none
    **************************************************************************/
    private void HitByRay()
    {
        hudManager.DisplayPrompt();

        if (hudManager.GetPickupCooldown() <= 0.0f && Input.GetKeyDown(KeyCode.E) && !hudManager.GetPauseStatus())
        {
              //checks if player doesn't have an equipped weapon yet
            if (!weaponManager.GetPrimaryWeaponEquipped())
            {
                  //sets equipped bool to true on the rifle attached to the player
                rifle.EquipRifle();
                weaponManager.UpdateWeaponStatPanel(); //displays weapon's stats
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

            firstPersonController.SetRifleOwnedStatus(true);

            Destroy(gameObject); //destroys the pickup version of the rifle.

            hudManager.DisplayPickUpText(gameObject.tag); //tells player they found the rifle
            hudManager.DisplayImage(gameObject.tag); //shows image of the rifle
        }
    }

    /**************************************************************************
   Function: SetLineRenderer

Description: Given a reference to a LineRenderer, this function retrieves the
             LineRenderer component that draws the rifle's laser sight.

      Input: lineRenderer - a reference to a LineRenderer component

     Output: none
    **************************************************************************/
    private void SetLineRenderer(ref LineRenderer lineRenderer)
    {
        lineRenderer = firstPersonController.GetWeapon("Rifle").GetComponentInChildren<LineRenderer>();
    }

    /**************************************************************************
   Function: EquipRifle

Description: This function plays the equipping animation and enables the 
             meshRender making the rifle visible and enables the meshCollider 
             for collision detection.

      Input: none

     Output: none
    **************************************************************************/
    public void EquipRifle()
    {
        rifleAnim.SetTrigger("isEquipping");

        meshRenderer.enabled = true;

        foreach (MeshRenderer meshes in rifleMeshRenderers)
        {
            meshes.enabled = true;
        }

        boxCollider.enabled = true;

        equipped = true;

        weaponManager.SetEquippedWeaponName(gameObject.tag);

        hudManager.SetWeaponNameText("Rifle");
        hudManager.SetMaxLoadedAmmoText("/ " + maxRifleMagazineCount.ToString());
        hudManager.DisplayWeaponInfo(true);
        hudManager.UpdateWeaponInfo();
    }

    /**************************************************************************
   Function: UnequipRifle

Description: This function disables the meshRenderer making the rifle invisible
             and disables the meshCollider to prevent any collisions.

      Input: none

     Output: none
    **************************************************************************/
    public void UnequipRifle()
    {
        meshRenderer.enabled = false;

        foreach (MeshRenderer meshes in rifleMeshRenderers)
        {
            meshes.enabled = false;
        }

        boxCollider.enabled = false;

        equipped = false;

        hudManager.DisplayWeaponInfo(false);
    }

    /**************************************************************************
   Function: CheckWhatWasHit

Description: This function checks if the tag of the updated hit structure is
             an enemy. If so, a message is sent to the enemy object to take
             damage using this gameObject's name to determine the damage. If
             it's a weakness, the enemy is immediately killed.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckWhatWasHit()
    {
        if (hit.collider.tag == "Enemy")
        {
            hit.transform.SendMessage("TakeBulletDamage", gameObject.name);
        }
        else if (hit.collider.tag == "Weakness")
        {
            hit.transform.SendMessageUpwards("Kill");
        }
    }

    /**************************************************************************
   Function: UpdateBulletOrigin

Description: This function sets the endOfBarrel to the position at the end of
             the gun.

      Input: none

     Output: none
    **************************************************************************/
    private void UpdateBulletOrigin()
    {
        endOfBarrel = barrelTip.transform.position;
    }

    /**************************************************************************
   Function: ResetRifleShooting

Description: This function resets the trigger for the shooting animation.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetRifleShooting()
    {
        rifleAnim.ResetTrigger("Shooting");
    }

    /**************************************************************************
   Function: SetDamage

Description: Given a float, this function sets currentDamage to the given 
             float.

      Input: damage - float that's assigned to currentDamage

     Output: none
    **************************************************************************/
    private void SetDamage(float damage)
    {
        currentDamage = damage;
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
