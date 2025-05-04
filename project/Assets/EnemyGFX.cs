using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    public float scale = 1f;

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-scale, scale, scale);
        }
    }
}
