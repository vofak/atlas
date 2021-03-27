using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * represents a single goal of a quest that requires leveling up couple of times
 */
[CreateAssetMenu(fileName = "NewLevelQuestGoal", menuName = "Quest System/Level Quest Goal")]
public class LevelQuestGoal : QuestGoal {

    internal override void Init()
    {
        base.Init();
        Events.Instance.onLevelUpCallback += LevelUp;
    }

    private void LevelUp()
    {
        this.completedAmount++;
        Eval();
    }

    internal void InitLevelQuestGoal(int requiredAmount)
    {
        throw new NotImplementedException();
    }
}
