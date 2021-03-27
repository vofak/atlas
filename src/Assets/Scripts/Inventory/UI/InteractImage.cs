using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script for the image that indicates to the player that he can interact with a game object
 */ 
public class InteractImage : MonoBehaviour {

    [SerializeField]
    private Text optionText;

	// Use this for initialization
	void Start () {
        optionText.text = "";
	}

    public void SetOptionText(string str)
    {
        optionText.text = str;
    }
	
	
}
