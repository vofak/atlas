using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * class representing a single exchange. Contains what NPC says and players possible responses
 */ 
[System.Serializable]
public class DialogExchange{

    public string NPCsays;
    public List<Response> responses;
    public int end;


    /*
     * class holding the text of my response and number of next dialog exchange in the dialog that this response leads to
     */ 
    [System.Serializable]
    public class Response
    {
        public string text;
        public int number;
    }

}
