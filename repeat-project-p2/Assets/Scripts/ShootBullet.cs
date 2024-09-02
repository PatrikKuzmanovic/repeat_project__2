using System.Collections;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 100f;
    public int maxBulletsBeforeCooldown = 5;
    public float cooldownDuration = 2f;

    public GameObject flameGunEffectPrefab;
    public float flameGunDuration = 3f;
    public int flameGunDamage = 5;
    public float flameGunCooldownDuration = 5f;

    private int bulletsFired = 0;
    private bool isCoolingDown = false;
    private bool isFlameGunActive = false;
    private bool isFlameGunShooting = false;
    private bool isFlameGunUnlocked = false;
    private GameObject activeFlameGunEffect;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCoolingDown)
        {
            if (isFlameGunActive)
            {
                StartCoroutine(FlameGunRoutine());
            }
            else
            {
                Shoot();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && isFlameGunShooting)
        {
            StopFlameGun();
        }

        if (Input.GetKeyDown(KeyCode.F) && isFlameGunUnlocked)
        {
            ToggleGun();
        }
    }

    void Shoot()
    {
        if (isCoolingDown) return;

        Debug.Log("Shooting Bullet");
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

    IEnumerator FlameGunRoutine()
    {
        if (isFlameGunShooting) yield break;

        isFlameGunShooting = true;

        StartFlameGun();

        float timeElapsed = 0f;
        while (timeElapsed < flameGunDuration && Input.GetKey(KeyCode.Space))
        {
            timeElapsed += Time.deltaTime;
            UpdateFlameGunPosition();
            yield return null;
        }

        StopFlameGun();
        yield return new WaitForSeconds(flameGunCooldownDuration);
        isCoolingDown = false;
    }

    void StartFlameGun()
    {
        Debug.Log("Starting Flame Gun");
        if (flameGunEffectPrefab != null)
        {
            if (activeFlameGunEffect == null)
            {
                activeFlameGunEffect = Instantiate(flameGunEffectPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                ParticleSystem particleSystem = activeFlameGunEffect.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.Play();
                    Destroy(activeFlameGunEffect, flameGunDuration);
                }
            }
        }
    }

    void StopFlameGun()
    {
        Debug.Log("Stopping Flame Gun");
        if (activeFlameGunEffect != null)
        {
            ParticleSystem particleSystem = activeFlameGunEffect.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Stop();
                Destroy(activeFlameGunEffect, particleSystem.main.duration);
            }
            else
            {
                Destroy(activeFlameGunEffect);
            }
        }
        activeFlameGunEffect = null;
        isFlameGunShooting = false;
        isCoolingDown = true;
    }

    void UpdateFlameGunPosition()
    {
        if (activeFlameGunEffect != null)
        {
            activeFlameGunEffect.transform.position = bulletSpawnPoint.position;
            activeFlameGunEffect.transform.rotation = bulletSpawnPoint.rotation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isFlameGunActive && isFlameGunShooting && other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(flameGunDamage);
            }
        }
    }
    public void SwitchToFlameGun()
    {
        Debug.Log("Switched to Flame Gun");
        isFlameGunActive = true;
    }

    public void SwitchToBullet()
    {
        Debug.Log("Switched to Bullet");
        isFlameGunActive = false;
        StopFlameGun();
    }

    public bool IsFlameGunActive()
    {
        return isFlameGunActive;
    }

    public void UnlockFlameGun()
    {
        Debug.Log("FlameGun Unlocked");
        isFlameGunUnlocked = true;
    }

    private void ToggleGun()
    {
        if (isFlameGunActive)
        {
            SwitchToBullet();
        }
        else
        {
            SwitchToFlameGun();
        }
    }
}
