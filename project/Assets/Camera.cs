using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset;   // Offset between the camera and the player

    // Start is called before the first frame update
    void Start()
    {
        // Find the player GameObject by tag and get its transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure it has the 'Player' tag.");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            // Update the camera's position to follow the player with the offset
            Vector3 newPosition = player.position + offset;
            newPosition.z = -10; // Ensure the Z position is always -10
            transform.position = newPosition;
        }
    }
}
