using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordHitBox : MonoBehaviour
{
    public int damage = 100; // Damage dealt to the player

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get the PlayerController component from the collided object
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.LoseHealth(damage); // Call LoseHealth on the player
            }
        }
    }
}
