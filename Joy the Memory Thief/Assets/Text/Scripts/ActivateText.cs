using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateText : MonoBehaviour {

    public TextAsset text;
    public int startLine;
    public int endLine;
   // public TextPrinter textPrinter;
    public TextIDManager textIDManager;
    
    public string triggerTextID = "@ID11111";

	// Use this for initialization
	void Start () {
        //textPrinter = FindObjectOfType<TextPrinter>();
        textIDManager = FindObjectOfType<TextIDManager>();
    }
	
	// Update is called once per frame
	

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Joy")
        {
            if (/*!textPrinter.isActive*/ !textIDManager.isActive)
            {
                //textPrinter.ReloadScript(text, endLine, startLine);
                textIDManager.LoadTextWithID(triggerTextID);
            }       
        }
    }

}
