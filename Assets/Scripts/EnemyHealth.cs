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
    public MeshCollider meshCollider;  
    public Rigidbody rigidbody;   
    public GameObject Scrap;
    NavMeshAgent nav;
         
    bool isDead;                               
    bool isSinking;
    bool isRising;
 

    void Start ()
    {
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
        if(currentHealth <= 0)
        {
            //There were two references for death. This was causing the death function to repeat
            //because as soon as currenthealth reached zero the Update just did the Death function
            //repeatedly.


            // ... the enemy is dead ... or in this case overkill Demello. :)
            //Death ();
        }
        if(isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);

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
        currentHealth -= amount;
        
        if(currentHealth <= 0)
        {
            Instantiate(Scrap,this.transform, true);
            // ... the enemy is dead.
            Death ();
        }
    }


    void Death ()
    {
        GameManager.Instance.OnAIDeath();
        meshCollider.isTrigger = true;
        // The enemy is dead.
        isDead = true;
        StartSinking();
        // Turn the collider into a trigger so shots can pass through it.

        // Tell the animator that the enemy is dead.
        //anim.SetTrigger ("Dead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        //enemyAudio.clip = deathClip;
        //enemyAudio.Play ();
    }


    public void StartSinking ()
    {
          // Find and disable the Nav Mesh Agent.
        nav.enabled  = false;
        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        rigidbody.isKinematic  = true;
        // The enemy should no sink.
        isSinking = true;
        // After 2 seconds destory the enemy.
        Destroy (gameObject, 6f);
    }
}

