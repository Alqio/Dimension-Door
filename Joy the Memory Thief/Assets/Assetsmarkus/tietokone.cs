﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tietokone : MonoBehaviour
{
    // Start is called before the first frame update
    bool playerInRange;
    public int taso;
    public List<GameObject> uhrit;
    public List<string> levelNames;
    public bool hasPassedLevel;

    private void OnTriggerEnter2D(Collider2D collision) => playerInRange = true;

    private void OnTriggerExit2D(Collider2D collision) => playerInRange = false;

    void Start()
    {
        hasPassedLevel = true;
        playerInRange = false;
        taso = 0;
        uhrit = new List<GameObject>();
        levelNames = new List<string>();
        //uhrit järjestyksessä
        uhrit.Add(GameObject.Find("tyyppi1"));
        uhrit.Add(GameObject.Find("tyyppi2"));
        uhrit.Add(GameObject.Find("tyyppi3"));
        levelNames.Add("Level1");
        levelNames.Add("Level1");
        levelNames.Add("Level1");

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
            if (playerInRange && hasPassedLevel) {
                taso++;
            }
            //print("oioiio");
        }
            
        
        if (uhrit.Count > taso-1 && taso > 0)
        {
            //print("Moi");
            SpriteRenderer sprender = uhrit[taso-1].GetComponent<SpriteRenderer>();
            Patient patient = uhrit[taso - 1].GetComponent<Patient>();
            patient.isInHub = true;
            sprender.enabled = true;
            if (taso != 1) {
                sprender = uhrit[taso - 2].GetComponent<SpriteRenderer>();
                patient = uhrit[taso - 2].GetComponent<Patient>();
                sprender.enabled = false;
                patient.isInHub = false;
            }
        }
    }

    
}
