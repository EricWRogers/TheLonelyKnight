using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;    
    public int attackDamage = 10;       
    public int enemyType;   //1.is melee 2. is ranged 3. is suicide    

    Animator anim;          
    Transform enemy;                 
    GameObject player;                         
    GameManager gameManager;                 
    EnemyHealth enemyHealth;                  
    bool playerInRange;                         
    float timer;                                
    float distToPlayer;
    float minDistPlayer;


    private void Awake ()
    {
        enemy = this.transform;
        player = GameObject.FindGameObjectWithTag ("Player");
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        playerInRange = false;

        if(enemyType == 1)
            minDistPlayer = 4f;
        if(enemyType == 2)
            minDistPlayer = 40f;
        if(enemyType == 3)
            minDistPlayer = 3f;

    }
    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        IsPlayerClose();
        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
            
        }


    }
    void IsPlayerClose()
    {
        distToPlayer = Vector3.Distance(enemy.transform.position,player.transform.position);
        if(distToPlayer <= minDistPlayer)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }
    void Shoot()
    {
        //instantiate bullets
    }

    void BlowUp()
    {
        //colliderbased explosion
         enemyHealth.currentHealth -= enemyHealth.currentHealth;
    }

    void Attack ()
    {
        timer = 0f;
        if(enemyType == 1)
        {//based off of the range the enemy and player
            attackDamage = 10;
        }

        if(enemyType == 2)
        {
            attackDamage = 15;
            Shoot();
        }

        if(enemyType == 3)
        {
            attackDamage = 30;
            BlowUp();
        }

        if(gameManager.PlyrHealth > 0)
        {
           
            gameManager.PlayerDamageTaken(attackDamage);
        } 
    }
    void Flee()
    {

    }
}
