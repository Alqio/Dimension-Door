using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateText : MonoBehaviour
{
    public TextAsset text;
    public int startLine;
    public int endLine;
    public TextIDManager textIDManager;
    public bool isInside;
    public Patient patient = null;
    private Patient[] patients;

    public string triggerTextID = "@ID11111";
    public string patient1ID = "@ID56789";
    public string patient2ID = "@ID54321";
    public string patient3ID = "@ID65432";

    // Use this for initialization
    void Start()
    {
        textIDManager = FindObjectOfType<TextIDManager>();
        patients = FindObjectsOfType<Patient>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePatient();
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!textIDManager.isActive && isInside)
            {
                if (patient == null)
                {
                    textIDManager.LoadTextWithID(triggerTextID);
                }
                else if (patient.gameObject.CompareTag("Patient1"))
                {
                    textIDManager.LoadTextWithID(patient1ID);
                }
                else if (patient.gameObject.CompareTag("Patient2"))
                {
                    textIDManager.LoadTextWithID(patient2ID);
                }
                else if (patient.gameObject.CompareTag("Patient3"))
                {
                    textIDManager.LoadTextWithID(patient3ID);
                }
            }
        }
    }

    void UpdatePatient()
    {
        for (int i = 0; i < patients.Length; i++)
        {
            if (patients[i].isInHub)
            {
                patient = patients[i];
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
