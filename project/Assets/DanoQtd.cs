using UnityEngine;
using TMPro; // Required for TextMeshPro

public class DanoQtd : MonoBehaviour
{
    public GameObject gameManager; // Reference to the GameManager object
    private TextMeshProUGUI textMeshPro; // Reference to the TextMeshPro component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager"); // Find the GameManager object by name
        textMeshPro = GetComponent<TextMeshProUGUI>(); // Get the TextMeshPro component attached to this object
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager != null)
        {
            GameManager gm = gameManager.GetComponent<GameManager>(); // Get the GameManager script
            if (gm != null)
            {
                textMeshPro.text = "" + gm.player_dmg; // Update the TextMeshPro text with the coins value
            }
        }
    }
}
