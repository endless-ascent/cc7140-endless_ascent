using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorOpened : MonoBehaviour
{
    public DoorScript doorScript;
    public GameObject F_UI; // Reference to the UI element to show when the player is in front of the exit

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the trigger
        if (collision.CompareTag("Player"))
        {
            doorScript.GetComponent<DoorScript>().SetPlayerInFrontOfExit(true);
            F_UI.SetActive(true); // Show the UI element when the player is in front of the exit
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player exits the trigger
        if (collision.CompareTag("Player"))
        {
            doorScript.GetComponent<DoorScript>().SetPlayerInFrontOfExit(false);
            F_UI.SetActive(false); // Hide the UI element when the player is not in front of the exit
        }
    }
}