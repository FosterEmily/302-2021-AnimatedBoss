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
    public Transform hand;
    private Vector3 handStartpos;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

        startingPos = transform.localPosition;
        startingRot = transform.localRotation;

        handStartpos = hand.localPosition;
    }


    void Update()
    {
        SlideArmBack();
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
    private void SlideArmBack()
    {
        transform.localPosition = AnimMath.Slide(transform.localPosition, startingPos, .25f);
   
    }

    void AnimateWalk()
    {
        Vector3 finalPos = startingPos;

        float time = ((Time.time + stepOffset) * playerController.stepSpeed);
        // lateral movement: (z + x)
        float frontToBack = Mathf.Sin(time);
        finalPos += playerController.moveDir * frontToBack * playerController.armScale.z;

        // vertical movement: (y)
        finalPos.y += Mathf.Cos(time) * playerController.armScale.y;
        finalPos.x *= playerController.armScale.x;

        transform.localPosition = finalPos;
      
    }

    void AnimateIdle()
    {
        hand.localPosition = handStartpos;
        transform.localRotation = startingRot;
        hand.localEulerAngles += new Vector3(0, 0, -20f);
        hand.position += -hand.forward * .01f;
    }


}
