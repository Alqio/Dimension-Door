using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject g;
    bool playerInRange;
    public bool wall;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerInRange = false;
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().color = g.GetComponent<SpriteRenderer>().color;
        GetComponent<BoxCollider2D>().isTrigger = g.GetComponent<BoxCollider2D>().isTrigger;
        if (!wall)
            gameObject.layer = g.gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && GetComponent<BoxCollider2D>().isTrigger)
            g.GetComponent<soundPlatform>().invisible = true;
        GetComponent<SpriteRenderer>().color = g.GetComponent<SpriteRenderer>().color;
        GetComponent<BoxCollider2D>().isTrigger = g.GetComponent<BoxCollider2D>().isTrigger;
        if(!wall)
            gameObject.layer = g.gameObject.layer;

    }
}
