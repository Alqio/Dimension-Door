using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioQueue : MonoBehaviour
{
    private bool playing;
    private int index;
    private AudioClip currentClip;
    private List<AudioClip> clips;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        playing = false;
        index = 0;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing && !audio.isPlaying && index < clips.Count)
        {
            currentClip = clips[index];
            audio.clip = currentClip;
            audio.Play();
            index++;
            
        }
        if (clips != null && index == clips.Count)
        {
            playing = false;
        }
        
    }

    public void PlayQueue(List<AudioClip> list)
    {
        playing = true;
        clips = list;
        index = 0;
        if (list.Count > 0)
            currentClip = list[0];
    }

}
