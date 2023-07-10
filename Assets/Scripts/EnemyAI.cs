using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health = 3; // Enemy's health
    public float speed = 20; // Speed at which the enemy moves
    public LayerMask enemyMask; // Layer mask to detect other enemies
    private Transform enemyTransform;
    private float width; // Width of the enemy's sprite

    [SerializeField] private Rigidbody2D rb; // Reference to the enemy's Rigidbody2D component
    [SerializeField] private Transform groundCheck; // Reference to the ground check position
    [SerializeField] private LayerMask groundLayer; // Layer mask to determine what is considered ground

    private bool isMovingRight = true; // Track the movement direction of the enemy

    private void Start()
    {
        enemyTransform = GetComponent<Transform>(); // Get the reference to the enemy's transform component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        width = spriteRenderer.bounds.size.x; // Get the width of the enemy's sprite
    }

    private void FixedUpdate()
    {
        Vector2 lineCastPosition = enemyTransform.position - enemyTransform.right * width;
        Debug.DrawLine(lineCastPosition, lineCastPosition + Vector2.down);

        if (!IsGrounded()) // Check if the enemy is not grounded
        {
            FlipEnemy(); // Flip the enemy's movement direction
        }

        if (isMovingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y); // Move the enemy to the right
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y); // Move the enemy to the left
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die(); // Call the Die function when the enemy's health reaches zero or below
        }
        else
        {
            // Add code for damage animation or effects
        }
    }

    private void FlipEnemy()
    {
        isMovingRight = !isMovingRight; // Reverse the movement direction of the enemy
        Vector3 currentScale = enemyTransform.localScale;
        currentScale.x *= -1; // Flip the enemy's sprite horizontally
        enemyTransform.localScale = currentScale;
    }

    private bool IsGrounded()
    {
        // Use a circle overlap to check if the enemy is touching the ground
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void Die()
    {
        Destroy(gameObject); // Destroy the enemy game object
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Check if the enemy collides with the player
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>(); // Get the PlayerController component from the player object

            if (IsPlayerAbove(collision.contacts[0].point) && rb.velocity.y <= 0f)
            {
                // Player jumped on top of the enemy
                player.KillEnemy(); // Call the KillEnemy function of the player
                Die(); // Call the Die function of the enemy
            }
            else
            {
                // Enemy collided with the player from other directions
                player.TakeDamage(); // Call the TakeDamage function of the player
            }
        }
    }

    private bool IsPlayerAbove(Vector2 collisionPoint)
    {
        return collisionPoint.y > enemyTransform.position.y; // Check if the collision point is above the enemy's position
    }
}
