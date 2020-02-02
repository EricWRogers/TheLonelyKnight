using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 15f;
    public float fireRate = 1f;
	public float turnSpeed = 10f;

    public float bulletSpeed = 100f;

    public Transform partToRotate;

    public Transform bulletHolder;

    public GameObject bullet;

    private bool GunActive = false;

    private float fireCountdown = 0.0f;

    private Transform target;

    private Vector3 originalPosition;
    private Vector3 forward = Vector3.zero;

    private Quaternion originalRotation;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(GunActive != false)
        {
            LockOnTarget();
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			nearestEnemy.transform.GetComponent<EnemyHealth>().TakeDamage(50);
		} else
		{
			target = null;
        }
	}

    void LockOnTarget()
	{
        if(target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

            partToRotate.rotation = Quaternion.Euler( rotation.x, rotation.y, 0f);
        } 
	}

    void Shoot()
    {

        GameObject bulletClone =  Instantiate(bullet, bulletHolder.position, bulletHolder.rotation);
        bulletClone.GetComponent<Rigidbody>().AddForce(bulletClone.transform.right * bulletSpeed, ForceMode.Impulse);
        BulletScript.Destroy(bullet,3.0f);

    }

    void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

    public void DisableGun()
    {
        GunActive = false;
    }

    public void EnableGun()
    {
        GunActive = true;
    }

}
