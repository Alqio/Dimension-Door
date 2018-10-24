using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateText : MonoBehaviour {

    public TextAsset text;
    public int startLine;
    public int endLine;
    public TextManager textmanager;


	// Use this for initialization
	void Start () {
        textmanager = FindObjectOfType<TextManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!textmanager.isActive)
            {
                textmanager.ReloadScript(text, endLine, startLine);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            if (!textmanager.isActive)
            {
                textmanager.ReloadScript(text, endLine, startLine);
            }       
        }
    }

}
