using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int BulletSpeed = 50;

    public Rigidbody rb;

	void Update () 
    {
        rb.position = (Vector3.right * Time.deltaTime * BulletSpeed);
	}

    void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Enemy")
        {

            other.transform.GetComponent<EnemyHealth>().TakeDamage(15);
        }

        //Destroy(gameObject, 1f);
    }
}
