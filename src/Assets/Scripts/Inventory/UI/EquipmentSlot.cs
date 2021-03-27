using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * script for a DaDItemSlot that can hold only a certain type of equipment
 */ 
public class EquipmentSlot : DaDItemSlot {

    [SerializeField]
    private Image itemPattern;

    internal override void UpdateItemSlot()
    {
        if (itemStack != null)
        {
            itemImage.sprite = itemStack.item.sprite;
            itemImage.enabled = true;
            itemPattern.enabled = false;
        }
        else
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
            itemPattern.enabled = true;
        }
    }

    /*
     * checks if the dropped item is the wanted equipment and puts it in this slot
     */ 
    public override void OnDrop(PointerEventData eventData)
    {
        ItemSlot draggedSlot = eventData.pointerDrag.GetComponent<ItemSlot>();

        if(draggedSlot == null || draggedSlot.itemStack == null)
        {
            return;
        }
        
        Equipment equipment = draggedSlot.itemStack.item as Equipment;
        if(equipment == null)
        {
            return;
        }
        if((int)equipment.slot != numberInInventory)
        {
            return;
        }

        playerInventory.RemoveItemStack(draggedSlot.numberInInventory);
        playerInventory.Equip(equipment);
        
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
        playerInventory.Unequip((int)((Equipment)itemStack.item).slot);
    }
}
