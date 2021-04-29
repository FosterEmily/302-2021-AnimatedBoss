using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        PlayerController player = other.GetComponent<PlayerController>();

        if (player)
        {

            if (PlayerController.health >= 10)
            {
                player.TakeDamage(10);

            }
            Destroy(gameObject);
        }

    }
}
