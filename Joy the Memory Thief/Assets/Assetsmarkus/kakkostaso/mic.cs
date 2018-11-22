using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;


public class mic : MonoBehaviour
{
    private AudioSource _audioSource;
    public bool mic_on;
    public AudioClip _audioClip;
    public string _selectedDivice;
    public AudioMixerGroup _mixerMic;

    float[] spectrum;
    int sampleRate;
    Queue<int> recent_notes;

    string[] notes;
    float[] notesHz;
    Vector3[] notesRGB;

    GameObject[] halos;
    GameObject[] platforms;

    private Vector3 latestColor;
    public char latestColor_char;
    public bool singing;
    // Start is called before the first frame update
    void Start()
    {
        singing = false;
        latestColor = new Vector3(0, 0, 0);
        //string[] notes_ = { "c", "c#", "d", "d#", "e", "f", "f#", "g", "g#", "a", "a#", "h", "c" };
        string[] notes_ = { "c", "d", "e", "f","g","a","h", "c" };
        notes = notes_;
        //float[] notesHz_ = { 16.35f, 17.32f, 18.35f, 19.45f, 20.60f, 21.83f, 23.12f, 24.50f, 25.96f, 27.50f, 29.14f, 30.87f, 32.70f };
        float[] notesHz_ = { 16.35f,18.35f, 20.60f, 21.83f, 24.50f, 27.50f, 30.87f, 32.70f };
        notesHz = notesHz_;
        Vector3[] notesRGB_ = {
            new Vector3(255,153,51), //orange
            new Vector3(255,153,51), //orange
            new Vector3(51,255,51), //green
            new Vector3(51,255,51), //green
            new Vector3(153,51,255), //violet
            new Vector3(153,51,255), //violet
            new Vector3(153,51,255), //violet
            new Vector3(255,153,51), //orange



            //new Vector3(255,255,51), //yellow
            //new Vector3(0,128,255), //blue
            //new Vector3(255,102,255), //pink
            //new Vector3(51,255,51), //green
            //new Vector3(255,153,51), //orange
            //new Vector3(255,51,51), //red
            //new Vector3(153,51,255), //violet
            //new Vector3(255,255,51), //yellow
        };
        notesRGB = notesRGB_;
        _audioSource = GetComponent<AudioSource>();
        spectrum = new float[1024];
        sampleRate = AudioSettings.outputSampleRate;
        recent_notes = new Queue<int>();
        if (mic_on)
        {
            if (Microphone.devices.Length > 0)
            {
                _selectedDivice = Microphone.devices[0].ToString();
                _audioSource.outputAudioMixerGroup = _mixerMic;
                _audioSource.clip = Microphone.Start(_selectedDivice, true, 1, sampleRate);//AudioSettings.outputSampleRate);
                _audioSource.Play();
            }
            else
            {
                mic_on = false;
            }
            
        }
        else {

        }

        halos = GameObject.FindGameObjectsWithTag("halos");
        platforms = GameObject.FindGameObjectsWithTag("musicPlatform");
        print(platforms.Length);
    }

    // Update is called once per frame
    void Update()
    {
        int quesize = recent_notes.Count;

        if (Input.GetKey(KeyCode.C))
        {
            if (quesize == 50)
                recent_notes.Dequeue();
            recent_notes.Enqueue(guessNote());

            int n = recent_notes.GroupBy(x => x)
                  .OrderByDescending(g => g.Count())
                  .Select(g => g.Key)
                  .First();
            if (n != -1 && halos.Length > 0) {
                foreach (GameObject g in halos)
                {
                    g.GetComponent<graphicsMovement>().color_rgb = notesRGB[n];
                    g.GetComponent<graphicsMovement>().alfa_speed = 0.5f;
                  
                }

                Color c = halos[0].GetComponent<SpriteRenderer>().color;
                GameObject halo = GameObject.FindGameObjectWithTag("finalHalo");
                float c_speed = halos[0].GetComponent<graphicsMovement>().color_speed;
                if (Mathf.Abs(c.r - notesRGB[n].x / 255f) < 2 / 255f &&
                    Mathf.Abs(c.g - notesRGB[n].y / 255f) < 2 / 255f &&
                    Mathf.Abs(c.b - notesRGB[n].z / 255f) < 2 / 255f &&
                    quesize > 40)
                {
                    halo.GetComponent<SpriteRenderer>().color = new Color(notesRGB[n].x / 255f, notesRGB[n].y / 255f, notesRGB[n].z / 255f, 1);
                    halo.GetComponent<graphicsMovement>().alfa_speed = 1f;
                    foreach (GameObject p in platforms)
                    {
                        float r = p.GetComponent<soundPlatform>().color_rgb_.x;
                        float g = p.GetComponent<soundPlatform>().color_rgb_.y;
                        float b = p.GetComponent<soundPlatform>().color_rgb_.z;

                        if ((p.GetComponent<soundPlatform>().always || (r == notesRGB[n].x && g == notesRGB[n].y && b == notesRGB[n].z)))
                        {
                            if (!latestColor.Equals(notesRGB[n]))
                            {
                                if (p.GetComponent<soundPlatform>().isMoving)
                                {
                                    if (!p.GetComponent<MoveFromTo>().onlyWhenSinging)
                                        p.GetComponent<MoveFromTo>().movingToA = !p.GetComponent<MoveFromTo>().movingToA;
                                }
                                p.GetComponent<soundPlatform>().invisible = !p.GetComponent<soundPlatform>().invisible;
                            }
                            if (p.GetComponent<soundPlatform>().isMoving)
                            {
                                p.GetComponent<MoveFromTo>().isMoving = true;
                                
                            }
                        }
                        else
                        {
                            if (p.GetComponent<soundPlatform>().isMoving)
                            {
                                p.GetComponent<MoveFromTo>().isMoving = false;                            
                            }
                        }
                        
                    }
                    singing = true;
                    latestColor = notesRGB[n];
                }
                else
                {
                    //halo.GetComponent<SpriteRenderer>().color = new Color(notesRGB[n].x / 255f, notesRGB[n].y / 255f, notesRGB[n].z / 255f, c.a);
                    halo.GetComponent<graphicsMovement>().alfa_speed = -1f;
                    singing = false;
                }

            }
            else
            {
                foreach (GameObject g in halos)
                {
                    g.GetComponent<graphicsMovement>().alfa_speed = -0.5f;
                    g.GetComponent<graphicsMovement>().color_rgb = new Vector3(1f, 1f, 1f);
                }
                GameObject halo = GameObject.FindGameObjectWithTag("finalHalo");
                halo.GetComponent<graphicsMovement>().alfa_speed = -1f;
                singing = false;

            }
        } else //if (quesize != 0)
        {
            recent_notes.Clear();
            foreach (GameObject g in halos) {
                g.GetComponent<graphicsMovement>().alfa_speed = -0.5f;
                
                g.GetComponent<graphicsMovement>().color_rgb = new Vector3(0f, 0f, 0f);
                g.GetComponent<SpriteRenderer>().color = new Color(0,0,0, g.GetComponent<SpriteRenderer>().color.a);

            }
            GameObject halo = GameObject.FindGameObjectWithTag("finalHalo");
            halo.GetComponent<graphicsMovement>().alfa_speed = -1f;
            latestColor = new Vector3(0, 0, 0);
            singing = false;

        }
    }

    int guessNote()
    {
        
        _audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
        float maxHz = sampleRate / 2;
        float blockSize = maxHz / spectrum.Length;

        float m = 0.001f;
        int max_ind = -1;
        for (int i = 0; i < spectrum.Length; i++)
        {
            float Hz = i * blockSize + blockSize / 2;
            if (Hz > 200f && Hz < 4000f)
            {
                if (m < spectrum[i])
                {
                    m = spectrum[i];
                    max_ind = i;
                }
            }
        }

        if (m < 0.001f || max_ind == -1)
            return -1;

        float max_Hz = max_ind * blockSize + blockSize / 2;
        //print("max_HZ " + max_Hz);
        while (max_Hz > 32.70f) 
        {
            max_Hz = max_Hz / 2;
        } 

        //print("max_HZ jäkeen " + max_Hz);

        int closest_ind = 0;
        float closesness = float.MaxValue;
        for (int i = 0; i < notesHz.Length; i++)
        {
            float c = Mathf.Abs(max_Hz - notesHz[i]);
            if (c < closesness)
            {
                closesness = c;
                closest_ind = i;
            }
        }
        //  if (m > 0.001f) {
        //      print(m);
        //      print(notes[closest_ind]);
        //  }
        
        return closest_ind;
        
    }
}
