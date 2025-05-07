using UnityEngine;

public class VontadeAncestral : MonoBehaviour
{
    public GameObject gameManager; // Reference to the GameManager object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager"); // Find the GameManager object by name
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager gm = gameManager.GetComponent<GameManager>(); // Get the GameManager script
            if (gm != null)
            {
                gm.coins += 1; // Increment the coins value in the GameManager

            }
            Destroy(gameObject); // Destroy the VontadeAncestral object
        }
    }
}
