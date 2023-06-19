using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private GameObject grabbedObj;

    private PlayerInputManager PIM;
    private PlayerHandManager PHM;
    private Rigidbody RB;
    public Rigidbody RB_
    {
        get { return RB; }
    }

    [SerializeField]
    private bool isRightHand, isGrabbing;
    public bool IsGrabbing
    {
        set { isGrabbing = value; }
    }
        


    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        PHM = GetComponentInParent<PlayerHandManager>();
        PIM = PHM.PIM_;

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isGrabbing)
        {
            if (PHM.LeftHandActive || PHM.RightHandActive)
            {
                if (other.GetComponent<Tool>())
                {
                    grabbedObj = other.gameObject;

                    if (isRightHand)
                    {
                        PHM.RightGrabbedObj = grabbedObj;
                        this.gameObject.transform.position = other.GetComponent<Tool>().holdingLocations[0].position;
                    }
                    else
                    {
                        PHM.LeftGrabbedObj = grabbedObj;
                        this.gameObject.transform.position = other.GetComponent<Tool>().holdingLocations[1].position;
                    }
                    PHM.SpringAdder(isRightHand);
                    isGrabbing = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrabbing)
        {
            if (PHM.LeftHandActive || PHM.RightHandActive)
            {
                if (collision.gameObject.GetComponent<InteractableObject>()) // Is interactable
                {
                    if (collision.gameObject.GetComponent<InteractableObject>().IsGrabbable) // Is grabbable
                    {
                        grabbedObj = collision.gameObject;
                        if (grabbedObj.GetComponent<PlayerInputManager>()) // Is a player
                        {
                            if (grabbedObj.GetComponent<PlayerInputManager>().PlayerID != PIM.PlayerID) // Make sure not to grab self
                            {
                                if (isRightHand)
                                    PHM.RightGrabbedObj = grabbedObj;
                                else
                                    PHM.LeftGrabbedObj = grabbedObj;

                                PHM.SpringAdder(isRightHand);
                                isGrabbing = true;
                            }
                        }
                        else // is not a player
                        {
                            if (isRightHand)
                                PHM.RightGrabbedObj = grabbedObj;
                            else
                                PHM.LeftGrabbedObj = grabbedObj;

                            PHM.SpringAdder(isRightHand);
                            isGrabbing = true;
                        }
                    }
                }
            }
        }
    }
}