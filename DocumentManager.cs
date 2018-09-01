/******************************************************************************
  File Name: DocumentManager.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains a function for managing all of the game's
             documents.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentManager : MonoBehaviour
{
      //displays text of currently selected document
    [SerializeField] private Text documentDetailPanelText = null;
      //displays title of currently selected document
    [SerializeField] private Text currentDocumentTitleText = null;
      //these are buttons that temporarily stay in place of location text
    [SerializeField] private Button documentLocationButton1 = null;
    [SerializeField] private Button documentLocationButton2 = null;
    [SerializeField] private Button documentLocationButton3 = null;
    [SerializeField] private Button documentLocationButton4 = null;
      //these replace the buttons that are where locations will be displayed
    [SerializeField] private Text documentLocationText1 = null;
    [SerializeField] private Text documentLocationText2 = null;
    [SerializeField] private Text documentLocationText3 = null;
    [SerializeField] private Text documentLocationText4 = null;
      //these are disabled when the player enters their area
    [SerializeField] private Button documentEmptySpaceButton1 = null;
    [SerializeField] private Button documentEmptySpaceButton2 = null;
    [SerializeField] private Button documentEmptySpaceButton3 = null;
    [SerializeField] private Button documentEmptySpaceButton4 = null;
      //these are buttons that display documents
    [SerializeField] private Text documentButtonText1 = null;
    [SerializeField] private Text documentButtonText2 = null;
    [SerializeField] private Text documentButtonText3 = null;
    [SerializeField] private Text documentButtonText4 = null;
    [SerializeField] private Text documentButtonText5 = null;
    [SerializeField] private Text documentButtonText6 = null;
    [SerializeField] private Text documentButtonText7 = null;
    [SerializeField] private Text documentButtonText8 = null;
    [SerializeField] private Text documentButtonText9 = null;
    [SerializeField] private Text documentButtonText10 = null;
    [SerializeField] private Text documentButtonText11 = null;
    [SerializeField] private Text documentButtonText12 = null;
    [SerializeField] private Text documentButtonText13 = null;
    [SerializeField] private Text documentButtonText14 = null;
    [SerializeField] private Text documentButtonText15 = null;
    [SerializeField] private Text documentButtonText16 = null;
    [SerializeField] private Text documentButtonText17 = null;
    [SerializeField] private Text documentButtonText18 = null;
    [SerializeField] private Text documentButtonText19 = null;
    [SerializeField] private Text documentButtonText20 = null;
    [SerializeField] private Text documentButtonText21 = null;
      //various arrays to contain buttons and their information
    private Text[] documentButtonTexts = new Text[21];
    private Button[] documentLocationButtons = new Button[4];
    private Text[] documentLocationTexts = new Text[4];
    private Button[] documentEmptySpaceButtons = new Button[4];
      //text that is displayed in the document detail panel
    private string[] documentTitles = new string[21];
    private string[] documentDetails = new string[21];
    private string[] documentLocations = new string[4]; //strings of each location
      //bools that represent if a specific document has been collected
    private bool[] documentsFound = new bool[21]; 
      //these are the titles of each document
    private const string documentTitle1 = "Document 1";
    private const string documentTitle2 = "Document 2";
    private const string documentTitle3 = "Document 3";
    private const string documentTitle4 = "Document 4";
    private const string documentTitle5 = "Document 5";
    private const string documentTitle6 = "Document 6";
    private const string documentTitle7 = "Document 7";
    private const string documentTitle8 = "Document 8";
    private const string documentTitle9 = "Document 9";
    private const string documentTitle10 = "Document 10";
    private const string documentTitle11 = "Document 11";
    private const string documentTitle12 = "Document 12";
    private const string documentTitle13 = "Document 13";
    private const string documentTitle14 = "Document 14";
    private const string documentTitle15 = "Document 15";
    private const string documentTitle16 = "Document 16";
    private const string documentTitle17 = "Document 17";
    private const string documentTitle18 = "Document 18";
    private const string documentTitle19 = "Document 19";
    private const string documentTitle20 = "Document 20";
    private const string documentTitle21 = "Document 21";
      //these are the contents of each document
    private const string documentTextDetails1 = "This is the first document. Congratulations!";
    private const string documentTextDetails2 = "This is the second document. Well done!";
    private const string documentTextDetails3 = "";
    private const string documentTextDetails4 = "This is the fourth document. Super!";
    private const string documentTextDetails5 = "";
    private const string documentTextDetails6 = "";
    private const string documentTextDetails7 = "";
    private const string documentTextDetails8 = "";
    private const string documentTextDetails9 = "";
    private const string documentTextDetails10 = "";
    private const string documentTextDetails11 = "";
    private const string documentTextDetails12 = "";
    private const string documentTextDetails13 = "";
    private const string documentTextDetails14 = "";
    private const string documentTextDetails15 = "";
    private const string documentTextDetails16 = "";
    private const string documentTextDetails17 = "";
    private const string documentTextDetails18 = "";
    private const string documentTextDetails19 = "";
    private const string documentTextDetails20 = "";
    private const string documentTextDetails21 = "";
      //these are the names of each area
    private const string location1 = "First Location";
    private const string location2 = "Second Location";
    private const string location3 = "";
    private const string location4 = "";

    private const int documentCount = 21; //number of documents
    private const int emptySpaceCount = 4; //number of spaces under loctions
    private const int locationCount = 4; //number of locations
    
    private int documentSlotNumber; //used to access any document
      //used to reveal next location
    private bool locationVisited1;
    private bool locationVisited2;
    private bool locationVisited3;
    private bool locationVisited4;

    void Start ()
    {
        StoreDocumentButtonTexts();
        StoreDocumentLocationTexts();
        StoreDocumentEmptySpaceButtons();
        StoreDocumentTitles();
        StoreDocumentDetails();
	}

    /**************************************************************************
   Function: SetDocumentSlotNumber

Description: Given an integer, this function sets the active slot as the given
             integer.

      Input: docSlotNumber - integer used to set the current chosen 
                             document

     Output: none
    **************************************************************************/
    public void SetDocumentSlotNumber(int docSlotNumber)
    {
        documentSlotNumber = docSlotNumber;
        CheckIfDocumentIsCollected();
    }

    /**************************************************************************
   Function: SetDocumentsFoundStatus

Description: Given an integer, this function sets the bool representing a
             specific document to true.

      Input: docSlotNumber - integer used to set the current chosen 
                             document

     Output: none
    **************************************************************************/
    public void SetDocumentsFoundStatus(int docSlotNumber)
    {
        documentsFound[docSlotNumber] = true;
        RevealDocument();
    }

    /**************************************************************************
   Function: CheckIfDocumentIsCollected

Description: This function checks the bool of the current document. If true,
             that document's details and title are displayed in the detail
             panel.

      Input: none

     Output: none
    **************************************************************************/
    public void CheckIfDocumentIsCollected()
    {
        if(documentsFound[documentSlotNumber])
        {
            DisplayDetails();
        }
    }

    /**************************************************************************
   Function: RemoveDetails

Description: This function clears the document panel's text.

      Input: none

     Output: none
    **************************************************************************/
    public void RemoveDetails()
    {
        currentDocumentTitleText.text = "";
        documentDetailPanelText.text = "";
    }

    /**************************************************************************
   Function: RevealDocument

Description: This function loops through the array of bools to see which ones
             were set to true. The title associated with these bools are
             revealed and the reveal location function is called if this is
             the first document from that area.

      Input: none

     Output: none
    **************************************************************************/
    private void RevealDocument()
    {
        //TODO: group documents into sections by location then either check for those
        //or automatically reveal the next set of collectibles when that location is entered
        for (int i = 0; i < documentCount; i++) //for loop because they can be picked up in any order
        {
            if(documentsFound[i])
            {
                documentButtonTexts[i].text = documentTitles[i];

                if(i == 0 && !locationVisited1)
                {
                    RevealLocation(0); //first location
                    locationVisited1 = true;
                }

                if(i == 3 && !locationVisited2)
                {
                    RevealLocation(1); //second location
                    locationVisited2 = true;
                }
            }
        }
    }

    /**************************************************************************
   Function: RevealLocation

Description: Given an integer, this function reveals the location associated
             with that integer by setting text and disabling placeholder
             buttons.

      Input: locationNumber - integer representing which location to reveal

     Output: none
    **************************************************************************/
    private void RevealLocation(int locationNumber)
    {
        documentEmptySpaceButtons[locationNumber].gameObject.SetActive(false);
        documentLocationButtons[locationNumber].gameObject.SetActive(false);
        documentLocationTexts[locationNumber].text = documentLocations[locationNumber];
    }

    /**************************************************************************
   Function: DisplayDetails

Description: This function sets the detail panel's text and title to the
             current document's title and details.

      Input: none

     Output: none
    **************************************************************************/
    private void DisplayDetails()
    {
        currentDocumentTitleText.text = documentTitles[documentSlotNumber];
        documentDetailPanelText.text = documentDetails[documentSlotNumber];
    }

    /**************************************************************************
   Function: StoreDocumentTitles

Description: This function stores all document title strings into a single 
             array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreDocumentTitles()
    {
        documentTitles[0] = documentTitle1;
        documentTitles[1] = documentTitle2;
        documentTitles[2] = documentTitle3;
        documentTitles[3] = documentTitle4;
        documentTitles[4] = documentTitle5;
        documentTitles[5] = documentTitle6;
        documentTitles[6] = documentTitle7;
        documentTitles[7] = documentTitle8;
        documentTitles[8] = documentTitle9;
        documentTitles[9] = documentTitle10;
        documentTitles[10] = documentTitle11;
        documentTitles[11] = documentTitle12;
        documentTitles[12] = documentTitle13;
        documentTitles[13] = documentTitle14;
        documentTitles[14] = documentTitle15;
        documentTitles[15] = documentTitle16;
        documentTitles[16] = documentTitle17;
        documentTitles[17] = documentTitle18;
        documentTitles[18] = documentTitle19;
        documentTitles[19] = documentTitle20;
        documentTitles[20] = documentTitle21;
    }

    /**************************************************************************
   Function: StoreDocumentDetails

Description: This function stores all document detail strings into a single
             array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreDocumentDetails()
    {
        documentDetails[0] = documentTextDetails1;
        documentDetails[1] = documentTextDetails2;
        documentDetails[2] = documentTextDetails3; 
        documentDetails[3] = documentTextDetails4; 
        documentDetails[4] = documentTextDetails5; 
        documentDetails[5] = documentTextDetails6; 
        documentDetails[6] = documentTextDetails7; 
        documentDetails[7] = documentTextDetails8; 
        documentDetails[8] = documentTextDetails9;
        documentDetails[9] = documentTextDetails10;
        documentDetails[10] = documentTextDetails11; 
        documentDetails[11] = documentTextDetails12; 
        documentDetails[12] = documentTextDetails13; 
        documentDetails[13] = documentTextDetails14; 
        documentDetails[14] = documentTextDetails15; 
        documentDetails[15] = documentTextDetails16; 
        documentDetails[16] = documentTextDetails17; 
        documentDetails[17] = documentTextDetails18; 
        documentDetails[18] = documentTextDetails19; 
        documentDetails[19] = documentTextDetails20;
        documentDetails[20] = documentTextDetails21;
    }

    /**************************************************************************
   Function: StoreDocumentButtonTexts

Description: This function stores all document buttons' text into a single 
             array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreDocumentButtonTexts()
    {
        documentButtonTexts[0] = documentButtonText1;
        documentButtonTexts[1] = documentButtonText2;
        documentButtonTexts[2] = documentButtonText3;
        documentButtonTexts[3] = documentButtonText4;
        documentButtonTexts[4] = documentButtonText5;
        documentButtonTexts[5] = documentButtonText6;
        documentButtonTexts[6] = documentButtonText7;
        documentButtonTexts[7] = documentButtonText8;
        documentButtonTexts[8] = documentButtonText9;
        documentButtonTexts[9] = documentButtonText10;
        documentButtonTexts[10] = documentButtonText11;
        documentButtonTexts[11] = documentButtonText12;
        documentButtonTexts[12] = documentButtonText13;
        documentButtonTexts[13] = documentButtonText14;
        documentButtonTexts[14] = documentButtonText15;
        documentButtonTexts[15] = documentButtonText16;
        documentButtonTexts[16] = documentButtonText17;
        documentButtonTexts[17] = documentButtonText18;
        documentButtonTexts[18] = documentButtonText19;
        documentButtonTexts[19] = documentButtonText20;
        documentButtonTexts[20] = documentButtonText21;
    }

    /**************************************************************************
   Function: StoreDocumentLocationTexts

Description: This function stores all document location buttons and document
             location texts into their own arrays for easy access in other 
             functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreDocumentLocationTexts()
    {
        documentLocationButtons[0] = documentLocationButton1;
        documentLocationButtons[1] = documentLocationButton2;
        documentLocationButtons[2] = documentLocationButton3;
        documentLocationButtons[3] = documentLocationButton4;

        documentLocationTexts[0] = documentLocationText1;
        documentLocationTexts[1] = documentLocationText2;
        documentLocationTexts[2] = documentLocationText3;
        documentLocationTexts[3] = documentLocationText4;

        documentLocations[0] = location1;
        documentLocations[1] = location2;
        documentLocations[2] = location3;
        documentLocations[3] = location4;
    }

    /**************************************************************************
   Function: StoreDocumentEmptySpaceButtons

Description: This function stores all empty space document buttons into a 
             single array for easy access in other functions.

      Input: none

     Output: none
    **************************************************************************/
    private void StoreDocumentEmptySpaceButtons()
    {
        documentEmptySpaceButtons[0] = documentEmptySpaceButton1;
        documentEmptySpaceButtons[1] = documentEmptySpaceButton2;
        documentEmptySpaceButtons[2] = documentEmptySpaceButton3;
        documentEmptySpaceButtons[3] = documentEmptySpaceButton4;
    }
}
