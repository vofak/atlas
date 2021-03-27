using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A dialog with the possibility to give the player a quest
 */ 
[CreateAssetMenu(fileName = "NewQuestDialog", menuName = "Dialog System/Quest Dialog")]
public class QuestDialog : Dialog {

	public Quest quest;
    public int questDialogExchange;
    public int acceptResponse;

    private Quest realQuest;

    /*
     * sets the dialog to the beginning and initializes the quest
     */ 
    public override void Reset()
    {
        base.Reset();
        realQuest = ScriptableObject.CreateInstance<Quest>();
        realQuest.Init(quest);
    }

    public override DialogExchange GetNextDialogExchange(int decision)
    {
        if (curr == questDialogExchange && decision == acceptResponse)
        {
            QuestSystem.Instance.AcceptQuest(realQuest);
        }
        return base.GetNextDialogExchange(decision);
    }
}