using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Vector3 respawnPosition;

    public GameObject explosionPrefab;
    private void Start()
    {
        currentHealth = maxHealth;
        respawnPosition = transform.position;
        Debug.Log("Initial Respawn Position: " + respawnPosition);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Hit! Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine(RespawnEnemy());
        }
    }

    public void TakeBurnDamage(int damage, float duration)
    {
        StartCoroutine(ApplyBurnDamage(damage, duration));
    }

    private IEnumerator ApplyBurnDamage(int damage, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            TakeDamage(damage);
            elapsedTime += 1f;
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator RespawnEnemy()
    {

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        currentHealth = maxHealth;
        transform.position = respawnPosition;

        gameObject.SetActive(true);
    }

    //void DestroyEnemy()
    //{
    //Destroy(gameObject);
    //Debug.Log("Enemy destroyed!");
    //}
}
