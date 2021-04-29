using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController pawn;
    private bool isWalking = false;
    public float walkSpeed = 5;

    public Vector3 walkScale = Vector3.one;

    void Start()
    {
        pawn = GetComponent<CharacterController>();
    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.forward * v + transform.right * h;
        if (moveDir.sqrMagnitude > 1) moveDir.Normalize();

        pawn.SimpleMove(moveDir * walkSpeed);
    }
}
