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

    private List<AudioClip> clips;
    private AudioQueue audioQueue;

    private Random rng = new Random();

    private void Shuffle(List<AudioClip> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            AudioClip temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void Awake()
    {
        clips = new List<AudioClip> { blip1, blip2, blip3, blip4, blip5, blip6 };
        audioQueue = GetComponent<AudioQueue>();
    }

    void ShuffleClips()
    {
        Shuffle(clips);
    }

    public void PlayClips()
    {
        ShuffleClips();
        audioQueue.PlayQueue(clips);
        
    }

}
