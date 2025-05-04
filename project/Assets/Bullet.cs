using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int strength = 34; // Damage dealt by the sword hitbox

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Try to get the MageController component
            MageController mage = collision.GetComponent<MageController>();
            if (mage != null)
            {
                mage.LoseHealth(strength); // Call LoseHealth on the mage
                Destroy(gameObject);
                return;
            }

            // Try to get the WarriorController component
            WarriorController warrior = collision.GetComponent<WarriorController>();
            if (warrior != null)
            {
                warrior.LoseHealth(strength); // Call LoseHealth on the warrior
                Destroy(gameObject);
                return;
            }
        }
        else if (!collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Destroy the fireball if it's not the player
        }
    }
}
