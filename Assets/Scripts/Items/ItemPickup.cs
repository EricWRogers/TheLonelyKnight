using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    void Awake()
    {
        transform.parent = null;
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.AddScrapToCount((int)Random.Range(2f, 5f));

            Destroy(gameObject);
        }
    }
}
