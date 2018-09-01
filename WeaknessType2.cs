/******************************************************************************
  File Name: WeaknessType2.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains a function that sets the location of the
             weak point of enemyType2.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaknessType2 : MonoBehaviour
{
    //TODO: return to this when enemy model is acquired
    //the possible positions of the weakness
    private Vector3 position1 = new Vector3(-0.024f, 0.835f, 0.329f);
    private Vector3 position2 = new Vector3(0.37f, -0.085f, 0.329f);
    private Vector3 position3 = new Vector3(-0.357f, 0.128f, 0.329f);
    private Vector3 position4 = new Vector3(-0.022f, 0.43f, -0.55f);
    private int weaknessPosition; //used to set position of weakness

    void Start()
    {
        weaknessPosition = Random.Range(1, 4); //randomly chooses a location
        SetWeaknessPosition(); //sets the weakness to the chosen location
    }

    /**************************************************************************
   Function: SetWeaknessPosition

Description: This function checks the random number assigned to 
             weaknessPosition and sets the weakness object to a location based
             on that random number.

      Input: none

     Output: none
    **************************************************************************/
    private void SetWeaknessPosition()
    {
        switch (weaknessPosition)
        {
            case 1:
                transform.localPosition = position1;
                break;
            case 2:
                transform.localPosition = position2;
                break;
            case 3:
                transform.localPosition = position3;
                break;
            case 4:
                transform.localPosition = position4;
                break;
            default:
                //Debug.Log("Something went wrong");
                break;
        }
    }
}
