using UnityEngine;

public class FloorSweepProjectile : MonoBehaviour
{
    public int damage = 1;
    public float speed = 5f;
    public float lifetime = 3f;
    public LayerMask playerLayer;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            WarriorController player = collision.gameObject.GetComponent<WarriorController>();
            if (player != null)
            {
                player.LoseHealth(damage);
            }
        }
    }
}
