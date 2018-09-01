

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Door : MonoBehaviour
{
    [SerializeField] private HUDManager hudManager = null;
      //rotation of the door as the game loads
    private Vector3 defaultRotation;
    private Vector3 closeRotation;
      //set this to false in Inspector on doors that are open/ajar by default
    [SerializeField] private bool closed = true;
    private bool opening;
    private bool closing;
    private float doorSpeed;

    void Start ()
    {
        doorSpeed = Time.deltaTime * 70.0f;
        defaultRotation = transform.parent.localEulerAngles;

          //TODO: if some doors will start partially open, need to fix logic.
          //Currently, open/ajar doors don't move when used once. Then they open
          //forever.
        if(closed) //if door isn't open/ajar at runtime
        {

            closeRotation = defaultRotation;
        }
        else
        {
            closeRotation = Vector3.zero;
        }

        //openRotation = new Vector3(closeRotation.x, closeRotation.y + 270.0f, closeRotation.z);
	}


    void Update ()
    {

	}

    void FixedUpdate()
    {
        if(opening) //if the door is opening
        {
              //if door isn't completely open
            if(transform.parent.localEulerAngles.y > closeRotation.y - 90.0f)
            {
               OpenDoor(true); //continue opening door
            }
            else
            {
                opening = false; //stop opening the door
                closed = false;
            }
        }

        if(closing) //if the door is closing
        {
              //if the door isn't completely closed
            if(transform.parent.localEulerAngles.y < closeRotation.y)
            {
                OpenDoor(false); //continue closing the door
            }
            else
            {
                closing = false; //stop closing the door
                closed = true;
            }
        }
    }

    private void UseDoor()
    {
        if(closed)
        {
            opening = true;
        }
        else
        {
            closing = true;
        }

        //if (transform.parent.localEulerAngles == closeRotation)
        //{
        //    transform.parent.localEulerAngles = openRotation;
        //}
        //else
        //{
        //    transform.parent.localEulerAngles = closeRotation;
        //}
    }

    private void HitByRay()
    {
        hudManager.DisplayPrompt();

        if(Input.GetKeyDown(KeyCode.E))
        {
            UseDoor();
        }
    }

    private void OpenDoor(bool status)
    {
         //gets door's current angle
        Vector3 currentAngle = transform.parent.localEulerAngles;
        Vector3 newAngle;

        if (status)
        {
            newAngle = new Vector3(currentAngle.x, currentAngle.y -= doorSpeed, currentAngle.z);
        }
        else
        {
            newAngle = new Vector3(currentAngle.x, currentAngle.y += doorSpeed, currentAngle.z);
        }

        transform.parent.localEulerAngles = newAngle;
    }


    public bool GetClosedDoorStatus()
    {
        return closed;
    }
}
