using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInputManager : MonoBehaviour
{

    private GameManager GM;
    private IntroPanelManager IPM;
    // #### BASE VARIABLES #### //
    [SerializeField]
    [Range(1, 4)]
    private int playerID = 1;       // Player ID value (to determine what player you are)
    public int PlayerID
    {
        get { return playerID; }
        set
        {
            playerID = value;
            SetInputStrings();
        }
    }


    private string myID;            // Convert ID(int) to string for input

    private Rigidbody RB;           // Rigidbody. Used for moving / physics


    // #### VARIABLES FOR HANDS #### //

    [SerializeField]
    private float handPower = 100;  // Set hands travel Distance
    public float HandPower
    {
        get { return handPower; }
    }

    private bool hasObjectGrabbed;
    private InteractableObject grabbedObject;

    public bool HasObject
    {
        set { hasObjectGrabbed = value; }
    }

    public InteractableObject GrabbedObject
    {
        set { grabbedObject = value; }
    }


    // #### VARIABLES FOR CONTOLLER INPUT #### //

    #region Joystick Input Variables

    // Joystick Input Axis.
    private string leftJoyHor;      // Left Horizontal String & Float
    private float leftJoyHorInput;

    private string leftJoyVer;      // Left Vertical String & Float
    private float leftJoyVerInput;

    private string rightJoyHor;     // Right Horizontal String & Float
    private float rightJoyHorInput;

    private string rightJoyVer;     // Right Vertical String & Float
    private float rightJoyVerInput;


    // Trigger Input Axis
    private string leftTrigger;
    public string LeftTrigger
    {
        get { return leftTrigger; }
    }

    private string rightTrigger;
    public string RightTrigger
    {
        get { return rightTrigger; }
    }


    // Button Input
    private string interactKey;
    private string rightBumper;

    #endregion

    // #### MOVING VARIABLES #### //

    private Vector3 playerRotationVec;      // Vector for rotation
    private Quaternion playerRotationQuat;  // The Quat. for rotation

    // Rotation Speed
    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float turningSpeed;             // Rotation Speed

    private Vector3 playerMovementVec;      // movement Vector (directional)

    [SerializeField]
    [Range(10, 100)]
    private float moveSpeed;                // Movement Speed
    [SerializeField]
    private float maxSpeed;                 // Max Movement Speed

    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private bool isGrounded = true;

    // ########################### END VARIABLES ########################### //

    #region setup

    private void Awake()
    {
        SetInputStrings();
    }

    private void SetInputStrings()
    {
        myID = playerID.ToString();

        leftJoyHor = "L_JoystickHorizontal_p" + myID;
        leftJoyVer = "L_JoystickVertical_p" + myID;
        rightJoyHor = "R_JoystickHorizontal_p" + myID;
        rightJoyVer = "R_JoystickVertical_p" + myID;

        leftTrigger = "L_Joystick_Trigger_p" + myID;
        rightTrigger = "R_Joystick_Trigger_p" + myID;

        interactKey = "Joystick_A_key_p" + myID;
        rightBumper = "Joystick_Right_Bumper_key_p" + myID;
    }

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        GM = FindObjectOfType<GameManager>();
        IPM = FindObjectOfType<IntroPanelManager>();
    }

    #endregion

    #region Base Unity Functions

    private void Update()
    {
        ButtonAPress();
    }

    private void ButtonAPress()
    {

        //float interactKeyF = Input.GetAxis(interactKey);
        if (Input.GetButtonDown(interactKey))
        {
            if (GM.gameState == GameManager.GameState.replPhase && isGrounded)
            {
                isGrounded = false;
                RB.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                StartCoroutine(groundChecker());
            }
        }
    }

    [SerializeField]
    private float extraGravity = 50f;
    [SerializeField]
    private float gravityIncrement = 15f;

    public float GroundCheckLength;
    IEnumerator groundChecker()
    {
        yield return new WaitForSeconds(0.5f);

        extraGravity = 50f;
        int layerMask = 1 << 4;
        layerMask = ~layerMask; // To make sure not to collide on players
        RaycastHit hit;

        while (!isGrounded)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, GroundCheckLength, layerMask))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                isGrounded = true;
            }
            else
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * GroundCheckLength, Color.white);
                print("in air");
                RB.AddForce(new Vector3(0, -1 * extraGravity, 0), ForceMode.Force);
                extraGravity += gravityIncrement;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void FixedUpdate()
    {
        LeftStickInput();
        RightstickInput();
    }

    #endregion

    #region joystick Inputs

    /// <summary>
    /// Aiming
    /// </summary>
    private void RightstickInput()
    {
        rightJoyHorInput = Input.GetAxis(rightJoyHor); // Get Input Axis Hor
        rightJoyVerInput = Input.GetAxis(rightJoyVer); // Get Input Axis Ver

        // Turn it into a vector
        playerRotationVec = Vector3.right * rightJoyVerInput + -Vector3.forward * rightJoyHorInput;

        if (playerRotationVec.sqrMagnitude > 0.1f) // Check the Mag. value (if greater then do the rotation)
        {
            playerRotationQuat = Quaternion.LookRotation(playerRotationVec); // Turn Vec to Quat.
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotationQuat, turningSpeed); // Slerp to the new rotational value
        }
    }

    /// <summary>
    /// Movement
    /// </summary>
    private void LeftStickInput()
    {
        leftJoyHorInput = Input.GetAxis(leftJoyHor);
        leftJoyVerInput = Input.GetAxis(leftJoyVer);
        playerMovementVec = new Vector3(leftJoyHorInput, 0, leftJoyVerInput);

        if (RB.velocity.magnitude < maxSpeed)
        {
            RB.AddForce(playerMovementVec * moveSpeed, ForceMode.Force);
        }
        else
        {
            Debug.Log("Max Speed has been Reached!");
        }
    }

    #endregion

}