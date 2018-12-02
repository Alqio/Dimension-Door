using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yo : MonoBehaviour
{
    // Start is called before the first frame update
    GameState g;
    void Start()
    {
        g = GameObject.FindGameObjectWithTag("state").GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((g.level == 1 || g.level == 2) && !g.hasPassedLevel)
            this.GetComponent<Text>().text = "press 'a' to activate";
        else
            this.GetComponent<Text>().text = "...";
    }
}
