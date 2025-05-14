using UnityEngine;
using System.Collections;
using Pathfinding;

public class BossScript1 : MonoBehaviour
{
    public int health = 100;
    private bool canBeHit = true;
    public float hitCooldown = 0.5f;
    public float flashDuration = 0.2f;
    public float deathAnimationDuration = 0.3f;

    public RuntimeAnimatorController walkController;
    public RuntimeAnimatorController deathController;
    public RuntimeAnimatorController attackController;

    public float attackAnimationDuration = 0.3f;
    public float attackCooldown = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isAttacking = false;
    private bool canAttack = true;
    private Transform player;

    public float attackRange = 1.5f;

    private Rigidbody2D rb;
    private MonoBehaviour aiMovementScript;
    public AIPath aiPath;
    public float scale = 1f;

    // 🔽 Novo: spawn de inimigos
    public GameObject enemyPrefab; // Prefab do inimigo a ser instanciado
    public Transform[] spawnPoints; // Locais possíveis para spawn
    public float spawnInterval = 10f; // Tempo entre spawns
    private float spawnTimer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        aiMovementScript = GetComponent<Pathfinding.AIPath>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        spawnTimer = spawnInterval; // Inicializa o timer de spawn
    }

    void Update()
    {
        if (player == null || player.transform == null) return;

        if (!isAttacking && canAttack && Vector2.Distance(transform.position, player.position) <= attackRange)
            StartCoroutine(HandleAttack());

        if (aiPath.desiredVelocity.x >= 0.01f)
            transform.localScale = new Vector3(scale, scale, scale);
        else if (aiPath.desiredVelocity.x <= -0.01f)
            transform.localScale = new Vector3(-scale, scale, scale);

        // 🔽 Timer e lógica de spawn
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval; // Reinicia o timer
        }
    }

    // 🔽 Spawna um inimigo
    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoints.Length == 0) return;

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, point.position, Quaternion.identity);
    }

    public void LoseHealth(int damage)
    {
        if (!canBeHit) return;

        health -= damage;
        canBeHit = false;

        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            animator.runtimeAnimatorController = deathController;
            Destroy(gameObject, deathAnimationDuration);
        }
        else
        {
            StartCoroutine(HitCooldownCoroutine());
        }
    }

    private IEnumerator HitCooldownCoroutine()
    {
        yield return new WaitForSeconds(hitCooldown);
        canBeHit = true;
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        SoundEffectManager.Play("HitEnemy");
        spriteRenderer.color = Color.white;
    }

    private IEnumerator HandleAttack()
    {
        isAttacking = true;
        canAttack = false;
        animator.runtimeAnimatorController = attackController;

        if (aiMovementScript != null) aiMovementScript.enabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;

        GameObject enemySwordHitBox = transform.Find("EnemySwordHitBox").gameObject;

        yield return new WaitForSeconds(attackAnimationDuration / 2);
        enemySwordHitBox.SetActive(true);
        yield return new WaitForSeconds(attackAnimationDuration / 2);
        enemySwordHitBox.SetActive(false);

        isAttacking = false;
        animator.runtimeAnimatorController = walkController;
        if (aiMovementScript != null) aiMovementScript.enabled = true;
        yield return new WaitForSeconds(attackCooldown - attackAnimationDuration);
        canAttack = true;
    }
}
