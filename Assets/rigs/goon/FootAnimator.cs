using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// this script animates the foot & the legs
/// by changing the local position of this objects(IK target).
/// </summary>
public class FootAnimator : MonoBehaviour
{
    PlayerController playerController;

    private Vector3 startingPos;

    public float stepOffset = 0;
    private float stepSpeed = 5;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

        startingPos = transform.localPosition;
    }

 
    void Update()
    {
        AnimateWalk();
        
    }

    void AnimateWalk()
    {
        Vector3 finalPos = startingPos;

        float time = ((Time.time + stepOffset) * stepSpeed);

        finalPos.z += Mathf.Sin(time) * playerController.walkScale.z;
        finalPos.y += Mathf.Cos(time) * playerController.walkScale.y;

        finalPos.x *= playerController.walkScale.x;

        if (finalPos.y < startingPos.y) finalPos.y = startingPos.y;

        transform.localPosition = finalPos; 
    }

    void AnimateIdle()
    {
        transform.localPosition = startingPos;
    }
}
