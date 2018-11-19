using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance = null;
    public int level = 1;
    public Patient patient = null;
    public bool hasPassedLevel = false;
    // Start is called before the first frame update
    void Awake()
    {
        //Check if there is already an instance of GameState
        if (instance == null)
        {
            //if not, set it to this
            instance = this;
        }            
        //If instance already exists:
        else if (instance != this)
        {
            //Destroy this, there can only be one instance of GameState.
            Destroy(gameObject);
        }           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
