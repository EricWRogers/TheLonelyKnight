using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float speedBoost = 2f;

    public float scrapRepairCost = 200f;
    public float scrapActaveCost = 250f;

    public int DamageToEnemy = 15;

    float originalSpeed = 0f;
    float originalGravity = 0f;

    Vector3 moveDirection = Vector3.zero;
    Vector3 originalPlayerSize = Vector3.zero;

    CharacterController characterControllerGO;
    RaycastHit hit;
    RaycastHit hitTower;
    Vector3 forward;

    void Start()
    {
        // Need original values for latter.
        originalPlayerSize = transform.localScale;
        originalGravity = gravity;
        originalSpeed = speed;

        // Get values form the Character Controller.
        characterControllerGO = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // Only need to update is the player is on the ground.
        if (characterControllerGO.isGrounded)
        {
            //Crouching();
            Movement();
            Jump();
            Sprinting();
            FireWeapon();
            TowerInteract();
        }

        // Needed to apply moveDirection to the characterController.
        moveDirection.y -= gravity * Time.deltaTime;
        characterControllerGO.Move(moveDirection * Time.deltaTime);
    }

    void Crouching()
    {
        // If only the Crouch button is being pressed then Crouch.
        if (Input.GetButton("Crouch") && Input.GetButton("Jump") == false)
        {
            // Set the player hight to 1/3 of the players original Size.
            gravity = originalGravity * 10f;
            Vector3 sizeHolder = originalPlayerSize;
            sizeHolder.y /= 3f;
            transform.localScale = sizeHolder;
        }
        // If gravity doesn't equal to the original gravity value and if the current player's localScale doesn't equal to the original size.
        else if (gravity != originalGravity || transform.localScale != originalPlayerSize)
        {
            // Then, set values back to the originals. 
            gravity = originalGravity;
            transform.localScale = Vector3.Lerp(transform.localScale, originalPlayerSize, 0.3f);
        }
    }

    void Jump()
    {
        // if only the jump button is being pressed.
        if (Input.GetButton("Jump") && Input.GetButton("Crouch") == false && Input.GetButton("Sprint") == false)
        {
            // then, jump.
            moveDirection.y = jumpSpeed;
        }
    }

    void Movement()
    {
        // Calculate the moveDirection for the player from the Horizontal and Vertical axis.
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;
    }

    void Sprinting()
    {
        // If only the Sprint button is being pressed.
        if (Input.GetButton("Sprint") && Input.GetButton("Crouch") == false && Input.GetButton("Jump") == false)
        {
            // Then times speed by the speed boost value.
            speed = originalSpeed * speedBoost;
        }
        // if speed doesn't equal the original speed.
        else if (speed != originalSpeed)
        {
            // Then, set speed back to the original.
            speed = originalSpeed;
        }
    }

    void FireWeapon()
    {
        if (Input.GetButton("Shoot"))
        {
            if (Physics.Raycast(transform.position, forward, out hit, 10))
            {
                Debug.Log(hit.transform.tag);

                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<EnemyHealth>().TakeDamage(DamageToEnemy);

                }
            }
        }
    }

    void TowerInteract()
    {
        if (Physics.Raycast(transform.position, forward, out hitTower, 3))
        {
            if (hitTower.transform.tag == "Tower")
            {
                if (hitTower.transform.GetComponent<Tower>().towerActiveOnStart)
                {
                    // Display message to player to repair
                    UIManager.Instance.ToastPopUp(scrapRepairCost);
                    if (Input.GetButton("Interact"))
                    {
                        GameManager.Instance.SubtractScrapFromCount(scrapRepairCost);
                        hitTower.transform.GetComponent<Tower>().repairTower();
                    }
                }
                else
                {
                    // Display message to player to Actave
                    UIManager.Instance.ToastPopUp(scrapActaveCost);
                    if (Input.GetButton("Interact"))
                    {
                        GameManager.Instance.SubtractScrapFromCount(scrapActaveCost);
                        hitTower.transform.GetComponent<Tower>().repairTower();
                    }
                }
            }

            if (hitTower.transform.tag == "Castle")
            {
                // Display message to player to repair
                UIManager.Instance.ToastPopUp(scrapRepairCost);
                if (Input.GetButton("Interact"))
                {
                    GameManager.Instance.SubtractScrapFromCount(scrapRepairCost);
                    // hitTower.transform.GetComponent<Castle>().repairTower();
                }
            }

            if (hitTower.transform.tag != "Tower" && hitTower.transform.tag != "Castle")
            {
                UIManager.Instance.CloseToastPopUp();
            }
        }
    }

    void UseShield()
    {
        if (Input.GetButton("Shield"))
        {

        }
    }

    void OnDrawGizmos()
    {
        forward = transform.TransformDirection(Vector3.forward) * 20;
        Debug.DrawRay(transform.position, forward, Color.green);

        forward = transform.TransformDirection(Vector3.forward) * 3;
        Debug.DrawRay(transform.position, forward, Color.red);
    }
}
