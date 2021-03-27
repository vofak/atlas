using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * represents a single goal of a quest that requires killing certain number of enemies
 */
[CreateAssetMenu(fileName = "NewKillQuestGoal", menuName = "Quest System/Kill Quest Goal")]
public class KillQuestGoal : QuestGoal {
    public string enemyName;

    public void InitKillQuestGoal(int requiredAmount, string enemyName)
    {
        Init(requiredAmount);
        this.enemyName = enemyName;
        Events.Instance.onEnemyDiedCallback += EnemyDied;
    }

    internal override void Init()
    {
        base.Init();
    }

    /*
     * increments completedAmount if the killed enemy is of type that this quest is ineterested in
     */ 
    private void EnemyDied(string enemyName)
    {
        
        if (this.enemyName.Equals("") || enemyName.Equals(this.enemyName))
        {
            completedAmount++;
            Eval();
        }
    }
}
