using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public bool towerActiveOnStart;

    private float towerHealth = 100f;
    private float originaTowerHealth;

    private GameObject Gun;

    void Start()
    {
        originaTowerHealth = towerHealth;

        if(towerActiveOnStart)
        {
            // Turn on rising Animation.
            Gun.GetComponentInChildren<Turret>().EnableGun();
        }
    }

    private void TowerDestroyed()
    {
        // Turn on falling Animation.
        Gun.GetComponentInChildren<Turret>().DisableGun();
    }

    public void repairTower()
    {
        // Turn on rising Animation.
        towerHealth = originaTowerHealth;
        Gun.GetComponentInChildren<Turret>().EnableGun();
    }

    public void DamageToTower(float Num)
    {
        if(towerHealth > Num)
        {
            towerHealth -= Num;
        }
        else
        {
            TowerDestroyed();
        }
    }
}
