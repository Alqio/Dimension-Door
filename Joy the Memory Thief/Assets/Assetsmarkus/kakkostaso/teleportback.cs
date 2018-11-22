using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportback : MonoBehaviour
{
    Vector3 pos;
    public float y;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < y)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().freezeRotation = true;
            GetComponent<Rigidbody2D>().freezeRotation = false;

            transform.position = pos;
            //GetComponent<Rigidbody2D>().isKinematic = false; ;


        }
    }
}
