using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * class representing a single goal of a quest
 */ 
[System.Serializable]
public class QuestGoal : ScriptableObject {

    public int requiredAmount;
    public int completedAmount;
    public bool completed;

    [HideInInspector]
    public string description;

    public delegate void OnGoalCompleted();
    public OnGoalCompleted OnGoalCompletedCallback;

    internal void Init(int requiredAmount)
    {
        this.requiredAmount = requiredAmount;
    }

    virtual internal void Init()
    {
        completed = false;
        completedAmount = 0;
    }

    public void Eval()
    {
        if(!completed && completedAmount >= requiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        completed = true;
        OnGoalCompletedCallback.Invoke();
    }
}
