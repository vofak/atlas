using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Class holding information about what item and how much of it is in an inventory slot
 */ 
[CreateAssetMenu(fileName = "New Item Stack", menuName = "Inventory/Item Stack")]
public class ItemStack : ScriptableObject{

    public Item item;
    public int amount;

    public void Init(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void Init(ItemStack itemStack)
    {
        this.item = itemStack.item;
        this.amount = itemStack.amount;
    }
}
