using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/*
 * inventory slot in players inventory
 */ 
public class PlayerInventoryItemSlot : DaDItemSlot
{
    public override void Start()
    {
        base.Start();
    }

    
    internal override void OnLeftClick()
    {
        
    }

    internal override void OnRightClick()
    {
        if(itemStack == null)
        {
            return;
        }
        Item temp = itemStack.item;
        if (itemStack.item.RemoveAfterUse())
        {
            playerInventory.RemoveAmount(numberInInventory, 1);
        }
        temp.Use();
    }
    
}
