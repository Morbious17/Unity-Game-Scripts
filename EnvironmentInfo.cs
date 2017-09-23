using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInfo : MonoBehaviour
{
    //This script will contain information about objects in the environment the player can
    //interact with.

    public string[] doorState = { "Open", "Closed", "Unlocked", "Locked", "Broken", "Barricaded" };
    public string[] chestState = { "Open", "Closed", "Unlocked", "Locked" };
    public string[] containerState = { "Empty", "Not Empty" };


	void Start ()
    {
		
	}

	void Update ()
    {
		
	}
}
