using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour {

    public float yDiff = 5;
    public float sizemin;
    public float sizemax;
    public float speed;
    public Transform playerTransform;
    float size;
    // Update is called once per frame
    private void Start()
    {
        size = sizemax;
    }
    void Update () {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + yDiff, -100);
        if(SceneManager.GetActiveScene().name == "taso2")
        {
            if (Input.GetKey(KeyCode.Z))
            {
                size = Mathf.Min(sizemax, size + (speed * Time.deltaTime));

            }
            else
            {
                size = Mathf.Max(sizemin, size-(speed*Time.deltaTime));

            }
            Camera.main.orthographicSize = size;

        }

    }
}
