using UnityEngine;

public class Crystal : MonoBehaviour
{
    public int health = 1;
    private BossScript boss;
    public Transform[] teleportPoints;

    public void SetBossReference(BossScript bossRef)
    {
        boss = bossRef;
    }

    public void Initialize()
    {
        InvokeRepeating(nameof(Teleport), 10f, 10f);
    }

    void Start()
    {
        boss = FindObjectOfType<BossScript>(); 
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            boss.LoseHealthBoss1(1);
            Destroy(gameObject);
        }
    }

    private void Teleport()
    {
        if (teleportPoints == null || teleportPoints.Length == 0) return;

        Transform target = null;
        int attempts = 10;

        while (attempts-- > 0)
        {
            Transform candidate = teleportPoints[Random.Range(0, teleportPoints.Length)];
            bool occupied = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(candidate.position, 0.1f);
            foreach (var col in colliders)
            {
                if (col.gameObject == this.gameObject) continue;
                if (col.GetComponent<Crystal>() != null || col.GetComponent<CrystalTrap>() != null)
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
