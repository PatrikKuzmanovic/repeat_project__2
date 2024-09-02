using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 20;
    public float spinSpeed = 100f;
    public float floatHeight = 0.5f;
    public float floatSpeed = 1f;
    public GameObject pickupEffectPrefab;

    private AudioSource healSound;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;

        healSound = GetComponent<AudioSource>();
    }
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

            if (healSound != null)
            {
                healSound.Play();
            }

            if (pickupEffectPrefab != null)
            {
                Instantiate(pickupEffectPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject, healSound != null ? healSound.clip.length : 0f);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
