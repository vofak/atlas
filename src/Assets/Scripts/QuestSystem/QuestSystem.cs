using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Sinngleton class handling accecpting and completing quests by the  player
 */ 
[System.Serializable]
public class QuestSystem {
    #region Singleton
    private static QuestSystem instance;

    public static QuestSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new QuestSystem();
            }
            return instance;

        }

        set
        {
            instance = value;
        }
    }
    #endregion

    List<Quest> activeQuests = new List<Quest>();
    List<Quest> completedQuests = new List<Quest>();

    private QuestSystem()
    {
        Events.Instance.onQuestCompletedCallback += CompleteQuest;
    }

    /*
     * initializes the quest and adds it to aaccccepted quests
     */ 
    public void AcceptQuest(Quest quest)
    {

        Debug.Log(quest.questName + " accepted");
        quest.Init();
        quest.onQuestCompletedCallback += CompleteQuest;
        activeQuests.Add(quest);
        JournalUI.Instance.ShowMessage("<b>Accepted quest: " + quest.questName + "</b>");
    }

    /*
     * removes the quest from accepted quests and adds it to completed quests
     */ 
    public void CompleteQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            return;
        }

        Debug.Log(quest.questName + " completed");
        activeQuests.Remove(quest);
        completedQuests.Add(quest);
        if(quest.followingQuest != null)
        {
            AcceptQuest(quest.followingQuest);
        }
        JournalUI.Instance.ShowMessage("<b>Completed quest: " + quest.questName + "</b>");
    }
}
