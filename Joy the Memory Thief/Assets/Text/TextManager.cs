using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {


    public GameObject textBox;
    public Text textShown;
    public TextAsset text;
    public string[] textLines;
    public int currentLine;
    public int endAtLine;
    public bool isActive;
    public bool limitActions;

    // Use this for initialization
    void Start()
    {     
        if (text != null)
        {
            textLines = text.text.Split('\n');

            if (endAtLine == 0)
            {
                endAtLine = textLines.Length - 1;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
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
        else {
            return;
        }
    }

    public void EnableBox()
    {
        textBox.SetActive(true);
        isActive = true;
        limitActions = true;
    }

    public void DisableBox()
    {
        textBox.SetActive(false);
        isActive = false;
        limitActions = false;
    }

    public void ReloadScript(TextAsset newText, int end, int start)
    {
        endAtLine = end;
        currentLine = start;
        
        if (newText != null)
        {
            textLines = new string[1];
            textLines = newText.text.Split('\n');
        }
        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
        EnableBox();
    }
}
