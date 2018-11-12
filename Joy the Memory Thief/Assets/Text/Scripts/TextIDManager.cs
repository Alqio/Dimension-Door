using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIDManager : MonoBehaviour
{
    public TextAsset allTextInGame;
    public Dictionary<string, (int, int)> TextIDs = new Dictionary<string, (int, int)>();
    public List<string> textIDList = new List<string>();
    public string[] textLines;
    public GameObject textBox;
    public Text textShown;
    public bool isActive;
    public bool limitActions;
    public int currentLine;
    public int endAtLine;
    public string textID = "@ID12345";
    public string firstTextID = "@ID23456";
    public PlayerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
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
                    textIDList.Add(ID);
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
        LoadTextWithID(firstTextID);
    }

    // Update is called once per frame
    void Update()
    {
         
        if (isActive)
        {
            if (currentLine == -1)
            {
                textShown.text = "ERROR";
            }
            else
            {
                if (endAtLine < currentLine)
                {
                    DisableBox();
                    
                }
                else
                {
                    textShown.text = textLines[currentLine];
                    if (Input.GetKeyDown(KeyCode.Return))
                    {                        
                        currentLine += 1;
                    }
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
