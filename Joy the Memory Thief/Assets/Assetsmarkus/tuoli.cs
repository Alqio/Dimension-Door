using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tuoli : MonoBehaviour
{
    bool playerInRange;
    tietokone tietokone_;
    private void OnTriggerEnter2D(Collider2D collision) => playerInRange = true;

    private void OnTriggerExit2D(Collider2D collision) => playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        tietokone_ = GameObject.Find("kone").GetComponent<tietokone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && playerInRange)
        {
            if (tietokone_.uhrit.Count > tietokone_.taso - 1 && tietokone_.taso > 0)
            {
                SceneManager.LoadScene(tietokone_.levelNames[tietokone_.taso - 1]);
            }
        }
    }
}
