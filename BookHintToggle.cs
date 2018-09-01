using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHintToggle : MonoBehaviour
{
    [SerializeField] private HUDManager hudManager;
    private bool show;

    void Start ()
    {
        hudManager = GameObject.Find("HUD Canvas").GetComponent<HUDManager>();
	}


    void Update ()
    {
		
	}

    private void HitByRay()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            show = !show;
            hudManager.ShowSurvivalIcon(show);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            show = !show;
            hudManager.DisplayHUD(show);
        }
    }
}
