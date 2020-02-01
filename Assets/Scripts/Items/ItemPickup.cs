using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    GameManager GameManagerGO;

    void Awake() 
    {
        GameManagerGO = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // Need to add function to gameManager to display a message to player. "Press F to Collect"
            if(Input.GetButton("Interact"))
            {
                // Need to add function to gameManager to add scrap and display on screen.
                Destroy(gameObject);
            }
        }
    }
}
