using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbandMinesFloorButtons : MonoBehaviour
{
    //This script makes the clicked button non-interactable while making all other
    //floor buttons interactable. 

    [SerializeField] private Button AbandonedMinesB3Button;
    [SerializeField] private Button AbandonedMinesB2Button;
    [SerializeField] private Button AbandonedMinesB1Button;
    [SerializeField] private Button AbandonedMinesF1Button;

    //TODO: When maps are created, add functionality so that the floor of each map is loaded when these functions run.

    void Start ()
    {
		
	}

	void Update ()
    {
		
	}

    public void PressB3Button()
    {
        AbandonedMinesB3Button.interactable = false;

        AbandonedMinesB2Button.interactable = true;
        AbandonedMinesB2Button.OnDeselect(null);

        AbandonedMinesB1Button.interactable = true;
        AbandonedMinesB1Button.OnDeselect(null);

        AbandonedMinesF1Button.interactable = true;
        AbandonedMinesF1Button.OnDeselect(null);
    }

    public void PressB2Button()
    {
        AbandonedMinesB3Button.interactable = true;
        AbandonedMinesB3Button.OnDeselect(null);

        AbandonedMinesB2Button.interactable = false;

        AbandonedMinesB1Button.interactable = true;
        AbandonedMinesB1Button.OnDeselect(null);

        AbandonedMinesF1Button.interactable = true;
        AbandonedMinesF1Button.OnDeselect(null);
    }

    public void PressB1Button()
    {
        AbandonedMinesB3Button.interactable = true;
        AbandonedMinesB3Button.OnDeselect(null);

        AbandonedMinesB2Button.interactable = true;
        AbandonedMinesB2Button.OnDeselect(null);

        AbandonedMinesB1Button.interactable = false;

        AbandonedMinesF1Button.interactable = true;
        AbandonedMinesF1Button.OnDeselect(null);
    }

    public void PressF1Button()
    {
        AbandonedMinesB3Button.interactable = true;
        AbandonedMinesB3Button.OnDeselect(null);

        AbandonedMinesB2Button.interactable = true;
        AbandonedMinesB2Button.OnDeselect(null);

        AbandonedMinesB1Button.interactable = true;
        AbandonedMinesB1Button.OnDeselect(null);

        AbandonedMinesF1Button.interactable = false;
    }
}
