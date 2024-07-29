using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 100f;
    public int maxBulletsBeforeCooldown = 5;
    public float cooldownDuration = 2f;

    private int bulletsFired = 0;
    private bool isCoolingDown = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCoolingDown)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.forward * bulletSpeed;

        bulletsFired++;

        if (bulletsFired >= maxBulletsBeforeCooldown)
        {
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownDuration);
        bulletsFired = 0;
        isCoolingDown = false;
    }
}
