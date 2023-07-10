using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player.IsJumping())
            {
                // Player jumped on top of the enemy
                player.KillEnemy();
                Destroy(transform.parent.gameObject); // Destroy the enemy
            }
        }
    }
}
