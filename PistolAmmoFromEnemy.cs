using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmoFromEnemy : MonoBehaviour
{
    //When the pistol ammo spawns from killing a monster, the yellow ring is enabled and the amount of ammo this item provides
    //is determined.    
    [SerializeField] private GameObject yellowRing;

    public ItemManager itemManager;
    public GameObject managerController;
    public int PistolAmmoAmount;
    public bool allAmmoTaken;

	void Start ()
    {
        allAmmoTaken = false;
        yellowRing.SetActive(true);
        //When done testing, switch this back to 3, 6.
        PistolAmmoAmount = Random.Range(20, 30);
	}

	void Update ()
    {
		if (allAmmoTaken == true)
        {
            DestroyAmmo();
        }
	}

    //Destroys the pistol ammo game object this script is attached to.
    public void DestroyAmmo()
    {
        Destroy(gameObject);
    }
}
