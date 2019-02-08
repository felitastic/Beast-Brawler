using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBehaviour : MonoBehaviour
{
    public Vector3 newScale;
    public Vector3 newTransform;
    public GameObject player;
    public Rigidbody2D pbody;
    private float x;
    private float y;

    // Use this for initialization
    void Start ()
    {
        newScale = this.transform.localScale;
        pbody = player.GetComponentInChildren<Rigidbody2D>();


        y = player.transform.position.y + 0.7f;

        if (player.GetComponent<Player>().facingRight)
        {
            x = player.transform.position.x + 0.4f;
        }
        else
        {
            x = player.transform.position.x - 0.4f;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player.GetComponent<Player>().facingRight)
        {
            x = player.transform.position.x + 0.4f;
        }
        else
        {
            x = player.transform.position.x - 0.4f;
        }

        if (transform.localScale != newScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime*3);
        }

        if (pbody.velocity.y > 0f)
        {
            NewScale(0.6f, 0.6f);
        }
        else if (pbody.velocity.y < 0f)
        {
            NewScale(1f, 1f);
        }
        else
        {
            NewScale(1f, 1f);
        }

        if (!player.GetComponentInChildren<Player>().grounded)
        {
            transform.position = new Vector3(x, -3.59f, 0f);            
        }
        else
        {
            transform.position = new Vector3(x, y, 0f);
        }
    }

    public void NewScale(float X, float Y)
    {
        newScale = new Vector3(X, Y, 1f);
    }
}
