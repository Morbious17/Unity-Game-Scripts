/******************************************************************************
  File Name: SurvivalNoteManager.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains a function for managing all of the game's
             survival notes.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalNoteManager : MonoBehaviour
{
      //used to deselect survival note button after viewing survival notes
    [SerializeField] private UIManager uiManager = null;
      //displays text and screenshots
    [SerializeField] private GameObject survivalNoteDetailPanel = null;
      //displays survival note text
    [SerializeField] private Text survivalNoteDetailText = null;
      //displays text on each survival note button
    [SerializeField] private Text survivalButtonText1 = null;
    [SerializeField] private Text survivalButtonText2 = null;
    [SerializeField] private Text survivalButtonText3 = null;
    [SerializeField] private Text survivalButtonText4 = null;
    [SerializeField] private Text survivalButtonText5 = null;
    [SerializeField] private Text survivalButtonText6 = null;
    [SerializeField] private Text survivalButtonText7 = null;
    [SerializeField] private Text survivalButtonText8 = null;
    [SerializeField] private Text survivalButtonText9 = null;
    [SerializeField] private Text survivalButtonText10 = null;

      //enabled when an unread survival note has been revealed
    [SerializeField] private Text newNoteText = null;

    private Text[] survivalButtonTexts = new Text[10];
    private string[] survivalNoteNames = new string[10];
    private string[] survivalNoteDetails = new string[10];
      //these arrays represent whether or not notes have been revealed and read
    private bool[] survivalNotesRead = new bool[10];
    private bool[] survivalNotesRevealed = new bool[10];

      //these will be the names of each note when they are revealed
    private const string survivalNoteName1 = "1. Switching Weapons";
    private const string survivalNoteName2 = "2. Weakpoints 1";
    private const string survivalNoteName3 = "3. This is note 3";
    private const string survivalNoteName4 = "4. This is note 4";
    private const string survivalNoteName5 = "5. This is note 5";
    private const string survivalNoteName6 = "6. This is note 6";
    private const string survivalNoteName7 = "7. This is note 7";
    private const string survivalNoteName8 = "8. This is note 8";
    private const string survivalNoteName9 = "9. This is note 9";
    private const string survivalNoteName10 = "10. This is note 10";

    private const string survivalNoteDetails1 = "You can quick swap weapons with Q key!";
    private const string survivalNoteDetails2 = "Hitting weak points will kill lesser enemies instantly!";
    private const string survivalNoteDetails3 = "";
    private const string survivalNoteDetails4 = "";
    private const string survivalNoteDetails5 = "";
    private const string survivalNoteDetails6 = "";
    private const string survivalNoteDetails7 = "";
    private const string survivalNoteDetails8 = "";
    private const string survivalNoteDetails9 = "";
    private const string survivalNoteDetails10 = "";
      //current number of survival notes
    private const int survivalNoteCount = 10;

      //used to access any survival note
    private int survivalNoteSlotNumber;


    void Start ()
    {
        StoreSurvivalButtonTexts();
        StoreSurvivalNoteNames();
        StoreSurvivalNoteDetails();
	}

    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.E))
        {
            ClearSurvivalNoteDetailPanel();
            uiManager.DeselectActiveSurvivalNote();
        }
	}

    /**************************************************************************
   Function: CheckIfThereIsNewNote

Description: This function loops through both arrays and checks if a note at
             that index has been revealed and not yet read. If so, the "New!"
             text is displayed right under the survival notes button and the
             function immediately returns. If not, the text is hidden.

      Input: none

     Output: none
    **************************************************************************/
    public void CheckIfThereIsNewNote()
    {
        for (int i = 0; i < survivalNoteCount; i++)
        {
            if(survivalNotesRevealed[i] == true && survivalNotesRead[i] == false)
            {
                newNoteText.enabled = true;
                return;
            }
        }

        newNoteText.enabled = false;
    }

    /**************************************************************************
   Function: GetRevealedNoteStatus

Description: This function returns the bool at the current index of the
             notesRevealed array that represents whether or not that note has
             been revealed.

      Input: none

     Output: Returns true if the note at the current index has been read,
             otherwise, returns false.
    **************************************************************************/
    public bool GetRevealedNoteStatus()
    {
        return survivalNotesRevealed[survivalNoteSlotNumber];
    }

    /**************************************************************************
   Function: ReadNote

Description: This function sets the survival note panel's text to the details
             in the array at the current index. Then it sets note read bool
             to true at the same index and displays the survival note panel.

      Input: none

     Output: none
    **************************************************************************/
    public void ReadNote()
    {
        survivalNoteDetailText.text = survivalNoteDetails[survivalNoteSlotNumber];
        survivalNotesRead[survivalNoteSlotNumber] = true;
        EnableSurvivalNoteDetailPanel(true);

        CheckIfThereIsNewNote();
    }

    /**************************************************************************
   Function: RevealNote

Description: Given an integer, this function sets the bool at the specified
             index to true as well as revealing the note button's name at the
             same index. Then the CheckIfThereIsNewNote function is called to
             see if the "New!" text should be displayed.

      Input: noteSloteNumber - integer that specifies which index to access

     Output: none
    **************************************************************************/
    public void RevealNote(int noteSlotNumber)
    {
        survivalNotesRevealed[noteSlotNumber] = true;
        survivalButtonTexts[noteSlotNumber].text = survivalNoteNames[noteSlotNumber];

        CheckIfThereIsNewNote();
    }

    /**************************************************************************
   Function: ClearSurvivalNoteDetailPanel

Description: This function hides the survival note panel and clears its text.

      Input: none

     Output: none
    **************************************************************************/
    public void ClearSurvivalNoteDetailPanel()
    {
        EnableSurvivalNoteDetailPanel(false);
        survivalNoteDetailText.text = "";
    }

    /**************************************************************************
   Function: SetSurvivalNoteSlotNumber

Description: Given an integer, this function sets the active slot as the given
             integer.

      Input: noteSlotNumber - integer used to set the current chosen survival 
                              note

     Output: none
    **************************************************************************/
    public void SetSurvivalNoteSlotNumber(int noteSlotNumber)
    {
        survivalNoteSlotNumber = noteSlotNumber;
    }

    /**************************************************************************
   Function: EnableSurvivalNoteDetailPanel

Description: Given a bool, this function either hides or displays the panel
             that contains the details of the survival note.

      Input: enable - bool used to hide or display the survival note panel

     Output: none
    **************************************************************************/
    private void EnableSurvivalNoteDetailPanel(bool enable)
    {
        survivalNoteDetailPanel.SetActive(enable);
    }

    /**************************************************************************
   Function: StoreSurvivalNoteDetails

Description: This function stores all survive note detail strings in an array
             for easy access by any other function.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreSurvivalNoteDetails()
    {
        survivalNoteDetails[0] = survivalNoteDetails1;
        survivalNoteDetails[1] = survivalNoteDetails2;
        survivalNoteDetails[2] = survivalNoteDetails3;
        survivalNoteDetails[3] = survivalNoteDetails4;
        survivalNoteDetails[4] = survivalNoteDetails5;
        survivalNoteDetails[5] = survivalNoteDetails6;
        survivalNoteDetails[6] = survivalNoteDetails7;
        survivalNoteDetails[7] = survivalNoteDetails8;
        survivalNoteDetails[8] = survivalNoteDetails9;
        survivalNoteDetails[9] = survivalNoteDetails10;
    }

    /**************************************************************************
   Function: StoreSurvivalNoteNames

Description: This function stores all survive note name strings in an array
             for easy access by any other function.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreSurvivalNoteNames()
    {
        survivalNoteNames[0] = survivalNoteName1;
        survivalNoteNames[1] = survivalNoteName2;
        survivalNoteNames[2] = survivalNoteName3;
        survivalNoteNames[3] = survivalNoteName4;
        survivalNoteNames[4] = survivalNoteName5;
        survivalNoteNames[5] = survivalNoteName6;
        survivalNoteNames[6] = survivalNoteName7;
        survivalNoteNames[7] = survivalNoteName8;
        survivalNoteNames[8] = survivalNoteName9;
        survivalNoteNames[9] = survivalNoteName10;
    }

    /**************************************************************************
   Function: StoreSurvivalButtonTexts

Description: This function stores all survival note buttons' text into a single
             array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreSurvivalButtonTexts()
    {
        survivalButtonTexts[0] = survivalButtonText1;
        survivalButtonTexts[1] = survivalButtonText2;
        survivalButtonTexts[2] = survivalButtonText3;
        survivalButtonTexts[3] = survivalButtonText4;
        survivalButtonTexts[4] = survivalButtonText5;
        survivalButtonTexts[5] = survivalButtonText6;
        survivalButtonTexts[6] = survivalButtonText7;
        survivalButtonTexts[7] = survivalButtonText8;
        survivalButtonTexts[8] = survivalButtonText9;
        survivalButtonTexts[9] = survivalButtonText10;
    }
}
