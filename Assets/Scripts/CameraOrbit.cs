using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public PlayerController moveScript;



    private Camera cam;

    private float yaw = 0;
    private float pitch = 0;

    public float cameraSenX = 10;
    public float cameraSenY = 10;


    void Start()
    {

        cam = GetComponentInChildren<Camera>();

    }


    void Update()
    {
        PlayerOrbitCamera();
        transform.position = moveScript.transform.position;
    }


    private void PlayerOrbitCamera()
    {
        float mx = Input.GetAxisRaw("Mouse X");
        float my = Input.GetAxisRaw("Mouse Y");

        yaw += mx * cameraSenX;
        pitch += my * cameraSenY;

        pitch = Mathf.Clamp(pitch, 0, 15);
        //yaw = Mathf.Clamp(yaw, -30, 30);
        transform.rotation = AnimMath.Slide(transform.rotation, Quaternion.Euler(pitch, yaw, 0), .001f);
    }
}
