using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public float yDiff = 5;
    public float size;
    public Transform playerTransform;

	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + yDiff, -100);
        //Camera.main.orthographicSize = size;

    }
}
