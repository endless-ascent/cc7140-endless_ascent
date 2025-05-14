using UnityEngine;

public class Fragmento : MonoBehaviour
{
    public GameObject gameManager; // Reference to the GameManager object
    private bool canCollide = false; // Flag to enable collision logic
    public GameObject fragmentoDialogue; // Reference to the fragmento dialogue prefab

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager"); // Find the GameManager object by name
        Invoke(nameof(EnableCollision), 2f); // Enable collision after 2 seconds
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnableCollision()
    {
        canCollide = true; // Enable collision logic
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canCollide) return; // Ignore collisions if the timeout hasn't passed

        if (collision.gameObject.CompareTag("Player"))
        {
            
            GameObject dialogue = Instantiate(fragmentoDialogue, new Vector3(0, 0, 0), Quaternion.identity); // Instantiate the fragmento dialogue

            GameManager gm = gameManager.GetComponent<GameManager>(); // Get the GameManager script
            if (gm != null)
            {
                gm.fragmentos += 1; // Increment the coins value in the GameManager
                gm.isMageUnlocked = true;
            }
            Destroy(gameObject); // Destroy the Fragmento object
        }
    }
}
