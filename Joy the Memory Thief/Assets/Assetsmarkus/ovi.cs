using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ovi : MonoBehaviour
{
    bool playerInRange;
    public float speed;
    
    public GameObject leftRoom;
    public GameObject rightRoom;

    public AudioClip closeSound;
    public AudioClip openSound;


    float open;
    float closed;

    bool atTop = false;
    bool atBot = true;

    private bool closeSoundPlayed = true;
    private bool openSoundPlayed;

    private void OnTriggerEnter2D(Collider2D collision) => playerInRange = true;

    private void OnTriggerExit2D(Collider2D collision) => playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        open = transform.position.y + 5.5f;
        closed = transform.position.y;
    }

    private void FixedUpdate()
    {

        if (playerInRange && !atTop)
        {
            atTop = MoveUp();
            atBot = false;
            closeSoundPlayed = false;

            leftRoom.GetComponent<darkness>().doorOpen = true;
            rightRoom.GetComponent<darkness>().doorOpen = true;
        }
        if (atTop && !openSoundPlayed)
        {
            SoundManager.instance.PlaySfx(openSound);
            openSoundPlayed = true;
        }

        if (!playerInRange && !atBot)
        {
            atBot = MoveDown();
            atTop = false;
            openSoundPlayed = false;

            rightRoom.GetComponent<darkness>().doorOpen = false;
            leftRoom.GetComponent<darkness>().doorOpen = false;
        }
        if (atBot && !closeSoundPlayed)
        {
            SoundManager.instance.PlaySfx(closeSound);
            closeSoundPlayed = true;
        }


    }
    private bool MoveUp() {
        transform.position += new Vector3(0, speed, 0);
        
        return transform.position.y >= open;
    }

    private bool MoveDown()
    {
        transform.position += new Vector3(0, -speed, 0);

        return transform.position.y <= closed;
    }

}
