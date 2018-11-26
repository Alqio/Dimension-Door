using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromTo : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform A;
    public Transform B;
    public float speed;
    public bool isMoving;
    public bool loops;
    public bool movingToA;
    public bool onlyWhenSinging;
    public Transform[] monimutkanen;
    private mic mic;

    public bool stopOnCollision;
    public bool stopWhenHittingPlayer;
    bool somethingOnTheWay;
    string who;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        somethingOnTheWay = true;
        who = collision.collider.gameObject.tag;
    }
    private void OnCollisionExit2D(Collision2D collision) => somethingOnTheWay = false;

    void Start()
    {
        transform.position = A.position;
        isMoving = false;
        movingToA = true;
        mic = GameObject.FindGameObjectWithTag("microphone").GetComponent<mic>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(somethingOnTheWay);
        if (!stopOnCollision || (stopOnCollision && !somethingOnTheWay))// && (who != "Player" || stopWhenHittingPlayer)))
        {
            Transform t;

            t = transform;

            if (!mic.singing && onlyWhenSinging)
                isMoving = false;

            float AB = Vector3.Distance(A.position, B.position);
            if (((!movingToA && AB < Vector3.Distance(this.transform.position, A.position)) ||
                (movingToA && AB < Vector3.Distance(this.transform.position, B.position))))
            {
                if (movingToA)
                    t.position = A.position;
                else
                    t.position = B.position;

                movingToA = !movingToA;
                isMoving = false;
            }
            else
            {
                if (isMoving)
                {
                    if (movingToA)
                    {
                        t.position = transform.position + -(B.position - A.position).normalized * Time.deltaTime * speed;

                    }
                    else
                    {
                        t.position = transform.position + -(A.position - B.position).normalized * Time.deltaTime * speed;

                    }
                    foreach (Transform i in monimutkanen)
                        i.position = t.position;
                }
            }
        }
           

    }
}
