using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_Script : MonoBehaviour
{
    Transform player;                
    EnemyHealth enemyHealth;        
    UnityEngine.AI.NavMeshAgent nav;               


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        
        if(enemyHealth.currentHealth > 0)
        {
            nav.SetDestination (player.position);
        }
     
        else
        {
            nav.enabled = false;
        }
        
    } 
}

