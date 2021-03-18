using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianMovement : MonoBehaviour
{
    public Animator animMachine;
    void Start()
    {
        animMachine = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxisRaw("Vertical");

        animMachine.SetFloat("currentSpeed", speed);

        transform.position += transform.forward * speed * Time.deltaTime * 10;
    }
}
