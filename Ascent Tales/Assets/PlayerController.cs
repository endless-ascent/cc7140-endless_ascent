using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed for left and right movement
    public float jumpForce = 10f; // Force applied for jumping
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Animator animator; // Reference to the Animator component
    private bool isGrounded = true; // Check if the player is on the ground
    private bool canAttack = true; // Cooldown flag for attacking
    private bool isAttacking = false; // Flag to check if the player is currently attacking

    public RuntimeAnimatorController idleController; // Assign in Unity Inspector
    public RuntimeAnimatorController runController; // Assign in Unity Inspector
    public RuntimeAnimatorController jumpController; // Assign in Unity Inspector
    public RuntimeAnimatorController attackController; // Assign in Unity Inspector

    public float attackAnimationDuration = 0.3f; // Duration of the attack animation
    public float attackCooldown = 0.5f; // Cooldown duration between attacks

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
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

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // Get raw input for A/D keys

        // Move left or right
        if (horizontalInput != 0)
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

            // Flip the player sprite if moving left
            if (horizontalInput < 0)
                transform.localScale = new Vector3(-2.75f, 2.75f, 2.75f);
            else
                transform.localScale = new Vector3(2.75f, 2.75f, 2.75f);
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
        }
    }

    private void HandleAnimation()
    {
        if (!isGrounded)
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

        // Wait for the attack animation to finish
        yield return new WaitForSeconds(attackAnimationDuration);

        isAttacking = false; // Reset attacking flag

        // Wait for the remaining cooldown duration
        yield return new WaitForSeconds(attackCooldown - attackAnimationDuration);

        canAttack = true; // Re-enable attacking
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
