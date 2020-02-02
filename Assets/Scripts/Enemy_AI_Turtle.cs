using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_Turtle : MonoBehaviour
{  
    public int attackDamage;       
    Animator anim;          
    Transform enemy;                 
    GameObject player;                                         
    EnemyHealth enemyHealth;                  
    bool playerInRange;                         
    float distToPlayer, minDistPlayer, multiplyBy;
    private UnityEngine.AI.NavMeshAgent navAgent;   
    GameObject castle;
    public GameObject particle;
    


    private void Awake ()
    {
        
        enemy = this.transform;
        navAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag ("Player");
        castle = GameObject.FindGameObjectWithTag ("Castle");
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator>();
        playerInRange = false;
        minDistPlayer = 30f;


    }
    void Update ()
    {
        IsPlayerClose();
        if(enemyHealth.currentHealth > 0)
        {
            navAgent.SetDestination (player.transform.position);
        }
        else
        {
            navAgent.enabled = false;
        }
        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(playerInRange && enemyHealth.currentHealth > 0 ) 
        {
            attackDamage = 15;
            Shoot();
            if(distToPlayer <= 7) GameManager.Instance.PlayerDamageTaken(attackDamage);
        }
        else 
        {
            particle.SetActive(false);
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
        particle.SetActive(true);
    }
}  

