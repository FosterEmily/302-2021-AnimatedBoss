using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimation : MonoBehaviour
{
    PlayerController playerController;

    private Vector3 startingPos;

    public float stepOffset = 0;
    //private float stepSpeed = 5;

    private Quaternion startingRot;

    private Vector3 targetPos;
    private Quaternion targetRot;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

        startingPos = transform.localPosition;
        startingRot = transform.localRotation;
    }


    void Update()
    {

        switch (playerController.state)
        {
            case PlayerController.States.Idle:
                AnimateIdle();
                break;
            case PlayerController.States.Walk:
                AnimateWalk();
                break;
        }
    }

    void AnimateWalk()
    {
        Vector3 finalPos = startingPos;

        float time = ((Time.time + stepOffset) * playerController.stepSpeed);
        // lateral movement: (z + x)
        float frontToBack = Mathf.Sin(time);
        finalPos += playerController.moveDir * frontToBack * playerController.walkScale.z;

        // vertical movement: (y)
        finalPos.y += Mathf.Cos(time) * playerController.walkScale.y;
        finalPos.x *= playerController.walkScale.x;

        //finalPos.x = playerController.walkScale.x;

        bool isOnGround = (finalPos.y < startingPos.y);

        if (isOnGround) finalPos.y = startingPos.y;


        // convert from z (-1 to 1) to p (0 to 1 to 0)
        float p = 1 - Mathf.Abs(frontToBack);

        float anklePitch = isOnGround ? 0 : -p * 20;

        transform.localPosition = finalPos;
        transform.localRotation = startingRot * Quaternion.Euler(0, 0, anklePitch);
    }

    void AnimateIdle()
    {
        transform.localPosition = startingPos;
        transform.localRotation = startingRot;
        FindGround();
    }

    void FindGround()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, .5f, 0), Vector3.down * 2);

        Debug.DrawRay(ray.origin, ray.direction);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            transform.position = hit.point;
            //transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        }
        else
        {

        }
    }
}
