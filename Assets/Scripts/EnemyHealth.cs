using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;     
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;           
    public int currentHealth;                  
    public float sinkSpeed = 2.5f;              
                
    public AudioClip deathClip;  
    Animator anim;                       
    AudioSource enemyAudio;                   
    ParticleSystem hitParticles;    
    PaintBlood paint;            
    public MeshCollider meshCollider;  
    public Rigidbody rigidbody;   
    public GameObject Scrap;
    NavMeshAgent nav;
         
    bool isDead;                               
    bool isSinking;
    bool isRising;
 

    void Start ()
    {
        paint= GetComponent<PaintBlood>();
        // Setting up the references.
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        meshCollider = GetComponent <MeshCollider> ();
        nav = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        currentHealth = startingHealth;
    }

    void Update ()
    {
        if(isSinking)
        {

            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            isSinking = false;
        }
        //if the enemy is spawning in
        if(isRising)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate (Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
    public void TakeDamage (int amount)
    {
        Debug.Log("IS TakingDamage");
        currentHealth -= amount;
        //paint.Paint(this.transform.position);
        if(currentHealth <= 0)
        {
            GameManager.Instance.OnAIDeath();
            Instantiate(Scrap,this.transform, true);
            // ... the enemy is dead.
            Death ();
            
        }
    }


    void Death ()
    {
        isDead = true;
        StartSinking();
    }


    public void StartSinking ()
    {
          // Find and disable the Nav Mesh Agent.
        nav.enabled  = false;
        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        rigidbody.isKinematic  = true;
        meshCollider.isTrigger = true;
        // The enemy should no sink.
        isSinking = true;
        // After 2 seconds destory the enemy.
        Destroy (gameObject, 2f);
    }
}

