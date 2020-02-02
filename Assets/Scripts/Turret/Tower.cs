using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public bool towerActiveOnStart;

    private float towerHealth = 100f;
    private float originaTowerHealth = 100f;

    public GameObject gun;

    public Animator animator;

    void Start()
    {
        if(towerActiveOnStart)
        {
            animator.GetComponent<Animator>().SetBool("TowerActive", true);
            gun.GetComponentInChildren<Turret>().EnableGun();
        }
    }

    void Update()
    {
        if(towerHealth <= 0)
        {
            TowerDestroyed();
        }
    }

    private void TowerDestroyed()
    {
        animator.GetComponent<Animator>().SetBool("TowerActive", false);
        gun.GetComponentInChildren<Turret>().DisableGun();
    }

    public void repairTower()
    {
        animator.GetComponent<Animator>().SetBool("TowerActive", true);
        towerHealth = originaTowerHealth;
        gun.GetComponentInChildren<Turret>().EnableGun();
    }

    public void DamageToTower(float Num)
    {
        towerHealth -= Num;
    }
}
