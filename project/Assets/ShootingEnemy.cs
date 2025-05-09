using UnityEngine;
using System.Collections;
using Pathfinding; // Ensure you have the A* Pathfinding Project installed

public class ShootingEnemy : MonoBehaviour
{
    public int health = 100; // Enemy's health
    private bool canBeHit = true; // Cooldown flag to prevent multiple hits
    public float hitCooldown = 0.5f; // Cooldown duration in seconds
    public float flashDuration = 0.2f; // Duration for the red flash
    public float deathAnimationDuration = 0.3f; // Duration of the death animation

    public RuntimeAnimatorController walkController; // Assign in Unity Inspector
    public RuntimeAnimatorController deathController; // Assign in Unity Inspector
    public RuntimeAnimatorController attackController; // Assign in Unity Inspector

    public float attackAnimationDuration = 0.3f; // Duration of the attack animation
    public float attackCooldown = 0.5f; // Cooldown duration between attacks

    public GameObject redBallPrefab; // Reference to the red ball prefab
    public float ballSpeed = 10f; // Speed of the red ball

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private Animator animator; // Reference to the Animator component
    private bool isAttacking = false; // Flag to check if the enemy is currently attacking
    private float attackCooldownTimer = 0f; // Timer to handle attack cooldown
    private Transform player; // Reference to the player
    public float attackRange = 1.5f; // Range within which the enemy can attack

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private MonoBehaviour aiMovementScript; // Reference to the AI movement script
    public AIPath aiPath;
    public float scale = 1f;

    // coin prefab to spawn
    public GameObject coinPrefab; // Assign in Unity Inspector
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        aiMovementScript = GetComponent<Pathfinding.AIPath>(); // Replace with your AI movement script type
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
    }

    void Update()
    {

        if (player == null || player.transform == null)
        {
            return; // Exit Update if player or its transform is null
        }

        // Handle attack cooldown
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        // Check if the enemy can attack
        if (!isAttacking && attackCooldownTimer <= 0 && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            StartCoroutine(HandleAttack());
        }

        // Flip the enemy based on movement direction
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-scale, scale, scale);
        }
    }

    // Reduce health by the given value
    public void LoseHealth(int damage)
    {
        if (!canBeHit) return; // Ignore if the enemy is in cooldown

        health -= damage;
        canBeHit = false; // Start cooldown

        StartCoroutine(FlashRed()); // Flash red when hit

        if (health <= 0)
        {
            // Change animation controller to death controller
            animator.runtimeAnimatorController = deathController;
            Instantiate(coinPrefab, transform.position, Quaternion.identity); // Spawn coin prefab
            // Destroy the enemy after the death animation duration
            Destroy(gameObject, deathAnimationDuration);
        }
        else
        {
            StartCoroutine(HitCooldownCoroutine()); // Start cooldown timer
        }
    }

    private System.Collections.IEnumerator HitCooldownCoroutine()
    {
        yield return new WaitForSeconds(hitCooldown); // Wait for the cooldown duration
        canBeHit = true; // Allow the enemy to be hit again
    }

    private System.Collections.IEnumerator FlashRed()
    {
        // Change the sprite color to red
        spriteRenderer.color = Color.red;

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Revert the sprite color to its original color
        spriteRenderer.color = Color.white;
    }

    private IEnumerator HandleAttack()
    {
        isAttacking = true; // Set attacking flag
        animator.runtimeAnimatorController = attackController; // Play attack animation

        // Temporarily disable AI movement
        if (aiMovementScript != null) aiMovementScript.enabled = false;

        // Stop Rigidbody2D movement
        if (rb != null) rb.linearVelocity = Vector2.zero;

        // Wait for the first half of the attack animation
        yield return new WaitForSeconds(attackAnimationDuration / 2);

        // Instantiate the red ball rotated -90 degrees on Z axis
        GameObject redBall = Instantiate(redBallPrefab, transform.position, Quaternion.Euler(0f, 0f, -90f));

        // Set the red ball's direction and speed
        Rigidbody2D redBallRb = redBall.GetComponent<Rigidbody2D>();
        if (redBallRb != null)
        {
            // Determine the direction based on the enemy's facing direction
            float direction = transform.localScale.x > 0 ? 1f : -1f;
            redBallRb.linearVelocity = new Vector2(direction * ballSpeed, 0f);

            // Apply the direction to the red ball's scale
            redBall.transform.localScale = new Vector3(direction * Mathf.Abs(redBall.transform.localScale.x), redBall.transform.localScale.y, redBall.transform.localScale.z);
        }

        // Wait for the second half of the attack animation
        yield return new WaitForSeconds(attackAnimationDuration / 2);

        isAttacking = false; // Reset attacking flag

        // Reset animator to walkController
        animator.runtimeAnimatorController = walkController;

        // Re-enable AI movement
        if (aiMovementScript != null) aiMovementScript.enabled = true;

        // Start the attack cooldown timer
        attackCooldownTimer = attackCooldown;
    }
}
