using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIType1 : MonoBehaviour
{
    [SerializeField] private EnemyInfo enemyInfo;

    private bool IdleState;
    private bool EnragedState;

    private RaycastHit hit1;
    private RaycastHit hit2;
    private Ray ray;

	void Start ()
    {
		
	}

	void Update ()
    {
        //This will be used by the enemy to detect the player
		if (Physics.Raycast(transform.position, transform.forward, out hit1, 10))
        {

        }

        //This will be used to stop moving if an obstacle is in front of the enemy.
        if (Physics.Raycast(transform.position, transform.forward, out hit2, 2))
        {

        }
	}

    private void WalkForward()
    {
        //Determines the walking distance when this function is called.
        float walkDistance = Random.Range(1, 10);
    }

    private void Turn()
    {
        //Determines how far the enemy will turn
        float turnDistance = Random.Range(5, 330);
        //TODO: Maybe add a conditional to see if the number is over 180 to turn in the opposite direction.
    }

    private void RunForward()
    {

    }

    private void Attack1()
    {

    }

    private void Attack2()
    {

    }

    private void Flinch()
    {

    }

    private void Die()
    {

    }
}
