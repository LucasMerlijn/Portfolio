using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerObselete : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float rotSpeed;

    [SerializeField]
    private float jumpPower;

    [SerializeField]
    private bool onGround = true;

    private Vector3 playerRotationVec;
    public Vector3 PlayerRotationQuat
    {
        get { return playerRotationVec; }
    }

    private Quaternion playerRotationQuat;


    [SerializeField]
    private float maxVelocity;

    private Rigidbody RB;


    private float LookingHor;
    private float LookingVer;
    private float HorInput;
    private float VerInput;


    private void Start()
    {
        RB = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        LookingHor = Input.GetAxis("LookingHor"); // Looking Horizontal
        LookingVer = Input.GetAxis("LookingVer"); // Looking Vertical

        HorInput = Input.GetAxis("Horizontal"); // Movement Horizontal
        VerInput = Input.GetAxis("Vertical"); // Movement Vertical

        LeftStickMovement();
        RightStickMovement();


    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && onGround == true)
        {
            Debug.Log("Jump!");
            onGround = false;
            RB.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void RightStickMovement()
    {
        playerRotationVec = Vector3.right * LookingVer + -Vector3.forward * LookingHor;

        if (playerRotationVec.sqrMagnitude > 0.1f)
        {
            playerRotationQuat = Quaternion.LookRotation(playerRotationVec);
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotationQuat, rotSpeed);
        }
    }

    private void LeftStickMovement()
    {
        if (RB.velocity.x < maxVelocity || RB.velocity.z < maxVelocity)
        {
            RB.AddForce(new Vector3(HorInput * moveSpeed, 0, VerInput * moveSpeed), ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
}