using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 20;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter by: " + other.gameObject.name);

        Transform parent = other.transform.parent;
        if (parent != null && parent.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");
            PlayerHealth playerHealth = parent.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.IncreaseHealth(healthAmount);
                Debug.Log("Health increased by: " + healthAmount);
            }

            Destroy(gameObject);
        }
    }
}
