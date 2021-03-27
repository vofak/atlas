using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * class handling the response button in dialog GUI
 */
public class ResponseUI : MonoBehaviour, IPointerClickHandler
{
    private int responseNumber;

    public void Init(int nb, string text)
    {
        GetComponent<Text>().text = text;
        this.responseNumber = nb;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(responseNumber);
        DialogSystem.Instance.Respond(responseNumber);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
