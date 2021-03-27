using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * represents a single goal of a quest that requires completing certain amount of quests to be completed
 */ 
[CreateAssetMenu(fileName = "NewCompleteQuestGoal", menuName = "Quest System/Complete Quest Goal")]
public class CompleteQuestGoal : QuestGoal {


    internal override void Init()
    {
        base.Init();
        Events.Instance.onQuestCompletedCallback += QuestCompleted;
    }

    private void QuestCompleted(Quest q)
    {
        this.completedAmount++;
        Eval();
    }
}
