using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// I would like some changes here to try and get animations, and it would be nice to have some more complexity for the movement. I do think that you should try and follow something like the following tutorial for some 2d movement ideas: https://www.youtube.com/watch?v=dwcT-Dch0bA&t=0s. Some suggestion for other platform ideas: traps (timed spikes or other damage) and something like moving platforms. Also consider having platforms that spin on their own rather than ones that the player spins, and potentially platforms you could bounce off of. 
/// For comments in your code, I do not think it is necessary to comment every single line of code, especially when it is obvious. Things like summaries can be good for letting me know what the general idea for some things are, or to explain more complex logic. Your comments, as is, I don't read in full because some things are intuitive (health, for example).
/// I like the character bouncing off of the seal but enhance this effect, and try to think about how it could be implemented in level design. A lot of the ways I like this type of platforming are done in a lot of Super Mario World rom hacks (see this video for some rom hack examples: https://www.youtube.com/watch?v=bS-t4rWma50_
/// For 2d animation here is a link: https://www.youtube.com/watch?v=hkaysu1Z-N8. In fact, all of these old Brackey videos will be helpful: https://www.youtube.com/watch?v=on9nwbZngyw.
/// </summary>
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
    private MenuThings menuThings;

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
    [SerializeField] private GameObject gameOverScreen;

    //friction changes


    void Start()
    {
        currentLivesText = GameObject.Find("PlayerLives").GetComponent<TextMeshProUGUI>();
        menuThings = GameObject.Find("Canvas").GetComponent<MenuThings>();
        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // Get horizontal input from the player

        Move(); // Move the player horizontally
        Jump(); // Handle player jumping
        Flip(); // Flip the player sprite based on movement direction
        ThrowSnowball();

        if(health <= 0)
        {
            Die();
        }
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
        currentLivesText.text = "Lives: " + health.ToString();
        if (isAttackable)
        {
            if (gameObject.layer == LayerMask.NameToLayer("PlayerInvulnerable"))
            {
                return; // Do nothing if the player is invulnerable
            }

            health--; // Decrease player's health

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
        gameOverScreen.SetActive(true);
        Destroy(gameObject);
    }
}
