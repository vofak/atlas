using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * represents a single goal of a quest that requires  certain amount of items to be added to the invetory
 */
[CreateAssetMenu(fileName = "NewGatherQuestGoal", menuName = "Quest System/Gather Quest Goal")]
public class GatherQuestGoal : QuestGoal {

    public int itemID;

    private PlayerInventory playerInventory;

    public void InitGatherQuestGoal(int requiredAmount, int itemID)
    {
        Init(requiredAmount);
        this.itemID = itemID;
        this.description = BuildDescription();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        playerInventory.onInventoryChangeCallback += InventoryChange;
    }

    internal override void Init()
    {
        base.Init();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        playerInventory.onInventoryChangeCallback += InventoryChange;
    }

    private string BuildDescription()
    {
        return "Gather " + requiredAmount + " " + itemID + "s";
    }

    private void InventoryChange()
    {
        completedAmount = Mathf.Min(requiredAmount, playerInventory.AmountOf(itemID));
        Eval();
    }
}
