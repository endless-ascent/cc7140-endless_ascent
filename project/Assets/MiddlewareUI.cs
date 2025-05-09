using UnityEngine;

public class MiddlewareUI : MonoBehaviour
{
    public GameObject gameManager; // Reference to the GameManager object
    public GameObject EscolhaEspecial; // Reference to the EscolhaEspecial object
    public GameObject ExitDoorOpened;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager"); // Find the GameManager object by name
        EscolhaEspecial = GameObject.Find("EscolhaEspecial");
        EscolhaEspecial.SetActive(false); // Disable the EscolhaEspecial object at the start
    }

    // Update is called once per frame
    public void BuyHealth() // Method to buy health
    {
        gameManager.GetComponent<GameManager>().BuyHealth(); // Call the BuyHealth method from the GameManager script
    }

    public void BuyDamage() // Method to buy damage
    {
        gameManager.GetComponent<GameManager>().BuyDamage(); // Call the BuyHealth method from the GameManager script
    }

    public void ChangeClassToGuerreiro() // Method to change the player's class to Warrior
    {
        gameManager.GetComponent<GameManager>().ChangeClassToGuerreiro(); // Call the ChangeClassToGuerreiro method from the GameManager script
    }
    public void ChangeClassToMago() // Method to change the player's class to Mage
    {
        gameManager.GetComponent<GameManager>().ChangeClassToMago(); // Call the ChangeClassToMago method from the GameManager script
    }

    public void AddHealthBuff() // Method to add health buff
    {
        Debug.Log("ButtonCLicked!"); // Log the addition of health buff
        gameManager.GetComponent<GameManager>().AddHealthBuff(); // Call the AddHealthBuff method from the GameManager script
        Destroy(EscolhaEspecial); // Destroy the EscolhaEspecial object
    
    }
    public void AddDamageBuff() // Method to add damage buff
    {
        gameManager.GetComponent<GameManager>().AddDamageBuff(); // Call the AddDamageBuff method from the GameManager script
        Destroy(EscolhaEspecial); // Destroy the EscolhaEspecial object
    }
}
