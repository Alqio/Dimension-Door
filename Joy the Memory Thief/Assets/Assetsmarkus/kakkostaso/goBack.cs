using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goBack : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform home;
    public Transform player;
    public Transform[] others;
    public mic mic;
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y > player.position.y  && Mathf.Abs(this.transform.position.y - home.position.y) > 0.5f && !mic.singing)
        {
            transform.position = transform.position + (home.position - transform.position).normalized * speed * Time.deltaTime;
            foreach (Transform i in others)
                i.position = transform.position;
        }
    }
}
