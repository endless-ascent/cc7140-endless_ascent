using UnityEngine;

public class CrystalShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform[] teleportPoints;
    public float shootInterval = 2f;
    public float teleportInterval = 10f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(Shoot), 1f, shootInterval);

    }

    void Shoot()
    {
        if (player == null || projectilePrefab == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().linearVelocity = direction * 5f; 
    }


}
