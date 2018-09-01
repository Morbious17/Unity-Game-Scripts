/******************************************************************************
  File Name: Document.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that enable the player to pick up
             documents.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : MonoBehaviour
{
      //communicates with hud and document managers
    [SerializeField] private DocumentManager documentManager = null;
    [SerializeField] private HUDManager hudManager = null;

    private int documentNumber; //the number of this specific document

    void Start ()
    {
        hudManager = GameObject.Find("HUD Canvas").GetComponent<HUDManager>();
        documentManager = GameObject.Find("DocumentsButton").GetComponent<DocumentManager>();
    }

    /**************************************************************************
   Function: GetDocumentNumber

Description: This function checks the tag of the gameObject it's attached to
             and sets the document number accordingly.

      Input: none

     Output: none
    **************************************************************************/
    private void GetDocumentNumber()
    {
        switch(gameObject.tag)
        {
            case "Document1":
                documentNumber = 0;
                break;
            case "Document2":
                documentNumber = 1;
                break;
            case "Document3":
                documentNumber = 2;
                break;
            case "Document4":
                documentNumber = 3;
                break;
            case "Document5":
                documentNumber = 4;
                break;
            case "Document6":
                documentNumber = 5;
                break;
            case "Document7":
                documentNumber = 6;
                break;
            case "Document8":
                documentNumber = 7;
                break;
            case "Document9":
                documentNumber = 8;
                break;
            case "Document10":
                documentNumber = 9;
                break;
            case "Document11":
                documentNumber = 10;
                break;
            case "Document12":
                documentNumber = 11;
                break;
            case "Document13":
                documentNumber = 12;
                break;
            case "Document14":
                documentNumber = 13;
                break;
            case "Document15":
                documentNumber = 14;
                break;
            case "Document16":
                documentNumber = 15;
                break;
            case "Document17":
                documentNumber = 16;
                break;
            case "Document18":
                documentNumber = 17;
                break;
            case "Document19":
                documentNumber = 18;
                break;
            case "Document20":
                documentNumber = 19;
                break;
            case "Document21":
                documentNumber = 20;
                break;
            default:
                Debug.Log("This shouldn't happen!");
                break;
        }
    }

    /**************************************************************************
   Function: HitByRay

Description: This function is called when the player looks at this gameObject.
             If the player presses the 'E' key, they pick up the document.

      Input: none

     Output: none
    **************************************************************************/
    private void HitByRay()
    {
        hudManager.DisplayPrompt(); //displays prompt to pick up document

        if (hudManager.GetPickupCooldown() <= 0.0f && Input.GetKeyDown(KeyCode.E) && !hudManager.GetPauseStatus())
        {
            GetDocumentNumber(); //sets the number of this document
             //sets bool associated with this document to true
            documentManager.SetDocumentsFoundStatus(documentNumber);


            Destroy(gameObject); //destroy this gameObject

            hudManager.DisplayPickUpText(gameObject.tag);
            hudManager.DisplayImage(gameObject.tag);
        }
    }
}
