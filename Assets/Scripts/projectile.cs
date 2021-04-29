using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
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
