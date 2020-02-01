using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ranged_Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private float attackRange = 3f;
    private float attackDamage = 3f;
    private float rayDistance = 5.0f;
    private float stoppingDistance = 2.0f;
    
    private Vector3 destination;
    private Quaternion desiredRotation;
    private Vector3 direction;
    private GameObject target;
    private GameObject Castle;
    private EnemyState currentState;
    void Update()
    {
        switch(currentState)
        {
            case EnemyState.GoToPlayer:
            {
                var targetToAttack = CheckForAttack();
                if(targetToAttack != null)
                {
                    target = targetToAttack.GetComponent<CastleHealth>();
                    currentState = EnemyState.Attack;
                }
                transform.LookAt(target.transform);
                transform.Translate(translation: Vector3.forward * Time.deltaTime * 5f);

                break;
            }
            //attack case
            case EnemyState.Attack:
            {
                if(target != null)
                {
                    //blow up 
                    Destroy(target.GameObject);
                }

                //play animation
                //play shooting
                break;
            }
            
        }
    }
    private bool IsPathBlocked()
    {
        Ray ray = new Ray(origin: transform.position, direction);
        var hitSomething = Physics.RaycastAll(ray,rayDistance,layerMask);
        return hitSomething.Any();
    }

    private void GetDestination()
    {
        //ranges to get position in front of player
        Vector3 testPos = (transform.position + (transform.forward * 4f)) +
                               new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f,
                                   UnityEngine.Random.Range(-4.5f, 4.5f));
        destination = new Vector3( testPos.x, y: 1f, testPos.z );
        direction = new Vector3( direction.x, y:0f, direction.z );
        desiredRotation = Quaternion.LookRotation(direction);
    }

    private bool NeedsDestionation()
    {
        if(destination == Vector3.zero)
        {
            return true;
        }
        var distance = Vector3.Distance(transform.position, destination);
        if(distance <= stoppingDistance)
        {
            return true;
        }

        return false;
    }
    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);
    
    private Transform CheckForAttack()
    {
        float attackRadius = 5f;
        
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for(var i = 0; i < 24; i++)
        {
            if(Physics.Raycast(pos, direction, out hit, attackRadius))
            {
                var enemyInSight = hit.collider.GetComponent<player>();
                if(enemyInSight != null)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return enemyInSight.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * attackRadius, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }
    public enum EnemyState
{
    GoToPlayer,
    Attack
}




}
