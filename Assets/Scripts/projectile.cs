using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private float lifespan = .4f;
    private float age = 0;
    void Update()
    {
        age += Time.deltaTime;
        if (age > lifespan)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (this.tag == ("Bullet") & other.tag == ("Enemy"))
        {
            EnemyController.health -= 50;
            print("enemy Damaged");
            Destroy(gameObject); 
        }

    }
}
