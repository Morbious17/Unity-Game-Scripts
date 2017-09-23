using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HospitalFloorButtons : MonoBehaviour
{
    //This script makes the clicked button non-interactable while making all other
    //floor buttons interactable. 

    [SerializeField] private Button HospitalB2Button;
    [SerializeField] private Button HospitalB1Button;
    [SerializeField] private Button HospitalF1Button;
    [SerializeField] private Button HospitalF2Button;
    [SerializeField] private Button HospitalF3Button;

    //TODO: When maps are created, add functionality so that the floor of each map is loaded when these functions run.

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void PressB2Button()
    {
        HospitalB2Button.interactable = false;

        HospitalB1Button.interactable = true;
        HospitalB1Button.OnDeselect(null);

        HospitalF1Button.interactable = true;
        HospitalF1Button.OnDeselect(null);

        HospitalF2Button.interactable = true;
        HospitalF2Button.OnDeselect(null);

        HospitalF3Button.interactable = true;
        HospitalF3Button.OnDeselect(null);
    }

    public void PressB1Button()
    {
        HospitalB2Button.interactable = true;
        HospitalB2Button.OnDeselect(null);

        HospitalB1Button.interactable = false;

        HospitalF1Button.interactable = true;
        HospitalF1Button.OnDeselect(null);

        HospitalF2Button.interactable = true;
        HospitalF2Button.OnDeselect(null);

        HospitalF3Button.interactable = true;
        HospitalF3Button.OnDeselect(null);
    }

    public void PressF1Button()
    {
        HospitalB2Button.interactable = true;
        HospitalB2Button.OnDeselect(null);

        HospitalB1Button.interactable = true;
        HospitalB1Button.OnDeselect(null);

        HospitalF1Button.interactable = false;

        HospitalF2Button.interactable = true;
        HospitalF2Button.OnDeselect(null);

        HospitalF3Button.interactable = true;
        HospitalF3Button.OnDeselect(null);
    }

    public void PressF2Button()
    {
        HospitalB2Button.interactable = true;
        HospitalB2Button.OnDeselect(null);

        HospitalB1Button.interactable = true;
        HospitalB1Button.OnDeselect(null);

        HospitalF1Button.interactable = true;
        HospitalF1Button.OnDeselect(null);

        HospitalF2Button.interactable = false;

        HospitalF3Button.interactable = true;
        HospitalF3Button.OnDeselect(null);
    }

    public void PressF3Button()
    {
        HospitalB2Button.interactable = true;
        HospitalB2Button.OnDeselect(null);

        HospitalB1Button.interactable = true;
        HospitalB1Button.OnDeselect(null);

        HospitalF1Button.interactable = true;
        HospitalF1Button.OnDeselect(null);

        HospitalF2Button.interactable = true;
        HospitalF2Button.OnDeselect(null);

        HospitalF3Button.interactable = false;
    }
}
