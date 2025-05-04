using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorOpened : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the trigger
        if (collision.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().SetPlayerInFrontOfExit(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player exits the trigger
        if (collision.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().SetPlayerInFrontOfExit(false);
        }
    }
}