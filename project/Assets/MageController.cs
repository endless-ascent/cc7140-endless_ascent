using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed for left and right movement
    public float jumpForce = 10f; // Force applied for jumping
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Animator animator; // Reference to the Animator component
    private bool isGrounded = true; // Check if the player is on the ground
    private bool canAttack = true; // Cooldown flag for attacking
    private bool isAttacking = false; // Flag to check if the player is currently attacking
    private bool isDead = false; // Flag to check if the player is dead

    public RuntimeAnimatorController idleController; // Assign in Unity Inspector
    public RuntimeAnimatorController runController; // Assign in Unity Inspector
    public RuntimeAnimatorController jumpController; // Assign in Unity Inspector
    public RuntimeAnimatorController attackController; // Assign in Unity Inspector
    public RuntimeAnimatorController deathController; // Assign in Unity Inspector

    public float scale = 1f; // Scale factor for the player sprite
    public int health = 100; // Player's health
    private bool canBeHit = true; // Cooldown flag to prevent multiple hits
    public float hitCooldown = 0.5f; // Cooldown duration in seconds
    public float flashDuration = 0.2f; // Duration for the red flash
    public float deathAnimationDuration = 0.3f; // Duration of the death animation
    public float attackAnimationDuration = 0.3f; // Duration of the attack animation
    public float attackCooldown = 0.5f; // Cooldown duration between attacks

    public GameObject fireballPrefab; // Reference to the Fireball prefab
    public float fireballSpeed = 10f; // Speed of the fireball

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead) // Only allow movement and actions if the player is not dead
        {
            if (!isAttacking) // Only allow movement and jumping if not attacking
            {
                HandleMovement();
            }

            if (canAttack && Input.GetMouseButtonDown(0)) // MouseButton1 (left mouse button)
            {
                StartCoroutine(HandleAttack());
            }
            else if (!isAttacking) // Only handle other animations if not attacking
            {
                HandleAnimation();
            }
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // Get raw input for A/D keys

        // Move left or right
        if (horizontalInput != 0)
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

            // Flip the player sprite if moving left
            if (horizontalInput < 0)
                transform.localScale = new Vector3(-scale, scale, scale);
            else
                transform.localScale = new Vector3(scale, scale, scale);
        }
        else
        {
            // Stop horizontal movement when no key is pressed
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;

            // Start a coroutine to reset isGrounded after 1.5 seconds
            StartCoroutine(ResetIsGrounded());
        }
    }

    private IEnumerator ResetIsGrounded()
    {
        // Wait for 1.5 seconds
        yield return new WaitForSeconds(1f);

        // Reset isGrounded to true
        isGrounded = true;
    }

    private void HandleAnimation()
    {
        if (isDead)
        {
            animator.runtimeAnimatorController = deathController; // Prioritize death animation
        }
        else if (!isGrounded)
        {
            animator.runtimeAnimatorController = jumpController;
        }
        else if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            animator.runtimeAnimatorController = runController;
        }
        else
        {
            animator.runtimeAnimatorController = idleController;
        }
    }

    private IEnumerator HandleAttack()
    {
        isAttacking = true; // Set attacking flag
        canAttack = false; // Disable attacking during cooldown
        rb.linearVelocity = Vector2.zero; // Stop player movement
        animator.runtimeAnimatorController = attackController; // Play attack animation

        // Wait for the first half of the attack animation
        yield return new WaitForSeconds(attackAnimationDuration / 2);

        // Instantiate the fireball
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        // Set the fireball's direction and speed
        Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();
        if (fireballRb != null)
        {
            // Determine the direction based on the Mage's facing direction
            float direction = transform.localScale.x > 0 ? 1f : -1f;
            fireballRb.linearVelocity = new Vector2(direction * fireballSpeed, 0f);

            // Apply the direction to the fireball's scale
            fireball.transform.localScale = new Vector3(direction * -Mathf.Abs(fireball.transform.localScale.x), fireball.transform.localScale.y, fireball.transform.localScale.z);
        }

        // Wait for the second half of the attack animation
        yield return new WaitForSeconds(attackAnimationDuration / 2);

        isAttacking = false; // Reset attacking flag

        // Wait for the remaining cooldown duration
        yield return new WaitForSeconds(attackCooldown - attackAnimationDuration);

        canAttack = true; // Re-enable attacking
    }

    public void LoseHealth(int damage)
    {

        if (!canBeHit) return; // Ignore if the player is in cooldown


        health -= damage;
        canBeHit = false; // Start cooldown

        StartCoroutine(FlashRed()); // Flash red when hit

        if (health <= 0)
        {
            isDead = true; // Set the player as dead
            HandleAnimation(); // Trigger death animation
            Destroy(gameObject, deathAnimationDuration); // Destroy the player after the death animation
        }
        else
        {
            StartCoroutine(HitCooldownCoroutine()); // Start cooldown timer
        }
    }

    private IEnumerator HitCooldownCoroutine()
    {
        yield return new WaitForSeconds(hitCooldown); // Wait for the cooldown duration
        canBeHit = true; // Allow the player to be hit again
    }

    private IEnumerator FlashRed()
    {
        // Change the sprite color to red
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = Color.red;

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Revert the sprite color to its original color
        spriteRenderer.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
