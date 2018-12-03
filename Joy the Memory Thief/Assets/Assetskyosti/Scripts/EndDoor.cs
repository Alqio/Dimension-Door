
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.instance.level >= 3 || (GameState.instance.level == 2 && GameState.instance.hasPassedLevel))
        {
            GetComponent<ovi>().enabled = true;
        }
        if (player.transform.position.x + 3 < transform.position.x)
        {
            GameObject.FindObjectOfType<TransitionAnimation>().LoadScene("Credits");
            GameState.instance.finished = true;
        }

    }
}
