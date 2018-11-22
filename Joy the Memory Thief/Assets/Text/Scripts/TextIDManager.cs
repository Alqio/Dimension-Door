using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIDManager : MonoBehaviour
{
    public TextAsset allTextInGame;
    public Dictionary<string, (int, int)> TextIDs = new Dictionary<string, (int, int)>();
    private string[] textLines;
    public GameObject textBox;
    public Text textShown;
    public bool isActive;
    public bool limitActions;
    private int currentLine;
    private int endAtLine;
    public string textID = "@ID12345";
    public string firstTextID = "@ID23456";
    public PlayerControl playerControl;
    private bool isTyping;
    private bool cancelTyping;
    public float typeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        isTyping = false;
        playerControl = FindObjectOfType<PlayerControl>();
        if (allTextInGame != null)
        {
            string ID = "";
            int start = 0;
            int end = 0;
            textLines = allTextInGame.text.Split('\n');
            for (int i = 0; i < textLines.Length; i++)
            {
                string currentLine = textLines[i];
                if (currentLine.Contains("@ID"))
                {
                    ID = currentLine.Trim();
                }
                else if (currentLine.Contains("@start"))
                {
                    start = i + 1;
                }
                else if (currentLine.Contains("@end"))
                {
                    end = i - 1;
                    TextIDs.Add(ID, (start, end));
                }
            }
        }
        if (isActive)
        {
            EnableBox();
        }
        else
        {
            DisableBox();
        }
        if (GameState.instance.level == 0)
        {
            LoadTextWithID(firstTextID);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // skips the text completely
            DisableBox();
        }
        if (isActive)
        {
            if (currentLine == -1)
            {
                textShown.text = "ERROR: Wrong text ID";
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!isTyping)
                {
                    currentLine += 1;
                    if (endAtLine < currentLine)
                    {
                        DisableBox();
                    }
                    else
                    {
                        StartCoroutine(TextScroll(textLines[currentLine]));                        
                    }                   
                }
                else if (isTyping && !cancelTyping)
                {
                    cancelTyping = true;
                }              
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!isActive)
                {
                    LoadTextWithID(textID);
                }
            }
            return;
        }
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        textShown.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            textShown.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(typeSpeed);
        }
        textShown.text = lineOfText;
        isTyping = false;
    }

    // returns the start and end index of a text according to its ID
    public (int, int) getTextPos(string ID)
    {
        (int, int) res = (-1, -1);
        if (TextIDs.ContainsKey(ID))
        {
            res = TextIDs[ID];
        }
        return res;
    }

    public void EnableBox()
    {     
        textBox.SetActive(true);
        isActive = true;
        limitActions = true;
        playerControl.canMove = false;
        StartCoroutine(TextScroll(textLines[currentLine]));
    }

    public void DisableBox()
    {
        textBox.SetActive(false);
        isActive = false;
        limitActions = false;
        playerControl.canMove = true;
    }

    public void LoadTextWithID(string ID)
    {
        (int, int) position = getTextPos(ID);
        currentLine = position.Item1;
        endAtLine = position.Item2;
        EnableBox();
    }
}
