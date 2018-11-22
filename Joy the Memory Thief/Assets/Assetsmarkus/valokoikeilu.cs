using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class valokoikeilu : MonoBehaviour
{
    // Start is called before the first frame update
    bool isHidden;
    public float duration;
    public float minW;
    public float maxW;
    public float maxAlfa;
    float startTime;
    SpriteRenderer sprender;
    void Start()
    {
        sprender = this.GetComponent<SpriteRenderer>();

        StartCoroutine(Flashing());
    }

    // Update is called once per frame
    void Update()
    {

    }

   

    IEnumerator Flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minW, maxW));
            if (!isHidden)
            {
                startTime = Time.time;
                isHidden = true;
                float t = (Time.time - startTime) / duration;
                sprender.color = new Color(sprender.color.r, sprender.color.g, sprender.color.b, Mathf.SmoothStep(0, 1, t));
            }
            else
            {
                startTime = Time.time;
                isHidden = false;
                float t = (Time.time - startTime) / duration;
                sprender.color = new Color(sprender.color.r, sprender.color.g, sprender.color.b, Mathf.SmoothStep(maxAlfa, 0, t));
            }


        }
    }
}
