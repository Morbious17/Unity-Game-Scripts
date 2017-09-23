using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolStationFloorButtons : MonoBehaviour
{
    //This script makes the clicked button non-interactable while making all other
    //floor buttons interactable. 

    [SerializeField] private Button PoliceStationF1Button;
    [SerializeField] private Button PoliceStationF2Button;

    //TODO: When maps are created, add functionality so that the floor of each map is loaded when these functions run.

    void Start ()
    {
		
	}

	void Update ()
    {
		
	}

    public void PressF1Button()
    {
        PoliceStationF1Button.interactable = false;

        PoliceStationF2Button.interactable = true;
        PoliceStationF2Button.OnDeselect(null);
    }

    public void PressF2Button()
    {
        PoliceStationF1Button.interactable = true;
        PoliceStationF1Button.OnDeselect(null);

        PoliceStationF2Button.interactable = false;
    }
}
