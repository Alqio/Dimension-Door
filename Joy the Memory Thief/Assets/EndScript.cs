using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        print("colliding");
        if (GameState.instance.level >= 3)
        {
            print("nyt pitäis siirtyä");
            GameObject.FindObjectOfType<TransitionAnimation>().LoadScene("Credits");
            GameState.instance.finished = true;
        }
    }

}
