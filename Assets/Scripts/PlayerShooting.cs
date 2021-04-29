using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject bulletPrefab;

    public Transform bulletSpawn;

    private float coolDownShot = 0;

    public float bulletSpeed = 10;
    public float lifeTime = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool fireBullet = Input.GetButtonDown("Fire1");

        coolDownShot -= Time.deltaTime;
        if (coolDownShot <= 0  && fireBullet ) Fire();


    }

    private void Fire()
    {
        coolDownShot = 1f;

        GameObject bullet = Instantiate(bulletPrefab);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());

        bullet.transform.position = bulletSpawn.position;

        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed * 2, ForceMode.Impulse);


        StartCoroutine(DestroyBulletAfterLifeTime(bullet, lifeTime));
    }

    private IEnumerator DestroyBulletAfterLifeTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);

    }
}

