using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 *  script for slot in an inventory that allows drag and drop
 */ 
[RequireComponent(typeof(CanvasGroup))]
public class DaDItemSlot : ItemSlot, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    private Transform originalParent;
    private Vector3 originalLocalPosition;
    private CanvasGroup canvasGroup;

    /*
     * updates the image and amount of an item shown
     */ 
    internal override void UpdateItemSlot()
    {
        base.UpdateItemSlot();
        if (itemStack == null)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
            amountText.text = "";
        }
        else
        {
            itemImage.sprite = itemStack.item.sprite;
            itemImage.enabled = true;
            if(itemStack.amount > 1)
            {
                amountText.text = itemStack.amount.ToString();
            }
            else
            {
                amountText.text = "";
            }
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemStack == null)
        {
            return;
        }
        originalParent = transform.parent;
        transform.SetParent(transform.parent.parent.parent);
        transform.position = eventData.position;
        canvasGroup.blocksRaycasts = false;
        
    }

    /*
     * updates the position of the slot
     */ 
    public void OnDrag(PointerEventData eventData)
    {
        if (itemStack != null)
        {
            transform.position = eventData.position;
        }
    }

    /*
     * if possible, puts the dragged item stack to this slot
     */ 
    public virtual void OnDrop(PointerEventData eventData)
    {
        EquipmentSlot draggedEquipmentSlot = eventData.pointerDrag.GetComponent<EquipmentSlot>();

        if (draggedEquipmentSlot != null)
        {
            if (draggedEquipmentSlot.itemStack == null)
            {
                return;
            }
            Equipment draggedEq = (Equipment)draggedEquipmentSlot.itemStack.item;
            if (itemStack == null)
            {
                playerInventory.SetItemStack(draggedEquipmentSlot.itemStack, numberInInventory);
                playerInventory.RemoveEquipment((int)draggedEq.slot);

            }
            else
            {
                Equipment eq = itemStack.item as Equipment;
                if (eq == null)
                {
                    playerInventory.Unequip((int)draggedEq.slot);
                }
                else
                {
                    if (eq.slot == draggedEq.slot)
                    {
                        playerInventory.SetItemStack(draggedEquipmentSlot.itemStack, numberInInventory);
                        playerInventory.RemoveEquipment((int)draggedEq.slot);
                        playerInventory.Equip(eq);
                    }
                    else
                    {
                        playerInventory.Unequip((int)draggedEq.slot);
                    }
                }
            }
            return;
        }


        ItemSlot draggedSlot = eventData.pointerDrag.GetComponent<ItemSlot>();
        if (draggedSlot != null && draggedSlot.itemStack != null)
        {
            if (itemStack == null || draggedSlot.itemStack.item.id != itemStack.item.id || itemStack.amount == itemStack.item.maxAmount)
            {
                SwapStacks(draggedSlot);
            }
            else
            {
                int wantToAdd = draggedSlot.itemStack.amount;
                int added = playerInventory.AddItemStack(draggedSlot.itemStack, numberInInventory);
                if (added < wantToAdd)
                {
                    //draggedSlot.itemStack.amount -= added;
                    playerInventory.RemoveAmount(draggedSlot.numberInInventory, added);
                }
                else
                {
                    playerInventory.RemoveItemStack(draggedSlot.numberInInventory);
                }
            }
        }
    }

    /*
     * swaps stacks with another slot
     */ 
    internal virtual void SwapStacks(ItemSlot other)
    {
        ItemStack otherItemStack = playerInventory.SetItemStack(other.itemStack, numberInInventory);
        playerInventory.SetItemStack(otherItemStack, other.numberInInventory);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        transform.localPosition = originalLocalPosition;
        canvasGroup.blocksRaycasts = true;
    }

    // Use this for initialization
    public override void Start () {
        base.Start();
        originalParent = transform.parent;
        originalLocalPosition = transform.localPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }
}
