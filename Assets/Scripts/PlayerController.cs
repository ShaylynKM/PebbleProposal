using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Variables for player movement and behavior
    private float horizontal; // Input for horizontal movement
    private float speed = 8f; // Movement speed
    private float maxSpeed = 12f; // The fastest the player can travel on ice
    private float jumpingPower = 7f; // Jumping force
    private float hurtTime = 1f; // Duration of invulnerability after getting hurt
    private float hurtRecoverTime = 3f; // Total duration of hurt recovery time
    public int health = 5; // Player's health
    public TextMeshProUGUI currentLivesText;

    // Flags for player behavior
    private bool isAttackable = true; // Flag to determine if the player can take damage
    private bool isInputEnabled = true; // Flag to determine if player input is enabled
    private bool isFacingRight = true; // Flag to determine if the player is facing right
    private bool isJumping = false; // Flag to determine if the player is jumping
    private bool isMoving = false; // Whether or not player is moving

    // References to components and objects in the scene
    [SerializeField] private Rigidbody2D rb; // Reference to the player's Rigidbody2D component
    [SerializeField] private Transform groundCheck; // Reference to the ground check position
    [SerializeField] private LayerMask groundLayer; // Layer mask to determine what is considered ground
    [SerializeField] private GameObject snowballPrefab; // Reference to the snowball prefab
    [SerializeField] private float throwSpeed = 20f; // Force applied to the thrown snowball

    void Start()
    {
        currentLivesText = GameObject.Find("PlayerLives").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // Get horizontal input from the player

        Move(); // Move the player horizontally
        Jump(); // Handle player jumping
        Flip(); // Flip the player sprite based on movement direction
        ThrowSnowball();
    }

    private void Move()
    {
        if (isInputEnabled)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // Set the player's velocity for horizontal movement
            isMoving = true;
        }
        else 
        {
            isMoving = false;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded() && isInputEnabled)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower); // Apply jumping force to the player
            isJumping = true; // Set the jumping flag to true
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f && isInputEnabled)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); // Reduce the player's upward velocity when the jump button is released
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight; // Reverse the facing direction
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // Flip the player's sprite horizontally
            transform.localScale = localScale;
        }
    }

    private void ThrowSnowball()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Instantiate a snowball at the player's position
            GameObject snowball = Instantiate(snowballPrefab, transform.position, Quaternion.identity);

            // Get the Rigidbody2D component of the snowball
            Rigidbody2D snowballRb = snowball.GetComponent<Rigidbody2D>();

            // Calculate the throw direction based on the cursor position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 throwDirection = (mousePosition - transform.position).normalized;

            // Calculate the initial velocity based on the throw direction and desired distance
            Vector2 initialVelocity = throwDirection * throwSpeed;

            // Apply the initial velocity to the snowball
            snowballRb.velocity = initialVelocity;

            // Destroy the snowball after a certain time (e.g., 3 seconds) to avoid cluttering the scene
            Destroy(snowball, 3f);
        }
    }

    private bool IsGrounded()
    {
        // Use a circle overlap to check if the player is touching the ground
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public bool IsJumping()
    {
        return isJumping; // Return the jumping flag
    }

    public void TakeDamage()
    {
        if (isAttackable)
        {
            if (gameObject.layer == LayerMask.NameToLayer("PlayerInvulnerable"))
            {
                return; // Do nothing if the player is invulnerable
            }

            health--; // Decrease player's health
            Debug.Log(health);

            // Add code to trigger hurt animation

            if (health <= 0)
            {
                Die(); // Call the Die function when the player's health reaches zero or below
            }
            else
            {
                // Disable player input temporarily
                isInputEnabled = false;

                // Start the invulnerability coroutine
                StartCoroutine(MakePlayerInvulnerable());

                currentLivesText.text = "Lives: " + health.ToString();
            }
        }
    }

    private IEnumerator MakePlayerInvulnerable()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerInvulnerable"); // Set the player's layer to "PlayerInvulnerable"
        isAttackable = false; // Disable attack while invulnerable

        yield return new WaitForSeconds(hurtTime); // Wait for the hurtTime duration

        isInputEnabled = true; // Enable player input

        yield return new WaitForSeconds(hurtRecoverTime - hurtTime); // Wait for the remaining hurt recovery time

        gameObject.layer = LayerMask.NameToLayer("Player"); // Reset the player's layer to default
        isAttackable = true; // Enable attack after the invulnerability period
    }

    public void KillEnemy()
    {
        // Add code to trigger kill animation or any other effects

        // Add code for bounce effect
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower * 0.5f); // Apply a bounce effect by adding vertical force

        isJumping = false; // Reset the flag after killing an enemy
    }

    public void BounceOnEnemy()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower); // Apply upward force to bounce on an enemy
        isInputEnabled = false; // Disable player input temporarily
    }

    private void EnableInput()
    {
        isInputEnabled = true; // Enable player input
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
