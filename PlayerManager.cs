using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpHeight = 5f;
    public float sphereRadius = 0.2f;
    public float sprintModifier = 1.5f;
    public float crouchModidier = 1.5f;

    public Transform playerBody;
    public Transform playerCamera;
    public Transform groundCheck;
    public LayerMask playerLayer;

    private Rigidbody rb;

    private Vector3 moveVector3;

    bool isJumping = false;
    bool isSprinting = false;
    bool isCrouching = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();

        GroundCheck();

        if (Input.GetButton("Jump") && isJumping == false)
        {
            Jump();
        }

        if (Input.GetButtonDown("Sprint") && isSprinting == false)
        {
            EnableSprint();
            isSprinting = true;
        }

        if (Input.GetButtonUp("Sprint") && isSprinting == true)
        {
            DisableSprint();
            isSprinting = false;
        }

        if (Input.GetButtonDown("Crouch") && isCrouching == false)
        {
            EnableCrouch();
            isCrouching = true;
        }

        if (Input.GetButtonUp("Crouch") && isCrouching == true)
        {
            DisableCrouch();
            isCrouching = false;
        }
    }

    void Move()
    {
        moveVector3 = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y, Input.GetAxisRaw("Vertical") * moveSpeed);
        moveVector3 = transform.TransformDirection(moveVector3);

        rb.velocity = moveVector3;
    }

    void GroundCheck()
    {
        if (Physics.CheckSphere(groundCheck.position, sphereRadius, ~playerLayer))
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }
    }

    void Jump()
    {
        rb.AddForce(0f, jumpHeight / 25f, 0f, ForceMode.Impulse);
    }

    void EnableSprint()
    {
        moveSpeed *= sprintModifier;
    }

    void DisableSprint()
    {
        moveSpeed /= sprintModifier;
    }

    void EnableCrouch()
    {
        playerBody.localScale = new Vector3(1f, 0.5f, 1f);
        playerBody.localPosition = new Vector3(0f, -0.5f, 0f);
        playerCamera.localPosition = new Vector3(0f, -0.2f, 0f);
        moveSpeed /= crouchModidier;
    }

    void DisableCrouch()
    {
        playerBody.localScale = new Vector3(1f, 1f, 1f);
        playerBody.localPosition = new Vector3(0f, 0f, 0f);
        playerCamera.localPosition = new Vector3(0f, 0.5f, 0f);
        moveSpeed *= crouchModidier;
    }
}