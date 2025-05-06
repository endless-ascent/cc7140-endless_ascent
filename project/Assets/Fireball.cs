using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int strength = 34; // Damage dealt by the sword hitbox

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Sound Spell Hit
            SoundEffectManager.Play("SpellHit");
            
            // Check for Enemy component
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.LoseHealth(strength); // Call LoseHealth on the enemy
                Destroy(gameObject);
                return;
            }

            // Check for ShootingEnemy component
            ShootingEnemy shootingEnemy = collision.GetComponent<ShootingEnemy>();
            if (shootingEnemy != null)
            {
                shootingEnemy.LoseHealth(strength); // Call LoseHealth on the shooting enemy
                Destroy(gameObject);
                return;
            }
        }
        else if (!collision.CompareTag("Player"))
        {
            SoundEffectManager.Play("SpellMiss");
            Destroy(gameObject); // Destroy the fireball if it's not the player
        }
    }
}
