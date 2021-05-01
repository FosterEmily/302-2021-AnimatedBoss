using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headRotate : MonoBehaviour
{

    public float rotate = 0f;
    public bool start = true;
    private Quaternion startPos;
    public Transform mytransform;

    PlayerController playerController;
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

    }
        void Update()
    {
        startPos = mytransform.localRotation;



        switch (playerController.state)
        {
            case PlayerController.States.Idle:
                HeadRotate();
                break;
            case PlayerController.States.Walk:
                //transform.rotation = startPos;
                break;
        }
    }

    public void HeadRotate()
    {
        transform.rotation = Quaternion.Euler(0, rotate + mytransform.localRotation.y, 0);
        print(rotate);
        if (start)
        {
            rotate += Time.deltaTime * 50;
            if (rotate >= 35) start = false;

        }
        if (!start)
        {
            rotate -= Time.deltaTime * 50;
            if (rotate <= -35) start = true;
        }
    }
}

