using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Singeton class handling everything about dialogs. Connects the GUI with the currently ongoing dialog
 * 
 */ 
public class DialogSystem : MonoBehaviour{
    #region Singleton
    private static DialogSystem instance;

    public static DialogSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DialogSystem();
            }
            return instance;

        }
    }
    #endregion

    

    private DialogUI dialogUI;
    private Interacter interacter;

    private Dialog dialog;
    private QuestDialog questDialog;
    private DialogNPC talker;

    private void Start()
    {
        instance = this;
        interacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Interacter>();
        dialogUI = GameObject.FindGameObjectWithTag("Dialog").GetComponent<DialogUI>();
    }

    private DialogSystem()
    {
        
    }

    /*
     * initializes the given dialog and tells the GUI to show the dialog
     */
    public void StartDialog(Dialog dialog, DialogNPC talker)
    {
        this.dialog = dialog;
        this.talker = talker;

        dialogUI.SetDialogExchange(dialog.GetCurrentDialogExchange());
        dialogUI.SetTalkerName(talker.NPCName);
        dialogUI.Show();
    }

    /*
     * Asks the current dialog what exchange is the next and then tells GUI to show that exchange
     */ 
    public void Respond(int responseNumber)
    {
        DialogExchange nextDialogExchange = dialog.GetNextDialogExchange(responseNumber);
        if (nextDialogExchange == null)
        {
            EndDialog();
        } else
        {
            dialogUI.SetDialogExchange(nextDialogExchange);
        }
    }

    /*
     * Hides the dialog GUI and tells the dialog that it ended
     */ 
    public void EndDialog()
    {
        dialogUI.Hide();
        dialog.End();
    }

}
