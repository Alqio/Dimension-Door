using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rotate : MonoBehaviour
{

    public float rotationSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Vector3 oldRot = transform.eulerAngles;

        

        if (Input.GetKey(KeyCode.Q))
        {
            transform.eulerAngles = new Vector3(oldRot.x, oldRot.y, oldRot.z - (rotationSpeed * Time.deltaTime)/transform.localScale.x);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.eulerAngles = new Vector3(oldRot.x, oldRot.y, oldRot.z + (rotationSpeed * Time.deltaTime) / transform.localScale.x);
        }
    }
}
