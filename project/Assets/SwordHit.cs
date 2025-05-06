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
            // Check for Enemy component
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.LoseHealth(strength); // Call LoseHealth on the enemy
                return;
            }

            // Check for ShootingEnemy component
            ShootingEnemy shootingEnemy = collision.GetComponent<ShootingEnemy>();
            if (shootingEnemy != null)
            {
                shootingEnemy.LoseHealth(strength); // Call LoseHealth on the shooting enemy
                return;
            }
        }
        else
        {
        // SoundEffectManager.Play("Attack");
            
        }
    }
    
}
