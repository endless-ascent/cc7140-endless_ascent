using UnityEngine;

public class ShooterProjectile : MonoBehaviour
{
    public int damage = 10;
    public float speed = 5f;
    public Vector2 direction;

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            WarriorController player = collision.gameObject.GetComponent<WarriorController>();
            MageController player1 = collision.gameObject.GetComponent<MageController>();
            if (player != null)
            {
                player.LoseHealth(damage);
            }
            if (player1 != null){
                player1.LoseHealth(damage);
            }
            Destroy(gameObject);
        }
        
        else if (collision.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
