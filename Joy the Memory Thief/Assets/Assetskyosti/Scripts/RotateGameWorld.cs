using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGameWorld : MonoBehaviour {

    private GameObject[] platforms;

    public float rotationSpeed = 15f;

    private Transform playerTransform;
    private int ringNumber = 0;
    private int maxRing = 2;
    public int targetAngle = 0;

	// Use this for initialization
	void Start () {
        playerTransform = GetComponent<Transform>();
    }
	
    public void Rotate(Vector3 direction)
    {
        platforms = GameObject.FindGameObjectsWithTag("Ring" + ringNumber);
        foreach (GameObject platform in platforms)
        {
            platform.transform.RotateAround(Vector3.zero, direction, rotationSpeed * Time.deltaTime);
        }


        platforms = GameObject.FindGameObjectsWithTag("ChildRing" + ringNumber);
        foreach (GameObject platform in platforms)
        {
            GameObject collectable = platform.transform.parent.gameObject;
            Quaternion rot = collectable.transform.rotation;
            collectable.transform.RotateAround(Vector3.zero, direction, rotationSpeed * 2 * Time.deltaTime);
            collectable.transform.rotation = rot; 
        }

        /*
        collectables = GameObject.FindGameObjectsWithTag("Coin")
        foreach (GameObject platform in platforms)
        {
            collectable.transform.RotateAround(Vector3.zero, direction, rotationSpeed * Time.deltaTime);
        }
         */
    }

    public void IncreaseRing()
    {
        ringNumber = Mathf.Min(ringNumber + 1, maxRing);
    }

    public void DecreaseRing()
    {
        ringNumber = Mathf.Max(ringNumber - 1, 0);
    }

}
