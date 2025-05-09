using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class GameManager : MonoBehaviour
{

    public int coins = 0; // Variable to store the number of coins
    public int player_dmg = 10; // Variable to store the player's damage
    public int player_health = 10; // Variable to store the player's health
    public int player_current_health = 10; // Variable to store the player's current health

    public int player_health_before_buff; // Variable to store the player's health before buff
    public int player_dmg_before_buff; // Variable to store the player's damage before buff
    public string classe = "Guerreiro"; // Variable to store the player's class

    public GameObject guerreiro;
    public GameObject mago;
    public int times_on_acampamento = 0; // Variable to count the number of times the player has been on the "Acampamento" scene
    public GameObject firstDialogue; // Reference to the first dialogue GameObject
    public GameObject secondDialogue; // Reference to the second dialogue GameObject
    void Start()
    {
        player_health_before_buff = player_health;
        player_dmg_before_buff = player_dmg; // Initialize the player's damage before buff
    }

    void Awake()
    {
        GameManager[] instances = FindObjectsOfType<GameManager>();

        if (instances.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {   
        Debug.Log("Scene loaded: " + scene.name); // Log the name of the loaded scene
        guerreiro = GameObject.Find("Player"); // Find the Warrior GameObject by name
        mago = GameObject.Find("PlayerMage"); // Find the Mage GameObject by name
        ChangeClass(); // Call ChangeClass when a new scene is loaded

        if (scene.name == "Acampamento") // Check if the scene is "Acampamento" and it's the first time
        {
            firstDialogue = GameObject.Find("FirstDialogue"); // Find the first dialogue GameObject by name
            firstDialogue.SetActive(false); // Deactivate the first dialogue GameObject

            secondDialogue = GameObject.Find("SecondDialogue"); // Find the second dialogue GameObject by name
            secondDialogue.SetActive(false); // Deactivate the second dialogue GameObject

            if (times_on_acampamento == 0) {
                
                firstDialogue.SetActive(true); // Activate the first dialogue GameObject
                player_current_health = player_health; // Set the player's current health to the maximum health
                player_health_before_buff = player_health; // Store the player's health before buff
                player_dmg_before_buff = player_dmg; // Store the player's damage before buff
            } else if (times_on_acampamento == 1) {
                secondDialogue.SetActive(true); // Activate the second dialogue GameObject
            } else {
                player_health = player_health_before_buff; // Reset the player's health to the value before buff
                player_dmg = player_dmg_before_buff; // Reset the player's damage to the value before buff
                player_current_health = player_health; // Reset the player's current health to the maximum health
            }

            times_on_acampamento++; // Increment the number of times on the "Acampamento" scene
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyHealth() // Method to buy health
    {
        if (coins >= 10) // Check if the player has enough coins
        {
            coins -= 10; // Deduct the cost from the coins
            player_health += 20; // Increase the player's maximum health
            Debug.Log("Health bought!"); // Log the purchase

            player_health_before_buff = player_health; // Store the player's health before buff
        }
    }

    public void AddHealthBuff() {
        Debug.Log("Health buff added!"); // Log the addition of health buff
        player_health += 20; // Increase the player's maximum health
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject by tag
        if (player != null)
        {
            WarriorController warriorController = player.GetComponent<WarriorController>();
            MageController mageController = player.GetComponent<MageController>();

            if (warriorController != null)
            {
            warriorController.health += 20; // Increase the Warrior's current health
            }
            else if (mageController != null)
            {
            mageController.health += 20; // Increase the Mage's current health
            }
        }
    }

    public void AddDamageBuff() {
        player_dmg += 20; // Increase the player's damage
    }

    public void BuyDamage() // Method to buy damage
    {
        if (coins >= 10) // Check if the player has enough coins
        {
            coins -= 10; // Deduct the cost from the coins
            player_dmg += 20; // Increase the player's damage
            Debug.Log("Damage bought!"); // Log the purchase

            player_dmg_before_buff = player_dmg; // Store the player's damage before buff
        }
    }

    public void ChangeClass()
    {
        if (classe == "Guerreiro")
        {
            Vector2 position = new Vector2(mago.transform.position.x, mago.transform.position.y); // Get the position of the Mage GameObject
            guerreiro.transform.position = position; // Set the Warrior GameObject's position to the Mage's position
            guerreiro.SetActive(true); // Activate the Warrior GameObject
            mago.SetActive(false); // Deactivate the Mage GameObject
        }
        else if (classe == "Mago")
        {
            Vector2 position = new Vector2(guerreiro.transform.position.x, guerreiro.transform.position.y); // Get the position of the Warrior GameObject
            mago.transform.position = position; // Set the Mage GameObject's position to the Warrior's position
            guerreiro.SetActive(false); // Deactivate the Warrior GameObject
            mago.SetActive(true); // Activate the Mage GameObject
        }
    }

    public void ChangeClassToGuerreiro() // Method to change the player's class to Warrior
    {   
        if (classe == "Guerreiro") // Check if the player is already a Warrior
        {
            Debug.Log("Already a Guerreiro!"); // Log that the player is already a Warrior
            return; // Exit the method if the player is already a Warrior
        }
        classe = "Guerreiro"; // Set the player's class to Warrior
        Debug.Log("Class changed to Guerreiro!"); // Log the class change
        ChangeClass(); // Call the ChangeClass method to update the GameObjects
    }

    public void ChangeClassToMago() // Method to change the player's class to Mage
    {
        if (classe == "Mago") // Check if the player is already a Mage
        {
            Debug.Log("Already a Mago!"); // Log that the player is already a Mage
            return; // Exit the method if the player is already a Mage
        }
        classe = "Mago"; // Set the player's class to Mage
        Debug.Log("Class changed to Mago!"); // Log the class change
        ChangeClass(); // Call the ChangeClass method to update the GameObjects
    }
}
