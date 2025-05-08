using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTower : MonoBehaviour
{

    private bool playerInFrontOfExit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInFrontOfExit && Input.GetKeyDown(KeyCode.F))
        {
            // Load next scene current index + 1
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the trigger
        if (collision.CompareTag("Player"))
        {
            playerInFrontOfExit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player exits the trigger
        if (collision.CompareTag("Player"))
        {
            playerInFrontOfExit = false;
        }
    }
}
