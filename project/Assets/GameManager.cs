using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class GameManager : MonoBehaviour
{
    public GameObject exitDoorOpened; // Reference to the ExitDoorOpened GameObject

    private bool playerInFrontOfExit = false; // Flag to check if the player is in front of the exit

    public int coins = 0; // Variable to store the number of coins

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Count the number of objects with the tag "Enemy"
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // If no enemies are left, enable the ExitDoorOpened GameObject
        if (enemyCount == 0)
        {
            exitDoorOpened.SetActive(true);
        }

        // Check if the player is in front of the exit and presses F
        if (playerInFrontOfExit && Input.GetKeyDown(KeyCode.F))
        {
            // Load next scene current index + 1
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void SetPlayerInFrontOfExit(bool isInFront)
    {
        playerInFrontOfExit = isInFront;
    }
}
