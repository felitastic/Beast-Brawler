using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to land trigger, triggers Player landing anim
/// </summary>
public class LandCheck : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<Player>().landCheck = true;
    }
}
