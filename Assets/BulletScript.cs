﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int BulletSpeed = 50;
    public Rigidbody rb;

	void Awake () 
    {
        Destroy(this.gameObject ,3.0f);
	}

    void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Enemy")
        {

            other.transform.GetComponent<EnemyHealth>().TakeDamage(15);
        }
    }
}
