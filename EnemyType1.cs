/******************************************************************************
  File Name: EnemyType1.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that manipulate the first type of 
             enemy's behavior and health.
******************************************************************************/
//TODO: many functions were written before I knew about navmesh. I could
//rewrite and/or remove many functions thanks to navmesh.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.AI;

public class EnemyType1 : MonoBehaviour
{
      //used to get the player's position
    [SerializeField] private FirstPersonController firstPersonController = null;
    [SerializeField] private WeaknessType1 weaknessType1 = null;
    [SerializeField] private Pipe pipe = null;
    [SerializeField] private Pickaxe pickaxe = null;
    [SerializeField] private Pistol pistol = null;
    [SerializeField] private Shotgun shotgun = null;
    [SerializeField] private Rifle rifle = null;
      //possible items that can spawn on death
    [SerializeField] private GameObject largeFirstAidKit = null;
    [SerializeField] private GameObject smallFirstAidKit = null;
    [SerializeField] private GameObject pistolAmmo = null;
    [SerializeField] private GameObject shotgunAmmo = null;
    [SerializeField] private GameObject rifleAmmo = null;
     //possible essence that can spawn on death
    [SerializeField] private GameObject blueEssenceS = null;
    [SerializeField] private GameObject blueEssenceM = null;
    [SerializeField] private GameObject blueEssenceL = null;
    [SerializeField] private GameObject yellowEssenceS = null;
    [SerializeField] private GameObject yellowEssenceM = null;
    [SerializeField] private GameObject yellowEssenceL = null;
    [SerializeField] private GameObject redEssenceS = null;
    [SerializeField] private GameObject redEssenceM = null;
    [SerializeField] private GameObject redEssenceL = null;
    //color of the enemy during idle state
    [SerializeField] private Material idleMaterial = null;
      //color of the enemy during aggro state
    [SerializeField] private Material aggroMaterial = null;
    private UpgradeManager upgradeManager = null;
    private NavMeshAgent agent; //used for pathing
    private Rigidbody rRigidbody; //contains the enemy's rigidbody component
    private MeshRenderer meshRenderer; //contains the enemy's meshRenderer component
    private Vector3 headRayPosition; //used to detect the player
      //used to detect short obstacles in front of enemy
    private Vector3 bodyRayPosition; 
    private Vector3 defaultPosition; //the enemy's staring location
    private Vector3 newDirection; //direction the enemy turns to
    private Quaternion rotationTowardsOrigin; //used to rotate towards default position
    private Vector3 playerPosition; //contains the player's last known location
      //used to calculate angle the player is from the front of the enemy
    private Vector3 directionToPlayer; 
    private RaycastHit headHit; //raycast that comes out of the front of enemy's head
    private RaycastHit leftBodyHit; //raycast that comes out of left side of enemy's lower half
    private RaycastHit rightBodyHit; //raycast that comes out of right side of enemy's lower half
    private const float objectDistance = 1.3f; //length of raycast that detects obstacles
    private const float playerDistance = 10.0f; //length of raycast that detects player
    private const float defaultDetectionLength = 15.0f; //default range of player raycast
    private const float maxWaitingPeriod = 7.0f; //max amount of time before moving again
    private const float minWaitingPeriod = 3.0f; //min amount of time before moving again
    private const float maxMoveTime = 3.0f; //max amount of time the enemy moves
    private const float minMoveTime = 1.0f; //min amount of time the enemy moves
    private const float maxAngleTime = 3.0f; //max amount of time the enemy turns
    private const float minAngleTime = 1.6f; //min amount of time the enemy turns
    private const float maxAngle = 359.0f; //max angle the enemy turns
    private const float minAngle = 0.0f; //min angle the enemy turns
    private const float defaultMaxHealth = 100.0f; //enemy's default max health
      //default time enemy tries to move around an obstacle
    private const float defaultMoveAroundTime = 0.9f; 
      //default time enemy turns away from obstacle
    private const float defaultTurnAroundTime = 0.2f;
    private const float defaultDeathTimer = 5.0f;
    private const float weakness1Range = 10.0f;
      //if player is within this angle, check distance between player and enemy
    private float angleOfView = 45.0f; //used to create a cone of vision
    private float deathTimer; //how long until the enemy object is destroyed
    private float detectionTimer; //length of time until enemy no longer detects player
    //private float moveAroundTimer; //length of time enemy moves away from obstacle
    //private float turnAroundTimer; //length of time enemy turns away from obstacle
    private float waitingPeriod; //how long until enemy moves again
    private float turningPeriod; //how long enemy turns
    private float moveTime; //how long until enemy stops moving
    private float moveAngle; //where the enemy turns to
    private float leftDistance; //distance of object on the left of the enemy
    private float rightDistance; //distance of object on the right of the enemy
    private float currentHealth; //enemy's current health
    private float detectionAngle; //angle to compare with angleOfView
      //used by spawn loot function
    private const int spawnTwoItems = 95;
    private const int spawnOneItem = 25;
    private const int spawnFirstAidKitChance = 75;
    private const int spawnAmmoChance = 40;
      //used by spawn first aid kit function
    private const int spawnLargeAidKitChance = 90;
      //used by spawn ammo function
    private const int spawnShotgunAmmoChance = 55;
    private const int spawnRifleAmmoChance = 80;
      //used by spawm essence function
    private const int redEssenceLSpawnChance = 480;
    private const int yellowEssenceLSpawnChance = 450;
    private const int blueEssenceLSpawnChance = 420;
    private const int redEssenceMSpawnChance = 250;
    private const int yellowEssenceMSpawnChance = 180;
    private const int blueEssenceMSpawnChance = 120;
    private const int redEssenceSSpawnChance = 65;
    private const int yellowEssenceSSpawnChance = 35;
    private bool detected; //is true when detected the player
    private bool awayFromOrigin; //used to return enemy to its default position
      //used to prevent enemy from taking damage multiple times per melee attack
    private bool tookDamage; //used to prevent enemy from taking damage more than once per melee swing
    private bool dead;
    private bool lootSpawned; //prevents loot from being spawned again
    private const int weaknessExperience = 1; //added to weakness 1 upgrade level
    private int layerMask = ~(1 << 11); //used to ignore enemys with the raycasts
    private string weaponName; //used to check which weapon does damage to the enemy

    void Start ()
    {
        SetRigidBody(ref rRigidbody); //retrieves rigidbody component
        SetMeshRenderer(ref meshRenderer); //retrieves the meshRenderer component
        defaultPosition = GetPosition(); //get's starting position
          //gets length of time the enemy doesn't move
        waitingPeriod = Random.Range(minWaitingPeriod, maxWaitingPeriod);
          //gets length of time the enemy is turning
        turningPeriod = Random.Range(minAngleTime, maxAngleTime);
        GetNewAngle(); //gets first angle to turn to
        GetNewMoveTime(); //gets first length of time to move
        SetHealth(defaultMaxHealth); //sets the enemy's health
        ResetDeathTimer(); //sets timer for when enemy is killed
          //retrieves player and weapon components
        firstPersonController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        pipe = GameObject.Find("Pipe").GetComponent<Pipe>();
        pickaxe = GameObject.Find("Pickaxe").GetComponent<Pickaxe>();
        pistol = GameObject.Find("Pistol").GetComponent<Pistol>();
        shotgun = GameObject.Find("Shotgun").GetComponent<Shotgun>();
        rifle = GameObject.Find("Rifle").GetComponent<Rifle>();
        upgradeManager = GameObject.Find("InvUpgradesPanel").GetComponent<UpgradeManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            rRigidbody.AddTorque(new Vector3(0.0f, 1.0f, 0.0f), ForceMode.VelocityChange);
        }

        //Debug.Log("This enemy is " + gameObject.name + " and health is " + currentHealth);
          
        UpdateRaycasts(); //updates the origin of the raycasts
        CheckForPlayer(); //checks if player is in the cone of detection

          //if enemy lost track of player and is far away from the origin
        if(!IsDead() && awayFromOrigin && !detected) 
        {
            MoveToOrigin(); //enemy attempts to find its way back to the default position
              //checks if enemy is less than 2 meters away from default position
            if(Vector3.Distance(transform.position, defaultPosition) < 4.0f)
            {
                awayFromOrigin = false; //enemy has returned to default position
                agent.ResetPath();
            }
        }
        else
        {
            if (!IsDead() && !detected) //if the player hasn't been detected
            {
                if (waitingPeriod > 0.0f) //if the enemy is still waiting between movements
                {
                    DecrementCounter(ref waitingPeriod); //reduce waiting period
                }
                else
                {
                    if (turningPeriod > 0.0f) //if the enemy is still waiting between turns
                    {
                        if (FarFromOrigin()) //if the enemy is too far from its starting position
                        {
                            LookAtOrigin(); //looks at the default position
                        }
                        else
                        {
                            Turn(moveAngle); //the enemy turns
                        }

                        DecrementCounter(ref turningPeriod); //reduce turning period

                        if (turningPeriod <= 0.0f) //if the enemy is done turning
                        {
                            GetNewMoveTime(); //get length of time the enemy moves
                        }
                    }
                    else
                    {
                          //checks if move timer is greater than 0 and a raycast is not
                          //hitting anything in front of the enemy
                        if (moveTime > 0.0f && !Physics.Raycast(bodyRayPosition, 
                                                                transform.forward, 
                                                                /*out bodyHit,*/ 
                                                                objectDistance, layerMask))
                        {
                            Move(); //move forward
                            DecrementCounter(ref moveTime); //reduce move time
                        }
                        else
                        {
                            ResetWaitingPeriod(); //resets the waiting period
                            GetNewAngle();  //gets a new angle
                            GetNewMoveTime(); //gets a new move time
                            ResetTurningPeriod(); //resets the turning period
                        }
                    }
                }
            }
            else if(!IsDead())
            {
                  //checks if enemy is still searching for the player, they are close to the
                  //player's last known position, and the player is not at that last
                  //known position
                if(detectionTimer > 0.0f && Vector3.Distance(transform.position, playerPosition) < 2.0f &&
                   Vector3.Distance(playerPosition, firstPersonController.transform.position) > 2.0f)
                {
                    SearchForPlayer();
                }
                else
                {
                    ChasePlayer(); //the enemy pursues the player
                }
            }
        }      

        if (detectionTimer > 0.0f) //if the enemy is still aggro
        {
            DecrementCounter(ref detectionTimer); //reduce detection timer
        }

        if (detectionTimer < 0.0f || IsDead()) //if the enemy is no longer aggro or dead
        {
            Detected(false); //enemy no longer detects player
            detectionTimer = 0; //sets detection timer to 0
        }

        if(upgradeManager.GetWeakness1UpgradeEquippedStatus() ||
           upgradeManager.GetWeakness2UpgradeEquippedStatus()/* ||
           upgradeManager.GetWeakness3UpgradeEquippedStatus()*/)
        {
            RevealWeakPoint(); //reveals or hides weak point based on distance from player
        }
        else
        {
            HideWeakPoint();
        }

        if(IsDead() || dead)
        {
            agent.enabled = false;
            rRigidbody.isKinematic = false;

            Die();
        }
    }

    /**************************************************************************
   Function: GetTookDamage

Description: This function returns the status of whether or not the enemy has
             taken damage from the current attack.

      Input: none

     Output: Returns true if the enemy took damage during the current attack,
             otherwise, returns false.
    **************************************************************************/
    public bool GetTookDamage()
    {
        return tookDamage;
    }

    /**************************************************************************
   Function: SetTookDamage

Description: Given a bool, this function prevents the enemy from taking damage
             multiple times from the same weapon swing.

      Input: wasHit - bool used to prevent enemy from taking damage every frame

     Output: none
    **************************************************************************/
    public void SetTookDamage(bool wasHit)
    {
        tookDamage = wasHit;
    }

    /**************************************************************************
   Function: SetDead

Description: This function sets the dead bool to true.

      Input: none

     Output: none
    **************************************************************************/
    public void SetDead()
    {
        dead = true;
    }

    /**************************************************************************
   Function: GetPosition

Description: This function returns the current position of the gameObject this
             script is attached to.

      Input: none

     Output: Returns this gameObject's current position.
    **************************************************************************/
    private Vector3 GetPosition()
    {
        return transform.position;
    }

    /**************************************************************************
   Function: UseRaycast

Description: This function casts a ray from the enemy's head and checks for the
             player. If found, the enemy has detected the player.

      Input: none

     Output: none
    **************************************************************************/
    private void UseRaycast()
    {
          //checks if a raycast from the enemy's head hits anything in front of it
        if (Physics.Raycast(headRayPosition, transform.forward, out headHit, playerDistance, layerMask))
        {
            if (headHit.collider.tag == "Player") //checks if player is detected
            {
                  //stores the player's position
                playerPosition = headHit.transform.position;
                Detected(true); //player is detected
            }
        }
    }

    /**************************************************************************
   Function: CheckForPlayer

Description: This function first checks the distance between the player to see
             if they're close enough to alert the enemy. Then it checks to see
             if the player is within the enemy's cone of vision and line of
             sight. If the player is too close or within the enemy's 'sight',
             the enemy has detected them.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForPlayer()
    {
          //retrieves the player's position
        Vector3 playerPos = firstPersonController.GetPlayerPosition();

        if(Vector3.Distance(playerPos, transform.position) < 2.0f)
        {
            //if there are no obstacles between player and enemy
            if (Physics.Raycast(headRayPosition, (playerPos - transform.position), out headHit, playerDistance, layerMask))
            {
                if (headHit.collider.tag == "Player")
                {
                    Detected(true); //player spotted!
                    return;
                }
            }
        }

         //calculates vector to the player
        directionToPlayer = playerPos - transform.position;
        detectionAngle = Vector3.Angle(directionToPlayer, transform.forward);
          //if the player is within the enemy's cone of vision
        if(detectionAngle < angleOfView)
        {
            //if player is within the enemy's line of sight
            if(Vector3.Distance(playerPos, transform.position) < playerDistance)
            {
                  //if there are no obstacles between player and enemy
                if (Physics.Raycast(headRayPosition, (playerPos - transform.position), out headHit, playerDistance, layerMask))
                {
                    if(headHit.collider.tag == "Player") 
                    {
                        Detected(true); //player spotted!
                    }
                }
            }
        }
    }

    /**************************************************************************
   Function: RevealWeakPoint

Description: This function checks to see if the player is within range to
             reveal the enemy's weak point. If they are, the weak point is
             revealed. Otherwise, it is hidden.

      Input: none

     Output: none
    **************************************************************************/
    private void RevealWeakPoint()
    {
        //TODO: when weakness 2 and possibly 3 are implemented, check for them first
        //in an if else statement. They'll reveal weaknesses of tougher monsters in
        //addition to weaker ones.

        if(upgradeManager.GetWeakness2UpgradeEquippedStatus())
        {
              //checks if the player is within weakness2's current range value
            if (Vector3.Distance(transform.position, firstPersonController.transform.position) <= upgradeManager.GetWeakness2Range())
            {
                weaknessType1.gameObject.SetActive(true);
            }
            else
            {
                weaknessType1.gameObject.SetActive(false);
            }
        }
        else if(upgradeManager.GetWeakness1UpgradeEquippedStatus())
        {
              //checks if the player is within weakness1's current range value
            if (Vector3.Distance(transform.position, firstPersonController.transform.position) <= upgradeManager.GetWeakness1Range())
            {
                weaknessType1.gameObject.SetActive(true);
            }
            else
            {
                weaknessType1.gameObject.SetActive(false);
            }
        }


    }

    /**************************************************************************
   Function: HideWeakPoint

Description: This function disables the enemy's weakpoint, preventing it from
             being detected by weapons.

      Input: none

     Output: none
    **************************************************************************/
    private void HideWeakPoint()
    {
        weaknessType1.gameObject.SetActive(false);
    }

    /**************************************************************************
   Function: LookAtOrigin

Description: This function calculates a vector towards the default location and
             converts it to a quaternion before using it to turn the enemy
             towards its default position.

      Input: none

     Output: none
    **************************************************************************/
    private void LookAtOrigin()
    {
          //normalizes vector from enemy position to origin
        newDirection = (defaultPosition - transform.position).normalized;
          //converts vector to quaternion
        rotationTowardsOrigin = Quaternion.LookRotation(newDirection);
          //rotates enemy towards origin
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationTowardsOrigin, Time.deltaTime * 3.0f);
    }

    /**************************************************************************
   Function: MoveToOrigin

Description: This function causes the enemy to navigate its way back to its
             spawn position.

      Input: none

     Output: none
    **************************************************************************/
    private void MoveToOrigin()
    {
        agent.SetDestination(defaultPosition);
    }

    /**************************************************************************
   Function: UpdateRaycasts

Description: This function updates the origion of the four raycasts used by
             the enemy.

      Input: none

     Output: none
    **************************************************************************/
    private void UpdateRaycasts()
    {
        headRayPosition = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        bodyRayPosition = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
    }

    /**************************************************************************
   Function: ResetMoveAroundTimer

Description: This function resets move around timer to its default length.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetMoveAroundTimer()
    {
        //moveAroundTimer = defaultMoveAroundTime;
    }

    /**************************************************************************
   Function: ResetTurnAroundTimer

Description: This function resets turn around timer to its default length.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetTurnAroundTimer()
    {
        //turnAroundTimer = defaultTurnAroundTime;
    }

    /**************************************************************************
   Function: SetDetectionTimer

Description: This function resets detection timer to its default length.

      Input: none

     Output: none
    **************************************************************************/
    private void SetDetectionTimer()
    {
        detectionTimer = defaultDetectionLength;
    }

    /**************************************************************************
   Function: DecrementCounter

Description: Given a reference to a float, this function decrements that float.

      Input: counter - a reference to a float to be decremented

     Output: none
    **************************************************************************/
    private void DecrementCounter(ref float counter)
    {
        counter -= Time.deltaTime;
    }

    /**************************************************************************
   Function: ResetWaitingPeriod

Description: This function sets the waiting period to a random value between
             a min and a max value.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetWaitingPeriod()
    {
        waitingPeriod = Random.Range(minWaitingPeriod, maxWaitingPeriod);
    }

    /**************************************************************************
   Function: ResetTurningPeriod

Description: This function sets turning period to a random value between a min 
             and a max value.

      Input: none

     Output: none
    **************************************************************************/
    private void ResetTurningPeriod()
    {
        turningPeriod = Random.Range(minAngleTime, maxAngleTime);
    }

    /**************************************************************************
   Function: GetNewMoveTime

Description: This function sets move time to a random value between a min and
             a max value.

      Input: none

     Output: none
    **************************************************************************/
    private void GetNewMoveTime()
    {
        moveTime = Random.Range(minMoveTime, maxMoveTime);
    }

    /**************************************************************************
   Function: GetNewAngle

Description: This function sets move angle to a random value between a min and
             a max value.

      Input: none

     Output: none
    **************************************************************************/
    private void GetNewAngle()
    {
        moveAngle = Random.Range(minAngle, maxAngle);
    }

    /**************************************************************************
   Function: SetRigidBody

Description: Given a reference to a rigidbody variable, this function retrieves
             the enemy's rigibody and stores it in the given variable.

      Input: rigidbody - a reference to a Rigidbody variable

     Output: none
    **************************************************************************/
    private void SetRigidBody(ref Rigidbody rigidbody)
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    /**************************************************************************
   Function: SetMeshRenderer

Description: Given a reference to a meshRenderer variable, this function
             retrieves the enemy's meshRenderer and stores it in the given
             variable.

      Input: meshRenderer - a reference to a MeshRenderer variable

     Output: none
    **************************************************************************/
    private void SetMeshRenderer(ref MeshRenderer meshRenderer)
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    /**************************************************************************
   Function: Turn

Description: Given a float, this function sets a new angle and applies it to
             the enemy's transform.

      Input: angle - float used to set the angle

     Output: none
    **************************************************************************/
    private void Turn(float angle)
    {
          //retrieves the enemy's current angle as a vector3
        newDirection = transform.eulerAngles;
        newDirection.y = angle; //assign angle to the y coordinate
          //applies new rotation to the enemy's transform
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newDirection), Time.deltaTime * 3.0f);
    }

    /**************************************************************************
   Function: TurnLeft

Description: This function sets a new angle and applies it to the enemy's
             transform, making it turn left.

      Input: none

     Output: none
    **************************************************************************/
    private void TurnLeft()
    {
          //retrieves the enemy's current angle as a vector3
        newDirection = transform.eulerAngles;
        newDirection.y -= 50.0f; //reduces y angle
          //applies new rotation to the enemy's transform
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newDirection), Time.deltaTime * 3.0f);
    }

    /**************************************************************************
   Function: TurnRight

Description: This function sets a new angle and applies it to the enemy's
             transform, making it turn right.

      Input: none

     Output: none
    **************************************************************************/
    private void TurnRight()
    {
          //retrieves the enemy's current angle as a vector3
        newDirection = transform.eulerAngles;
        newDirection.y += 50.0f; //increases y angle
          //applies new rotation to the enemy's transform
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newDirection), Time.deltaTime * 3.0f);
    }

    /**************************************************************************
   Function: Move

Description: Given a float, this function uses the float as a speed to make
             the enemy move foward.

      Input: speed - float used to determine enemy's movement speed

     Output: none
    **************************************************************************/
    private void Move(float speed = 3.0f)
    {
          //NOTE: MovePosition should be called in fixed update to work properly
        rRigidbody.MovePosition(transform.position + transform.forward * (Time.deltaTime * speed));
    }

    /**************************************************************************
   Function: ChasePlayer

Description: This function makes the enemy look at the player and move towards
             them.

      Input: none

     Output: none
    **************************************************************************/
    private void ChasePlayer()
    {
        LookAtPlayer();
        
        if(Vector3.Distance(transform.position, playerPosition) > 2.0f)
        {
            agent.SetDestination(playerPosition);
        }
        else
        {
            agent.ResetPath(); //stops the enemy from moving
        }
    }

    /**************************************************************************
   Function: LookAtPlayer

Description: This function makes the enemy look at the player's last known
             position.

      Input: none

     Output: none
    **************************************************************************/
    private void LookAtPlayer()
    {
        transform.LookAt(playerPosition); //look at the player's lost known position
    }

    /**************************************************************************
   Function: FarFromOrigin

Description: This function calculates the distance from the enemy to its
             default position and returns a bool if the enemy is too far from
             that position.

      Input: none

     Output: Returns true if the enemy is too far from its default position,
             otherwise, returns false.
    **************************************************************************/
    private bool FarFromOrigin()
    {
          //if the enemy wanders far enough from its default position
        return (Vector3.Distance(defaultPosition, transform.position) > 8.0f);
    }

    /**************************************************************************
   Function: Detected

Description: Given a bool, this function changes the enemy's behavior if
             it detects the player.

      Input: seePlayer - bool used to change enemy's behavior

     Output: none
    **************************************************************************/
    private void Detected(bool seePlayer)
    {
        detected = seePlayer; 

        if(detected) //if the enemy is detected
        {
            SetDetectionTimer();
            meshRenderer.material = aggroMaterial; //change to aggro color
            playerPosition = firstPersonController.GetPlayerPosition(); //store player's position
        }
        else
        {
            meshRenderer.material = idleMaterial; //return to idle color
        }

        if(!detected) //if the enemy lost sight of the player
        {
            awayFromOrigin = true; //enemy must now return to its default position
        }
    }

    /**************************************************************************
   Function: SearchForPlayer

Description: This function makes the enemy turn quickly and at shorter
             intervals than when the enemy does while in the idle state.

      Input: none

     Output: none
    **************************************************************************/
    private void SearchForPlayer()
    {
        if(waitingPeriod > 0.0f) //if enemy is still doing nothing
        {
            DecrementCounter(ref waitingPeriod);
        }
        else
        {
            if (turningPeriod > 0.0f) //if enemy is still turning
            {
                  //retrieves the enemy's current angle as a vector3
                newDirection = transform.eulerAngles;
                newDirection.y = moveAngle; //assign angle to the y coordinate
                                            //applies new rotation to the enemy's transform
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newDirection), Time.deltaTime * 6.0f);
                DecrementCounter(ref turningPeriod);
            }
            else
            {
                GetNewAngle(); //calculate a new angle
                ResetWaitingPeriod(); //reset time until acting again
                ResetTurningPeriod(); //reset length of turning
                  //lowers values because enemy is aggressive
                turningPeriod = (turningPeriod / 2.0f);
                waitingPeriod = (waitingPeriod / 5.0f);
            }
        }
    }

    /**************************************************************************
   Function: CheckForDamage

Description: This function checks if the enemy is alive and hasn't taken
             damage. If both of those criteria are met, damage is applied and
             the player is detected if not previously detected.

      Input: none

     Output: none
    **************************************************************************/
    private void CheckForDamage()
    {
          //if the enemy is alive and didn't already take damage from this attack
        if(!IsDead()) 
        {
            TakeDamage(weaponName); //damage the enemy
            tookDamage = true; //don't take damage again during this attack

            if(!detected) //check if the player wasn't detected
            {
                Detected(true);
            }
        }
    }

    /**************************************************************************
   Function: ResetTookDamage

Description: This function sets the tookDamage bool to false..

      Input: none

     Output: none
    **************************************************************************/
    private void ResetTookDamage()
    {
        tookDamage = false;
    }

    /**************************************************************************
   Function: TakeDamage

Description: This function subtracts the damage of the weapon that hit the enemy 
             from the enemy's current health.

      Input: weaponName - name of weapon to take damage from

     Output: none
    **************************************************************************/
    private void TakeDamage(string weaponName)
    {
        switch(weaponName) //checks which weapon touched the enemy
        {
            case "Pipe":
                SetHealth(-pipe.GetPipeDamage());
                Detected(true);
                break;
            case "Pickaxe":
                SetHealth(-pickaxe.GetPickaxeDamage());
                Detected(true);
                break;
            case "Pistol":
                SetHealth(-pistol.GetPistolDamage());
                Detected(true);
                break;
            case "Shotgun":
                SetHealth(-shotgun.GetShotgunDamage());
                Detected(true);
                break;
            case "Rifle":
                SetHealth(-rifle.GetRifleDamage());
                Detected(true);
                break;
            default:
                //Debug.Log("Invalid Weapon Name");
                break;
 
        }
    }

    /**************************************************************************
   Function: SetHealth

Description: Given a float, this function adds it to the enemy's current 
             health. If the enemy has no health left, remove constraints so
             it can fall over.

      Input: health - float added to the enemy's current health

     Output: none
    **************************************************************************/
    public void SetHealth(float health)
    {
        currentHealth += health;

        if (currentHealth <= 0.0f)
        {
              //unfreezes rotation so enemy falls over
            rRigidbody.constraints = RigidbodyConstraints.None;
        }
    }

    /**************************************************************************
   Function: IsDead

Description: This function checks the enemy's current health and returns a bool
             based on whether or not it has 0 health or less.

      Input: none

     Output: Returns true if the enemy is dead, otherwise, returns false.
    **************************************************************************/
    private bool IsDead()
    {
        return (currentHealth <= 0.0f);
    }

    /**************************************************************************
   Function: Die

Description: This function checks if the death timer is above zero. If so, the
             enemy rotates to look like its falling over. After the timer is
             zero or less, the enemy is destroyed.

      Input: none

     Output: none
    **************************************************************************/
    private void Die()
    {
        if(deathTimer > 0.0f)
        {
            DecrementCounter(ref deathTimer);
              //retrieves the enemy's current angle as a vector3
            newDirection = transform.eulerAngles;
            newDirection.x -= 10.0f; //assign angle to the y coordinate
              //applies new rotation to the enemy's transform
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newDirection), Time.deltaTime * 4.0f);
        }
        else
        {
            Destroy(gameObject);
        }

        if(!lootSpawned)
        {
            SpawnLoot();

            lootSpawned = true;
        }
    }

    /**************************************************************************
  Function: ResetDeathTimer

Description: This function sets the death timer back to its default value.

     Input: none

    Output: none
   **************************************************************************/
    private void ResetDeathTimer()
    {
        deathTimer = defaultDeathTimer;
    }

    /**************************************************************************
   Function: OnTriggerStay

Description: This function is called when another object colliders and remains
             collided with the enemy. If it's a pipe and the pipe is swinging,
             damage check is called.

      Input: other - collider of object that collides with the enemy

     Output: none
    **************************************************************************/
    private void OnTriggerStay(Collider other)
    {
        switch(other.name)
        {
            case "Pipe":
                if (pipe.GetSwingingStatus() && !tookDamage)
                {
                    weaponName = other.name;
                    CheckForDamage();
                }
                break;
            case "Pickaxe":
                if (pickaxe.GetSwingingStatus() && !tookDamage)
                {
                    weaponName = other.name;
                    CheckForDamage();
                }
                break;
            default:
                weaponName = "";
                break;
        }
    }

    /**************************************************************************
   Function: OnTriggerExit

Description: This function is called when the other object stops colliding with
             the enemy. If the object is a melee weapon and the player has
             finished swinging, reset the took damage bool.

      Input: other - collider of other object that collides with the enemy

     Output: none
    **************************************************************************/
    private void OnTriggerExit(Collider other)
    {
        switch(other.name)
        {
            case "Pipe":
                if(!pipe.GetSwingingStatus())
                {
                    ResetTookDamage();
                }
                break;
            case "Pickaxe":
                if(!pickaxe.GetSwingingStatus())
                {
                    ResetTookDamage();
                }
                break;
        }
    }

    /**************************************************************************
   Function: TakeBulletDamage

Description: Given a string, this function passes that string to the TakeDamage
             function.

      Input: weaponName - string of the gun's name

     Output: none
    **************************************************************************/
    private void TakeBulletDamage(string weaponName)
    {
        TakeDamage(weaponName);
    }

    /**************************************************************************
   Function: SpawnLoot

Description: This function determines how many items to spawn in addition to a
             single essence. Then the type of item is spawned based on a random
             number.

      Input: none

     Output: none
    **************************************************************************/
    private void SpawnLoot()
    {
          //determines how many items to spawn
        int numberOfSpawnsChance = Random.Range(1, 100);
        int numberOfSpawns = 0; //number of items to spawn besides first essence

        if(numberOfSpawnsChance >= spawnTwoItems) //spawn 2 items besides the first essence
        {
            numberOfSpawns = 2; 
        }
        else if(numberOfSpawnsChance >= spawnOneItem) //spawn 1 item besides the first essence
        {
            numberOfSpawns = 1;
        }

        for (int i = 0; i < numberOfSpawns; i++)
        {
              //determines what type of item to spawn
            int itemChance = Random.Range(1, 100);

            if(itemChance >= spawnFirstAidKitChance)
            {
                SpawnFirstAidKit(i + 1); 
            }
            else if(itemChance >= spawnAmmoChance)
            {
                SpawnAmmo(i + 1);
            }
            else
            {
                SpawnEssence(i + 1);
            }         
        }

        SpawnEssence(); //enemy will always spawn at least 1 essence
    }

    /**************************************************************************
   Function: SpawnFirstAidKit

Description: Given an integer, this function randomly generates a number within
             the specified range then checks its value and spawns the 
             appropriate first aid kit. The integer is used to ensure multiple
             spawns aren't right on top of each other.

      Input: positionOffset - integer used to move second or third spawn away
                              from previous spawn

     Output: none
    **************************************************************************/
    private void SpawnFirstAidKit(int positionOffset = 0)
    {
          //determines which type of first aid kit to spawn
        int firstAidKitChance = Random.Range(1, 100);
        Vector3 spawnPosition = new Vector3(transform.position.x + (0.3f * positionOffset),
                                            transform.position.y,
                                            transform.position.z);

        if (firstAidKitChance >= spawnLargeAidKitChance)
        {
            Instantiate(largeFirstAidKit, spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(smallFirstAidKit, spawnPosition, Quaternion.identity);
        }
    }

    /**************************************************************************
   Function: SpawnAmmo

Description: Given an integer, this function randomly generates a number within
             the specified range then checks its value and spawns the 
             appropriate ammo. The integer is used to ensure multiple
             spawns aren't right on top of each other. If the player doesn't
             have the specific gun, the next earliest attainable gun's ammo
             is spawned. If the player doesn't have that gun or any gun, then
             essence is spawned instead.

      Input: positionOffset - integer used to ensure multiple items aren't
                              spawned on top of each other

     Output: none
    **************************************************************************/
    private void SpawnAmmo(int positionOffset = 0)
    {
          //determines which type of first aid kit to spawn
        int ammoChance = Random.Range(1, 100);
        Vector3 spawnPosition = new Vector3(transform.position.x,
                                            transform.position.y,
                                            transform.position.z + (0.3f * positionOffset));

        if(ammoChance >= spawnRifleAmmoChance)
        {
            if(firstPersonController.GetRifleOwnedStatus())
            {
                Instantiate(rifleAmmo, spawnPosition, Quaternion.identity);
            }
            else if(firstPersonController.GetShotgunOwnedStatus())
            {
                Instantiate(shotgunAmmo, spawnPosition, Quaternion.identity);
            }
            else if(firstPersonController.GetPistolOwnedStatus())
            {
                Instantiate(pistolAmmo, spawnPosition, Quaternion.identity);
            }
            else
            {
                SpawnEssence(positionOffset); //player doesn't have a gun yet but got lucky
            }
        }
        else if(ammoChance >= spawnShotgunAmmoChance)
        {
            if (firstPersonController.GetShotgunOwnedStatus())
            {
                Instantiate(shotgunAmmo, spawnPosition, Quaternion.identity);
            }
            else if (firstPersonController.GetPistolOwnedStatus())
            {
                Instantiate(pistolAmmo, spawnPosition, Quaternion.identity);
            }
            else
            {
                SpawnEssence(positionOffset); //player doesn't have a gun yet but got lucky
            }
        }
        else
        {
            if(firstPersonController.GetPistolOwnedStatus())
            {
                Instantiate(pistolAmmo, spawnPosition, Quaternion.identity);
            }
            else
            {
                SpawnEssence(positionOffset); //player doesn't have a gun yet but got lucky
            }
        }
    }

    /**************************************************************************
   Function: SpawnEssence

Description: Given an integer, this function randomly generates a number within
             the specified range then checks its value and spawns the 
             appropriate essence. The integer is used to ensure multiple
             spawns aren't right on top of each other

      Input: positionOffset - integer used to ensure multiple spawns aren't on
                              top of each other

     Output: none
    **************************************************************************/
    private void SpawnEssence(int positionOffset = 0)
    {
          //determines which type of essence to spawn from Type1 enemies
        int essenceSpawnChance = Random.Range(1, 500);
        Vector3 spawnPosition = new Vector3(transform.position.x,
                                            transform.position.y,
                                            transform.position.z + (0.3f * positionOffset));

        if (essenceSpawnChance >= redEssenceLSpawnChance)
        {
            Instantiate(redEssenceL, spawnPosition, Quaternion.identity);
        }
        else if(essenceSpawnChance >= yellowEssenceLSpawnChance)
        {
            Instantiate(yellowEssenceL, spawnPosition, Quaternion.identity);
        }
        else if(essenceSpawnChance >= blueEssenceLSpawnChance)
        {
            Instantiate(blueEssenceL, spawnPosition, Quaternion.identity);
        }
        else if(essenceSpawnChance >= redEssenceMSpawnChance)
        {
            Instantiate(redEssenceM, spawnPosition, Quaternion.identity);
        }
        else if (essenceSpawnChance >= yellowEssenceMSpawnChance)
        {
            Instantiate(yellowEssenceM, spawnPosition, Quaternion.identity);
        }
        else if(essenceSpawnChance >= blueEssenceMSpawnChance)
        {
            Instantiate(blueEssenceM, spawnPosition, Quaternion.identity);
        }
        else if(essenceSpawnChance >= redEssenceSSpawnChance)
        {
            Instantiate(redEssenceS, spawnPosition, Quaternion.identity);
        }
        else if(essenceSpawnChance >= yellowEssenceSSpawnChance)
        {
            Instantiate(yellowEssenceS, spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(blueEssenceS, spawnPosition, Quaternion.identity);
        }
    }

    /**************************************************************************
   Function: Kill

Description: This function subtract's the enemy's max health from its current
             health, killing it immediately.

      Input: none

     Output: none
    **************************************************************************/
    private void Kill()
    {
        //NOTE: This function is currently only called from hitting a weakness,
        //so this is where weakness exp will be added for now.
        if(currentHealth > 0)
        {
            if(upgradeManager.GetWeakness1UpgradeEquippedStatus())
            {
                upgradeManager.AddToUpgradeExperience("Reveal Weakness 1", weaknessExperience);
            }

            if(upgradeManager.GetWeakness2UpgradeEquippedStatus())
            {
                upgradeManager.AddToUpgradeExperience("Reveal Weakness 2", weaknessExperience);
            }
        }

        agent.enabled = false;
        rRigidbody.isKinematic = false;

        SetHealth(-defaultMaxHealth);

        if(!lootSpawned)
        {
            SpawnLoot(); //spawn some loot for the player

            lootSpawned = true;
        }
    }
}
