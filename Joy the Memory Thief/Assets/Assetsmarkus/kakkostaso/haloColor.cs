using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haloColor : MonoBehaviour
{
    public float max;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color c = GameObject.FindGameObjectWithTag("halos").GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, Mathf.Min(c.a/2,max));// GetComponent<SpriteRenderer>().color.a);
    }
}
