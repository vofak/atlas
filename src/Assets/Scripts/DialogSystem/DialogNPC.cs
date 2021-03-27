using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Represents an NPC that we can talk to us when we interact with it
 */ 
public class DialogNPC : MonoBehaviour, IInteractable {

    public Dialog dialog;
    public string NPCName;

    private bool active = true;

    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            active = value;
        }
    }

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

    // Use this for initialization
    void Start()
    {
        dialog.Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * Tells the dialog system that we just started this dialog
     */ 
    public void Interact()
    {
        DialogSystem.Instance.StartDialog(dialog, this);
    }

}
