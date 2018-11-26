using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tietokone : MonoBehaviour
{
    // Start is called before the first frame update
    bool playerInRange;
    public List<GameObject> uhrit;
    public List<string> levelNames;
    public ActivateText computerTrigger;

    public Text activateText;

    private void OnTriggerEnter2D(Collider2D collision) => playerInRange = true;
    private void OnTriggerExit2D(Collider2D collision) => playerInRange = false;

    private ComputerSound computerSound;
    private GameState gamestate;

    public AudioClip patientChange;

    private void Awake()
    {
        gamestate = FindObjectOfType<GameState>();
    }

    void Start()
    {
        playerInRange = false;
        uhrit = new List<GameObject>();
        levelNames = new List<string>();
        //uhrit järjestyksessä
        uhrit.Add(GameObject.Find("tyyppi1"));
        uhrit.Add(GameObject.Find("tyyppi2"));
        levelNames.Add("Level1");
        levelNames.Add("taso2");

        //uhrien levelit järjestyksessä

        foreach (GameObject g in uhrit) {
            SpriteRenderer sprender = g.GetComponent<SpriteRenderer>();
            sprender.enabled = false;
        }
        computerSound = GetComponent<ComputerSound>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            Color c = activateText.color;
            activateText.color = new Color(c.r, c.g, c.b, 1);
        }
        else
        {
            Color c = activateText.color;
            activateText.color = new Color(c.r, c.g, c.b, 0);
        }

        if (Input.GetKeyDown(KeyCode.A) && !computerTrigger.textIDManager.limitActions)
        {
            if (playerInRange && gamestate.hasPassedLevel) {
                // TÄHÄN PATIENTIN VAIHTOÄÄNI
                gamestate.level++;
                gamestate.hasPassedLevel = false;                
                FindObjectOfType<tuoli>().hasPatient = true;
                //SoundManager.instance.PlaySfx(patientChange);
            }
            computerSound.PlayClips();
            computerTrigger.PatientText(uhrit[gamestate.level - 1].GetComponent<Patient>());

            if (uhrit.Count >= gamestate.level && gamestate.level > 0)
            {
                SpriteRenderer sprender = uhrit[gamestate.level - 1].GetComponent<SpriteRenderer>();
                Patient patient = uhrit[gamestate.level - 1].GetComponent<Patient>();
                patient.isInHub = true;
                sprender.enabled = true;
                if (gamestate.level != 1)
                {
                    sprender = uhrit[gamestate.level - 2].GetComponent<SpriteRenderer>();
                    patient = uhrit[gamestate.level - 2].GetComponent<Patient>();
                    sprender.enabled = false;
                    patient.isInHub = false;
                }
            }
        }               
    }
}
