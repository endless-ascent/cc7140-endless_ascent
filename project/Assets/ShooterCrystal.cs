using UnityEngine;

public class ShooterCrystal : MonoBehaviour
{
    public Transform[] teleportPoints;
    public GameObject projectilePrefab;
    public float shootInterval = 3f;
    public float projectileSpeed = 5f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(Teleport), 10f, 10f);
        InvokeRepeating(nameof(Shoot), 1f, shootInterval);
    }

    private void Shoot()
    {
        if (player == null || projectilePrefab == null) return;

        Vector2 direction = (player.position - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.linearVelocity = direction * projectileSpeed;
    }

    private void Teleport()
    {
        if (teleportPoints == null || teleportPoints.Length == 0) return;

        Transform target = null;
        int attempts = 10;

        while (attempts-- > 0)
        {
            Transform candidate = teleportPoints[Random.Range(0, teleportPoints.Length)];
            Collider2D[] colliders = Physics2D.OverlapCircleAll(candidate.position, 0.1f);
            bool occupied = false;

            foreach (var col in colliders)
            {
                if (col.gameObject == this.gameObject) continue;
                if (col.GetComponent<Crystal>() != null || col.GetComponent<CrystalTrap>() != null || col.GetComponent<ShooterCrystal>() != null)
                {
                    occupied = true;
                    break;
                }
            }

            if (!occupied)
            {
                target = candidate;
                break;
            }
        }

        if (target != null)
            transform.position = target.position;
    }
}
