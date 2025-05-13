using UnityEngine;
using System.Collections;
using Pathfinding;

public class BossScript : MonoBehaviour
{
    public int health = 3;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private Transform player;
    public AIPath aiPath;

    public GameObject floorSweepProjectilePrefab;

    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;
    public float enemySpawnInterval = 15f;

    private bool isAttacking = false;
    private bool canAttack = true;
    private GameObject currentTrapCrystal;
    private GameObject currentCrystal;

    public RuntimeAnimatorController walkController;
    public RuntimeAnimatorController deathController;
    public RuntimeAnimatorController attackController;
    public int maxEnemies = 5; // Número máximo de inimigos vivos permitidos


    public float attackRange = 1.5f;
    public float attackAnimationDuration = 0.3f;
    public float attackCooldown = 0.5f;
    public float scale = 1f;

    public GameObject crystalPrefab;
    public GameObject trapCrystalPrefab;
    public GameObject shooterCrystalPrefab;
    public Transform shooterCrystalSpawnPoint;
    private bool baon = false;

    public Transform[] crystalSpawnPoints;
    public Transform[] trapCrystalSpawnPoints; // <-- NOVO campo adicionado!
    public Transform[] crystalTeleportPoints;
    public Transform[] trapTeleportPoints;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(CrystalSpawnCycle());
        StartCoroutine(EnemySpawnRoutine());
    }

    void Update()
    {
        if (player == null) return;

        if (!isAttacking && canAttack && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            StartCoroutine(HandleAttack());
            baon = false;
        }

        if (aiPath.desiredVelocity.x >= 0.01f)
            transform.localScale = new Vector3(scale, scale, scale);
        else if (aiPath.desiredVelocity.x <= -0.01f)
            transform.localScale = new Vector3(-scale, scale, scale);
    }

    private IEnumerator CrystalSpawnCycle()
    {
        while (health > 0)
        {
            yield return new WaitForSeconds(20f);

            if (currentCrystal != null)
                Destroy(currentCrystal);
            if (currentTrapCrystal != null)
                Destroy(currentTrapCrystal);

            if (health == 3)
            {
                Transform spawnPoint = crystalSpawnPoints[Random.Range(0, crystalSpawnPoints.Length)];
                currentCrystal = Instantiate(crystalPrefab, spawnPoint.position, Quaternion.identity);

                Crystal crystalScript = currentCrystal.GetComponent<Crystal>();
                if (crystalScript != null)
                {
                    crystalScript.teleportPoints = crystalTeleportPoints;
                    crystalScript.SetBossReference(this);
                    crystalScript.Initialize();
                }
            }
            else if (health == 2)
            {
                Transform spawnPoint1 = crystalSpawnPoints[Random.Range(0, crystalSpawnPoints.Length)];
                currentCrystal = Instantiate(crystalPrefab, spawnPoint1.position, Quaternion.identity);

                Crystal crystalScript = currentCrystal.GetComponent<Crystal>();
                if (crystalScript != null)
                {
                    crystalScript.teleportPoints = crystalTeleportPoints;
                    crystalScript.SetBossReference(this);
                    crystalScript.Initialize();
                }

                Transform spawnPoint2 = trapCrystalSpawnPoints[Random.Range(0, trapCrystalSpawnPoints.Length)];
                currentTrapCrystal = Instantiate(trapCrystalPrefab, spawnPoint2.position, Quaternion.identity);

                CrystalTrap trapScript = currentTrapCrystal.GetComponent<CrystalTrap>();
                if (trapScript != null)
                {
                    trapScript.teleportPoints = trapTeleportPoints;
                    trapScript.Initialize();
                }
            }
            else if (health == 1)
            {
                Transform spawnPoint1 = crystalSpawnPoints[Random.Range(0, crystalSpawnPoints.Length)];
                currentCrystal = Instantiate(crystalPrefab, spawnPoint1.position, Quaternion.identity);

                Crystal crystalScript = currentCrystal.GetComponent<Crystal>();
                if (crystalScript != null)
                {
                    crystalScript.teleportPoints = crystalTeleportPoints;
                    crystalScript.SetBossReference(this);
                }

                Transform spawnPoint2 = trapCrystalSpawnPoints[Random.Range(0, trapCrystalSpawnPoints.Length)];
                currentTrapCrystal = Instantiate(trapCrystalPrefab, spawnPoint2.position, Quaternion.identity);

                CrystalTrap trapScript = currentTrapCrystal.GetComponent<CrystalTrap>();
                if (trapScript != null)
                {
                    trapScript.teleportPoints = trapTeleportPoints;
                }

                if (shooterCrystalSpawnPoint != null && shooterCrystalPrefab != null)
                {
                    Instantiate(shooterCrystalPrefab, shooterCrystalSpawnPoint.position, Quaternion.identity);
                }
            }
        }
    }

    private IEnumerator EnemySpawnRoutine()
    {
        while (health > 0)
        {
            yield return new WaitForSeconds(enemySpawnInterval);

            int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (enemyPrefab != null && enemySpawnPoints.Length > 0 && currentEnemyCount < maxEnemies)
            {
                Transform spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }


    public void LoseHealthBoss1(int damage)
    {
        health -= damage;
        StartCoroutine(FlashRed());

        Crystal[] normalCrystals = FindObjectsOfType<Crystal>();
        foreach (Crystal c in normalCrystals)
            Destroy(c.gameObject);

        CrystalTrap[] trapCrystals = FindObjectsOfType<CrystalTrap>();
        foreach (CrystalTrap c in trapCrystals)
            Destroy(c.gameObject);

        ShooterCrystal[] shooterCrystals = FindObjectsOfType<ShooterCrystal>();
        foreach (ShooterCrystal c in shooterCrystals)
            Destroy(c.gameObject);

        if (health <= 0)
        {
            animator.runtimeAnimatorController = deathController;
            Destroy(gameObject, 0.3f);
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    private IEnumerator HandleAttack()
    {
        isAttacking = true;
        canAttack = false;
        animator.runtimeAnimatorController = attackController;

        GameObject enemySwordHitBox = transform.Find("EnemySwordHitBox").gameObject;

        yield return new WaitForSeconds(attackAnimationDuration);

        animator.runtimeAnimatorController = walkController;
        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
