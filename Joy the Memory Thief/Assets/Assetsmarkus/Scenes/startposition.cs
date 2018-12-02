using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startposition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameState state = GameObject.FindGameObjectWithTag("state").GetComponent<GameState>();
        if (state.level == 0)
            transform.position = new Vector3(-28, -13.5f, 0);
        else
            transform.position = new Vector3(-3, -18f, 0);

    }


}
