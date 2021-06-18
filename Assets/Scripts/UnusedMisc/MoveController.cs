using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class MoveController : MonoBehaviour
{

   public CharacterController _controller;
    public float _speed = 10;
    public float _rotationSpeed = 180;
 
    private Vector3 rotation;
        public float gravity = 20.0f;

 
    public void Update()
    {
        this.rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * _rotationSpeed * Time.deltaTime, 0);
 
        Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime);
        move = this.transform.TransformDirection(move);
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        move.y -= gravity * Time.deltaTime;
        _controller.Move(move * _speed);
        this.transform.Rotate(this.rotation);
    }
/*

    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
   public float _rotationSpeed = 180;
public float Sensitivity;
 
   private Vector3 rotation;
 
    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;


        this.rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * _rotationSpeed * Time.deltaTime, 0);
 
  //      Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime);
  //      move = this.transform.TransformDirection(move);
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
//        _controller.Move(move * _speed);
        this.transform.Rotate(this.rotation);    
    
    }
    */
}

