﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundPlatform : MonoBehaviour
{
    public bool coloured;
    public char color;
    public Vector3 color_rgb_;
    private Vector3 color_rgb;
    public float min_alfa;
    public bool invisible;
    public float speed;
    public bool isMoving;
    public bool always;
    private Color c;
    public string aitausName;
    // Start is called before the first frame update
    bool playerInRange;
    public bool disable;


    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Player" )
        {
            playerInRange = true;
        }
        if (collision.gameObject.name == aitausName)
        {
            disable = false;
            invisible = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerInRange = true;
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
            collision.collider.transform.SetParent(transform);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
            playerInRange = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerInRange = false ;
        if (collision.gameObject.name == aitausName)
        {
            disable = true;
            invisible = true;
        }
        print(collision.gameObject.name);

    }


    void Start()
    {
        disable = false;
        if (color == 'v')
        {
            color_rgb_ = new Vector3(153, 51, 255);
        } else if (color == 'o')
        {
            color_rgb_ = new Vector3(255, 153, 51);
        } else if (color == 'g')
        {
            color_rgb_ = new Vector3(51, 255, 51);
        }
        else
        {
            color_rgb_ = new Vector3(255,255,255);
        }
        c = GetComponent<SpriteRenderer>().color;
        color_rgb = new Vector3(color_rgb_.x / 255, color_rgb_.y / 255, color_rgb_.z / 255);
        int i = 1;
        if (invisible) i = 0;
        if(coloured)
            GetComponent<SpriteRenderer>().color = new Color(color_rgb.x, color_rgb.y, color_rgb.z, i);
        else
            GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, i);
        if (this.GetComponent<MoveFromTo>() != null)
            isMoving = true;
        else
            isMoving = false;
        print(isMoving);
    }

    // Update is called once per frame
    void Update()
    {
      
       
        color_rgb = new Vector3(color_rgb_.x / 255, color_rgb_.y / 255, color_rgb_.z / 255);

        float alfa = GetComponent<SpriteRenderer>().color.a;
        if (!invisible && playerInRange && GetComponent<Collider2D>().isTrigger)
            invisible = true;

        if (invisible)
        {
            if (coloured)
                GetComponent<SpriteRenderer>().color = new Color(color_rgb.x, color_rgb.y, color_rgb.z, Mathf.Max(min_alfa, alfa - Mathf.Abs(speed) * Time.deltaTime));
            else
                GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, Mathf.Max(min_alfa, alfa - Mathf.Abs(speed) * Time.deltaTime));

        }
        else
        {
            if (coloured)
                GetComponent<SpriteRenderer>().color = new Color(color_rgb.x, color_rgb.y, color_rgb.z, Mathf.Min(1, alfa + Mathf.Abs(speed) * Time.deltaTime));
            else
                GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, Mathf.Min(1, alfa + Mathf.Abs(speed) * Time.deltaTime));

        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            if (alfa < 0.5f)
            {
                collider.isTrigger = true;
                gameObject.layer = 0;
            }
            else
            {
                if(aitausName == "")
                {
                    collider.isTrigger = false;
                    gameObject.layer = 8;
                }
                
            }
        }
        

    
    }

    
}
