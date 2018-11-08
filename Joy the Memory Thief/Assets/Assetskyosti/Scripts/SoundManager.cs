﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sfx;

    public static SoundManager instance = null;

    // Start is called before the first frame update
    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        //Not neede at the moment as we want the musics to start over.
        //DontDestroyOnLoad(gameObject);
    }

    public void PlaySfx(AudioClip audio)
    {
        sfx.clip = audio;
        sfx.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
