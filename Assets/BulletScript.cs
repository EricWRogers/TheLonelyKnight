using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Enemy")
        {
            other.transform.GetComponent<EnemyHealth>().TakeDamage(15);
        }

        Destroy(gameObject);
    }
}
