using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController pawn;
    //private bool isWalking = false;
    private float moveSpeed = 5;
    public float stepSpeed = 5;
    public Vector3 walkScale = Vector3.one;
    public Vector3 armScale = Vector3.one;

    public AnimationCurve ankleRotationCurve;

    public static float health { get; set; }
    public float healthMax = 100;

    public float gravity = 10;
    public float jumpImpulse = 5;
    private float timeLeftGrounded = 0;
    private float verticalVelocity = 0;

    private Camera cam;

    public States state { get; private set; }
    public Vector3 moveDir { get;  set; }

    public enum States
    {
        Idle,
        Walk
    }
    public bool isGrounded
    {
        get
        {
            return pawn.isGrounded || timeLeftGrounded > 0;
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        state = States.Idle;
        pawn = GetComponent<CharacterController>();

        health = healthMax;
        cam = Camera.main;
    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire3"))
        {
            moveSpeed = 20;
        }
        if (Input.GetButtonUp("Fire3")) moveSpeed = 5;

        bool isJumpHeld = Input.GetButton("Jump");
        bool onJumpPress = Input.GetButtonDown("Jump");


        bool isTryingToMove = (h != 0 || v != 0);

        if (isTryingToMove)
        {
            //print("Lerp");
            float camYaw = cam.transform.eulerAngles.y;
            transform.rotation = AnimMath.Slide(transform.rotation, Quaternion.Euler(0, camYaw, 0), .02f);
        }

        moveDir = transform.forward * v + transform.right * h;
        if (moveDir.sqrMagnitude > 1) moveDir.Normalize();

        pawn.SimpleMove(moveDir * moveSpeed);


        state = (moveDir.sqrMagnitude > .1f) ? States.Walk : States.Idle;


        verticalVelocity += gravity * Time.deltaTime;
        Vector3 moveDelta = moveDir * moveSpeed + verticalVelocity * Vector3.down;
        CollisionFlags flags = pawn.Move(moveDelta * Time.deltaTime);
      
        if (pawn.isGrounded)
        {
            verticalVelocity = 0;// on ground, zero-out vertical-velocity
            timeLeftGrounded = .2f;
        }
        if (isGrounded)
        {
            if (isJumpHeld)
            {
                verticalVelocity = -jumpImpulse;
                timeLeftGrounded = 0;
            }
        }
    }



    public void OnTriggerEnter(Collider other)
    {
        if (this.tag == ("Player") & other.tag == ("Enemy"))
        {
            TakeDamage(50);
            print("Player Damaged");
            // Destroy(gameObject); 
        }
        
    }
    public void TakeDamage(int amt)
    {
        if (amt <= 0) return;
        health -= amt;

        if (health <= 0) Die();
    }
    public void Die()
    {
        print("DEAD");
        Destroy(gameObject);
        
    }

}
