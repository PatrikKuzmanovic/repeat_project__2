using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public int damageAmount = 20;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                Debug.Log("Applying " + damageAmount + " damage to player.");
                playerHealth.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on: " + collision.gameObject.name);
            }
        }
    }
}
