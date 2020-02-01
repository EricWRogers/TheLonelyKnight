using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float speedBoost = 2f;

    float originalSpeed = 0f;
    float gravityHolder = 0f;

    Vector3 moveDirection = Vector3.zero;
    Vector3 playerSize = Vector3.zero;

    CharacterController characterController;

    void Start()
    {
        playerSize = transform.localScale;
        gravityHolder = gravity;
        originalSpeed = speed;

        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            Crouching();
            Movement();
            Jump();
            Sprinting();
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void Crouching()
    {
        if(Input.GetButton("Crouch") && Input.GetButton("Jump") == false)
        {
            gravity = gravityHolder * 10f; 
            Vector3 sizeHolder = playerSize;
            sizeHolder.y /= 3f;
            transform.localScale = sizeHolder;
        }
        else
        {
            gravity = gravityHolder;
            Vector3 sizeHolder = playerSize;
            if(transform.localScale != playerSize)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, sizeHolder, 0.3f);
            }
        }
    }

    void Jump()
    {
        if (Input.GetButton("Jump") && Input.GetButton("Crouch") == false && Input.GetButton("Sprint") == false)
        {
            moveDirection.y = jumpSpeed;
        }
    }

    void Movement()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;
    }

    void Sprinting()
    {
        if (Input.GetButton("Sprint") && Input.GetButton("Crouch") == false && Input.GetButton("Jump") == false)
        {
            speed = originalSpeed * speedBoost;
        }
        else if(speed != originalSpeed)
        {
            speed = originalSpeed;
        }
    }
}
