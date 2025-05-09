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

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
    }
}
