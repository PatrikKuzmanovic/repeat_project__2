using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Hit! Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }
    void DestroyEnemy()
    {
        Destroy(gameObject);
        Debug.Log("Enemy destroyed!");
    }
}
