using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public Transform GroundCheck;
    public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Vector3 velocity;
    public LayerMask Ground;
    private bool isGrounded;

    private float xRotation = 0f;
    [SerializeField] float Sensitivity;
    [SerializeField] float MovementSpeed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraMovement();
        PhysicalMovement();
        
    }

    void PhysicalMovement(){
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        
        controller.Move(move * Time.deltaTime * MovementSpeed);

        //applying gravity

        isGrounded = Physics.CheckSphere(GroundCheck.position , -0.4f , Ground);

        
        if(isGrounded && velocity.y <= 0f)
        {
            velocity.y = -1f;
        }

        if(Input.GetKey(KeyCode.Space) && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * gravityValue * -2f);
        }

        velocity.y +=  gravityValue * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }



    void CameraMovement(){
        Vector2 mouseInput = Vector2.zero;
        mouseInput += new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        
        transform.Rotate(new Vector3(0f,mouseInput.x,0f) * Time.deltaTime * Sensitivity * 300  ,Space.Self);

        xRotation -= mouseInput.y * Sensitivity * 300 * Time.deltaTime;

        xRotation = Math.Clamp(xRotation,-90f,90f);

        GetComponentInChildren<Camera>().transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
    }


    
}