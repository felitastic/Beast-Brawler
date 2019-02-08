using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerKnockBack : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<Player>().cornered = true;        
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<Player>().cornered = false;        
    }
}
