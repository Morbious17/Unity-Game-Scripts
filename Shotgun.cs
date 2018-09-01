/******************************************************************************
  File Name: Shotgun.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that manage the shotgun's stats, 
             behavior, and animations.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private GameObject cube = null; //used for testing to show where pellets hit
      //used to communicate with player object
    [SerializeField] private FirstPersonController firstPersonController = null;
    [SerializeField] private SurvivalNoteManager survivalNoteManager = null;
    [SerializeField] private WeaponManager weaponManager = null;
    [SerializeField] private ItemManager itemManager = null;
      //used to communicate with HUD
    [SerializeField] private HUDManager hudManager = null;
    [SerializeField] private Shotgun shotgun = null; //used by shotgun pickup only
    [SerializeField] private Animator shotgunAnim = null; //contains all animations
    [SerializeField] private GameObject barrelTip = null; //used to cast pellet rays from
    [SerializeField] private AudioSource audioSource = null; //plays shotgun sound effects
    [SerializeField] private AudioClip shotgunFire1Clip = null; //plays shotgun firing clip
    [SerializeField] private AudioClip shotgunReloadClip = null; //plays shotgun reload clip
    private MeshRenderer[] shotgunMeshRenderers = null; //contains all child MeshRenderers
    private BoxCollider boxCollider = null; //used to detect collisions
    private Vector3 pelletDirection; //direction the 'pellet' goes
    private Vector3 endOfBarrel; //position where the 'pellet' raycasts originate
    private RaycastHit hit; //contains info on what the gun hits
    private const float defaultRange = 25.0f; //default range of shotgun bullets
    private const float defaultDamage = 7.0f; //this is the starting damage of each shotgun 'pellet'
    private const float defaultRateOfFire = 4.0f; //default rate of shotgun fire
    private const float defaultReloadSpeed = 3.0f; //default reload speed
      //these two variables are used to determine direction of each 'pellet', the cone of all the pellets
    private const float minAngleRange = -0.015f; 
    private const float maxAngleRange = 0.015f;
    private const float firstDamageBonus = 2.0f; //value of 1st damage upgrade
    private const float secondDamageBonus = 4.0f; //value of 2nd damage upgrade
    private const float firstRateOfFireBonus = 2.0f; //value of first RoF upgrade
    private const float firstRangeBonus = 25.0f; //value of first range upgrade
    private const float firstReloadSpeedBonus = 1.0f; //value of first reload upgrade
    private float currentTotalDamage; //the current damage of all pellets together
    private float currentRange; //the current range of the shotgun
    private float currentRateOfFire; //the current rate of fire of the shotgun
    private float currentReloadSpeed; //the current reload speed of the shotgun
      //these are applied to each pellet's direction from the default forward direction
    private float[] pelletXAngles;
    private float[] pelletYAngles; 
    private float[] pelletZAngles; 
    private float currentDamage; //the damage value of each pellet
    private const int defaultPelletCount = 12; //default number of pellets that are fired
    private const int maxShotgunMagazineCount = 8; //most shells that can fit in the gun at once
    private int currentShotgunMagazineCount; //the current number of shells in the shotgun
    private int currentPelletCount; //will be upgraded with final shotgun upgrade
    private bool isRunning; //used to play the running animation
    private bool equipped; //used to check if the shotgun is equipped
    private bool secondary; //is true if this weapon is in secondary slot
      //used to check if player owns this gun for when enemy drops are calculated
    private bool acquired;
    private bool damageUpgrade1; //increases weapon damage once
    private bool damageUpgrade2; //increases weapon damage a second time
    private bool rangeUpgrade1; //increases weapon range once
    private bool rateOfFireUpgrade1; //increases rate of fire once
    private bool reloadSpeedUpgrade1; //increases reload speed once
    private bool messageSent; //prevents shotgun pellets from 

    void Start()
    {
        currentPelletCount = defaultPelletCount; //sets the default pellet count (no upgrade yet) //TODO: write a set function for this
        pelletXAngles = new float[currentPelletCount]; //creates an array of x angle offsets
        pelletYAngles = new float[currentPelletCount]; //creates an array of z angle offsets
        pelletZAngles = new float[currentPelletCount]; //creates an array of y angle offsets
        SetBoxCollider(ref boxCollider);
        shotgunMeshRenderers = GetComponentsInChildren<MeshRenderer>();
        SetAudioSource(ref audioSource);
        currentShotgunMagazineCount = maxShotgunMagazineCount; //TODO: add code to make this happen when gun is picked up for first time

        SetDamage(defaultDamage);
        SetRange(defaultRange);
        SetRateOfFire(defaultRateOfFire);
        SetReloadSpeed(defaultReloadSpeed);

        UpgradeShotgun(); //if this is loaded from a save file and player has upgrades, they'll be applied now
    }


    void Update()
    {
        UpdateBulletOrigin();

        Debug.DrawRay(endOfBarrel, transform.forward * defaultRange, Color.green);

        if (Input.GetKeyDown(KeyCode.G)) //test code to equip/unequip pipe
        {
            if (equipped)
            {
                UnequipShotgun();
            }
            else
            {
                EquipShotgun();
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            UnequipShotgun();
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
             to true. Then it calls the UpgradeShotgun function.

      Input: upgradeName - string of the upgrade to set

     Output: none
    **************************************************************************/
    public void SetUpgrade(string upgradeName)
    {
        switch (upgradeName)
        {
            case "ShotgunDamageUpgrade1":
                damageUpgrade1 = true;
                break;
            case "ShotgunDamageUpgrade2":
                damageUpgrade2 = true;
                break;
            case "ShotgunRateOfFireUpgrade1":
                rateOfFireUpgrade1 = true;
                break;
            case "ShotgunReloadSpeedUpgrade1":
                reloadSpeedUpgrade1 = true;                
                break;
            case "ShotgunRangeUpgrade1":
                rangeUpgrade1 = true;
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }

        UpgradeShotgun(); //NOTE: this must be called once for each upgrade
    }

    /**************************************************************************
   Function: UpgradeShotgun

Description: This function checks each upgrade bool. If any are true, the
             shotgun's current stat related to that bool is updated.

      Input: none

     Output: none
    **************************************************************************/
    public void UpgradeShotgun()
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

     Output: Returns true if player has the first shotgun damage upgrade,
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

     Output: Returns true if player has the second shotgun damage upgrade,
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

     Output: Returns true if player has the first shotgun range upgrade,
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

     Output: Returns true if player has the first shotgun reload upgrade,
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

     Output: Returns true if player has the first shotgun rate of fire upgrade,
             otherwise, returns false.
    **************************************************************************/
    public bool GetRateUpgrade1()
    {
        return rateOfFireUpgrade1;
    }

    /**************************************************************************
   Function: GetDamageForStatPanel

Description: This function calculates the current total damage of the shotgun
             per shot.

      Input: none

     Output: Returns total damage of a single shotgun shot.
    **************************************************************************/
    public float GetDamageForStatPanel()
    {
        return (defaultDamage * currentPelletCount);
    }

    /**************************************************************************
   Function: GetRangeForStatPanel

Description: This function returns the current range of the shotgun.

      Input: none

     Output: Returns the range of the shotgun.
    **************************************************************************/
    public float GetRangeForStatPanel()
    {
        return defaultRange;
    }

    /**************************************************************************
   Function: GetReloadSpeedForStatPanel

Description: This function returns the current reload speed of the shotgun.

      Input: none

     Output: Returns the reload speed of the shotgun.
    **************************************************************************/
    public float GetReloadSpeedForStatPanel()
    {
        return defaultReloadSpeed;
    }

    /**************************************************************************
   Function: GetRateOfFireForStatPanel

Description: This function returns current attack speed of the shotgun.

      Input: none

     Output: Returns the rate of fire of the shotgun.
    **************************************************************************/
    public float GetRateOfFireForStatPanel()
    {
        return defaultRateOfFire;
    }

    /**************************************************************************
   Function: GetUpgradedDamageForStatPanel

Description: This function returns the current damage per shotgun shot. If the
             player has any damage upgrade, current damage is modified
             beforehand.

      Input: none

     Output: Returns the current damage per shotgun shot.
    **************************************************************************/
    public float GetUpgradedDamageForStatPanel()
    {
        return (currentDamage * currentPelletCount);
    }

    /**************************************************************************
   Function: GetUpgradedRangeForStatPanel

Description: This function returns the current range of the shotgun. If the
             player has any range upgrade, current range is modified
             beforehand.

      Input: none

     Output: Returns the current range per shotgun shot.
    **************************************************************************/
    public float GetUpgradedRangeForStatPanel()
    {
        return currentRange;
    }

    /**************************************************************************
   Function: GetUpgradedReloadSpeedForStatPanel

Description: This function returns the current reload speed of the shotgun. If 
             the player has any reload upgrade, current reload speed is 
             modified beforehand.

      Input: none

     Output: Returns the current reload speed of the shotgun.
    **************************************************************************/
    public float GetUpgradedReloadSpeedForStatPanel()
    {
        return currentReloadSpeed;
    }

    /**************************************************************************
   Function: GetUpgradedRateOfFireForStatPanel

Description: This function returns the current rate of fire of the shotgun. If 
             the player has any rate of fire upgrade, current rate of fire is 
             modified beforehand.

      Input: none

     Output: Returns the current rate of fire of the shotgun.
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
            shotgunAnim.SetBool("isMovingForward", true);
        }
        else
        {
            shotgunAnim.SetBool("isMovingForward", false);
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
            shotgunAnim.SetBool("isAiming", true);
        }
        else
        {
            shotgunAnim.SetBool("isAiming", false);
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
        shotgunAnim.SetTrigger("Reloading");
        PlayShotgunReloadAudio();
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
        if (isRunning && !ShotgunIsShooting() && !ShotgunIsReloading() && !ShotgunIsInAimAnimation())
        {
            shotgunAnim.SetBool("isSprinting", true);
            shotgunAnim.speed = 2.0f;
        }
        else
        {
            shotgunAnim.SetBool("isSprinting", false);
            shotgunAnim.speed = 1.0f;
        }
    }

    /**************************************************************************
   Function: GetEquippedStatus

Description: This function returns the status of whether or not the shotgun
             is equipped.

      Input: none

     Output: Returns true if the shotgun is equipped, otherwise, returns false.
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

Description: This function returns the status of whether or not the shotgun
             is the secondary weapon.

      Input: none

     Output: Returns true if the shotgun is the secondary weapon, otherwise, 
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
   Function: ShotgunIsInAimAnimation

Description: This function checks if either of the two aim animations are
             playing and returns a bool based on that check.

      Input: none

     Output: Returns true if any shotgun aim animation is playing, otherwise,
             returns false.
    **************************************************************************/
    public bool ShotgunIsInAimAnimation()
    {
        return (shotgunAnim.GetCurrentAnimatorStateInfo(0).IsName("ShotgunAim") ||
                shotgunAnim.GetCurrentAnimatorStateInfo(0).IsName("ShotgunAimShot"));
    }

    /**************************************************************************
   Function: ShotgunIsShooting

Description: This function checks if the shooting animation is playing and 
             returns a bool based on that check.

      Input: none

     Output: Returns true if the shotgun shooting animation is playing, 
             otherwise, returns false.
    **************************************************************************/
    public bool ShotgunIsShooting()
    {
        return (shotgunAnim.GetCurrentAnimatorStateInfo(0).IsName("ShotgunShoot") ||
                shotgunAnim.GetCurrentAnimatorStateInfo(0).IsName("ShotgunAimShot"));
    }

    /**************************************************************************
   Function: ShotgunIsReloading

Description: This function the status of whether or not the reload animation is
             currently playing.

      Input: none

     Output: Returns true if the reload animation is playing, otherwise, 
             returns false.
    **************************************************************************/
    public bool ShotgunIsReloading()
    {
        return (shotgunAnim.GetCurrentAnimatorStateInfo(0).IsName("ShotgunReload"));
    }

    /**************************************************************************
   Function: FireShotgun

Description: This function sets the pelletDirection to forward from the end of the
             shotgun's barrel. Then, for each pellet, a random angle within the
             minimum and maximum range is calculated. Then a ray is cast with
             that angle and the CheckWhatWasHit function is called to see if
             an enemy was hit. Finally, 1 shotgun ammo is deducted from the
             current shotgun ammo count.

      Input: none

     Output: none
    **************************************************************************/
    public void FireShotgun()
    {
        if (currentShotgunMagazineCount > 0) //checks if there's ammo in the gun
        {
            if (rateOfFireUpgrade1)
            {
                shotgunAnim.SetFloat("fireRate", 1.4f);
            }

            shotgunAnim.SetTrigger("Shooting");
            PlayShotgunFireAudio();

            //gets first part of pellet direction which is facing forward
            pelletDirection = transform.forward; 

            UseMagazineAmmo(); //deducts from the magazine
            hudManager.UpdateWeaponInfo(); //updates HUD
            weaponManager.SetCurrentMagazineCount(currentShotgunMagazineCount);

              //randomly chooses Y and Z values for each pellet's angle
              //NOTE: try using a single float instead of an array
            for (int i = 0; i < currentPelletCount; i++)
            {
                  //creates random x, y, and z values for each pellet
                pelletXAngles[i] = Random.Range(minAngleRange, maxAngleRange);
                pelletYAngles[i] = Random.Range(minAngleRange, maxAngleRange);
                pelletZAngles[i] = Random.Range(minAngleRange, maxAngleRange);
                  //offsets each pellet angle from the forward vector
                pelletDirection = new Vector3(pelletDirection.x + pelletXAngles[i], 
                                              pelletDirection.y + pelletYAngles[i], 
                                              pelletDirection.z + pelletZAngles[i]);
            
                  //casts a ray for each pellet
                Physics.Raycast(endOfBarrel, pelletDirection, out hit, currentRange);
                CheckWhatWasHit(); //check if an enemy was hit

                //NOTE: used to spawn cubes where the bullets hit for testing purposes
                Instantiate(cube, hit.point, Quaternion.identity);
            }
        }
    }

    /**************************************************************************
   Function: GetShotgunDamage

Description: This function returns the shotgun's current damage per pellet.

      Input: none

     Output: Returns the shotgun's current damage.
    **************************************************************************/
    public float GetShotgunDamage()
    {
        return currentDamage;
    }

    /**************************************************************************
   Function: GetMagazineAmmoCount

Description: This function returns the number of bullets currently loaded in
             the shotgun.

      Input: none

     Output: Returns the shotgun's current magazine count.
    **************************************************************************/
    public int GetMagazineAmmoCount()
    {
        return currentShotgunMagazineCount;
    }

    /**************************************************************************
   Function: GetMaxMagazineAmmoCount

Description: This function returns the max number of bullets that can be
             loaded in the shotgun.

      Input: none

     Output: Returns the shotgun's max magazine count.
    **************************************************************************/
    public int GetMaxMagazineAmmoCount()
    {
        return maxShotgunMagazineCount;
    }

    /**************************************************************************
   Function: SetMagazineAmmoCount

Description: Given an integer, this function sets the current number of bullets
             in the shotgun to the given integer.

      Input: ammoCount - integer used to set magazine count

     Output: none
    **************************************************************************/
    public void SetMagazineAmmoCount(int ammoCount)
    {
        currentShotgunMagazineCount = ammoCount;
    }

    /**************************************************************************
   Function: UseMagazineAmmo

Description: This function reduces the number of bullets currently in the
             shotgun by one.

      Input: none

     Output: none
    **************************************************************************/
    private void UseMagazineAmmo()
    {
        --currentShotgunMagazineCount;
    }

    /**************************************************************************
   Function: ReloadShotgun

Description: This function searches the item slots in reverse order for ammo
             to load into the shotgun. Support ammo is used before regular
             ammo. Then the HUD info is updated.

      Input: none

     Output: none
    **************************************************************************/
    public void ReloadShotgun()
    {
        if(currentShotgunMagazineCount < maxShotgunMagazineCount)
        {
              //if support shotgun ammo was found first
            if (itemManager.ReverseCheckForOccupiedPanel("SupportShotgunAmmo"))
            {
                  //use the ammo from that item slot
                itemManager.UseAmmoFromItemPanel("SupportShotgunAmmo");
            }
              //then if regular shotgun ammo was found
            else if (itemManager.ReverseCheckForOccupiedPanel("ShotgunAmmo"))
            {
                  //use the ammo from that item slot
                itemManager.UseAmmoFromItemPanel("ShotgunAmmo");
            }
        }

        hudManager.UpdateWeaponInfo(); //updated current magazine count on HUD
        weaponManager.SetCurrentMagazineCount(currentShotgunMagazineCount);
    }

    /**************************************************************************
   Function: ShotgunIsInEquippingAnimation

Description: This function checks if the equipping animation is playing and
             returns a bool based on this check.

      Input: none

     Output: Returns true if the equipping animation is playing, otherwise,
             returns false.
    **************************************************************************/
    public bool ShotgunIsInEquippingAnimation()
    {
        return shotgunAnim.GetCurrentAnimatorStateInfo(0).IsName("ShotgunEquip");
    }

    /**************************************************************************
   Function: ResetAnimSpeed

Description: This function resets the animation speed of the weapon's animator.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetAnimSpeed()
    {
        shotgunAnim.speed = 1.0f;
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
        skinnedMeshRenderer = firstPersonController.GetWeapon("Shotgun").GetComponentInChildren<SkinnedMeshRenderer>();
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
        boxCollider = firstPersonController.GetWeapon("Shotgun").GetComponentInChildren<BoxCollider>();
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
        shotgunAnim.ResetTrigger("isEquipping");
    }

    /**************************************************************************
   Function: HitByRay

Description: This function is called when the player's raycast hits this
             gameObject. The HUD displays a message to pick up the shotgun. 
             If the player presses the E key, the shotgun is equipped.

      Input: none

     Output: none
    **************************************************************************/
    private void HitByRay()
    {
        hudManager.DisplayPrompt(); //display prompt to pick up the shotgun

        if (hudManager.GetPickupCooldown() <= 0.0f && Input.GetKeyDown(KeyCode.E) && !hudManager.GetPauseStatus())
        {
              //checks if player doesn't have an equipped weapon yet
            if (!weaponManager.GetPrimaryWeaponEquipped())
            {
                  //sets equipped bool to true on the pipe attached to the player
                shotgun.EquipShotgun();
                weaponManager.UpdateWeaponStatPanel(); //displays shotgun's stats
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

            firstPersonController.SetShotgunOwnedStatus(true);

            Destroy(gameObject);

            hudManager.DisplayPickUpText(gameObject.tag); //tells player they found a shotgun
            hudManager.DisplayImage(gameObject.tag); //shows image of the shotgun
        }
    }

    /**************************************************************************
   Function: ResetReloading

Description: This function resets the trigger that plays the reloading animation.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetReloading()
    {
        if (reloadSpeedUpgrade1)
        {
            shotgunAnim.SetFloat("reloadSpeed", 1.5f);
        }

        shotgunAnim.ResetTrigger("Reloading");
    }


    private void PlayShotgunFireAudio()
    {
        audioSource.clip = shotgunFire1Clip;

        audioSource.PlayOneShot(shotgunFire1Clip);
    }


    private void PlayShotgunReloadAudio()
    {
        if (audioSource.clip != shotgunReloadClip)
        {
            audioSource.clip = shotgunReloadClip;
            audioSource.PlayOneShot(shotgunReloadClip);
        }
          //if player just reloaded but picked up more ammo and wants to reload again
        else if (audioSource.clip == shotgunReloadClip && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(shotgunReloadClip);
        }
    }

    /**************************************************************************
   Function: EquipShotgun

Description: This function enables the meshRender making the shotgun visible and
             enables the meshCollider for collision detection.

      Input: none

     Output: none
    **************************************************************************/
    public void EquipShotgun()
    {
        shotgunAnim.SetTrigger("isEquipping");

        foreach (MeshRenderer meshes in shotgunMeshRenderers)
        {
            meshes.enabled = true;
        }

        equipped = true;

        weaponManager.SetEquippedWeaponName(gameObject.tag);

        hudManager.SetWeaponNameText("Shotgun");
        hudManager.SetMaxLoadedAmmoText("/ " + maxShotgunMagazineCount.ToString());
        hudManager.DisplayWeaponInfo(true);
        hudManager.UpdateWeaponInfo();
    }

    /**************************************************************************
   Function: UnequipShotgun

Description: This function disables the meshRenderer making the shotgun invisible
             and disables the meshCollider to prevent any collisions.

      Input: none

     Output: none
    **************************************************************************/
    public void UnequipShotgun()
    {
        foreach (MeshRenderer meshes in shotgunMeshRenderers)
        {
            meshes.enabled = false;
        }

        equipped = false;

        hudManager.DisplayWeaponInfo(false);
    }


    private void ResetMessageSent()
    {
        messageSent = false; //weakness can be shot again
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
        else if (hit.collider.tag == "Weakness" && !messageSent)
        {
            hit.transform.SendMessageUpwards("Kill");

            messageSent = true; //prevent shotgun from scoring extra upgrade experience
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
   Function: ResetShotgunShooting

Description: This function resets the trigger for the shooting animation.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetShotgunShooting()
    {
        shotgunAnim.ResetTrigger("Shooting");
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
