using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform playerTransform;

	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 5, -100);
	}
}
