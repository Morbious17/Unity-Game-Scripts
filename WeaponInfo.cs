using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private EnemyInfo enemyInfo;
    [SerializeField] private GameObject Pipe;
    [SerializeField] private GameObject Pickaxe;
    [SerializeField] private GameObject Pistol;
    [SerializeField] private GameObject Shotgun;
    [SerializeField] private GameObject Rifle;

    public bool PossessPistol;
    public bool PossessShotgun;
    public bool PossessRifle;

    //TODO: Add weapon stats and current/max magazine size.

    public float PipeAttackStamCost1 = 10;
    public float PipeAttackStamCost2 = 7;
    public float PipeAttackStamCost3 = 20;

	void Start ()
    {
        //This is for testing. Remove later.
        PossessPistol = true;
        PossessShotgun = false;
        PossessRifle = false;
	}

	void Update ()
    {

	}

    public void PipeAttack1()
    {
        playerInfo.currentStamina -= PipeAttackStamCost1;
    }

    public void PipeAttack2()
    {
        playerInfo.currentStamina -= PipeAttackStamCost2;
    }

    public void PipeAttack3()
    {
        playerInfo.currentStamina -= PipeAttackStamCost3;
    }
}

enum Weapon { Pipe, Pickaxe, Pistol, Shotgun, Rifle}
