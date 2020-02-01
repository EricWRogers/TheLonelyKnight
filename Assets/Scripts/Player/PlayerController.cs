using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    float originalSpeed;
    float gravityHolder;

    Vector3 moveDirection = Vector3.zero;
    Vector3 playerSize;

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

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void Crouching()
    {
        if(Input.GetButton("Crouch"))
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

    void Movement()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;
    }
}
