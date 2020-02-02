using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float scrapRepairCost = 20;

    void Update()
    {
        if (Input.GetButton("Interact"))
        {
            GameManager.Instance.SubtractScrapFromCount(scrapRepairCost);
            RepairCastle();
        }
    }

    void RepairCastle()
    {
        GameManager.Instance.RepairCastleHealth();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (hitTower.transform.tag == "Player")
        {
            UIManager.Instance.ToastPopUp(scrapRepairCost);
        }
    }

    void OnTriggerExit(Collider other) 
    {
        
    }
}
