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
            if (Input.GetButton("Interact"))
            {
                GameManager.Instance.SubtractScrapFromCount(scrapRepairCost);
                RepairCastle();
            }
        }

    }

    void RepairCastle()
    {
        GameManager.Instance.RepairCastleHealth();
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
