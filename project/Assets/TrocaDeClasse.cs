using UnityEngine;

public class TrocaDeClasse : MonoBehaviour
{
    public GameObject gameManager; // Reference to the GameManager object
    public GameObject magoButton; // Reference to the Mage button
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable() {
        gameManager = GameObject.Find("GameManager"); // Find the GameManager object by name
        if (gameManager != null)
        {
            GameManager gm = gameManager.GetComponent<GameManager>(); // Get the GameManager script
            if (gm != null)
            {
                if (gm.isMageUnlocked)
                {
                    magoButton.SetActive(true); // Show the Mage button if the Mage class is unlocked
                }
                else
                {
                    magoButton.SetActive(false); // Hide the Mage button if the Mage class is not unlocked
                }
            }
        }
    }
}
