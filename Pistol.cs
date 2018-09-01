/******************************************************************************
  File Name: Pistol.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that manage the pistol's stats, 
             behavior, and animations.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Pistol : MonoBehaviour
{
    [SerializeField] private SurvivalNoteManager survivalNoteManager = null;
      //used to communicate with player object
    [SerializeField] private FirstPersonController firstPersonController = null;
    [SerializeField] private WeaponManager weaponManager = null;
    [SerializeField] private ItemManager itemManager = null;
      //used to communicate with HUD
    [SerializeField] private HUDManager hudManager = null;
    [SerializeField] private Pistol pistol = null; //used by pistol pickup only
    [SerializeField] private Animator pistolAnim = null; //contains all animations
    [SerializeField] private GameObject barrelTip = null; //position the bullets originate from
    [SerializeField] private LineRenderer lineRenderer = null; //beam of the laser sight
    [SerializeField] private LineRenderer laserPoint = null; //tip of laser sight to signify where it hits
    [SerializeField] private AudioSource audioSource = null; //what plays gun's sound effects
    [SerializeField] private AudioClip pistolFire1Clip = null; //sound of pistol firing
    [SerializeField] private AudioClip pistolReloadClip = null; //sound of pistol reloading
    private SkinnedMeshRenderer skinnedMeshRenderer = null; //used to make visible/invisible
    private BoxCollider boxCollider = null; //used to detect collisions
    private Vector3 endOfBarrel; //position where rays for bullets are cast
    private RaycastHit hit; //contains info on what the gun hits
    private const float defaultRange = 75.0f; //default range of pistol bullets
    private const float defaultDamage = 30.0f; //this is the starting damage of the pistol
    private const float defaultRateOfFire = 6.0f; //default rate of fire of pistol
    private const float defaultReloadSpeed = 5.0f; //default reload speed of pistol
    private const float firstDamageBonus = 10.0f; //value of 1st damage upgrade
    private const float secondDamageBonus = 20.0f; //value of 2nd damage upgrade
    private const float firstRateOfFireBonus = 2.5f; //value of first RoF upgrade
    private const float firstRangeBonus = 25.0f; //value of first range upgrade
    private const float firstReloadSpeedBonus = 5.0f; //value of first reload upgrade
    private float currentDamage; //the current damage of each bullet
    private float currentRange; //the current range of the pistol
    private float currentRateOfFire; //the current rate of fire of the pistol
    private float currentReloadSpeed; //the current reload speed of the pistol
    private const int maxPistolMagazineCount = 10; //number of rounds in each pistol magazine
    private int currentPistolMagazineCount; //number of rounds currently in magazine
    private bool isRunning; //used to play the running animation
    private bool equipped; //used to check if the pistol is equipped
    private bool secondary; //is true if this weapon is in secondary slot
      //used to check if player owns this gun for when enemy drops are calculated
    private bool acquired;
    private bool damageUpgrade1; //increases weapon damage once
    private bool damageUpgrade2; //increases weapon damage a second time
    private bool rangeUpgrade1; //increases weapon range once
    private bool rateOfFireUpgrade1; //increases rate of fire once
    private bool reloadSpeedUpgrade1; //increases reload speed once


    void Start ()
    {
        SetMeshRenderer(ref skinnedMeshRenderer);
        SetBoxCollider(ref boxCollider);
        SetLineRenderer(ref lineRenderer);
        SetAudioSource(ref audioSource);
        currentPistolMagazineCount = maxPistolMagazineCount; //TODO: add code to make this only happen when pistol is equipped for first time

        SetDamage(defaultDamage);
        SetRange(defaultRange);
        SetRateOfFire(defaultRateOfFire);
        SetReloadSpeed(defaultReloadSpeed);

        UpgradePistol(); //if this is loaded from a save file and player has upgrades, they'll be applied now
	}

    void Update ()
    {
        UpdateBulletOrigin(); //ensures origin of bullets follow the end of gun's barrel
        ShowLaserSight(); //displays a laser sight

        Debug.DrawRay(endOfBarrel, transform.right * currentRange, Color.yellow);

        if (Input.GetKeyDown(KeyCode.G)) //test code to equip/unequip pistol
        {
            if (equipped)
            {
                UnequipPistol();
            }
            else
            {
                EquipPistol();
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            UnequipPistol();
        }

        if (hudManager.GetPauseStatus()) //checks if game is paused
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
             to true. Then it calls the UpgradePistol function.

      Input: upgradeName - string of the upgrade to set

     Output: none
    **************************************************************************/
    public void SetUpgrade(string upgradeName)
    {
        switch(upgradeName)
        {
            case "PistolDamageUpgrade1":
                damageUpgrade1 = true;
                break;
            case "PistolDamageUpgrade2":
                damageUpgrade2 = true;
                break;
            case "PistolRateOfFireUpgrade1":
                rateOfFireUpgrade1 = true;
                break;
            case "PistolReloadSpeedUpgrade1":
                reloadSpeedUpgrade1 = true;
                break;
            case "PistolRangeUpgrade1":
                rangeUpgrade1 = true;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        UpgradePistol(); //NOTE: this must be called once for each upgrade
    }

    /**************************************************************************
   Function: UpgradePistol

Description: This function checks each upgrade bool. If any are true, the
             pistol's current stat related to that bool is updated.

      Input: none

     Output: none
    **************************************************************************/
    public void UpgradePistol()
    {
        if(damageUpgrade2) //if player has 2nd damage upgrade
        {
            SetDamage(defaultDamage + secondDamageBonus);
        }
        else if(damageUpgrade1) //if the player only has 1st damage upgrade
        {
            SetDamage(defaultDamage + firstDamageBonus);
        }

        if(rateOfFireUpgrade1) //if player has 1st rate of fire upgrade
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

     Output: Returns true if player has the first pistol damage upgrade,
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

     Output: Returns true if player has the second pistol damage upgrade,
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

     Output: Returns true if player has the first pistol range upgrade,
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

     Output: Returns true if player has the first pistol reload upgrade,
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

     Output: Returns true if player has the first pistol rate of fire upgrade,
             otherwise, returns false.
    **************************************************************************/
    public bool GetRateUpgrade1()
    {
        return rateOfFireUpgrade1;
    }

    /**************************************************************************
   Function: GetDamageForStatPanel

Description: This function returns the default damage per pistol shot.

      Input: none

     Output: Returns the default damage per pistol shot.
    **************************************************************************/
    public float GetDamageForStatPanel()
    {
        return defaultDamage;
    }

    /**************************************************************************
   Function: GetRangeForStatPanel

Description: This function returns the default range of the pistol.

      Input: none

     Output: Returns the default range of the pistol.
    **************************************************************************/
    public float GetRangeForStatPanel()
    {
        return defaultRange;
    }

    /**************************************************************************
   Function: GetReloadSpeedForStatPanel

Description: This function returns the default reload speed of the pistol.

      Input: none

     Output: Returns the default reload speed of the pistol.
    **************************************************************************/
    public float GetReloadSpeedForStatPanel()
    {
        return defaultReloadSpeed;
    }

    /**************************************************************************
   Function: GetRateOfFireForStatPanel

Description: This function returns current attack speed of the pistol.

      Input: none

     Output: Returns the current rate of fire of the pistol.
    **************************************************************************/
    public float GetRateOfFireForStatPanel()
    {
        return defaultRateOfFire;
    }

    /**************************************************************************
   Function: GetUpgradedDamageForStatPanel

Description: This function returns the current damage per pistol shot. If the
             player has any damage upgrade, current damage is modified
             beforehand.

      Input: none

     Output: Returns the current damage per pistol shot.
    **************************************************************************/
    public float GetUpgradedDamageForStatPanel()
    {
        return currentDamage;
    }

    /**************************************************************************
   Function: GetUpgradedRangeForStatPanel

Description: This function returns the current range of the pistol. If the
             player has any range upgrade, current range is modified
             beforehand.

      Input: none

     Output: Returns the current range per pistol shot.
    **************************************************************************/
    public float GetUpgradedRangeForStatPanel()
    {
        return currentRange;
    }

    /**************************************************************************
   Function: GetUpgradedReloadSpeedForStatPanel

Description: This function returns the current reload speed of the pistol. If 
             the player has any reload upgrade, current reload speed is 
             modified beforehand.

      Input: none

     Output: Returns the current reload speed of the pistol.
    **************************************************************************/
    public float GetUpgradedReloadSpeedForStatPanel()
    {
        return currentReloadSpeed;
    }

    /**************************************************************************
   Function: GetUpgradedRateOfFireForStatPanel

Description: This function returns the current rate of fire of the pistol. If 
             the player has any rate of fire upgrade, current rate of fire is 
             modified beforehand.

      Input: none

     Output: Returns the current rate of fire of the pistol.
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
            pistolAnim.SetBool("isMovingForward", true);
        }
        else
        {
            pistolAnim.SetBool("isMovingForward", false);
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
        if(aiming)
        {
            pistolAnim.SetBool("isAiming", true);
        }
        else
        {
            pistolAnim.SetBool("isAiming", false);
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
        if(reloadSpeedUpgrade1)
        {
            pistolAnim.SetFloat("reloadSpeed", 1.5f);
        }

        pistolAnim.SetTrigger("Reloading");

        PlayPistolReloadAudio();
    }

    /**************************************************************************
   Function: ResetReloading

Description: This function resets the trigger that plays the reloading animation.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetReloading()
    {
        pistolAnim.ResetTrigger("Reloading");
    }


    private void PlayPistolFireAudio()
    {
        audioSource.clip = pistolFire1Clip;

        audioSource.PlayOneShot(pistolFire1Clip);
    }


    private void PlayPistolReloadAudio()
    {
        if (audioSource.clip != pistolReloadClip)
        {
            audioSource.clip = pistolReloadClip;
            audioSource.PlayOneShot(pistolReloadClip);
        }
          //if player just reloaded but picked up more ammo and wants to reload again
        else if (audioSource.clip == pistolReloadClip && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(pistolReloadClip);
        }
    }


    private void StopPistolAudio()
    {
        audioSource.Stop();
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
        if (isRunning && !PistolIsShooting() && !PistolIsReloading() && !PistolIsInAimAnimation())
        {
            pistolAnim.SetBool("isSprinting", true);
            pistolAnim.speed = 2.0f;
        }
        else
        {
            pistolAnim.SetBool("isSprinting", false);
            pistolAnim.speed = 1.0f;
        }
    }

    /**************************************************************************
   Function: GetEquippedStatus

Description: This function returns the status of whether or not the pistol
             is equipped.

      Input: none

     Output: Returns true if the pistol is equipped, otherwise, returns false.
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

Description: This function returns the status of whether or not the pistol
             is the secondary weapon.

      Input: none

     Output: Returns true if the pistol is the secondary weapon, otherwise, 
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
   Function: PistolIsInAimAnimation

Description: This function checks if either of the two aim animations are
             playing and returns a bool based on that check.

      Input: none

     Output: Returns true if any pistol aim animation is playing, otherwise,
             returns false.
    **************************************************************************/
    public bool PistolIsInAimAnimation()
    {
        return (pistolAnim.GetCurrentAnimatorStateInfo(0).IsName("PistolAim") ||
                pistolAnim.GetCurrentAnimatorStateInfo(0).IsName("PistolAimShot"));
    }

    /**************************************************************************
   Function: PistolIsShooting

Description: This function checks if the shooting animation is playing and 
             returns a bool based on that check.

      Input: none

     Output: Returns true if the pistol shooting animation is playing, 
             otherwise, returns false.
    **************************************************************************/
    public bool PistolIsShooting()
    {
        return (pistolAnim.GetCurrentAnimatorStateInfo(0).IsName("PistolShoot") ||
                pistolAnim.GetCurrentAnimatorStateInfo(0).IsName("PistolAimShot"));
    }

    /**************************************************************************
   Function: PistolIsReloading

Description: This function the status of whether or not the reload animation is
             currently playing.

      Input: none

     Output: Returns true if the reload animation is playing, otherwise, 
             returns false.
    **************************************************************************/
    public bool PistolIsReloading()
    {
        return (pistolAnim.GetCurrentAnimatorStateInfo(0).IsName("PistolReload"));
    }

    /**************************************************************************
   Function: FirePistol

Description: This function sets the trigger for the shooting animation, then
             checks if a raycast out of the barrel hits anything. If so, the
             laserPoint's position is set to the point the ray hit. Lastly, 
             one bullet is deducted from the current magazine count.

      Input: none

     Output: none
    **************************************************************************/
    public void FirePistol()
    {
        if(currentPistolMagazineCount > 0) //checks if there's ammo in the gun
        {
            if(rateOfFireUpgrade1)
            {
                pistolAnim.SetFloat("fireRate", 1.4f);
            }

            pistolAnim.SetTrigger("Shooting");
            PlayPistolFireAudio();

            UseMagazineAmmo(); //deducts from the magazine
            hudManager.UpdateWeaponInfo(); //updates HUD
              //updates current magazine count in inventory
            weaponManager.SetCurrentMagazineCount(currentPistolMagazineCount);

              //NOTE: transform.right is straight out of the front of the gun
            if(Physics.Raycast(endOfBarrel, transform.right, out hit, currentRange))
            {
                laserPoint.transform.position = hit.point;
            }
        }
    }

    /**************************************************************************
   Function: GetPistolDamage

Description: This function returns the pistol's current damage per bullet.

      Input: none

     Output: Returns the pistol's current damage.
    **************************************************************************/
    public float GetPistolDamage()
    {
        return currentDamage;
    }

    /**************************************************************************
   Function: GetMagazineAmmoCount

Description: This function returns the number of bullets currently loaded in
             the pistol.

      Input: none

     Output: Returns the pistol's current magazine count.
    **************************************************************************/
    public int GetMagazineAmmoCount()
    {
        return currentPistolMagazineCount;
    }

    /**************************************************************************
   Function: GetMaxMagazineAmmoCount

Description: This function returns the max number of bullets that can be
             loaded in the pistol.

      Input: none

     Output: Returns the pistol's max magazine count.
    **************************************************************************/
    public int GetMaxMagazineAmmoCount()
    {
        return maxPistolMagazineCount;
    }

    /**************************************************************************
   Function: SetMagazineAmmoCount

Description: Given an integer, this function sets the current number of bullets
             in the pistol to the given integer.

      Input: ammoCount - integer used to set magazine count

     Output: none
    **************************************************************************/
    public void SetMagazineAmmoCount(int ammoCount)
    {
        currentPistolMagazineCount = ammoCount;
    }

    /**************************************************************************
   Function: UseMagazineAmmo

Description: This function reduces the number of bullets currently in the
             pistol by one.

      Input: none

     Output: none
    **************************************************************************/
    private void UseMagazineAmmo()
    {
        --currentPistolMagazineCount;
    }

    /**************************************************************************
   Function: ReloadPistol

Description: This function searches the item slots in reverse order for ammo
             to load into the pistol. Support ammo is used before regular ammo.
             Then the HUD info is updated.

      Input: none

     Output: none
    **************************************************************************/
    public void ReloadPistol()
    {    
        if(currentPistolMagazineCount < maxPistolMagazineCount)
        {
              //if support pistol ammo was found first
            if(itemManager.ReverseCheckForOccupiedPanel("SupportPistolAmmo"))
            {
                  //use the ammo from that item slot
                itemManager.UseAmmoFromItemPanel("SupportPistolAmmo");
            }
              //then if regular pistol ammo was found
            else if (itemManager.ReverseCheckForOccupiedPanel("PistolAmmo"))
            {
                  //use the ammo from that item slot
                itemManager.UseAmmoFromItemPanel("PistolAmmo");
            }
        }

        hudManager.UpdateWeaponInfo(); //updated current magazine count on HUD
        weaponManager.SetCurrentMagazineCount(currentPistolMagazineCount);
    }

    /**************************************************************************
   Function: PistolIsInEquippingAnimation

Description: This function checks if the equipping animation is playing and
             returns a bool based on this check.

      Input: none

     Output: Returns true if the equipping animation is playing, otherwise,
             returns false.
    **************************************************************************/
    public bool PistolIsInEquippingAnimation()
    {
        return pistolAnim.GetCurrentAnimatorStateInfo(0).IsName("PistolEquip");
    }

    /**************************************************************************
   Function: ResetAnimSpeed

Description: This function resets the animation speed of the weapon's animator.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetAnimSpeed()
    {
        pistolAnim.speed = 1.0f;
    }

    /**************************************************************************
   Function: SetMeshRenderer

Description: Given a reference to a meshRenderer variable, this function 
             retrieves this gameObject's MeshRenderer component and stores it
             in the given variable.

      Input: meshRenderer - a reference to a meshRenderer variable

     Output: none
    **************************************************************************/
    private void SetMeshRenderer(ref SkinnedMeshRenderer skinnedMeshRenderer)
    {
        skinnedMeshRenderer = firstPersonController.GetWeapon("Pistol").GetComponentInChildren<SkinnedMeshRenderer>();
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
        boxCollider = firstPersonController.GetWeapon("Pistol").GetComponentInChildren<BoxCollider>();
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
        pistolAnim.ResetTrigger("isEquipping");
    }

    /**************************************************************************
   Function: HitByRay

Description: This function is called when the player's raycast hits this
             gameObject. The HUD displays a message to pick up the pistol. 
             If the player presses the E key, the pistol is equipped.

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
                  //sets equipped bool to true on the pistol attached to the player
                pistol.EquipPistol();
                weaponManager.UpdateWeaponStatPanel(); //displays pistol's status
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

            firstPersonController.SetPistolOwnedStatus(true);

            Destroy(gameObject); //destroys the pickup version of the pistol.

            hudManager.DisplayPickUpText(gameObject.tag); //tells player they found the pistol
            hudManager.DisplayImage(gameObject.tag); //shows image of the pistol
        }
    }

    /**************************************************************************
   Function: SetLineRenderer

Description: Given a reference to a LineRenderer, this function retrieves the
             LineRenderer component that draws the pistol's laser sight.

      Input: lineRenderer - a reference to a LineRenderer component

     Output: none
    **************************************************************************/
    private void SetLineRenderer(ref LineRenderer lineRenderer)
    {
        lineRenderer = firstPersonController.GetWeapon("Pistol").GetComponentInChildren<LineRenderer>();
    }

    /**************************************************************************
   Function: EquipPistol

Description: This function plays the equipping animation, enables the 
             meshRender making the pistol visible, enables the meshCollider for
             collision detection, displays the text that shows the gun name and
             ammo in the gun, and updates that text to display number of rounds
             currently in the gun.

      Input: none

     Output: none
    **************************************************************************/
    public void EquipPistol()
    {
        pistolAnim.SetTrigger("isEquipping");

        skinnedMeshRenderer.enabled = true;
        boxCollider.enabled = true;
        lineRenderer.enabled = true;
        laserPoint.enabled = true;

        equipped = true;

        weaponManager.SetEquippedWeaponName(gameObject.tag);

        hudManager.SetWeaponNameText("Pistol");
        hudManager.SetMaxLoadedAmmoText("/ " + maxPistolMagazineCount.ToString());
        hudManager.DisplayWeaponInfo(true);
        hudManager.UpdateWeaponInfo();
    }

    /**************************************************************************
   Function: UnequipPistol

Description: This function disables the meshRenderer making the pistol
             invisible, disables the meshCollider to prevent any collisions,
             and hides the text showing gun name and number of bullets in it.

      Input: none

     Output: none
    **************************************************************************/
    public void UnequipPistol()
    {
        skinnedMeshRenderer.enabled = false;
        boxCollider.enabled = false;
        lineRenderer.enabled = false;
        laserPoint.enabled = false;

        equipped = false;

        hudManager.DisplayWeaponInfo(false);
    }

    /**************************************************************************
   Function: ShowLaserSight

Description: This function checks if the raycast hit anything. If so, it draws
             the tip of the laser sight at that position.

      Input: none

     Output: none
    **************************************************************************/
    private void ShowLaserSight()
    {
        if(equipped)
        {
            if (Physics.Raycast(endOfBarrel, transform.right, out hit, defaultRange))
            {
                laserPoint.enabled = true;
                laserPoint.transform.position = hit.point;
            }
            else
            {
                laserPoint.enabled = false;
            }
        }

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
        if(hit.collider.tag == "Enemy")
        {        
            hit.transform.SendMessage("TakeBulletDamage", gameObject.name);
        }
        else if(hit.collider.tag == "Weakness")
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
   Function: ResetPistolShooting

Description: This function resets the trigger for the shooting animation.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetPistolShooting()
    {
        pistolAnim.ResetTrigger("Shooting");
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
