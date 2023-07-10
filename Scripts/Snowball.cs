using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public int damage = 1; // Amount of damage caused by the snowball

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyAI enemy = collision.GetComponent<EnemyAI>(); // Get the EnemyAI component from the enemy object
            enemy.TakeDamage(damage); // Call the TakeDamage function of the enemy and pass the damage amount
            Destroy(gameObject); // Destroy the snowball
        }
    }
}