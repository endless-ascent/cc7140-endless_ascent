using UnityEngine;
using UnityEngine.UI; // Import the UI namespace

public class HealthBarUI : MonoBehaviour
{

    public float Health, MaxHealth, Width, Height; // Health, MaxHealth, Width, and Height variables
    public GameObject player; // Reference to the player GameObject

    [SerializeField]
    private RectTransform healthBar; // Reference to the health bar UI element

    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth; // Set the maximum health
    }

    public void SetHealth(float health)
    {
        Health = health; 
        float newWidth = Width * (Health / MaxHealth); // Calculate the new width based on health percentage
        healthBar.sizeDelta = new Vector2(newWidth, Height); // Update the health bar size
    }

    void Start()
    {
        if (player == null) // Check if the player reference is not set
        {
            player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject by tag
        }

        // get the player's health component and set the max health (can be MageController or WarriorController)
        if (player != null)
        {
            MageController mageController = player.GetComponent<MageController>();
            if (mageController != null)
            {
                SetMaxHealth(mageController.health); // Set the max health from the MageController
            }
            else
            {
                WarriorController warriorController = player.GetComponent<WarriorController>();
                if (warriorController != null)
                {
                    SetMaxHealth(warriorController.health); // Set the max health from the WarriorController
                }
            }
        }
    }
        
    void Update()
    {
        if (player != null) // Check if the player reference is set
        {
            MageController mageController = player.GetComponent<MageController>();
            if (mageController != null)
            {
                SetHealth(mageController.health); // Update health from the MageController
            }
            else
            {
                WarriorController warriorController = player.GetComponent<WarriorController>();
                if (warriorController != null)
                {
                    SetHealth(warriorController.health); // Update health from the WarriorController
                }
            }
        }
    }
}
