using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject Bullet;
    private int Bulletx = 1000;
    Rigidbody m_Rigidbody;
    float m_Speed;

    void Start()
    {
    m_Rigidbody = Bullet.GetComponent<Rigidbody>();
    m_Speed = 40.0f;
    }


    // Update is called once per frame
    void Update()
    {
        while(Bulletx >= 0)
        {
            m_Rigidbody.velocity = transform.up * m_Speed;
            Bulletx--;
        }
        
    }
}
