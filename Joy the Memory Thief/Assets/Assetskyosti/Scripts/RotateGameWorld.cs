using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGameWorld : MonoBehaviour {

    private GameObject[] platforms;

    public float rotationSpeed = 1f;

	// Use this for initialization
	void Start () {
        platforms = GameObject.FindGameObjectsWithTag("Block");
	}
	
    public void Rotate(Vector3 direction)
    {
        foreach (GameObject platform in platforms)
        {
            platform.transform.RotateAround(Vector3.zero, direction, rotationSpeed * Time.deltaTime);
        }
    }

}
