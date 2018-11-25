using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class tuoli : MonoBehaviour
{
    bool playerInRange;
    tietokone tietokone_;
    private GameState gamestate;
    private void OnTriggerEnter2D(Collider2D collision) => playerInRange = true;

    private void OnTriggerExit2D(Collider2D collision) => playerInRange = false;
    public ActivateText machineTrigger;
    private List<Patient> patients = new List<Patient>();
    private bool hasBeenPressed;

    public bool hasPatient = false;

    public Text activateText;

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
        gamestate = FindObjectOfType<GameState>();

    }   

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            Color c = activateText.color;
            activateText.color = new Color(c.r, c.g, c.b, 1);
        } else
        {
            Color c = activateText.color;
            activateText.color = new Color(c.r, c.g, c.b, 0);
        }

        if (playerInRange && hasPatient)
        {
            GetComponent<AudioSource>().volume = 0.7f;
        } else
        {
            GetComponent<AudioSource>().volume = 0f;
        }

        if (Input.GetKeyDown(KeyCode.A) && playerInRange)
        {
            machineTrigger.PatientText(patients[gamestate.level - 1]);
            hasBeenPressed = true;
        }
        if (!machineTrigger.textIDManager.isActive && hasBeenPressed)
        {
            GameObject.FindObjectOfType<TransitionAnimation>().LoadScene(tietokone_.levelNames[gamestate.level - 1]);
            hasBeenPressed = false;
        }
    }
}
