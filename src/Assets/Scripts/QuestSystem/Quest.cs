using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * class representing a quest consisting of   several goals
 * 
 */ 
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject {

    public List<QuestGoal> goals;
    public bool active;
    public bool completed;
    public string questName;
    public string description;
    public Quest followingQuest;

    public int expReward;

    public delegate void OnQuestCompleted(Quest q);
    public OnQuestCompleted onQuestCompletedCallback;


    /*
     * creates new instances of the quest goals 
     */
    public void Init(Quest q)
    {
        
        this.questName = q.questName;
        this.description = q.description;
        if(q.followingQuest != null)
        {
            followingQuest = ScriptableObject.CreateInstance<Quest>();
            followingQuest.Init(q.followingQuest);
        }
        goals = new List<QuestGoal>();
        foreach(QuestGoal qg in q.goals)
        {
            KillQuestGoal newKillQuestGoal = qg as KillQuestGoal;
            if(newKillQuestGoal != null)
            {
                KillQuestGoal newGoal = ScriptableObject.CreateInstance<KillQuestGoal>();
                newGoal.InitKillQuestGoal(newKillQuestGoal.requiredAmount, newKillQuestGoal.enemyName);
                goals.Add(newGoal);
                continue;
            }
            GatherQuestGoal newGatherQuestGoal = qg as GatherQuestGoal;
            if (newGatherQuestGoal != null)
            {
                GatherQuestGoal newGoal = ScriptableObject.CreateInstance<GatherQuestGoal>();
                newGoal.InitGatherQuestGoal(newGatherQuestGoal.requiredAmount,newGatherQuestGoal.itemID);
                goals.Add(newGoal);
                continue;
            }
            LevelQuestGoal newLevelQuestGoal = qg as LevelQuestGoal;
            if (newLevelQuestGoal != null)
            {
                LevelQuestGoal newGoal = ScriptableObject.CreateInstance<LevelQuestGoal>();
                newGoal.Init(newLevelQuestGoal.requiredAmount);
                goals.Add(newGoal);
                continue;
            }
            HoardQuestGoal newHoardQuestGoal = qg as HoardQuestGoal;
            if (newHoardQuestGoal != null)
            {
                HoardQuestGoal newGoal = ScriptableObject.CreateInstance<HoardQuestGoal>();
                newGoal.Init(newHoardQuestGoal.requiredAmount);
                goals.Add(newGoal);
                continue;
            }
            CompleteQuestGoal newCompleteQuestGoal = qg as CompleteQuestGoal;
            if (newCompleteQuestGoal != null)
            {
                CompleteQuestGoal newGoal = ScriptableObject.CreateInstance<CompleteQuestGoal>();
                newGoal.Init(newCompleteQuestGoal.requiredAmount);
                goals.Add(newGoal);
                continue;
            }
        }
        Init();
    }

    public void Init(string questName, List <QuestGoal> goals, string description)
    {
        this.goals = goals;
        this.questName = name;
        this.description = description;
        foreach(QuestGoal questGoal in goals)
        {
            questGoal.OnGoalCompletedCallback += Eval;
        }
    }

    public void Init()
    {
        completed = false;
        foreach (QuestGoal questGoal in goals)
        {
            questGoal.OnGoalCompletedCallback += Eval;
            questGoal.Init();
        }
    }

    public void Eval()
    {
        foreach (QuestGoal questGoal in goals)
        {
            if (!questGoal.completed)
            {
                break;
            }
        }
        Complete();
    }

    public void Complete()
    {
        completed = true;
        if(onQuestCompletedCallback != null)
        {
            onQuestCompletedCallback.Invoke(this);
        }
        
        Events.Instance.QuestCompleted(this);


    }
}
