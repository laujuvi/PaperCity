using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;

    [Header("General")]
    [SerializeField] private float gravityScale = -20f;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float runSpeed = 7;

    [Header("Rotation")]
    [SerializeField] private float rotationSensibility = 200;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 1.9f;

    [Header("Crouch")]
    public float crouchHeight;
    public bool crouch;

    [Header("LookUp")]
    public float lookUphHeight;
    public bool lookUp;

    [SerializeField] private float smooth = 4f;
    private float cameraVerticalAngle;
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Look();
        Move();
        Crouch();
        LookUp();
    }
    private void Move()
    {
        if(characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            if (Input.GetButton("Sprint"))
            {
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }
            if (Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            }
        }
        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }
    private void Look()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        cameraVerticalAngle += rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

        transform.Rotate(Vector3.up * rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);
    }
    private void Crouch()
    {
        crouch = Input.GetKey(KeyCode.LeftControl);

        float crouchLocalScaleY = crouch ? crouchHeight : 1f;
        float newCrouchScaleY = Mathf.Lerp(transform.localScale.y, crouchLocalScaleY, Time.deltaTime * smooth);
        transform.localScale = new Vector3(1, newCrouchScaleY, 1);
    }
    private void LookUp()
    {
        lookUp = Input.GetKey(KeyCode.V);
        float targetLocalScaleY = lookUp ? lookUphHeight : 1f;
        float newScaleY = Mathf.Lerp(transform.localScale.y, targetLocalScaleY, Time.deltaTime * smooth);
        transform.localScale = new Vector3(1, newScaleY, 1);
    }
}
