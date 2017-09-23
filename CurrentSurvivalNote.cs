using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSurvivalNote : MonoBehaviour
{
    [SerializeField] private Text survivalNoteText;
    [SerializeField] private Text currentSurvivalText;
    [SerializeField] private RawImage currentSurvivalImage;
    [SerializeField] private Text detailedSurvivalText;


	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void ChangeCurrentText()
    {
        currentSurvivalText.text = survivalNoteText.text;
    }

    public void ChangeCurrentScreenshot()
    {
        //TODO: Take screenshots and store them in a folder so they can be loaded into the correct panel.
    }

    public void ChangeCurrentDetails()
    {
        //TODO: Determine the full text of each survival note so they can be store in the panel below each image.
    }
}
