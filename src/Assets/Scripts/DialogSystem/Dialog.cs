using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class representing a dialog of an NPC with player. It's basically a DFM
 */ 
[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog System/Dialog")]
public class Dialog : ScriptableObject {

    public List<DialogExchange> dialogExchanges = new List<DialogExchange>();
    

    public int curr = 0;

    public virtual void Reset()
    {
        curr = 0;
    }

    /*
     * Returns the dialogExchange (state) that the DFT is in
     */
    public DialogExchange GetCurrentDialogExchange()
    {
        
        if (curr < 0)
            curr = 0;
        return dialogExchanges[curr];
    }

    /*
     * depending on the current state and the chosen decision, returns the next dialogExchange
     */ 
    public virtual DialogExchange GetNextDialogExchange(int decision)
    {

        int nextDialogExchangeNumber = dialogExchanges[curr].responses[decision].number;
        if (nextDialogExchangeNumber < 0)
        {
            return null;
        }

        curr = nextDialogExchangeNumber;
        return dialogExchanges[curr];
    }

    /*
     * Ends the dialog and sets the current state to wherever desired state
     */
    public void End()
    {
        curr = dialogExchanges[curr].end;
    }
}
