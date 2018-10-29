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

    public int score;
    public Text scoreText;
    public Text endText;
 
    // Use this for initialization
    void Start () {
        score = 0;
        SetText(scoreText, "Score: " + score);
        SetText(endText, "");
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
            score += 100;
            SetText(scoreText, "Score: " + score);
        }
        if (other.gameObject.CompareTag("RedKey"))
        {
            other.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("RedDoor").transform.parent.gameObject.SetActive(false);
        }
    }

    public void SetText(Text textObject, string text)
    {
        textObject.text = text;
    }

}
