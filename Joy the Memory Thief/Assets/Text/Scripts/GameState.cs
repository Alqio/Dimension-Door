using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance = null;
    public int level;
    public Patient patient;
    public bool hasPassedLevel;
    // Start is called before the first frame update
    void Awake()
    {
        level = 0;
        patient = null;
        hasPassedLevel = true;
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
        Object.DontDestroyOnLoad(this);
    }

}
