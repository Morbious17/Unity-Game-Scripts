using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MapsManager : MonoBehaviour
{
    //This script will manage all maps in the game. The map revealed will depend on which map button
    //and floor button being pressed. And the parts of the map revealed will depend on when the
    //player collides with invisible triggers.

    [SerializeField] private GameObject PlayerPanel;

    [SerializeField] private GameObject FPSController;

    [SerializeField] private RawImage TestMap1;
    [SerializeField] private RawImage TestMap2;
    [SerializeField] private GameObject TestRoom1APanel;
    [SerializeField] private GameObject TestRoom2APanel;
    [SerializeField] private GameObject TestRoom3APanel;
    [SerializeField] private GameObject TestRoom4APanel;
    [SerializeField] private GameObject TestHallway1APanel;

    private string CollideObjectName;
    private Vector3 PlayerLocation;

    void Start()
    {

    }        

	void Update ()
    {
        CollideObjectName = FirstPersonController.collideObjectName;        
        PlayerLocation = FPSController.transform.position;
        PlayerPanel.transform.localPosition = new Vector3(PlayerLocation.x * 10, PlayerLocation.z * 10, 0);
        //Debug.Log(PlayerPanel.transform.position);

        //Rotates the player icon on top of a map so that it turns when the FPSController turns.
        PlayerPanel.transform.rotation = new Quaternion(PlayerPanel.transform.rotation.x, PlayerPanel.transform.rotation.y, FPSController.transform.rotation.y, PlayerPanel.transform.rotation.w);

        //If I try to use the player's coordinates to move a panel on the maps to represent the player, I'll need to
        //use the player's z coordinate to represent the panel's y coordinate.
        //This code does move the player panel, but the difference is so small, it's barely noticeable. All maps need to
        //have the same aspect ratio OR there should be specific code for each map that determine at what rate
        //the panel moves.


        switch (CollideObjectName)
        {
            case "TestRoom1A":
                PlayerPanel.transform.parent = TestMap2.transform;
                RevealMap2();               
                TestRoom1APanel.SetActive(false);
                break;

            case "TestRoom2A":
                RevealMap1();
                TestRoom2APanel.SetActive(false);
                break;

            case "TestRoom3A":
                TestRoom3APanel.SetActive(false);
                break;

            case "TestRoom4A":
                TestRoom4APanel.SetActive(false);
                break;

            case "TestHallway1A":
                TestHallway1APanel.SetActive(false);
                break;
        }
    }

    private void RevealMap1()
    {
        TestMap1.enabled = true;
        TestMap2.enabled = false;
    }

    private void RevealMap2()
    {
        TestMap1.enabled = false;
        TestMap2.enabled = true;
    }
}
