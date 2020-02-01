using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
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
                GameManagerGO.OnAIDeath();
                Destroy(gameObject);

        }
    }
}
