using UnityEngine;

public class EnableTree : MonoBehaviour
{
    public GameObject EscolhaEspecial; // Reference to the UI_Shop GameObject
    public GameObject player;

    // Called when another collider enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            if (EscolhaEspecial != null) {
                EscolhaEspecial.SetActive(true); // Enable the UI_Shop GameObject
            }
            try
            {
                other.GetComponent<MageController>().disableClickInput = true; // Disable click input for the player
            }
            catch
            {
                // MageController not found, do nothing
            }

            try
            {
                other.GetComponent<WarriorController>().disableClickInput = true; // Disable click input for the player
            }
            catch
            {
                // WarriorController not found, do nothing
            }
        }
    }

    // Called when another collider exits the trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            if (EscolhaEspecial != null) {
                EscolhaEspecial.SetActive(false); // Disable the UI_Shop GameObject
            }
            try
            {
                other.GetComponent<MageController>().disableClickInput = false; // Enable click input for the player
            }
            catch
            {
                // MageController not found, do nothing
            }

            try
            {
                other.GetComponent<WarriorController>().disableClickInput = false; // Enable click input for the player
            }
            catch
            {
                // WarriorController not found, do nothing
            }
        }
    }
}
