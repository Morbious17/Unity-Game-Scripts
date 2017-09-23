using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApartmentsFloorButtons : MonoBehaviour
{
    //This script makes the clicked button non-interactable while making all other
    //floor buttons interactable. 

    [SerializeField] private Button ApartmentsB1Button;
    [SerializeField] private Button ApartmentsF1Button;
    [SerializeField] private Button ApartmentsF2Button;
    [SerializeField] private Button ApartmentsF3Button;
    [SerializeField] private Button ApartmentsF4Button;

    //TODO: When maps are created, add functionality so that the floor of each map is loaded when these functions run.

    void Start ()
    {
		
	}

	void Update ()
    {
		
	}

    public void PressB1Button()
    {
        ApartmentsB1Button.interactable = false;

        ApartmentsF1Button.interactable = true;
        ApartmentsF1Button.OnDeselect(null);

        ApartmentsF2Button.interactable = true;
        ApartmentsF2Button.OnDeselect(null);

        ApartmentsF3Button.interactable = true;
        ApartmentsF3Button.OnDeselect(null);

        ApartmentsF4Button.interactable = true;
        ApartmentsF4Button.OnDeselect(null);
    }

    public void PressF1Button()
    {
        ApartmentsB1Button.interactable = true;
        ApartmentsB1Button.OnDeselect(null);

        ApartmentsF1Button.interactable = false;

        ApartmentsF2Button.interactable = true;
        ApartmentsF2Button.OnDeselect(null);

        ApartmentsF3Button.interactable = true;
        ApartmentsF3Button.OnDeselect(null);

        ApartmentsF4Button.interactable = true;
        ApartmentsF4Button.OnDeselect(null);
    }

    public void PressF2Button()
    {
        ApartmentsB1Button.interactable = true;
        ApartmentsB1Button.OnDeselect(null);

        ApartmentsF1Button.interactable = true;
        ApartmentsF1Button.OnDeselect(null);

        ApartmentsF2Button.interactable = false;

        ApartmentsF3Button.interactable = true;
        ApartmentsF3Button.OnDeselect(null);

        ApartmentsF4Button.interactable = true;
        ApartmentsF4Button.OnDeselect(null);
    }

    public void PressF3Button()
    {
        ApartmentsB1Button.interactable = true;
        ApartmentsB1Button.OnDeselect(null);

        ApartmentsF1Button.interactable = true;
        ApartmentsF1Button.OnDeselect(null);

        ApartmentsF2Button.interactable = true;
        ApartmentsF2Button.OnDeselect(null);

        ApartmentsF3Button.interactable = false;

        ApartmentsF4Button.interactable = true;
        ApartmentsF4Button.OnDeselect(null);
    }

    public void PressF4Button()
    {
        ApartmentsB1Button.interactable = true;
        ApartmentsB1Button.OnDeselect(null);

        ApartmentsF1Button.interactable = true;
        ApartmentsF1Button.OnDeselect(null);

        ApartmentsF2Button.interactable = true;
        ApartmentsF2Button.OnDeselect(null);

        ApartmentsF3Button.interactable = true;
        ApartmentsF3Button.OnDeselect(null);

        ApartmentsF4Button.interactable = false;
    }
}
