using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * represents a single goal of a quest that requires  certain amount of money to be present in the inventory
 */
[CreateAssetMenu(fileName = "NewHoardQuestGoal", menuName = "Quest System/Hoard Quest Goal")]
public class HoardQuestGoal : QuestGoal {

    PlayerInventory inv;

    internal override void Init()
    {
        base.Init();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        inv.onInventoryChangeCallback += InventoryChange;
    }

    private void InventoryChange()
    {
        if(inv.money < requiredAmount)
        {
            completedAmount = inv.money;
        }
        else
        {
            completedAmount = requiredAmount;
        }
        Eval();
    }
}
