using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public static float health { get; set; }
    public float healthMax = 100;

    public float moveSpeed = 5;
    public float stepSpeed = 5;
    public Vector3 walkScale = Vector3.one;
    public Vector3 armScale = Vector3.one;
    public enum States
    {
        Idle,
        Walk,
        Attack,
        Death
    }

    //Enemy Detection 
    public Transform headTransform;

    public float headDetectRaduis = 8;
    public float headRangeRaduis = 3;
    private RaycastHit hitTarget;

    //general AI stuff
    public bool isOnRoute;
    public bool isNavPaused;
    public PlayerController PC;
    public Transform myTarget;
    private NavMeshAgent nav;

    //Layer Masks
    public LayerMask playerLayer;
    public LayerMask sightLayer;

    public NavMeshAgent myNavMeshAgent;
    private Transform myTransform;

    private float destroyDelay = 2;


    public States state { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
        state = States.Idle;
        nav = GetComponent<NavMeshAgent>();
        myTransform = transform;

    }

    // Update is called once per frame
    void Update()
    {
        CarryOutDetection();
        if (health <= 0) Die();

       
        if (headTransform == null) headTransform = myTransform;
        // state = (myTarget == null) ? States.Walk : States.Idle ;

        if (state == States.Death) destroyDelay -= Time.deltaTime;
        if (myTarget == null) state = States.Idle;

        print(state);
    }
    bool CanSeeTarget(Transform potentialTarget)
    {
        print("HELP");
        if (Physics.Linecast(headTransform.position, potentialTarget.position, out hitTarget, sightLayer))
        {
        
            if (hitTarget.transform == potentialTarget)
            {
                print("I SEE");
                myTarget = potentialTarget;
               // state = States.Walk;
                return true;
            }
            else  
            {
                print("hello");
                myTarget = null;
                state = States.Idle;
                return false;
            }
        }
        else
        {
            print("World");
            myTarget = null;
            state = States.Idle;
            return false;
        }
    }

    public void CarryOutDetection()
    {
            Collider[] colliders = Physics.OverlapSphere(myTransform.position, headDetectRaduis, playerLayer);
            Collider[] inRange = Physics.OverlapSphere(myTransform.position, headRangeRaduis, playerLayer);
            if (colliders.Length > 0)
            {
                foreach (Collider potentialTargetCollider in colliders)
                {
                    if (CanSeeTarget(potentialTargetCollider.transform))
                    {
                        if (myTarget != null) nav.SetDestination(myTarget.position);
                        //myTarget = null;
                        state = States.Walk;

                        break;
                    }
                }
                
                foreach (Collider potentialTargetCollider in inRange)
                {
                    if (CanSeeTarget(potentialTargetCollider.transform))
                    {

                        print("I WANNA ATTACK");
                        state = States.Attack;
                        break;
                    }
                }
                
            }
        if(myNavMeshAgent.speed == 0) { state = States.Idle; }
    }

    public void TakeDamage(int amt)
    {
        if (amt <= 0) return;
        health -= amt;

        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        print("DEAD");
        state = States.Death;

        if (destroyDelay <= 0)
        {
            Destroy(gameObject);
        }

    }
}
