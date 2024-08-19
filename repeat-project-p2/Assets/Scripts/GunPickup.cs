using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject gunPickupEffectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GunPickup Trigger Enter with: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected and processing pickup");

            // Attempt to find the ShootBullet script in the parent object
            ShootBullet shootBulletScript = other.GetComponent<ShootBullet>();

            if (shootBulletScript != null)
            {
                Debug.Log("ShootBullet component found directly on " + other.gameObject.name);
            }
            else
            {
                // If not found, search for ShootBullet in child objects
                shootBulletScript = other.GetComponentInChildren<ShootBullet>();

                if (shootBulletScript != null)
                {
                    Debug.Log("ShootBullet component found in child of " + other.gameObject.name);
                }
                else
                {
                    Debug.LogWarning("ShootBullet component NOT found on " + other.gameObject.name);
                }
            }

            if (shootBulletScript != null)
            {
                shootBulletScript.UnlockFlameGun();
            }

            if (gunPickupEffectPrefab != null)
            {
                Instantiate(gunPickupEffectPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
