using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class WarriorController : MonoBehaviour
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

    public int health = 100; // Player's health
    private bool canBeHit = true; // Cooldown flag to prevent multiple hits
    public float hitCooldown = 0.5f; // Cooldown duration in seconds
    public float flashDuration = 0.2f; // Duration for the red flash
    public float deathAnimationDuration = 0.3f; // Duration of the death animation
    public float attackAnimationDuration = 0.3f; // Duration of the attack animation
    public float attackCooldown = 0.5f; // Cooldown duration between attacks

    public float stepSFXCooldown = 0.4f; // Tempo entre sons de passos
    private float lastStepTime = -999f; // Último tempo em que o som foi tocado

    public GameObject gameManager; // Reference to the GameManager object
    public bool disableClickInput = false; // Flag to disable click input
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        gameManager = GameObject.Find("GameManager"); // Find the GameManager object by name

        if (gameManager != null)
        {
            GameManager gm = gameManager.GetComponent<GameManager>(); // Get the GameManager script
            if (gm != null)
            {
                health = gm.player_current_health; // Set the player's health from the GameManager
            }
        }
    }

    void Update()
    {
        if (gameManager != null)
        {
            GameManager gm = gameManager.GetComponent<GameManager>(); // Get the GameManager script
            if (gm != null)
            {
                if (SceneManager.GetActiveScene().name == "Acampamento")
                {
                    health = gm.player_health; // Set the player's health from the GameManager
                }
                gm.player_current_health = health; // Update the GameManager with the player's current health
            }
        }

        if (!isDead) // Only allow movement and actions if the player is not dead
        {
            if (!isAttacking) // Only allow movement and jumping if not attacking
            {
                HandleMovement();
            }

            if (canAttack && Input.GetMouseButtonDown(0) && !disableClickInput) // MouseButton1 (left mouse button)
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
            
            //Walk sound
            Walk();
            //SoundEffectManager.Play("Walk");
            // Flip the player sprite if moving left
            if (horizontalInput < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
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
        yield return new WaitForSeconds(0.8f);

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

        // Reference to the SwordHitBox GameObject
        GameObject swordHitBox = transform.Find("SwordHitBox").gameObject;
        
        // Wait for the first half of the attack animation
        yield return new WaitForSeconds(attackAnimationDuration / 2);

        // Enable the SwordHitBox
        swordHitBox.SetActive(true);
        
        // Wait for the second half of the attack animation
        yield return new WaitForSeconds(attackAnimationDuration / 2);
        SoundEffectManager.Play("Attack");
        // Disable the SwordHitBox
        swordHitBox.SetActive(false);

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

            StartCoroutine(LoadGameOverScene(deathAnimationDuration)); // Load Game Over scene after delay
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
        
        //Hit sound
        SoundEffectManager.Play("HitPlayer");
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

    private IEnumerator LoadGameOverScene(float delay)
    {
        Debug.Log("Game Over!"); // Log Game Over message
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SceneManager.LoadScene("GameOver"); // Load the Game Over scene
    }
    private void Walk()
    {
        if ((Time.time - lastStepTime > stepSFXCooldown) && isGrounded)
        {
            SoundEffectManager.Play("Walk");
            lastStepTime = Time.time;
        }
    }

    public void DisableClickInput()
    {
        disableClickInput = true; // Disable click input
    }
    public void EnableClickInput()
    {
        disableClickInput = false; // Enable click input
    }

}



