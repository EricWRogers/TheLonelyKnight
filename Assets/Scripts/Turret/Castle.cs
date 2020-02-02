using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float scrapRepairCost = 20;

    bool ifInTrigger = false;

    void Update()
    {
        if(ifInTrigger)
        {
            if (Input.GetButtonDown("Interact") && GameManager.Instance.castleHealth < 100)
            {
                if(GameManager.Instance.scrapCount >= scrapRepairCost)
                {
                    GameManager.Instance.SubtractScrapFromCount(scrapRepairCost);
                    GameManager.Instance.RepairCastleHealth();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.transform.tag == "Player")
        {
            UIManager.Instance.ToastPopUp(scrapRepairCost);
            ifInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.transform.tag == "Player")
        {
            UIManager.Instance.CloseToastPopUp();
            ifInTrigger = false;
        }
    }
}
