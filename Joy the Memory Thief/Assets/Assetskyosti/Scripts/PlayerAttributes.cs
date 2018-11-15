using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttributes : MonoBehaviour {

    public float spd;
    public float maxSpd;

    public float jumpPower;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    public Transform groundCheck;

    private RotateGameWorld rotateScript;

    public int score;
    public int maxScore = 4;
    public Text scoreText;
    public Text endText;
 
    // Use this for initialization
    void Start () {
        score = 0;
        SetText(scoreText, "Score: " + score);
        SetText(endText, "");
    }

    private void Awake()
    {
        rotateScript = GetComponent<RotateGameWorld>();
    }

    // Update is called once per framFcoine
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            SetText(scoreText, "Memory fractions left: " + (maxScore - score));
        }
        if(other.transform.IsChildOf(GameObject.FindGameObjectWithTag("Mazes").transform))
        {

            rotateScript.activeMaze = other.gameObject;
            Debug.Log(rotateScript.activeMaze.tag);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.IsChildOf(GameObject.FindGameObjectWithTag("Mazes").transform))
        {
            rotateScript.activeMaze = null;

        }
    }

    public void SetText(Text textObject, string text)
    {
        textObject.text = text;
    }

}
