using System.Collections;
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
    bool playerInRange,castleInRange;                         
    float distToPlayer,distToCastle, minDistCastle, minDistPlayer, multiplyBy;
    public NavMeshAgent navAgent;   
    GameObject castle;
    public float radius;
    public float force;
    public GameObject explosion;
    private void Awake ()
    {
        
        enemy = this.transform;
        player = GameObject.FindGameObjectWithTag ("Player");
        castle = GameObject.FindGameObjectWithTag ("Castle");
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator>();
        playerInRange = false;
        castleInRange = false;
        minDistPlayer = 7f;
        minDistCastle = radius;

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
        if((castleInRange || playerInRange) && enemyHealth.currentHealth > 0 ) 
        {
            attackDamage = 30;
            BlowUp();
        }
    }
    void IsPlayerClose()
    {
        distToPlayer = Vector3.Distance(enemy.transform.position,player.transform.position);
        distToCastle = Vector3.Distance(enemy.transform.position,castle.transform.position);

        if(distToPlayer <= minDistPlayer)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if(distToCastle <= minDistCastle)
        {
            castleInRange = true;
        }
        else
        {
            castleInRange = false;
        }
    }
    void BlowUp()
    {
        //explosion effect here!!
        Collider[] colliders = Physics.OverlapSphere(transform.position,radius);
        foreach(Collider nearbyObject in colliders)
        {
            if(nearbyObject.transform.tag == "Player")
            {
                Debug.Log("explosion on player");
                GameManager.Instance.PlayerDamageTaken(attackDamage);
            }
            if(nearbyObject.transform.tag == "Castle")
            {
                Debug.Log("explosion on castle");
                GameManager.Instance.CastleDamageIsHere(attackDamage);
            }
        }
        Explode();
        Debug.Log("explosion in the castle");
        enemyHealth.TakeDamage(enemyHealth.currentHealth);
    }
    void Explode()
    {
        Instantiate(explosion,this.transform);
    }

    void OnDestroy()
    {
        GameManager.Instance.AddScrapToCount(3);
        GameManager.Instance.OnAIDeath();
    }

}
