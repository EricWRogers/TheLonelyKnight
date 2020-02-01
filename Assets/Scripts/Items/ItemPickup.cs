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
            GameManagerGO.AddScrapToCount((int)Random.Range(2f, 5f));
            Destroy(gameObject);
        }
    }
}
