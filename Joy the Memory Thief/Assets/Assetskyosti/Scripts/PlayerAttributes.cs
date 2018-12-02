using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerAttributes : MonoBehaviour {

    public float spd;
    public float maxSpd;

    public float jumpPower;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    public Transform groundCheck;

    private RotateGameWorld rotateScript;

    public int score;
    public int maxScore;
    public Text scoreText;
    public Text menuText;
 
    // Use this for initialization
    void Start () {
        score = 0;
        maxScore = 19;
     
    }

    private void Awake()
    {
        rotateScript = GetComponent<RotateGameWorld>();
    }

    // Update is called once per framFcoine
    void Update()
    {
        if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.Q))
        {
            GameState.instance.hasPassedLevel = true;
            GameObject.FindObjectOfType<TransitionAnimation>().LoadScene("SampleScene");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Memory"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            SetText(scoreText, "Memory fractions left: " + (maxScore - score));
            Debug.Log(score);
            if (score >= maxScore) // pass condition for now
            {
                Debug.Log("Level passed!");
                GameState.instance.hasPassedLevel = true;
                GameObject.FindObjectOfType<TransitionAnimation>().LoadScene("SampleScene");               
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level1" && other.transform.IsChildOf(GameObject.FindGameObjectWithTag("Mazes").transform))
        {
            GameObject maze = GameObject.FindGameObjectWithTag("Mazes");

            if (maze != null && other.transform.IsChildOf(maze.transform))
            {
                rotateScript.activeMaze = other.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (SceneManager.GetActiveScene().name == "Level1" && other.transform.IsChildOf(GameObject.FindGameObjectWithTag("Mazes").transform))
        {
            rotateScript.activeMaze = null;
        }
    }

    public void SetText(Text textObject, string text)
    {
        if (textObject != null)
        {
            textObject.text = text;
        }
    }

}
