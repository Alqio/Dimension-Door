using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tietokone : MonoBehaviour
{
    // Start is called before the first frame update
    bool playerInRange;
    public int taso;
    public List<GameObject> uhrit;
    public List<string> levelNames;
    private void OnTriggerEnter2D(Collider2D collision) => playerInRange = true;

    private void OnTriggerExit2D(Collider2D collision) => playerInRange = false;

    void Start()
    {
        playerInRange = false;
        taso = 0;
        uhrit = new List<GameObject>();
        levelNames = new List<string>();
        //uhrit järjestyksessä
        uhrit.Add(GameObject.Find("tyyppi1"));
        uhrit.Add(GameObject.Find("tyyppi2"));
        uhrit.Add(GameObject.Find("tyyppi3"));
        levelNames.Add("taso1");
        levelNames.Add("taso2");
        levelNames.Add("taso3");

        //uhrien levelit järjestyksessä

        foreach (GameObject g in uhrit) {
            SpriteRenderer sprender = g.GetComponent<SpriteRenderer>();
            sprender.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            print(playerInRange);
            if (playerInRange) {
                taso++;
            }
            
            print("oioiio");
        }
            
        //if (true)
        //{
        //    print("moi1");
        //    if (input.getkeydown(keycode.a) && playerinrange)
        //    {
        //        taso++;
        //        print("moi2 " + taso);

        //    }
        //}
        if (uhrit.Count > taso-1 && taso > 0)
        {
            print("Moi");
            SpriteRenderer sprender = uhrit[taso-1].GetComponent<SpriteRenderer>();
            sprender.enabled = true;
            if (taso != 1) {
                sprender = uhrit[taso - 2].GetComponent<SpriteRenderer>();
                sprender.enabled = false;
            }
        }
    }

    
}
