using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ovi : MonoBehaviour
{
    bool playerInRange;
    public float speed;

    public GameObject leftRoom;
    public GameObject rightRoom;


    float open;
    float closed;
   
    private void OnTriggerEnter2D(Collider2D collision) => playerInRange = true;

    private void OnTriggerExit2D(Collider2D collision) => playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        open = this.transform.position.y + 4;
        closed = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (playerInRange) {
            if (this.transform.position.y < open)
            {
                move(true);
            }
               
            leftRoom.GetComponent<darkness>().doorOpen = true;
            rightRoom.GetComponent<darkness>().doorOpen = true;              
            
        }
        else if (!playerInRange)
        {
            if(this.transform.position.y > closed)
            {
                move(false);
            }
          
            rightRoom.GetComponent<darkness>().doorOpen = false;            
            leftRoom.GetComponent<darkness>().doorOpen = false;
                
            
        }

    }
    void move(bool up) {
        int i = 1;
        if (!up) i = -1;
        Vector3 pos = this.transform.position;
        Vector3 plus = new Vector3(0, speed*i, 0);
        this.transform.position = pos + plus;
    }
   
}
