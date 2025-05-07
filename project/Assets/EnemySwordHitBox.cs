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
            // Try to get the MageController component
            MageController mage = collision.GetComponent<MageController>();
            if (mage != null)
            {
                mage.LoseHealth(damage); // Call LoseHealth on the mage
                // SoundEffectManager.Play("HitEnemy");
                return;
            }

            // Try to get the WarriorController component
            WarriorController warrior = collision.GetComponent<WarriorController>();
            if (warrior != null)
            {
                // SoundEffectManager.Play("HitEnemy");
                warrior.LoseHealth(damage); // Call LoseHealth on the warrior
                return;
            }
        }
        SoundEffectManager.Play("Attack");
    }
}
