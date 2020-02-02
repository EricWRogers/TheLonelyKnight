﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_AI : MonoBehaviour
{
    //Ai for Bird type 
    public int attackDamage;
    Animator anim;          
    Transform enemy;                 
    GameObject player;                                         
    EnemyHealth enemyHealth;                  
    bool playerInRange;                         
    float distToPlayer, minDistPlayer, multiplyBy;
    private NavMeshAgent navAgent;   
    GameObject castle;
    public float radius;
    public float force;
    public GameObject explosion;
    private void Awake ()
    {
        
        enemy = this.transform;
        navAgent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag ("Player");
        castle = GameObject.FindGameObjectWithTag ("Castle");
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator>();
        playerInRange = false;
        minDistPlayer = 7f;

    }
    void Update ()
    {
        IsPlayerClose();
        if(enemyHealth.currentHealth > 0)
        {  
            navAgent.SetDestination (castle.transform.position);
        }
        else
        {
            navAgent.enabled = false;
        }
        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(playerInRange && enemyHealth.currentHealth > 0 ) 
        {
            attackDamage = 30;
            BlowUp();
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
    void BlowUp()
    {
        //explosion effect here!!
        Collider[] colliders = Physics.OverlapSphere(transform.position,radius);
        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb!= null)
            {
                rb.AddExplosionForce(force,transform.position,radius);
            }
            if(nearbyObject.transform.tag == "Player")
            {
                GameManager.Instance.PlayerDamageTaken(attackDamage);
            }
        }
        Explode();
        enemyHealth.TakeDamage(enemyHealth.currentHealth);
    }
    void Explode()
    {
        Instantiate(explosion,this.transform);
    }

}
