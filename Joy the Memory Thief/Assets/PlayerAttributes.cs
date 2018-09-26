using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour {

    public float spd;
    public Rigidbody2D body;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump"))
        {
            body.transform.Translate(new Vector3(-spd, 0, 0));
        }
	}
}
