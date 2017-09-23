using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    //This script controls the object and camera the player controls which allows for movement and looking around.

    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        //[SerializeField] private AnimationClip PipeIdleAnim;
        //[SerializeField] private AnimationClip PipeMoveForwardAnim;

        [SerializeField] private GameObject PipeWeapon;

        private MouseLook[] mouse;

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;
        private CharacterController characterController;
        private Vector3 rayOrigin;

        [SerializeField] private PlayerInfo playerInfo;
        [SerializeField] private WeaponInfo weaponInfo;
        [SerializeField] private EnemyInfo enemyInfo;
        [SerializeField] private TestAI testAI;

        //These are the animation variables
        private Animator PipeAnim;
        private bool WalkForward;
        private bool RunForward;
        private bool SidestepLeft;
        private bool SidestepRight;
        private bool WalkBackwards;
        private float WalkAgain;
        private float Attack2Opportunity;
        private float Attack3Opportunity;
        private bool Attack1Damage;
        private bool Attack2Damage;
        private bool Attack3Damage;

        private InteractPrompts interactPrompts;

        //Create scripts for item and collectible, then add references here

        private Canvas hudCanvas;
        private Canvas invCanvas;

        //Stores the player object's layer, so the camera's raycast can ignore it.
        //public LayerMask layer = 9;
        public int layerMask = 1 << 9;

        public RaycastHit hit;
        public Ray ray;

        //This will be the collided object's name.
        public static string collideObjectName;

        //This will be the tag of the object a raycast hits.
        public new static string tag;

        //This will be the name of the object a raycast hits.
        public static string gameObjectName;

        //This will be the address of the game object a raycast hits.
        public static GameObject targetedObject;

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
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;          

            invCanvas = GameObject.Find("Inventory Canvas").GetComponent<Canvas>();

            PipeAnim = PipeWeapon.GetComponent<Animator>();

            //This should invert the mask so the raycast that uses it will detect all layers EXCEPT the ignore raycast layer and this layer.
            layerMask = ~layerMask;
        }

        private void Update()
        {
            //rayOrigin = new Vector3(m_Camera.transform.position.x, m_Camera.transform.position.y, m_Camera.transform.position.z - 0.5f);
            //Draws a ray in the scene window to show where exactly the ray is coming from and pointing while ignoring the layer
            //that is stored in the variable "layer".
            Debug.DrawRay(m_Camera.transform.position, m_Camera.transform.forward, Color.red, 10f);
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, 2, layerMask))
            {
                
                gameObjectName = hit.collider.gameObject.name;
                tag = hit.collider.tag;
                targetedObject = hit.transform.gameObject;
                Debug.Log("Name: " + targetedObject.name);
                Debug.Log("Tag: " + tag);
                Debug.Log("Targeted Object's Layer: " + targetedObject.layer);
            }
           else
            {
                gameObjectName = "";
                tag = "";
                targetedObject = null;
            }

            //TODO: If settings that allow sensitivity to be changed are made possible, change the else to change values 
            //to what the player set them as.

            if (invCanvas.enabled == true)
            {
                m_MouseLook.XSensitivity = 0;
                m_MouseLook.YSensitivity = 0;
                m_CharacterController.enabled = false;
            }
            else
            {
                m_MouseLook.XSensitivity = 2;
                m_MouseLook.YSensitivity = 2;
                m_CharacterController.enabled = true;
            }

            //Conditional statements that toggle a bool so walking and running forward animations can play
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                WalkForward = true;
                PipeAnim.SetBool("WalkForward", WalkForward);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    RunForward = true;
                    WalkForward = false;
                    PipeAnim.SetBool("RunForward", RunForward);
                    PipeAnim.SetBool("WalkForward", WalkForward);
                }
                else
                {
                    RunForward = false;
                    PipeAnim.SetBool("RunForward", RunForward);
                }
            }
            else
            {
                RunForward = false;
                WalkForward = false;
                PipeAnim.SetBool("RunForward", RunForward);
                PipeAnim.SetBool("WalkForward", WalkForward);
            }

            //Conditional statements that toggle a bool so strafe left animation can be played or cancelled.
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                SidestepLeft = true;
                PipeAnim.SetBool("SidestepLeft", SidestepLeft);
            }
            else
            {
                SidestepLeft = false;
                PipeAnim.SetBool("SidestepLeft", SidestepLeft);
            }

            //Conditional statements that toggle a bool so strafe right animation can be played or cancelled.
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                SidestepRight = true;
                PipeAnim.SetBool("SidestepRight", SidestepRight);
            }
            else
            {
                SidestepRight = false;
                PipeAnim.SetBool("SidestepRight", SidestepRight);
            }

            //Conditional statements that toggle a bool so walk backwards animation can be played or cancelled.
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                WalkBackwards = true;
                PipeAnim.SetBool("WalkBackwards", WalkBackwards);
            }
            else
            {
                WalkBackwards = false;
                PipeAnim.SetBool("WalkBackwards", WalkBackwards);
            }

            //TODO: Add additional conditionals to ensure attack 3 ONLY plays when attack 2 collides with a "living" enemy.
            //TODO: Add additional conditionals that check to see if stamina is remaining before each attack plays.

            //TODO: Damage is being dealt multiple times if clicked. Maybe instead of dealing it immediately, set a 
            //bool and/or float to true to apply damage.

            //Conditional statements that determine which and when melee attacks can be executed.
            if (Input.GetMouseButtonDown(0))
            {
                if (playerInfo.staminaCooldown1 == 0 && playerInfo.staminaCooldown2 == 0)
                {
                    PipeAnim.Play("Pipe Attack 1");
                    //playerInfo.staminaCooldown1 = 2;
                    WalkAgain = 1.08f;
                    Attack2Opportunity = 2.0f;
                    playerInfo.staminaCooldown1 = 3;
                    playerInfo.staminaRegenWait = 1.5f;
                    if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, 2))
                    {
                        if (hit.collider.gameObject.name == "EnemyType1" && enemyInfo.Enemy1Health > 0 && Attack1Damage == false)
                        {
                            //TODO: Change this back to 20 or something similar.
                            enemyInfo.Enemy1Health -= 100;
                            Attack1Damage = true;
                            testAI.Attacked = true;             
                        }
                    }
                    Attack3Damage = false;
                }
                else if (Attack2Opportunity <= 1.5f &&
                    Attack2Opportunity >= 1.2f && playerInfo.staminaCooldown2 == 0)
                {
                    PipeAnim.Play("Pipe Attack 2");
                    WalkAgain = 1.08f;
                    Attack3Opportunity = 2.0f;
                    playerInfo.staminaCooldown1 = 3;
                    playerInfo.staminaRegenWait = 1.5f;
                    if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, 2))
                    {
                        if (hit.collider.gameObject.name == "EnemyType1" && enemyInfo.Enemy1Health > 0 && Attack2Damage == false)
                        {
                            enemyInfo.Enemy1Health -= 10;
                            Attack2Damage = true;
                            testAI.Attacked = true;
                        }
                    }
                }
                else if (Attack3Opportunity <= 1.5f &&
                    Attack3Opportunity >= 1.2f && playerInfo.staminaCooldown2 == 0)
                {
                    PipeAnim.Play("Pipe Attack 3");
                    WalkAgain = 1.08f;
                    playerInfo.staminaCooldown1 = 3;
                    playerInfo.staminaRegenWait = 1.5f;
                    if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, 2))
                    {
                        if (hit.collider.gameObject.name == "EnemyType1" && enemyInfo.Enemy1Health > 0 && Attack3Damage == false)
                        {
                            enemyInfo.Enemy1Health -= 30;
                            Attack3Damage = true;
                            testAI.Attacked = true;
                        }
                    }
                }
            }

            //Conditional statements that ensures a cooldown and opportunities occur before attacking again is possible.
            if (Attack2Opportunity > 0f)
            {
                Attack2Opportunity -= Time.deltaTime;
            }
            else if (Attack2Opportunity < 0f)
            {
                Attack2Opportunity = 0;
                Attack1Damage = false;
            }

            if (Attack3Opportunity > 0f)
            {
                Attack3Opportunity -= Time.deltaTime;
            }
            else if (Attack3Opportunity < 0f)
            {
                Attack3Opportunity = 0;
                Attack2Damage = false;
            }

            //Conditional statement that ensures a cooldown occurs before normal movement again is possible.
            if (WalkAgain > 0f)
            {
                m_WalkSpeed = 1f;
                m_RunSpeed = 1f;
                WalkAgain -= Time.deltaTime;
            }
            else if (WalkAgain < 0f)
            {
                m_WalkSpeed = 5f;
                m_RunSpeed = 10f;
                WalkAgain = 0f;
            }



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
        }


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
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
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

        public void OnTriggerEnter(Collider other)
        {
            collideObjectName = other.gameObject.name;
        }
    }
}
