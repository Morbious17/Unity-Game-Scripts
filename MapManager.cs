/******************************************************************************
  File Name: MapManager.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that manipulate the maps in the game.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MapManager : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController = null;
      //sprite the represents the player on the map
    [SerializeField] private GameObject playerIcon = null;
      //used to disable blank map segments
    [SerializeField] private BoxCollider2D playerIconBoxCollider = null;
      //this panel displays the maps
    //[SerializeField] private GameObject mapDetailPanel = null; //TODO: this will need to display the correct map and then display the player icon only if they are on that map
      //panel that displays the map in the scroll view object
    [SerializeField] private RectTransform mapContentView = null;
    [SerializeField] private GameObject blankMap = null;

    [SerializeField] private ScrollRect mapContentScrollRect = null;
      //zooms in and out of the currently selected map
    [SerializeField] private Slider mapZoomSlider = null;

    [SerializeField] private Button OutskirtsMapButton = null;
    [SerializeField] private Button OutskirtsMap1stFloorButton = null;

    [SerializeField] private Button HospitalMapButton = null;
    [SerializeField] private Button Hospital2ndBasementButton = null;
    [SerializeField] private Button Hospital1stBasementButton = null;
    [SerializeField] private Button Hospital1stFloorButton = null;
    [SerializeField] private Button Hospital2ndFloorButton = null;
    [SerializeField] private Button Hospital3rdFloorButton = null;

    [SerializeField] private Button LacierNilMapButton = null;
    [SerializeField] private Button LacierNil1stFloorButton = null;

    [SerializeField] private Button ApartmentsMapButton = null;
    [SerializeField] private Button Apartments1stBasementButton = null;
    [SerializeField] private Button Apartments1stFloorButton = null;
    [SerializeField] private Button Apartments2ndFloorButton = null;
    [SerializeField] private Button Apartments3rdFloorButton = null;
    [SerializeField] private Button Apartments4thFloorButton = null;

    [SerializeField] private Button PoliceStationMapButton = null;
    [SerializeField] private Button PoliceStation1stFloorButton = null;
    [SerializeField] private Button PoliceStation2ndFloorButton = null;

    [SerializeField] private Button AbandonedMinesMapButton = null;
    [SerializeField] private Button AbandonedMines3rdBasementButton = null;
    [SerializeField] private Button AbandonedMines2ndBasementButton = null;
    [SerializeField] private Button AbandonedMines1stBasementButton = null;
    [SerializeField] private Button AbandonedMines1stFloorButton = null;


    private Image[] mapBlankSegments = new Image[196];

    private Vector3 playerIconRotation; //the angle of the player's icon on maps

    private const float defaultZoomValue = 0.6f; //value map zoom is set to when switching maps
    private const float defaultPositionScale = 0.2f;
    private const float maxPosX = 550;
    private const float maxPosY = 320;
    private const float minPosX = -214;
    private const float minPosY = -664;
      //based on size of map, this will reduce distance player icon moves
    private float positionScale;

    private Collider2D[] overlap;

    void Start()
    {
        positionScale = defaultPositionScale;
        StoreMapBlankSegments();
        mapZoomSlider.value = defaultZoomValue;
    }

    void Update()
    {
        UpdatePlayerIconRotation(); //makes icon rotate as the player rotates

        overlap = Physics2D.OverlapAreaAll(playerIconBoxCollider.bounds.min, playerIconBoxCollider.bounds.max);

        for (int i = 0; i < overlap.Length; i++)
        {
            if(overlap[i].tag == "BlankSegment")
            {
                overlap[i].gameObject.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            mapContentScrollRect.normalizedPosition = new Vector2(0, 0);
        }
    }

    /**************************************************************************
   Function: PressOutskirtsMapButton

Description: This function sets the outskirts map button as clicked, reveals
             its floor button, and deselects all other map buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressOutskirtsMapButton()
    {
        OutskirtsMapButton.interactable = false; //button is clicked
        OutskirtsMap1stFloorButton.gameObject.SetActive(true); //reveal floor button
          //only one button, so it's clicked by default
        OutskirtsMap1stFloorButton.interactable = false;

        DeselectHospitalMapButton();
        DeselectLacierNilMapButton();
        DeselectApartmentsMapButton();
        DeselectPoliceStationMapButton();
        DeselectAbandonedMinesMapButton();
    }

    /**************************************************************************
   Function: PressHospitalMapButton

Description: This function sets the hospital map button as clicked, reveals
             its floor button, and deselects all other map buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressHospitalMapButton()
    {
        HospitalMapButton.interactable = false;
        //reveal all floor buttons
        Hospital2ndBasementButton.gameObject.SetActive(true);
        Hospital1stBasementButton.gameObject.SetActive(true);
        Hospital1stFloorButton.gameObject.SetActive(true);
        Hospital2ndFloorButton.gameObject.SetActive(true);
        Hospital3rdFloorButton.gameObject.SetActive(true);
        //first floor button clicked by default
        Hospital1stFloorButton.interactable = false;

        DeselectOutskirtsMapButton();
        DeselectLacierNilMapButton();
        DeselectApartmentsMapButton();
        DeselectPoliceStationMapButton();
        DeselectAbandonedMinesMapButton();
    }

    /**************************************************************************
   Function: PressHospital2ndBasementButton

Description: This function sets the hospital's 2nd basement button as clicked
             and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressHospital2ndBasementButton()
    {
        Hospital2ndBasementButton.interactable = false;

        DeselectHospital1stBasementButton();
        DeselectHospital1stFloorButton();
        DeselectHospital2ndFloorButton();
        DeselectHospital3rdFloorButton();
    }

    /**************************************************************************
   Function: PressHospital1stBasementButton

Description: This function sets the hospital's 1st basement button as clicked
             and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressHospital1stBasementButton()
    {
        Hospital1stBasementButton.interactable = false;

        DeselectHospital2ndBasementButton();
        DeselectHospital1stFloorButton();
        DeselectHospital2ndFloorButton();
        DeselectHospital3rdFloorButton();
    }

    /**************************************************************************
   Function: PressHospital1stFloorButton

Description: This function sets the hospital's 1st floor button as clicked
             and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressHospital1stFloorButton()
    {
        Hospital1stFloorButton.interactable = false;

        DeselectHospital2ndBasementButton();
        DeselectHospital1stBasementButton();
        DeselectHospital2ndFloorButton();
        DeselectHospital3rdFloorButton();
    }

    /**************************************************************************
   Function: PressHospital2ndFloorButton

Description: This function sets the hospital's 2nd floor button as clicked
             and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressHospital2ndFloorButton()
    {
        Hospital2ndFloorButton.interactable = false;

        DeselectHospital2ndBasementButton();
        DeselectHospital1stBasementButton();
        DeselectHospital1stFloorButton();
        DeselectHospital3rdFloorButton();
    }

    /**************************************************************************
   Function: PressHospital3rdFloorButton

Description: This function sets the hospital's 3rd floor button as clicked
             and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressHospital3rdFloorButton()
    {
        Hospital3rdFloorButton.interactable = false;

        DeselectHospital2ndBasementButton();
        DeselectHospital1stBasementButton();
        DeselectHospital1stFloorButton();
        DeselectHospital2ndFloorButton();
    }

    /**************************************************************************
   Function: PressLacierNilMapButton

Description: This function sets the lacier nil map button as clicked, reveals
             its floor button, and deselects all other map buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressLacierNilMapButton()
    {
        LacierNilMapButton.interactable = false;
        LacierNil1stFloorButton.gameObject.SetActive(true);
        //first floor button clicked by default
        LacierNil1stFloorButton.interactable = false;

        DeselectOutskirtsMapButton();
        DeselectHospitalMapButton();
        DeselectApartmentsMapButton();
        DeselectPoliceStationMapButton();
        DeselectAbandonedMinesMapButton();
    }

    /**************************************************************************
   Function: PressApartmentsMapButton

Description: This function sets the apartments map button as clicked, reveals
             its floor button, and deselects all other map buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressApartmentsMapButton()
    {
        ApartmentsMapButton.interactable = false;
        Apartments1stBasementButton.gameObject.SetActive(true);
        Apartments1stFloorButton.gameObject.SetActive(true);
        Apartments2ndFloorButton.gameObject.SetActive(true);
        Apartments3rdFloorButton.gameObject.SetActive(true);
        Apartments4thFloorButton.gameObject.SetActive(true);
        //first floor button clicked by default
        Apartments1stFloorButton.interactable = false;

        DeselectOutskirtsMapButton();
        DeselectHospitalMapButton();
        DeselectLacierNilMapButton();
        DeselectPoliceStationMapButton();
        DeselectAbandonedMinesMapButton();
    }

    /**************************************************************************
   Function: PressApartments1stBasementButton

Description: This function sets the apartment's 1st basement button as clicked
             and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressApartments1stBasementButton()
    {
        Apartments1stBasementButton.interactable = false;

        DeselectApartments1stFloorButton();
        DeselectApartments2ndFloorButton();
        DeselectApartments3rdFloorButton();
        DeselectApartments4thFloorButton();
    }

    /**************************************************************************
   Function: PressApartments1stFloorButton

Description: This function sets the apartment's 1st floor button as clicked
             and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressApartments1stFloorButton()
    {
        Apartments1stFloorButton.interactable = false;

        DeselectApartments1stBasementButton();
        DeselectApartments2ndFloorButton();
        DeselectApartments3rdFloorButton();
        DeselectApartments4thFloorButton();
    }

    /**************************************************************************
   Function: PressApartments2ndFloorButton

Description: This function sets the apartment's 2nd floor button as clicked
             and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressApartments2ndFloorButton()
    {
        Apartments2ndFloorButton.interactable = false;

        DeselectApartments1stBasementButton();
        DeselectApartments1stFloorButton();
        DeselectApartments3rdFloorButton();
        DeselectApartments4thFloorButton();
    }

    /**************************************************************************
   Function: PressApartments3rdFloorButton

Description: This function sets the apartment's 3rd floor button as clicked
             and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressApartments3rdFloorButton()
    {
        Apartments3rdFloorButton.interactable = false;

        DeselectApartments1stBasementButton();
        DeselectApartments1stFloorButton();
        DeselectApartments2ndFloorButton();
        DeselectApartments4thFloorButton();
    }

    /**************************************************************************
   Function: PressApartments4thFloorButton

Description: This function sets the apartment's 4th floor button as clicked
             and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressApartments4thFloorButton()
    {
        Apartments4thFloorButton.interactable = false;

        DeselectApartments1stBasementButton();
        DeselectApartments1stFloorButton();
        DeselectApartments2ndFloorButton();
        DeselectApartments3rdFloorButton();
    }

    /**************************************************************************
   Function: PressPoliceStationMapButton

Description: This function sets the police station map button as clicked, 
             reveals its floor button, and deselects all other map buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressPoliceStationMapButton()
    {
        PoliceStationMapButton.interactable = false;
        PoliceStation1stFloorButton.gameObject.SetActive(true);
        PoliceStation2ndFloorButton.gameObject.SetActive(true);
        //first floor button clicked by default
        PoliceStation1stFloorButton.interactable = false;

        DeselectOutskirtsMapButton();
        DeselectHospitalMapButton();
        DeselectLacierNilMapButton();
        DeselectApartmentsMapButton();
        DeselectAbandonedMinesMapButton();
    }

    /**************************************************************************
   Function: PressPoliceStation1stFloorButton

Description: This function sets the police station 1st floor button as clicked
             and deselects the other floor button.

      Input: none

     Output: none
    **************************************************************************/
    public void PressPoliceStation1stFloorButton()
    {
        PoliceStation1stFloorButton.interactable = false;

        DeselectPoliceStation2ndFloorButton();
    }

    /**************************************************************************
   Function: PressPoliceStation2ndFloorButton

Description: This function sets the police station 2nd floor button as clicked
             and deselects the other floor button.

      Input: none

     Output: none
    **************************************************************************/
    public void PressPoliceStation2ndFloorButton()
    {
        PoliceStation2ndFloorButton.interactable = false;

        DeselectPoliceStation1stFloorButton();
    }

    /**************************************************************************
   Function: PressAbandonedMinesMapButton

Description: This function sets the abandoned mines map button as clicked, 
             reveals its floor button, and deselects all other map buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressAbandonedMinesMapButton()
    {
        AbandonedMinesMapButton.interactable = false;
        AbandonedMines3rdBasementButton.gameObject.SetActive(true);
        AbandonedMines2ndBasementButton.gameObject.SetActive(true);
        AbandonedMines1stBasementButton.gameObject.SetActive(true);
        AbandonedMines1stFloorButton.gameObject.SetActive(true);
        //first floor button clicked by default
        AbandonedMines1stFloorButton.interactable = false;

        DeselectOutskirtsMapButton();
        DeselectHospitalMapButton();
        DeselectLacierNilMapButton();
        DeselectApartmentsMapButton();
        DeselectPoliceStationMapButton();
    }

    /**************************************************************************
   Function: PressAbandonedMines3rdBasementButton

Description: This function sets the abandoned mines' 3rd basement button as 
             clicked and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressAbandonedMines3rdBasementButton()
    {
        AbandonedMines3rdBasementButton.interactable = false;

        DeselectAbandonedMines2ndBasementButton();
        DeselectAbandonedMines1stBasementButton();
        DeselectAbandonedMines1stFloorButton();
    }

    /**************************************************************************
   Function: PressAbandonedMines2ndBasementButton

Description: This function sets the abandoned mines' 2nd basement button as 
             clicked and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressAbandonedMines2ndBasementButton()
    {
        AbandonedMines2ndBasementButton.interactable = false;

        DeselectAbandonedMines3rdBasementButton();
        DeselectAbandonedMines1stBasementButton();
        DeselectAbandonedMines1stFloorButton();
    }

    /**************************************************************************
   Function: PressAbandonedMines1stBasementButton

Description: This function sets the abandoned mines' 1st basement button as 
             clicked and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressAbandonedMines1stBasementButton()
    {
        AbandonedMines1stBasementButton.interactable = false;

        DeselectAbandonedMines3rdBasementButton();
        DeselectAbandonedMines2ndBasementButton();
        DeselectAbandonedMines1stFloorButton();
    }

    /**************************************************************************
   Function: PressAbandonedMines1stFloorButton

Description: This function sets the abandoned mines' 1st floor button as 
             clicked and deselects all other floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressAbandonedMines1stFloorButton()
    {
        AbandonedMines1stFloorButton.interactable = false;

        DeselectAbandonedMines3rdBasementButton();
        DeselectAbandonedMines2ndBasementButton();
        DeselectAbandonedMines1stBasementButton();
    }

    /**************************************************************************
   Function: DeselectAllButtons

Description: This function deselects all the main buttons and the panels
             attached to them.

      Input: none

     Output: none
    **************************************************************************/
    public void DeselectAllButtons()
    {
        DeselectOutskirtsMapButton();
        DeselectHospitalMapButton();
        DeselectLacierNilMapButton();
        DeselectApartmentsMapButton();
        DeselectPoliceStationMapButton();
        DeselectAbandonedMinesMapButton();
    }

    /**************************************************************************
   Function: AdjustMapZoom

Description: This function sets the scale of the gameObject that displays the
             map to the value of the map zoom slider and sets the position
             scale of the player icon to reduce its speed to match the new size
             of map.

      Input: none

     Output: none
    **************************************************************************/
    public void AdjustMapZoom()
    {
          //shrinks content to appear to zoom out
        mapContentView.transform.localScale = new Vector3(mapZoomSlider.value, mapZoomSlider.value, mapContentView.transform.localScale.z);
          //reduces position speed to match smaller map
        positionScale = defaultPositionScale * mapZoomSlider.value;
    }

    /**************************************************************************
   Function: UpdatePlayerIconRotation

Description: This function retrieves the player's y-coordinate rotation in
             euler angles and uses it to set the z-coordinate of the player
             icon on the map.

      Input: none

     Output: none
    **************************************************************************/
    private void UpdatePlayerIconRotation()
    {
        playerIconRotation = new Vector3(0.0f, 0.0f, -firstPersonController.GetPlayerRotation().y + 270.0f);

        playerIcon.transform.eulerAngles = playerIconRotation;
    }

    /**************************************************************************
   Function: SetPlayerIconPosition

Description: Given a Vector3, this function multiplies it by a scalar before
             adding it to the player icon's current position.

      Input: newPosition - Vector3 that's added to the playerIcon multiplied by
                           positionScale

     Output: none
    **************************************************************************/
    public void SetPlayerIconPosition(Vector3 newPosition)
    {
        playerIcon.transform.position += newPosition * positionScale;
    }


    public void RevealMap()
    {
        for (int i = 0; i < mapBlankSegments.Length; i++)
        {
            mapBlankSegments[i].gameObject.SetActive(false);
        }
    }


    public void UpdateContentScrollBars()
    {
        //Debug.Log("Icon position is " + playerIcon.transform.position);
        //float posX = playerIcon.transform.position.x / maxPosX;
        //float posY = playerIcon.transform.position.y / maxPosY;

        //float tempMaxX = (maxPosX > 0) ? maxPosX : -maxPosX;
        //float tempMinX = (minPosX > 0) ? minPosX : -minPosX;
        //float tempMaxY = (maxPosY > 0) ? maxPosY : -maxPosY;
        //float tempMinY = (minPosY > 0) ? minPosY : -minPosY;

        //float totalX = tempMaxX + tempMinX;
        //float totalY = tempMaxY + tempMinY;

        //if(posX > 1)
        //{
        //    posX = 1;
        //}

        //if(posX < 0)
        //{
        //    posX = 0;
        //}

        //if(posY > 1)
        //{
        //    posY = 1;
        //}

        //if (posY < 0)
        //{
        //    posY = 0;
        //}

        //mapContentScrollRect.normalizedPosition = new Vector2(posX, posY);
    }

    /**************************************************************************
   Function: DeselectOutskirtsMapButton

Description: This function deselects the outskirts map button and hits its
             floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectOutskirtsMapButton()
    {
        OutskirtsMapButton.interactable = true;
        OutskirtsMapButton.OnDeselect(null);

        DeselectOutskirts1stFloorButton();
        OutskirtsMap1stFloorButton.gameObject.SetActive(false);
    }

    /**************************************************************************
   Function: DeselectOutskirts1stFloorButton

Description: This function deselects the outskirts map's 1st floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectOutskirts1stFloorButton()
    {
        OutskirtsMap1stFloorButton.interactable = true;
        OutskirtsMap1stFloorButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectOutskirtsMapButton

Description: This function deselects the hospital map button and hides its
             floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectHospitalMapButton()
    {
        HospitalMapButton.interactable = true;
        HospitalMapButton.OnDeselect(null);

        DeselectHospital2ndBasementButton();
        Hospital2ndBasementButton.gameObject.SetActive(false);

        DeselectHospital1stBasementButton();
        Hospital1stBasementButton.gameObject.SetActive(false);

        DeselectHospital1stFloorButton();
        Hospital1stFloorButton.gameObject.SetActive(false);

        DeselectHospital2ndFloorButton();
        Hospital2ndFloorButton.gameObject.SetActive(false);

        DeselectHospital3rdFloorButton();
        Hospital3rdFloorButton.gameObject.SetActive(false);
    }

    /**************************************************************************
   Function: DeselectHospital2ndBasementButton

Description: This function deselects the hospital's 2nd basement map button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectHospital2ndBasementButton()
    {
        Hospital2ndBasementButton.interactable = true;
        Hospital2ndBasementButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectHospital1stBasementButton

Description: This function deselects the hospital's 1st basement map button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectHospital1stBasementButton()
    {
        Hospital1stBasementButton.interactable = true;
        Hospital1stBasementButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectHospital1stFloorButton

Description: This function deselects the hospital's 1st floor map button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectHospital1stFloorButton()
    {
        Hospital1stFloorButton.interactable = true;
        Hospital1stFloorButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectHospital2ndFloorButton

Description: This function deselects the hospital's 2nd floor map button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectHospital2ndFloorButton()
    {
        Hospital2ndFloorButton.interactable = true;
        Hospital2ndFloorButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectHospital3rdFloorButton

Description: This function deselects the hospital's 3rd floor map button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectHospital3rdFloorButton()
    {
        Hospital3rdFloorButton.interactable = true;
        Hospital3rdFloorButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectApartmentsMapButton

Description: This function deselects the hospital map button and hides its
             floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectApartmentsMapButton()
    {
        ApartmentsMapButton.interactable = true;
        ApartmentsMapButton.OnDeselect(null);

        DeselectApartments1stBasementButton();
        Apartments1stBasementButton.gameObject.SetActive(false);

        DeselectApartments1stFloorButton();
        Apartments1stFloorButton.gameObject.SetActive(false);

        DeselectApartments2ndFloorButton();
        Apartments2ndFloorButton.gameObject.SetActive(false);

        DeselectApartments3rdFloorButton();
        Apartments3rdFloorButton.gameObject.SetActive(false);

        DeselectApartments4thFloorButton();
        Apartments4thFloorButton.gameObject.SetActive(false);
    }

    /**************************************************************************
   Function: DeselectApartments1stBasementButton

Description: This function deselects the apartment's 1st basement floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectApartments1stBasementButton()
    {
        Apartments1stBasementButton.interactable = true;
        Apartments1stBasementButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectApartments1stFloorButton

Description: This function deselects the apartment's 1st floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectApartments1stFloorButton()
    {
        Apartments1stFloorButton.interactable = true;
        Apartments1stFloorButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectApartments2ndFloorButton

Description: This function deselects the apartment's 2nd floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectApartments2ndFloorButton()
    {
        Apartments2ndFloorButton.interactable = true;
        Apartments2ndFloorButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectApartments3rdFloorButton

Description: This function deselects the apartment's 3rd floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectApartments3rdFloorButton()
    {
        Apartments3rdFloorButton.interactable = true;
        Apartments3rdFloorButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectApartments4thFloorButton

Description: This function deselects the apartment's 4th floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectApartments4thFloorButton()
    {
        Apartments4thFloorButton.interactable = true;
        Apartments4thFloorButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectLacierNilMapButton

Description: This function deselects the lacier nil map button and hides its
             floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectLacierNilMapButton()
    {
        LacierNilMapButton.interactable = true;
        LacierNilMapButton.OnDeselect(null);

        DeselectLacierNil1stFloorButton();
        LacierNil1stFloorButton.gameObject.SetActive(false);
    }

    /**************************************************************************
   Function: DeselectLacierNil1stFloorButton

Description: This function deselects the lacier nil's 1st floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectLacierNil1stFloorButton()
    {
        LacierNil1stFloorButton.interactable = true;
        LacierNil1stFloorButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectPoliceStationMapButton

Description: This function deselects the police station map button and hides 
             its floor buttons.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectPoliceStationMapButton()
    {
        PoliceStationMapButton.interactable = true;
        PoliceStationMapButton.OnDeselect(null);

        DeselectPoliceStation1stFloorButton();
        PoliceStation1stFloorButton.gameObject.SetActive(false);

        DeselectPoliceStation2ndFloorButton();
        PoliceStation2ndFloorButton.gameObject.SetActive(false);
    }

    /**************************************************************************
   Function: DeselectPoliceStation1stFloorButton

Description: This function deselects the police station's 1st floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectPoliceStation1stFloorButton()
    {
        PoliceStation1stFloorButton.interactable = true;
        PoliceStation1stFloorButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectPoliceStation2ndFloorButton

Description: This function deselects the police station's 2nd floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectPoliceStation2ndFloorButton()
    {
        PoliceStation2ndFloorButton.interactable = true;
        PoliceStation2ndFloorButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectAbandonedMinesMapButton

Description: This function deselects the abandoned mines map button and hides 
             its floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectAbandonedMinesMapButton()
    {
        AbandonedMinesMapButton.interactable = true;
        AbandonedMinesMapButton.OnDeselect(null);

        DeselectAbandonedMines3rdBasementButton();
        AbandonedMines3rdBasementButton.gameObject.SetActive(false);

        DeselectAbandonedMines2ndBasementButton();
        AbandonedMines2ndBasementButton.gameObject.SetActive(false);

        DeselectAbandonedMines1stBasementButton();
        AbandonedMines1stBasementButton.gameObject.SetActive(false);

        DeselectAbandonedMines1stFloorButton();
        AbandonedMines1stFloorButton.gameObject.SetActive(false);
    }

    /**************************************************************************
   Function: DeselectAbandonedMines3rdBasementButton

Description: This function deselects the abandoned mines's 3rd basement button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectAbandonedMines3rdBasementButton()
    {
        AbandonedMines3rdBasementButton.interactable = true;
        AbandonedMines3rdBasementButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectAbandonedMines2ndBasementButton

Description: This function deselects the abandoned mines's 2nd basement button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectAbandonedMines2ndBasementButton()
    {
        AbandonedMines2ndBasementButton.interactable = true;
        AbandonedMines2ndBasementButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectAbandonedMines1stBasementButton

Description: This function deselects the abandoned mines's 1st basement button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectAbandonedMines1stBasementButton()
    {
        AbandonedMines1stBasementButton.interactable = true;
        AbandonedMines1stBasementButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectAbandonedMines1stFloorButton

Description: This function deselects the abandoned mines's 1st floor button.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectAbandonedMines1stFloorButton()
    {
        AbandonedMines1stFloorButton.interactable = true;
        AbandonedMines1stFloorButton.OnDeselect(null);
    }


    private void StoreMapBlankSegments()
    {
        mapBlankSegments = blankMap.GetComponentsInChildren<Image>();
    }
}