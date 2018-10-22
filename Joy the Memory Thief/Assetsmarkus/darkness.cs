using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkness : MonoBehaviour
{
    bool roomIsHidden;
    float duration;
    float startTime;
    public bool doorOpen;
    bool playerInRange;

    private void OnTriggerEnter2D(Collider2D collision) => playerInRange = true;
    private void OnTriggerExit2D(Collider2D collision) => playerInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        roomIsHidden = true;
        duration = 1;
        doorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen || playerInRange)
            revealRoom();
        else
            hideRoom();
    }

    void revealRoom() {
        if (roomIsHidden)
        {
            startTime = Time.time;
            roomIsHidden = false;
        }
        SpriteRenderer sprender = this.GetComponent<SpriteRenderer>();
        float t = (Time.time - startTime) / duration;
        sprender.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(1, 0, t));
    }

    void hideRoom()
    {
        if (!roomIsHidden)
        {
            startTime = Time.time;
            roomIsHidden = true;
        }
        SpriteRenderer sprender = this.GetComponent<SpriteRenderer>();
        float t = (Time.time - startTime) / duration;
        sprender.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(0, 1, t));
    }
}
