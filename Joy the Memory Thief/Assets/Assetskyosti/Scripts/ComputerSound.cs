using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerSound : MonoBehaviour
{
    public AudioClip blip1;
    public AudioClip blip2;
    public AudioClip blip3;
    public AudioClip blip4;
    public AudioClip blip5;
    public AudioClip blip6;

    private AudioClip[] clips;

    private Random rng = new Random();

    private void Shuffle(AudioClip[] list)
    {
        int n = list.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            AudioClip value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void Awake()
    {
        clips.add(blip1, blip2, blip3, blip4, blip5, blip6);
    }

    void ShuffleClips()
    {
        Shuffle(clips);
    }

    void PlayClips()
    {
        ShuffleClips();
        for (int i = 0; i < clips.Length; i++)
        {
            SoundManager.instance.PlaySfx(clips[i]);
        }
    }

}
