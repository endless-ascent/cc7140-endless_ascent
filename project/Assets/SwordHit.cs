using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    public int strength = 1; // Damage dealt by the sword hitbox

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Get the Enemy component from the collided object
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.LoseHealth(strength); // Call LoseHealth on the enemy
            }
        }
    }
}
