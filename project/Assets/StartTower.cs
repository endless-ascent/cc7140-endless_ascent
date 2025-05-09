using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTower : MonoBehaviour
{

    private bool playerInFrontOfExit = false;
    public GameObject F_UI; // Reference to the UI element to show when the player is in front of the exit
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        F_UI = GameObject.Find("F"); // Find the UI element by name
        F_UI.SetActive(false); // Hide the UI element at the start
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
            F_UI.SetActive(true); // Show the UI element when the player is in front of the exit
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player exits the trigger
        if (collision.CompareTag("Player"))
        {
            playerInFrontOfExit = false;
            F_UI.SetActive(false); // Hide the UI element when the player is not in front of the exit
        }
    }
}
