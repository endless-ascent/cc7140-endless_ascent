using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int strength = 34; // Damage dealt by the sword hitbox

    public GameObject gameManager; // Reference to the GameManager object

    void Start()
    {
        gameManager = GameObject.Find("GameManager"); // Find the GameManager object by name

        if (gameManager != null)
        {
            GameManager gm = gameManager.GetComponent<GameManager>(); // Get the GameManager script
            if (gm != null)
            {
                strength = gm.player_dmg; // Set the sword strength from the GameManager
            }
        }
    }

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

            Crystal crystal = collision.GetComponent<Crystal>();
            if (crystal != null)
            {
                crystal.LoseHealth(1);
                return;
            }

            CrystalTrap trap = collision.GetComponent<CrystalTrap>();
            if (trap != null){

                trap.LoseHealth(1);
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
