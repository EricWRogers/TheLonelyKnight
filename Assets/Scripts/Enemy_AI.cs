using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;    
    public int attackDamage = 10;               

    Animator anim;                             
    GameObject player;                         
    // PlayerHealth playerHealth;                 
    EnemyHealth enemyHealth;                  
    bool playerInRange;                         
    float timer;                                


    private void Awake ()
    {
        
        player = GameObject.FindGameObjectWithTag ("Player");
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        playerInRange = true;
    }
    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack ();
        }


    }
    void Attack ()
    {
        timer = 0f;
    }
}
