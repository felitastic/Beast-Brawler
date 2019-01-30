using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to ground trigger, checks if the player has touched the ground
/// </summary>
public class GroundCheck : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<Player>().grounded = true;
    }
}
