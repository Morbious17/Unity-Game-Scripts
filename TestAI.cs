using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private EnemyInfo enemyInfo;
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private ItemInfo itemInfo;
    [SerializeField] private ItemManager itemManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject infoController;
    [SerializeField] private GameObject roundCutS;
    [SerializeField] private GameObject roundCutM;
    [SerializeField] private GameObject roundCutL;
    [SerializeField] private GameObject pistolAmmo;
    [SerializeField] private GameObject shotgunAmmo;
    [SerializeField] private GameObject rifleAmmo;
    
    public float WalkSpeed = 2;
    public float RunSpeed = 4;
    public float TurnDegrees;
    public Quaternion EndTurnPosition;
    public float WhenToMove;
    public float WhenToTurn;
    public bool TimeToMove;
    public bool TimeToTurn;
    public float MoveSpeed;
    public float MoveDistance;
    public float OutOfSight;
    public float WhenToDespawn;
    public bool IdleState;
    public bool EnragedState;
    public bool Attacked;
    public bool TimeToDie;
    public bool IsDead;
    public string Tag;
    public RaycastHit hit;
    public Vector3 Upright = new Vector3(0, 0, 0);
    public Vector3 LyingDown = new Vector3(-90, 0, 0);
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public Vector3 LastSighted;
    //This will be used in the smooth damp function the enemy uses to pursue the player.
    public Vector3 SmoothVelocity;
    //This will be the time it takes for the enemy to reach the player.
    public float SmoothTime;
    public Quaternion StartRotation;
    public Animator EnemyType1Anim;
    public float DistanceToPlayer;
    public Vector3 DeathPosition;
    public CapsuleCollider capsuleCollider;

	void Start ()
    {
        material.SetColor("_Color", Color.yellow);
        MoveSpeed = WalkSpeed;
        transform.eulerAngles = Upright;
        WhenToTurn = Random.Range(2, 5);
        StartPosition = transform.position;
        SmoothVelocity = Vector3.zero;
        SmoothTime = 3f;
        StartRotation = transform.rotation;
        IdleState = true;
        EnemyType1Anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

	void Update ()
    {
        Debug.Log(enemyInfo.Enemy1Health);

        //Casts a ray to detect tags 10 units in front of the object this script is attached to.
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            tag = hit.collider.tag;
        }

        //Changes states if player tag is detected
        if (tag == "Player")
        {
            OutOfSight = 10f;
            EnragedState = true;
            TimeToMove = false;
            TimeToTurn = false;
            IdleState = false;
            EndPosition = player.transform.position;
            MoveSpeed = RunSpeed;
            tag = "Untagged"; 
        }
    
        //Returns move speed to walk speed and color to normal color when no longer enraged. Then Sets and decrement certain
        //variables to know when to turn and when to move.
        if (IdleState == true && IsDead == false)
        {
            if (WhenToTurn > 0)
            {
                WhenToTurn -= Time.deltaTime;
            }

            if (WhenToTurn < 0)
            {
                Turn();
                WhenToMove = Random.Range(1, 3);
                WhenToTurn = 0;
            }

            //Turns the enemy around before it begins to move.
            if (TimeToTurn)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, EndTurnPosition, Time.deltaTime * 6f);

                //Decrement the variable before calling the move function.
                if (WhenToMove > 0)
                {
                    WhenToMove -= Time.deltaTime;
                }
                //Determines a move distance before setting when to move to 0.
                if (WhenToMove < 0)
                {
                    MoveDistance = Random.Range(3, 8);
                    MoveForward();
                    WhenToMove = 0;
                }
            }
        
            //Moves until the distance has been covered
            if (TimeToMove)
            {
                MoveDistance -= Time.deltaTime;
                transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);

                if (TimeToDie)
                {
                    MoveDistance = 0;
                }

                //Determines the time it takes to move again
                if (MoveDistance < 0)
                {
                    TimeToMove = false;
                    WhenToTurn = Random.Range(2, 5);
                    MoveDistance = 0;
                }
            }
        }

        //Changes move speed, color, and starts a cooldown when enraged
        if (EnragedState == true && IsDead == false)
        {
            material.SetColor("_Color", Color.red);

            //This should cause the enemy to look at the player while enraged.
            transform.LookAt(player.transform);

            //This sets the distance value between player and enemy to this variable then displays it to console.
            DistanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
       
            //This makes the enemy move towards the player's position. Speed changes based on distance from player. Maybe a
            //different statement should be chosen.
            if (DistanceToPlayer > 1.5f)
            {
                if (player.transform.position.y > 0.98f)
                {
                    Vector3 targetToRunTowards = new Vector3(player.transform.position.x, 0.98f, player.transform.position.z);
                    transform.position = Vector3.SmoothDamp(transform.position, targetToRunTowards, ref SmoothVelocity, SmoothTime);
                }
                else
                {
                    transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref SmoothVelocity, SmoothTime);
                }
            }
            else
            {
                transform.position = transform.position;
            }


            //This makes the enemy move towards the player's position. Maybe change the Time delta time to some over variable.
            //transform.position = Vector3.Lerp(transform.position, EndPosition, Time.deltaTime);       

            //This only works if the enemy never has to go up or down stairs or other terrain.
            //Vector3 moveForward = transform.forward;
            //moveForward.y = 0;
            //transform.position += moveForward.normalized * MoveSpeed * Time.deltaTime;

            //Reduces the cooldown when the raycast doesn't detect the player.
            if (OutOfSight > 0)
            {
                OutOfSight -= Time.deltaTime;
            }

            //Makes sure the variable is set to 0 after changing other variables.
            if (OutOfSight < 0)
            {
                EnragedState = false;
                IdleState = true;
                MoveSpeed = WalkSpeed;
                material.SetColor("_Color", Color.yellow);
                WhenToTurn = Random.Range(2, 5);
                transform.rotation = Quaternion.Lerp(transform.rotation, StartRotation, 1);
                OutOfSight = 0;
            }
        }

        //This keeps the bool true to the flinch animation plays continuously. fix it!!!
        if (Attacked)
        {
            EnemyType1Anim.SetBool("Attacked", true);
            Attacked = false;
        }

        if (TimeToDie)
        {
            //Makes enemy rotate to look like it is lying down and go lower on the y axis to get closer to the ground
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0.5f, transform.position.z),
                4 * Time.deltaTime);
            Upright = Vector3.Lerp(Upright, LyingDown, 4 * Time.deltaTime);
            transform.eulerAngles = Upright;
            IsDead = true;
        }

        if (WhenToDespawn > 0)
        {
            WhenToDespawn -= Time.deltaTime;
        }

        if (WhenToDespawn < 0)
        {
            Destroy(gameObject);
            WhenToDespawn = 0;
        }

        if (enemyInfo.Enemy1Health <= 0 && IsDead == false)
        {
            if (enemyInfo.Enemy1Health < 0)
            {
                EnemyType1Anim.SetBool("Alive", false);
                Die();
                capsuleCollider.enabled = false;
                SpawnLoot();
                enemyInfo.Enemy1Health = 0;
            }
            else
            {
                EnemyType1Anim.SetBool("Alive", false);
                Die();
                capsuleCollider.enabled = false;
                SpawnLoot();
            }
        }
    }

    //Sets turning values and turn bool to true.
    private void Turn()
    {
        //Sets value the enemy will turn.
        TurnDegrees = Random.Range(15, 345);

        //Sets the end position the enemy will smoothly turn to face.
        EndTurnPosition = Quaternion.Euler(transform.rotation.x, TurnDegrees, transform.rotation.z);
        WhenToTurn = 4f;
        TimeToTurn = true;
    }

    //Sets time to move to true so enemy can move forward.
    private void MoveForward()
    {
        TimeToMove = true;
    }

    private void Die()
    {
        EnragedState = false;
        IdleState = false;
        TimeToDie = true;
        //This was originally 5f. Change it back when done testing.
        WhenToDespawn = 5f;
        DeathPosition = transform.position;     
    }

    public void Flinched()
    {
        EnemyType1Anim.SetBool("Attacked", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OutOfSight = 10f;
            EnragedState = true;
            TimeToMove = false;
            TimeToTurn = false;
            IdleState = false;
            EndPosition = player.transform.position;
            MoveSpeed = RunSpeed;
        }
    }

    private void SpawnLoot()
    {
        int gemChance = Random.Range(1, 100);
        //TODO: Change this back to 1, 100 when finished testing.
        int ammoChance = Random.Range(99, 100);      
        int ammoType = Random.Range(1, 100);

        if (gemChance > 80)
        {            
            //This sets the rotation the gem will have upon spawning.
            Quaternion rotation = Quaternion.identity;
            //This takes death position and changes it so the gem spawns a bit above ground and falls down.
            Vector3 gemSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z);
            Instantiate(roundCutL, gemSpawnPosition, rotation);
        }
        else if (gemChance > 50)
        {
            Quaternion rotation = Quaternion.identity;
            Vector3 gemSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z);
            Instantiate(roundCutM, gemSpawnPosition, rotation);
        }
        else
        {
            Quaternion rotation = Quaternion.identity;
            Vector3 gemSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z);
            Instantiate(roundCutS, gemSpawnPosition, rotation);
        }

        //TODO: Change this so that the prefabs pistol, shotgun, or rifle ammo is instantiated. Then create and attach
        //scripts to those prefabs to determine the amount of ammo they give.
        //TODO: Edit these if statements so that when all weapons are possessed, the other conditional branches are not also called.
        //This could be done by adding the possess bools in them and say they must be set to false.
        if (weaponInfo.PossessPistol == true && weaponInfo.PossessShotgun == true && weaponInfo.PossessRifle == true && ammoChance >= 66)
        {
            if (ammoType > 80)
            {
                Quaternion rotation = Quaternion.identity;
                Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
                Instantiate(rifleAmmo, ammoSpawnPosition, rotation);
            }
            else if (ammoType > 50)
            {
                Quaternion rotation = Quaternion.identity;
                Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
                Instantiate(shotgunAmmo, ammoSpawnPosition, rotation);
            }
            else
            {
                Quaternion rotation = Quaternion.identity;
                Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
                Instantiate(pistolAmmo, ammoSpawnPosition, rotation);
            }
        }
        else if (weaponInfo.PossessPistol == true && weaponInfo.PossessShotgun == true && ammoChance >= 66)
        {
            if (ammoType > 66)
            {
                Quaternion rotation = Quaternion.identity;
                Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
                Instantiate(shotgunAmmo, ammoSpawnPosition, rotation);
            }
            else
            {
                Quaternion rotation = Quaternion.identity;
                Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
                Instantiate(pistolAmmo, ammoSpawnPosition, rotation);
            }
        }
        else if (weaponInfo.PossessPistol == true && weaponInfo.PossessRifle == true && ammoChance >= 66)
        {
            if (ammoType > 66)
            {
                Quaternion rotation = Quaternion.identity;
                Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
                Instantiate(rifleAmmo, ammoSpawnPosition, rotation);
            }
            else
            {
                Quaternion rotation = Quaternion.identity;
                Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
                Instantiate(pistolAmmo, ammoSpawnPosition, rotation);
            }
        }
        else if (weaponInfo.PossessPistol == true && ammoChance >= 66)
        {
            Quaternion rotation = Quaternion.identity;
            Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
            Instantiate(pistolAmmo, ammoSpawnPosition, rotation);
        }

        if (weaponInfo.PossessShotgun == true && weaponInfo.PossessRifle == true && ammoChance >= 66)
        {
            if (ammoType > 66)
            {
                Quaternion rotation = Quaternion.identity;
                Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
                Instantiate(rifleAmmo, ammoSpawnPosition, rotation);
            }
            else
            {
                Quaternion rotation = Quaternion.identity;
                Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
                Instantiate(shotgunAmmo, ammoSpawnPosition, rotation);
            }
        }
        else if (weaponInfo.PossessShotgun == true && ammoChance >= 66)
        {
            Quaternion rotation = Quaternion.identity;
            Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
            Instantiate(shotgunAmmo, ammoSpawnPosition, rotation);
        }

        if (weaponInfo.PossessRifle == true && ammoChance >= 66)
        {
            Quaternion rotation = Quaternion.identity;
            Vector3 ammoSpawnPosition = new Vector3(DeathPosition.x, DeathPosition.y + 0.8f, DeathPosition.z + 0.5f);
            Instantiate(rifleAmmo, ammoSpawnPosition, rotation);
        }  
    }
}
