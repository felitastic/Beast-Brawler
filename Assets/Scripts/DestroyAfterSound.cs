using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to SFX, destroys the gameobject after playing
/// </summary>

public class DestroyAfterSound : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasStarted = false;

    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying)
        {
            hasStarted = true;
        }

        if (audioSource.isPlaying == false && hasStarted)
        {
            Destroy(gameObject);
        }
    }
}

