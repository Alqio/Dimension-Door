﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateText : MonoBehaviour
{
    public TextAsset text;
    public int startLine;
    public int endLine;
    public TextIDManager textIDManager;
    public bool isInside;
    //public Patient patient = null;
    private Patient[] patients;


    public string triggerTextID;
    public string patient1ID;
    public string patient2ID;
    public string patient3ID;
    public GameState gamestate;

    // Use this for initialization
    void Start()
    {
        textIDManager = FindObjectOfType<TextIDManager>();
        patients = FindObjectsOfType<Patient>();
        gamestate = FindObjectOfType<GameState>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePatient();
        if (Input.GetKeyDown(KeyCode.X))
        {
            PatientText(gamestate.patient);
        }
    }

    public void PatientText(Patient patientInput)
    {
        if (!textIDManager.isActive && isInside)
        {
            if (patientInput == null)
            {
                
                textIDManager.LoadTextWithID(triggerTextID);
            }
            else if (patientInput.gameObject.CompareTag("Patient1"))
            {
                textIDManager.LoadTextWithID(patient1ID);
            }
            else if (patientInput.gameObject.CompareTag("Patient2"))
            {
                textIDManager.LoadTextWithID(patient2ID);
            }
            else if (patientInput.gameObject.CompareTag("Patient3"))
            {
                textIDManager.LoadTextWithID(patient3ID);
            }
        }
    }

    void UpdatePatient()
    {
        for (int i = 0; i < patients.Length; i++)
        {
            if (patients[i].isInHub)
            {
                gamestate.patient = patients[i];
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isInside = false;
        }
    }
}
