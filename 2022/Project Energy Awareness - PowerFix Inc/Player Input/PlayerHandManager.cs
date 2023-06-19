using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerHandManager : MonoBehaviour
{
    // Player Input Manager
    private PlayerInputManager PIM;
    public PlayerInputManager PIM_
    {
        get { return PIM; }
    }

    // Get player hands
    [SerializeField]
    private PlayerHand rightHand, leftHand;

    // GET COMPONENTS
    private Rigidbody RB;


    // Variables

    private Vector3 moveUp; // upwards force when grabbing object

    [SerializeField]
    private GameObject leftGrabbedObj, rightGrabbedObj; // objects that is being grabbed
    public GameObject LeftGrabbedObj
    {
        set
        {
            moveUp = Vector3.up * upwardPower;
            leftGrabbedObj = value;
        }
    }
    public GameObject RightGrabbedObj
    {
        set
        {
            moveUp = Vector3.up * upwardPower;
            rightGrabbedObj = value;
        }
    }


    private float handPower; // Distance / power of hands
    [SerializeField]
    [Range(0.1f, 2)]
    private float upwardPower;

    private bool rightHandActive, leftHandActive;
    public bool RightHandActive
    {
        get { return rightHandActive; }
    }
    public bool LeftHandActive
    {
        get { return leftHandActive; }
    }



    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        PIM = GetComponentInParent<PlayerInputManager>();
        handPower = PIM.HandPower;
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis(PIM.RightTrigger) > 0.05f)
        {
            rightHand.RB_.AddForce((transform.parent.forward + moveUp) * (Input.GetAxis(PIM.RightTrigger) * handPower), ForceMode.Force);
            rightHandActive = true;
        }
        else
        {
            rightHandActive = false;
            DestroySpring(true);
        }
        if (Input.GetAxis(PIM.LeftTrigger) > 0.05f)
        {
            leftHand.RB_.AddForce((transform.parent.forward + moveUp) * (Input.GetAxis(PIM.LeftTrigger) * handPower), ForceMode.Force);
            leftHandActive = true;
        }
        else
        {
            leftHandActive = false;
            DestroySpring(false);
        }
    }


    public void SpringAdder(bool isRighthand)
    {
        if (isRighthand)
        {
            rightHand.gameObject.AddComponent<FixedJoint>();
            rightHand.GetComponent<FixedJoint>().connectedBody = rightGrabbedObj.GetComponent<Rigidbody>();
        }
        else
        {
            leftHand.gameObject.AddComponent<FixedJoint>();
            leftHand.GetComponent<FixedJoint>().connectedBody = leftGrabbedObj.GetComponent<Rigidbody>();
        }
    }

    private void DestroySpring(bool isRightSpring)
    {
        if (isRightSpring)
        {
            Destroy(rightHand.GetComponent<FixedJoint>());
            rightGrabbedObj = null;
            rightHand.IsGrabbing = false;
        }
        else
        {
            Destroy(leftHand.GetComponent<FixedJoint>());
            leftGrabbedObj = null;
            leftHand.IsGrabbing = false;
        }
    }
}