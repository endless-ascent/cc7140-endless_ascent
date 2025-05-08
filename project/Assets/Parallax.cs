using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform player;
    private Vector3 previousPlayerPosition;
    [SerializeField] private float parallaxFactor = 0.1f;

    private void Start()
    {
        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            previousPlayerPosition = player.position;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player GameObject has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        Vector3 delta = player.position - previousPlayerPosition;

        // Move the background in the opposite direction to the player movement
        transform.position += new Vector3(delta.x, delta.y, 0) * parallaxFactor;

        previousPlayerPosition = player.position;
    }
}
