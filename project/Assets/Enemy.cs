using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Pathfinding; // Ensure you have the A* Pathfinding Project installed

public class Enemy : MonoBehaviour
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

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private Animator animator; // Reference to the Animator component
    private bool isAttacking = false; // Flag to check if the enemy is currently attacking
    private bool canAttack = true; // Cooldown flag for attacking
    private Transform player; // Reference to the player
    
    public float attackRange = 1.5f; // Range within which the enemy can attack

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private MonoBehaviour aiMovementScript; // Reference to the AI movement script
    public AIPath aiPath;
    public float scale = 1f;

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

        if (!isAttacking && canAttack && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            StartCoroutine(HandleAttack());
        }

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
        SoundEffectManager.Play("HitEnemy");
        // Revert the sprite color to its original color
        spriteRenderer.color = Color.white;
    }

    private IEnumerator HandleAttack()
    {
        isAttacking = true; // Set attacking flag
        canAttack = false; // Disable attacking during cooldown
        animator.runtimeAnimatorController = attackController; // Play attack animation

        // Temporarily disable AI movement
        if (aiMovementScript != null) aiMovementScript.enabled = false;

        // Stop Rigidbody2D movement
        if (rb != null) rb.linearVelocity = Vector2.zero;

        // Reference to the EnemySwordHitBox GameObject
        GameObject enemySwordHitBox = transform.Find("EnemySwordHitBox").gameObject;

        // Wait for the first half of the attack animation
        yield return new WaitForSeconds(attackAnimationDuration / 2);

        // Enable the EnemySwordHitBox
        enemySwordHitBox.SetActive(true);

        // Wait for the second half of the attack animation
        yield return new WaitForSeconds(attackAnimationDuration / 2);

        // Disable the EnemySwordHitBox
        enemySwordHitBox.SetActive(false);

        isAttacking = false; // Reset attacking flag

        // Reset animator to walkController
        animator.runtimeAnimatorController = walkController;

        // Re-enable AI movement
        if (aiMovementScript != null) aiMovementScript.enabled = true;

        // Wait for the remaining cooldown duration
        yield return new WaitForSeconds(attackCooldown - attackAnimationDuration);

        canAttack = true; // Re-enable attacking
    }
}
