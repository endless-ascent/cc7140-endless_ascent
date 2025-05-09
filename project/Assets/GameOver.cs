using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject gameManager; // Reference to the GameManager object
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager"); // Find the GameManager object by name
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame() {

        // Reset the player's health in the GameManager
        GameManager gm = gameManager.GetComponent<GameManager>();
        if (gm != null)
        {
            gm.player_current_health = gm.player_health; // Reset current health to max health
        }
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu() {
        // Reset the player's health in the GameManager
        GameManager gm = gameManager.GetComponent<GameManager>();
        if (gm != null)
        {
            gm.player_current_health = gm.player_health; // Reset current health to max health
        }
        SceneManager.LoadScene(0);
    }
}
