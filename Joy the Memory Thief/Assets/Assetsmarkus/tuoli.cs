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
    public ActivateText machineTrigger;
    private List<Patient> patients = new List<Patient>();
    private bool hasBeenPressed;

    static int sortByNumber(Patient p1, Patient p2)
    {
        return p1.number.CompareTo(p2.number);
    }

    // Start is called before the first frame update
    void Start()
    {
        hasBeenPressed = false;
        Patient[] res = FindObjectsOfType<Patient>();
        for (int i = 0; i < res.Length; i++)
        {
            patients.Add(res[i]);
        }
        patients.Sort(sortByNumber);
        tietokone_ = GameObject.Find("kone").GetComponent<tietokone>();       
    }   

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A) && playerInRange)
        {
            machineTrigger.PatientText(patients[tietokone_.taso - 1]);
            hasBeenPressed = true;
        }
        if (!machineTrigger.textIDManager.isActive && hasBeenPressed)
        {
            SceneManager.LoadScene(tietokone_.levelNames[tietokone_.taso - 1]);
            hasBeenPressed = false;
        }
    }
}
