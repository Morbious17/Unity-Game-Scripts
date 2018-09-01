/******************************************************************************
  File Name: FirstPersonController.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that control the player and weapon
             animations.
******************************************************************************/

using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed = 0.0f;
        [SerializeField] private float m_RunSpeed = 0.0f;
        [SerializeField] private float m_CrouchSPeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten = 0.0f;
        [SerializeField] private float m_JumpSpeed = 0.0f;
        [SerializeField] private float m_StickToGroundForce = 0.0f;
        [SerializeField] private float m_GravityMultiplier = 0.0f;
        [SerializeField] private MouseLook m_MouseLook = null;
        [SerializeField] private bool m_UseFovKick = false;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob = false;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval = 0.0f;
        [SerializeField] private AudioClip[] m_FootstepSounds = null;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound = null;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound = null;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private Vector3 m_CrouchPosition;
        private Vector3 m_StandPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private bool m_Crouching;
        private AudioSource m_AudioSource;

          //enables accessing the user interface that displays items, weapons, options, etc.
        [SerializeField] private UIManager uiManager = null;
          //enables communication with the UI including inventory, map, and collectibles
        [SerializeField] private Canvas inventoryCanvas = null;
          //communicates with item manager to use items
        [SerializeField] private ItemManager itemManager = null;
          //communicates with weapon manager to quick swap weapons
        [SerializeField] private WeaponManager weaponManager = null;
          //enables communcation with the hud
        [SerializeField] private HUDManager hudManager = null;
          //retrieves crouch toggle status
        [SerializeField] private OptionsManager optionsManager = null;

        [SerializeField] private MapManager mapManager = null;
          //the player's flashglight
        [SerializeField] private Light flashlight = null;
          //the player's first melee weapon
        [SerializeField] private Pipe pipe = null;
          //the player's second melee weapon
        [SerializeField] private Pickaxe pickaxe = null;
          //the player's first gun
        [SerializeField] private Pistol pistol = null;
          //the player's second gun
        [SerializeField] private Shotgun shotgun = null;
          //the player's final gun
        [SerializeField] private Rifle rifle = null;
        private RaycastHit hit; //structure used to get information back from a raycast
        private Vector3 newVelocity;
        private const float rayDistance = 3.0f; //length of the ray
        private const float ledIntensity = 2.5f; //brightness of upgraded flashlight
        private const float defaultFieldOfView = 60.0f;
        private const float pistolZoom = 30.0f; //amount of zoom when pistol is aimed
        private const float shotgunZoom = 30.0f; //amount of zoom when shotgun is aimed
        private const float rifleZoom = 15.0f; //amount of zoom when rifle is aimed
        private const float zoomRate = 100.0f; //rate at which camera zooms in and out
        private bool gotOldFlashlight; //used to toggle flashlight on and off
        private bool gotLEDFlashlight;
        private bool zoomedIn; //used to zoom the camera in and out
        //private bool sprinting; //used to play sprinting animations with weapons
        private bool pistolOwned; //used by enemies to determine if pistol ammo can be dropped
        private bool shotgunOwned; //used by enemies to determine if shotgun ammo can be dropped
        private bool rifleOwned; //used by enemies to determine if rifle ammo can be dropped
        private bool rayHitSomething; //checked before raycast hit is accessed
        private const int ledRange = 15; //range of upgraded flashlight
        private const int ledSpotAngle = 90; //cone width of upgraded flashlight
          //Stores the player object's layer, so the camera's raycast can ignore it.
        private int layerMask = 1 << 9;
        private int swingAnimation; //used to determine which camera animation to play

        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);

              //This should invert the mask so the raycast that uses it will detect all 
              //layers EXCEPT the ignore raycast layer and this layer.
            layerMask = ~layerMask;
        }

        private void Update()
        {
            ManageWeaponZoom();
            RotateView();
              // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }

            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;

              //the pipe isn't being swung, and shift key and w keys are held
            pipe.Running(pipe.GetEquippedStatus() &&
                         !pipe.PipeIsInAttackAnimation() &&
                         hudManager.GetCurrentStamina() > 0.0f &&
                         Input.GetKey(KeyCode.LeftShift) && 
                         Input.GetKey(KeyCode.W));

              //the pickaxe running animation is played if the pickaxe is equipped,
              //stamina is > 0, the pickaxe isn't being swung, and shift and w keys are held
            pickaxe.Running(pickaxe.GetEquippedStatus() &&
                            !pickaxe.PickaxeIsInAttackAnimation() &&
                            hudManager.GetCurrentStamina() > 0.0f &&
                            Input.GetKey(KeyCode.LeftShift) &&
                            Input.GetKey(KeyCode.W));

              //pistol running animation is played if pistol is equipped, stamina is > 0, 
              //the shift key is held, and the player isn't reloading
            pistol.Running(pistol.GetEquippedStatus() && 
                           !pistol.PistolIsReloading() &&
                           hudManager.GetCurrentStamina() > 0.0f &&
                           Input.GetKey(KeyCode.LeftShift) &&
                           Input.GetKey(KeyCode.W));

              //shotgun running animation is played if shotgun is equipped, stamina is > 0, 
              //the shift key is held, and the player isn't reloading
            shotgun.Running(shotgun.GetEquippedStatus() && 
                            !shotgun.ShotgunIsReloading() &&
                            hudManager.GetCurrentStamina() > 0.0f &&
                            Input.GetKey(KeyCode.LeftShift) && 
                            Input.GetKey(KeyCode.W));

              //rifle running animation is played if rifle is equipped, stamina is > 0, 
              //the shift key is held, and the player isn't reloading
            rifle.Running(rifle.GetEquippedStatus() &&
                          !rifle.RifleIsReloading() &&
                          hudManager.GetCurrentStamina() > 0.0f &&
                          Input.GetKey(KeyCode.LeftShift) &&
                          Input.GetKey(KeyCode.W));

            if (hudManager.GetTextDisplayed()) //checks if popup text is displayed
            {
                hudManager.ClearPrompt(); //clear prompt while game is paused

                if(hudManager.GetPromptCounter() > 0.0f)
                {
                    hudManager.DecrementPromptCounter();
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) ||
                        Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) ||
                        Input.GetKeyDown(KeyCode.E) ||
                        Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        hudManager.HideImage(); //hides image and unpauses game
                        hudManager.ClearText(); //removes popup text
                          //brief cooldown before another item can be picked up
                        hudManager.ResetPickupCooldown(); 
                          //checks if reveal note should be displayed after picking something up
                        if(hudManager.GetDisplayNoteMessageStatus())
                        {
                              //displays the message
                            hudManager.DisplaySurvivalNoteRevealedText();
                              //sets bool to false so message won't be displayed again after picking up another item
                            hudManager.SetDisplayNoteMessage(false);
                        }
                    }
                }
            }

            //TODO: if cutscenes are implemented, need to check for them here
            if (Input.GetKeyDown(KeyCode.Tab) && !hudManager.GetDisplayImageStatus())
            {
                if(!hudManager.GetPauseStatus()) //if game isn't paused
                {
                    hudManager.PauseGameForMenus(!hudManager.GetPauseStatus()); //pauses game and hides hud
                    uiManager.PressInventoryButton();
                    inventoryCanvas.enabled = true;
                }
                else
                {
                    hudManager.PauseGameForMenus(!hudManager.GetPauseStatus()); //unpauses game and displays hud

                    inventoryCanvas.enabled = false;
                    uiManager.PressCloseButton();
                }
            }

            if(Input.GetKeyDown(KeyCode.Q) && weaponManager.GetSecondaryStatus() && 
               !hudManager.GetPauseStatus() && !AnyWeaponIsInAnimation())
            {
                weaponManager.SwapWeapons(); //switch equipped weapon with secondary weapon
            }

              //if crouch hold toggle is active and player holds crouch key while game isn't paused
            if (Input.GetKey(KeyCode.C) && !m_Crouching && 
                !hudManager.GetPauseStatus() && optionsManager.GetCrouchHoldStatus()) 
            {
                m_Crouching = true; //player is crouching
                m_CharacterController.height = 0.5f;  //reduces gameObject's height
            }
              //if crouch toggle toggle is active and player presses crouch key while game isn't paused
              //and nothing is immediately above them for 0.8f units
            else if(Input.GetKeyDown(KeyCode.C) && !hudManager.GetPauseStatus() &&
                    !Physics.Raycast(m_Camera.transform.position, Vector3.up, out hit, 0.8f) && 
                    !optionsManager.GetCrouchHoldStatus())
            {
                m_Crouching = !m_Crouching;

                if(m_Crouching)
                {
                    m_CharacterController.height = 0.5f;
                }
                else
                {
                    m_CharacterController.height = 1.5f;
                }
            }

              //checks if player is crouching, not holding crouch key, and there's 
              //nothing immediately above them for 0.8 units
            if(m_Crouching && !Input.GetKey(KeyCode.C) && 
               !Physics.Raycast(m_Camera.transform.position, Vector3.up, out hit, 0.8f) && 
               optionsManager.GetCrouchHoldStatus())
            {
                m_CharacterController.height = 1.5f; //returns gameObject's height
                m_Crouching = false; //the player is no longer crouching
            }

              //checks if player releases crouch key and there's nothing above them for 0.8 units
            if (Input.GetKeyUp(KeyCode.C) && !Physics.Raycast(m_Camera.transform.position, Vector3.up, out hit, 0.8f) && optionsManager.GetCrouchHoldStatus())
            {
                m_Crouching = false; //the player is no longer crouching
                m_CharacterController.height = 1.5f; //returns gameObject's height
            }

              //reduces stamina if the player is running forward, is grounded, has stamina, 
              //game isn't paused, the player isn't attacking, and the player isn't aiming
            if(Input.GetKey(KeyCode.LeftShift) && m_CharacterController.isGrounded &&
               Input.GetKey(KeyCode.W) && hudManager.GetCurrentStamina() > 0.0f && 
               !hudManager.GetPauseStatus() && !AnyWeaponIsInAnimation() && !m_Crouching)
            {
                hudManager.Sprint(); //decrements stamina for sprinting
                //sprinting = true;
            }
            else
            {
                //sprinting = false;
            }

              //if player presses F key and has old flashlight
            if(Input.GetKeyDown(KeyCode.F) && gotOldFlashlight)
            {
                flashlight.enabled = !flashlight.enabled; //toggles flashlight
            }
              //if the player left clicks, game isn't paused, and player can pick up an item again
            if(Input.GetMouseButtonDown(0) && !hudManager.GetPauseStatus() && hudManager.GetPickupCooldown() <= 0.0f)
            {
                  //if the pipe is equipped
                if(pipe.GetEquippedStatus())
                {
                    SwingPipe(); //swings the pipe weapon
                }
                else if(pickaxe.GetEquippedStatus()) //if the pickaxe is equipped
                {
                    SwingPickaxe(); //swing the pickaxe
                }
                else if(pistol.GetEquippedStatus() && !pistol.PistolIsInEquippingAnimation() && 
                        !pistol.PistolIsReloading() && !pistol.PistolIsShooting())
                {
                    pistol.FirePistol(); //fire the pistol
                }
                else if (shotgun.GetEquippedStatus() && !shotgun.ShotgunIsInEquippingAnimation() &&
                         !shotgun.ShotgunIsReloading() && !shotgun.ShotgunIsShooting())
                {
                    shotgun.FireShotgun(); //fire the shotgun 
                }
                else if (rifle.GetEquippedStatus() && !rifle.RifleIsInEquippingAnimation() &&
                         !rifle.RifleIsReloading() && !rifle.RifleIsShooting())
                {
                    rifle.FireRifle(); //fire the rifle
                }
            }
              
            if(Input.GetMouseButton(1)) //if the player attempts to aim a gun
            {
                if(!pistol.PistolIsReloading() && !rifle.RifleIsReloading() && !shotgun.ShotgunIsReloading() &&
                   !pistol.PistolIsShooting()  && !rifle.RifleIsShooting()  && !shotgun.ShotgunIsShooting())
                {
                    if (pistol.GetEquippedStatus())
                    {
                        AimPistol(true);
                        zoomedIn = true;
                    }
                    else if (shotgun.GetEquippedStatus())
                    {
                        AimShotgun(true);
                        zoomedIn = true;
                    }
                    else if (rifle.GetEquippedStatus())
                    {
                        AimRifle(true);
                        zoomedIn = true;
                    }
                    else if(pipe.GetEquippedStatus())
                    {
                        if(!pipe.PipeIsInAttackAnimation() || !pipe.PipeIsInEquippingAnimation())
                        {
                            pipe.ChargeSwing();
                        }
                    }
                    else if(pickaxe.GetEquippedStatus())
                    {
                        if (!pickaxe.PickaxeIsInAttackAnimation() || !pickaxe.PickaxeIsInEquippingAnimation())
                        {
                            pickaxe.ChargeSwing();
                        }
                    }
                }          
            }
            else if(Input.GetMouseButtonUp(1)) //if the player releases right click
            {
                AimPistol(false);
                AimShotgun(false);
                AimRifle(false);
                zoomedIn = false;
            }

            //if the player presses the reloading key while game is not paused
            if (Input.GetKeyDown(KeyCode.R) && !hudManager.GetPauseStatus())
            {
                  //checks if gun is equipped, not in a shooting or aiming animation, 
                  //magazine is not full, and there's some ammo or support ammo in inventory
                if(pistol.GetEquippedStatus() && !pistol.PistolIsShooting() && 
                   !pistol.PistolIsInAimAnimation() && 
                   (pistol.GetMagazineAmmoCount() < pistol.GetMaxMagazineAmmoCount()) &&
                   (itemManager.GetCurrentInventoryItemCount("SupportPistolAmmo") > 0 || 
                   itemManager.GetCurrentInventoryItemCount("PistolAmmo") > 0))
                {
                    pistol.Reloading();
                }
                else if(shotgun.GetEquippedStatus() && !shotgun.ShotgunIsShooting() &&
                        !shotgun.ShotgunIsInAimAnimation() && 
                        (shotgun.GetMagazineAmmoCount() < shotgun.GetMaxMagazineAmmoCount()) &&
                        (itemManager.GetCurrentInventoryItemCount("SupportShotgunAmmo") > 0 ||
                        itemManager.GetCurrentInventoryItemCount("ShotgunAmmo") > 0))
                {
                    shotgun.Reloading();
                }
                else if (rifle.GetEquippedStatus() && !rifle.RifleIsShooting() && 
                         !rifle.RifleIsInAimAnimation() && 
                         (rifle.GetMagazineAmmoCount() < rifle.GetMaxMagazineAmmoCount()) &&
                         (itemManager.GetCurrentInventoryItemCount("SupportRifleAmmo") > 0 ||
                         itemManager.GetCurrentInventoryItemCount("RifleAmmo") > 0))
                {
                    rifle.Reloading();
                }
            }

              //if staminaCooldown is > 0, start decreasing it
            if(m_IsWalking && hudManager.GetStaminaCooldown() > 0.0f)
            {
                hudManager.DecrementStaminaCooldown(); //reduces cooldown until regeneration
            }

              //if currentStamina < maxStamina, the player isn't trying to sprint, 
              //and stamina regeneration is <= 0, recover stamina. Clamp it at current max stamina.
            if(hudManager.GetCurrentStamina() < hudManager.GetMaxStamina() && 
               m_IsWalking && hudManager.GetStaminaCooldown() <= 0.0f &&
               !hudManager.GetPauseStatus())
            {
                hudManager.RegenerateStamina(); //regenerates stamina
            }

            if(!hudManager.GetPauseStatus())
            {
                UseRayCast(); //uses raycast to detect interactable objects
            }
        }
/*****************************************************************************/

    /**************************************************************************
   Function: GetPlayerPosition

Description: This function returns a Vector3 of the player's current position.

      Input: none

     Output: Returns the position of the player gameObject.
    **************************************************************************/
        public Vector3 GetPlayerPosition()
        {
            return transform.position;
        }


        public Vector3 GetPlayerRotation()
        {
            return transform.eulerAngles;
        }

    /**************************************************************************
   Function: GetRaycastHit

Description: This function returns the hit structure which contains information
             on the last object hit by the raycast.

      Input: none

     Output: Returns the hit structure from the raycast
    **************************************************************************/
        public RaycastHit GetRaycastHit()
        {
            return hit;
        }

    /**************************************************************************
   Function: GetMouseLook

Description: This function returns the class that manipulates the mouse
             behavior in game.

      Input: none

     Output: Returns the MouseLook class that manipulates mouse behavior.
    **************************************************************************/
        public MouseLook GetMouseLook()
        {
            return m_MouseLook;
        }

    /**************************************************************************
   Function: GetPistolOwnedStatus

Description: This function returns the bool that represents whether or not the
             player has the pistol.

      Input: none

     Output: Returns true if the pistol has been acquired, otherwise, returns
             false.
    **************************************************************************/
        public bool GetPistolOwnedStatus()
        {
            return pistolOwned;
        }

    /**************************************************************************
   Function: GetShotgunOwnedStatus

Description: This function returns the bool that represents whether or not the
             player has the shotgun.

      Input: none

     Output: Returns true if the shotgun has been acquired, otherwise, returns
             false.
    **************************************************************************/
        public bool GetShotgunOwnedStatus()
        {
            return shotgunOwned;
        }

    /**************************************************************************
   Function: GetRifleOwnedStatus

Description: This function returns the bool that represents whether or not the
             player has the rifle.

      Input: none

     Output: Returns true if the rifle has been acquired, otherwise, returns
             false.
    **************************************************************************/
        public bool GetRifleOwnedStatus()
        {
            return rifleOwned;
        }

    /**************************************************************************
   Function: SetPistolOwnedStatus

Description: Given a bool, this function sets the pistolOwned bool to the
             given bool.

      Input: acquired - bool used to set pistolOwned variable

     Output: none
    **************************************************************************/
        public void SetPistolOwnedStatus(bool acquired)
        {
            pistolOwned = acquired;
        }

    /**************************************************************************
   Function: SetShotgunOwnedStatus

Description: Given a bool, this function sets the shotgunOwned bool to the
             given bool.

      Input: acquired - bool used to set shotgunOwned variable

     Output: none
    **************************************************************************/
        public void SetShotgunOwnedStatus(bool acquired)
        {
            shotgunOwned = acquired;
        }

    /**************************************************************************
   Function: SetRifleOwnedStatus

Description: Given a bool, this function sets the rifleOwned bool to the
             given bool.

      Input: acquired - bool used to set rifleOwned variable

     Output: none
    **************************************************************************/
        public void SetRifleOwnedStatus(bool acquired)
        {
            rifleOwned = acquired;
        }

    /**************************************************************************
   Function: GetOldFlashlight

Description: This function returns the status of the bool that determines if
             the player has the first flashlight.

      Input: none

     Output: Returns true if player has old flashlight, otherwise, returns 
             false.
    **************************************************************************/
        public bool GetOldFlashlight()
        {
            return gotOldFlashlight;
        }

    /**************************************************************************
   Function: SetOldFlashlight

Description: Given a bool, this function sets the gotOldFlashlight bool to the
             given bool.

      Input: status - bool used to set gotOldFlashlight

     Output: none
    **************************************************************************/
        public void SetOldFlashlight(bool status)
        {
            gotOldFlashlight = status;
        }

    /**************************************************************************
   Function: EnableFlashlight

Description: Given a bool, this function enables or disables the player's
             flashlight based on the bool.

      Input: enabled - bool used to set enabled property

     Output: none
    **************************************************************************/
        public void EnableFlashlight(bool enabled)
        {
            flashlight.enabled = enabled;
        }

    /**************************************************************************
   Function: GetFlashlightOnStatus

Description: This function returns a bool that represents whether or not the
             flashlight is currently enabled.

      Input: none

     Output: Returns true if flashlight is on, otherwise, returns false.
    **************************************************************************/
        public bool GetFlashlightOnStatus()
        {
            return flashlight.enabled;
        }

    /**************************************************************************
   Function: UpgradeFlashlight

Description: This function increases the flashlight's range, spotAngle, and
             intensity which makes the flashlight's light larger and brighter.

      Input: none

     Output: none
    **************************************************************************/
        public void UpgradeFlashlight()
        {
            flashlight.range = ledRange; //increases light range
            flashlight.spotAngle = ledSpotAngle; //increases cone width
            flashlight.intensity = ledIntensity; //increases brightness
        }

    /**************************************************************************
   Function: GetWeapon

Description: Given a string, this function returns the gameObject specified
             by the string.

      Input: name - string that specifies which weapon gameObject to get

     Output: Returns the gameObject the specified script is attached to.
    **************************************************************************/
        public GameObject GetWeapon(string name)
        {
            switch(name)
            {
                case "Pipe":
                    return pipe.gameObject;
                case "Pickaxe":
                    return pickaxe.gameObject;
                case "Pistol":
                    return pistol.gameObject;
                case "Shotgun":
                    return shotgun.gameObject;
                case "Rifle":
                    return rifle.gameObject;
                default:
                    //Debug.Log("Invalid weapon name");
                    return null;
            }
        }

    /**************************************************************************
   Function: RaycastHitSomething

Description: This function returns a bool representing whether or no the
             raycast hit anything.

      Input: none

     Output: Returns true if the raycast hit anything, otherwise, returns
             false.
    **************************************************************************/
        public bool RaycastHitSomething()
        {
            return rayHitSomething;
        }

    /**************************************************************************
   Function: SwingPipe

Description: This function first checks if the player has stamina before
             determining which swing the player is able to use. Then that
             swing is executed.

      Input: none

     Output: none
    **************************************************************************/
        private void SwingPipe()
        {
            if(hudManager.GetCurrentStamina() > 0.0f) //if the player has stamina
            {
                pipe.Running(false); //returns to normal animation speed

                if(pipe.GetFirstSwingStatus()) //if first swing is ready
                {
                    pipe.UseFirstSwing(); //use first attack
                }
                else if(pipe.GetSecondSwingStatus()) //if second swing is ready
                {
                    pipe.UseSecondSwing(); //use second attack
                }
                else if (pipe.GetThirdSwingStatus()) //if third swing is ready
                {
                    pipe.UseThirdSwing(); //use third attack
                }
            }
        }

    /**************************************************************************
   Function: SwingPickaxe

Description: This function first checks if the player has stamina before
             determining which swing the player is able to use. Then that
             swing is executed.

      Input: none

     Output: none
    **************************************************************************/
        private void SwingPickaxe()
        {
            pickaxe.Running(false); //returns to normal animation speed

            if (hudManager.GetCurrentStamina() > 0.0f) //if the player has stamina
            {
                if (pickaxe.GetFirstSwingStatus()) //if first swing is ready
                {
                    pickaxe.UseFirstSwing(); //use first attack
                }
                else if (pickaxe.GetSecondSwingStatus()) //if second swing is ready
                {
                    pickaxe.UseSecondSwing(); //use second attack
                }
            }
        }

    /**************************************************************************
   Function: ManageWeaponZoom

Description: This function checks if a gun is equipped and if zoomedIn is true.
             If both are true, the camera smoothly zooms in to the equipped gun's 
             max zoom. If no gun is equipped, the camera is zoomed out to its 
             default field of view.

      Input: none

     Output: none
    **************************************************************************/
        private void ManageWeaponZoom()
        {
            if (zoomedIn) //if the player is aiming with a gun
            {
                if (pistol.GetEquippedStatus()) //if the player is using the pistol
                {
                      //if the camera isn't fully zoomed in
                    if (m_Camera.fieldOfView > pistolZoom)
                    {
                        //zoom in
                        m_Camera.fieldOfView -= Time.deltaTime * zoomRate;
                    }
                }
                  //if the player is using the shotgun
                else if (shotgun.GetEquippedStatus())
                {
                      //if the camera isn't fully zoomed in
                    if (m_Camera.fieldOfView > shotgunZoom)
                    {
                        //zoom in
                        m_Camera.fieldOfView -= Time.deltaTime * zoomRate;
                    }
                }
                else if (rifle.GetEquippedStatus())
                {
                      //if the camera isn't fully zoomed in
                    if (m_Camera.fieldOfView > rifleZoom)
                    {
                        //zoom in
                        m_Camera.fieldOfView -= Time.deltaTime * zoomRate;
                    }
                }
            }
            else
            {
                //if the camera hasn't fully zoomed out
                if (m_Camera.fieldOfView < defaultFieldOfView)
                {
                    //zoom out
                    m_Camera.fieldOfView += Time.deltaTime * zoomRate;
                }
            }
        }

    /**************************************************************************
   Function: AnyweaponIsInAnimation

Description: This function checks if any weapon is being aimed, used to attack,
             being equipped, or reload and returns a bool based on these checks.

      Input: none

     Output: Returns true if any weapon is equipped, aimed, being reloaded, or 
             used to attack, otherwise, returns false.
    **************************************************************************/
        private bool AnyWeaponIsInAnimation()
        {
            return (pipe.PipeIsInEquippingAnimation() || pipe.PipeIsInAttackAnimation() || 
                    pickaxe.PickaxeIsInEquippingAnimation() || pickaxe.PickaxeIsInAttackAnimation() ||
                    pistol.PistolIsInEquippingAnimation() || pistol.PistolIsInAimAnimation() || 
                    pistol.PistolIsShooting() || pistol.PistolIsReloading() || 
                    shotgun.ShotgunIsInEquippingAnimation() || shotgun.ShotgunIsInAimAnimation() ||
                    shotgun.ShotgunIsShooting() || shotgun.ShotgunIsReloading() ||
                    rifle.RifleIsInEquippingAnimation() || rifle.RifleIsInAimAnimation() || 
                    rifle.RifleIsShooting() || rifle.RifleIsReloading());
        }

    /**************************************************************************
   Function: AimPistol

Description: Given a bool, this function either aims or stops aiming the
             pistol based on the bool.

      Input: aiming - bool that determines if pistol is being aimed

     Output: none
    **************************************************************************/
        private void AimPistol(bool aiming)
        {
            pistol.Aiming(aiming);
        }

    /**************************************************************************
   Function: AimShotgun

Description: Given a bool, this function either aims or stops aiming the
             shotgun based on the bool.

      Input: aiming - bool that determines if shotgun is being aimed

     Output: none
    **************************************************************************/
        private void AimShotgun(bool aiming)
        {
            shotgun.Aiming(aiming);
        }

    /**************************************************************************
   Function: AimRifle

Description: Given a bool, this function either aims or stops aiming the
             rifle based on the bool.

      Input: aiming - bool that determines if rifle is being aimed

     Output: none
    **************************************************************************/
        private void AimRifle(bool aiming)
        {
            rifle.Aiming(aiming);
        }

        /**************************************************************************
       Function: UseRayCast

    Description: This function creates a ray of rayDistance length out of the
                 camera which ignores the player's body. When it collides with
                 certain objects, it sends a message to that object to call a
                 function on that object.

          Input: none

         Output: none
        **************************************************************************/
        private void UseRayCast()
        {
              //casts a ray 3 meters out of the camera
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, 
                                out hit, rayDistance, layerMask))
            {

                //TODO: When all interactable object types have been created/tagged, 
                //move sendmessage into switch statement.
                switch (hit.collider.tag)
                {
                    case "Item":
                    case "Door":
                    case "Flashlight":
                    case "Pipe":
                    case "Pickaxe":
                    case "Pistol":                  
                    case "Shotgun":
                    case "Rifle":
                    case "LargeFirstAidKit":
                    case "SmallFirstAidKit":
                    case "PistolAmmo":
                    case "ShotgunAmmo":
                    case "RifleAmmo":
                    case "Fuel":
                    case "SupportLargeAidKit":
                    case "SupportSmallAidKit":
                    case "SupportPistolAmmo":
                    case "SupportShotgunAmmo":
                    case "SupportRifleAmmo":
                    case "Weakness1":
                    case "Weakness2":
                    case "BlueEssenceS":
                    case "BlueEssenceM":
                    case "BlueEssenceL":
                    case "YellowEssenceS":
                    case "YellowEssenceM":
                    case "YellowEssenceL":
                    case "RedEssenceS":
                    case "RedEssenceM":
                    case "RedEssenceL":
                    case "Document1":
                    case "Document2":
                    case "Document3":
                    case "Document4":
                    case "Document5":
                    case "Document6":
                    case "Document7":
                    case "Document8":
                    case "Document9":
                    case "Document10":
                    case "Document11":
                    case "Document12":
                    case "Document13":
                    case "Document14":
                    case "Document15":
                    case "Document16":
                    case "Document17":
                    case "Document18":
                    case "Document19":
                    case "Document20":
                        //calls HitByRay function on the hit gameObject
                        hit.transform.SendMessage("HitByRay");
                        break;
                    case "Generator":
                          //don't continue to show prompt until message goes away
                        if(hudManager.GetNeedFuelCounter() <= 0.0f)
                        {
                            hit.transform.SendMessage("HitByRay");
                        }
                        else
                        {
                            hudManager.ClearPrompt();
                        }
                        break;
                    default:
                        hudManager.ClearPrompt(); //clears hud prompt
                        break;
                }

                rayHitSomething = true;
            }
            else
            {
                hudManager.ClearPrompt(); //clears hud prompt

                rayHitSomething = false;
            }
        }


/*****************************************************************************/
//standard asset functions//
        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;

            newVelocity = new Vector3(m_CharacterController.velocity.z, -m_CharacterController.velocity.x, 0.0f);

            mapManager.SetPlayerIconPosition(newVelocity); //moves player icon on map to match player position and speed

            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }

              //checks if character is on the ground and crouching
            if(m_CharacterController.isGrounded && m_Crouching)
            {
                newCameraPosition.y -= 0.5f; //lowers camera to crouch height
            }

            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            //checks if the game is paused
            if (hudManager.GetPauseStatus())
            {
                speed = 0; //sets speed to 0
                return; //exits this function
            }

            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
              //checks if player is pressing shift and w and has stamina
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && 
               hudManager.GetCurrentStamina() > 0.0f && 
               !pistol.PistolIsReloading()   && !pistol.PistolIsShooting() &&
               !shotgun.ShotgunIsReloading() && !shotgun.ShotgunIsShooting() &&
               !rifle.RifleIsReloading()     && !rifle.RifleIsShooting())
            {
                m_IsWalking = false; //player is sprinting
            }
            else
            {
                m_IsWalking = true; //player is walking
            }

#endif
            // set the desired speed to be walking or running or crouching
              //checks if player is crouching or swinging pipe
            if(m_Crouching || pipe.PipeIsInAttackAnimation() || pickaxe.PickaxeIsInAttackAnimation() ||
               pistol.PistolIsInAimAnimation() || shotgun.ShotgunIsInAimAnimation() ||
               rifle.RifleIsInAimAnimation())
            {
                speed = m_WalkSpeed / 4f; //reduce movement speed for crouching
            }
            else
            {
                 //if walking, use walk speed, else use run speed
               speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
                  //if pickaxe is equipped, reduce movement speed
                if(pickaxe.GetEquippedStatus())
                {
                    if(m_IsWalking)
                    {
                        speed = m_WalkSpeed * 0.75f;
                    }
                    else
                    {
                        speed = m_RunSpeed * 0.75f;
                    }
                }
            }

            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
              //if the game is paused exit function without rotating
            if(hudManager.GetPauseStatus())
            {
                return;
            }

            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}