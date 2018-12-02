using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreCount : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = GameObject.FindGameObjectsWithTag("Memory").Length.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        int i = GameObject.FindGameObjectsWithTag("Memory").Length;
        this.GetComponent<Text>().text = i.ToString();
        if(i == 0)
        {
            GameState g = GameObject.FindGameObjectWithTag("state").GetComponent<GameState>();
            g.hasPassedLevel = true;
            g.trigger = true;
        }
    }
}
