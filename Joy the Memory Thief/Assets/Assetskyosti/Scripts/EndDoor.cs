using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameState.instance.level >= 3)
        {
            GetComponent<ovi>().enabled = true;
        }
    }
}
