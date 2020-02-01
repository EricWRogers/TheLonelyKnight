using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_AI_Script : MonoBehaviour
{
    Transform player;                
    EnemyHealth enemyHealth;        
    NavMeshAgent nav;   

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();

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

