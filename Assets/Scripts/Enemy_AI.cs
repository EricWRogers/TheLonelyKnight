using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_AI : MonoBehaviour
{
    public float timeBetweenAttacks;    
    public int attackDamage;       
    public int enemyType;   //1.is melee 2. is ranged 3. is suicide    
    Animator anim;          
    Transform enemy;                 
    GameObject player;                                         
    EnemyHealth enemyHealth;                  
    bool playerInRange;                         
    float timer;                                
    float distToPlayer;
    float minDistPlayer;
    float multiplyBy;
    private NavMeshAgent navAgent;   
    public Rigidbody Projectile;
    public GameObject firePoint;
    public float bulletSpeed;
    public float radius;
    public float force;

    private void Awake ()
    {
        enemy = this.transform;
        navAgent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag ("Player");
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator>();
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
        timer += Time.deltaTime;
        IsPlayerClose();
        if(enemyHealth.currentHealth > 0)
        {
            if(enemyType == 2 && playerInRange)
            {
                Vector3 runTo = enemy.position + ((enemy.position - player.transform.position) * 1); 
                navAgent.SetDestination(runTo);
                enemy.LookAt(player.transform);
            }
            else
            {
                navAgent.SetDestination (player.transform.position);
            }
        }
        else
        {
            navAgent.enabled = false;
        }
        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && distToPlayer <= minDistPlayer && enemyHealth.currentHealth > 0 && enemyType != 2) 
        {
            Attack (); 
        }
        else if(enemyType ==2 && distToPlayer >= minDistPlayer && timer >= timeBetweenAttacks)
        {
            Attack();
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
        Rigidbody bulletTemp = Instantiate(Projectile,firePoint.transform.position,firePoint.transform.rotation) as Rigidbody;
        bulletTemp.velocity = transform.TransformDirection(new Vector3(0, 0,bulletSpeed));
        Destroy(bulletTemp, 15f);
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
        //kill the enemy
         enemyHealth.currentHealth -= enemyHealth.currentHealth;
    }

    void Attack ()
    {
        timer = 0f;
        if(enemyType == 1)
        {
            //based off of the range the enemy and player
            attackDamage = 10;
            GameManager.Instance.PlayerDamageTaken(attackDamage);
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
    }  
}
