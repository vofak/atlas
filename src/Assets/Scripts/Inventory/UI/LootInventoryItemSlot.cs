using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * inventory of dead enemies or chests
 */ 
public class LootInventoryItemSlot : ItemSlot {

    [SerializeField]
    private Text itemNameText;

    [HideInInspector]
    public Inventory lootInventory;

    public override void Start()
    {
        base.Start();

    }

    internal override void UpdateItemSlot()
    {
        base.UpdateItemSlot();
        if (itemStack == null)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
            amountText.text = "";
            itemNameText.text = "";
        }
        else
        {
            itemImage.sprite = itemStack.item.sprite;
            itemImage.enabled = true;
            itemNameText.text = itemStack.item.itemName;
            if (itemStack.amount > 1)
            {
                amountText.text = itemStack.amount.ToString();
            }
            else
            {
                amountText.text = "";
            }
        }
        
    }


    internal override void OnLeftClick()
    {
        if (itemStack != null) {
            int added = playerInventory.AddItemStack(itemStack);
            lootInventory.RemoveAmount(numberInInventory, added);
        }
    }

    internal override void OnRightClick()
    {
        if (itemStack != null)
        {
            int added = playerInventory.AddItemStack(itemStack);
            lootInventory.RemoveAmount(numberInInventory, added);
        }
    }

}
