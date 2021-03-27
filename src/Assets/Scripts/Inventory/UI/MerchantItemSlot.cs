using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Item slot int the trading window
 */ 
public class MerchantItemSlot : DaDItemSlot {

    internal bool isPlayersSlot = true;
    internal Merchant merchant;

    public override void Start()
    {
        base.Start();
    }


    internal override void OnLeftClick()
    {

    }

    /*
     * buys or sells the clicked item
     */ 
    internal override void OnRightClick()
    {
        if (itemStack == null)
        {
            return;
        }
        if (isPlayersSlot)
        {
            merchant.Buy(numberInInventory);
        } 
        else 
        {
            merchant.Sell(numberInInventory);
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        MerchantItemSlot draggedSlot = eventData.pointerDrag.GetComponent<MerchantItemSlot>();

        if (draggedSlot == null || draggedSlot.itemStack == null)
        {
            return;
        }

        if(isPlayersSlot && draggedSlot.isPlayersSlot)
        {
            PlayerToPlayerDrag(draggedSlot);
        }
        else if (!isPlayersSlot && !draggedSlot.isPlayersSlot)
        {
            MerchantToMerchantDrag(draggedSlot);
        }
        else if (isPlayersSlot && !draggedSlot.isPlayersSlot)
        {
            MerchantToPlayerDrag(draggedSlot);
        }
        else if (!isPlayersSlot && draggedSlot.isPlayersSlot)
        {
            PlayerToMerchantDrag(draggedSlot);
        }
    }

    /*
     * player sells the item
     */ 
    private void PlayerToMerchantDrag(MerchantItemSlot draggedSlot)
    {
        if (itemStack != null)
        {
            return;
        }
        merchant.Buy(draggedSlot.numberInInventory, numberInInventory);
    }

    /*
     * player buys the item
     */ 
    private void MerchantToPlayerDrag(MerchantItemSlot draggedSlot)
    {
        if(itemStack != null)
        {
            return;
        }
        merchant.Sell(draggedSlot.numberInInventory, numberInInventory);
    }

    private void MerchantToMerchantDrag(MerchantItemSlot draggedSlot)
    {
        if (itemStack == null || draggedSlot.itemStack.item.id != itemStack.item.id || itemStack.amount == itemStack.item.maxAmount)
        {
            ItemStack otherItemStack = merchant.SetItemStack(draggedSlot.itemStack, numberInInventory);
            merchant.SetItemStack(otherItemStack, draggedSlot.numberInInventory);
        }
        else
        {
            int wantToAdd = draggedSlot.itemStack.amount;
            int added = merchant.AddItemStack(draggedSlot.itemStack, numberInInventory);
            if (added < wantToAdd)
            {
                merchant.RemoveAmount(draggedSlot.numberInInventory, added);
            }
            else
            {
                merchant.RemoveItemStack(draggedSlot.numberInInventory);
            }
        }
    }

    private void PlayerToPlayerDrag(MerchantItemSlot draggedSlot)
    {
        if (itemStack == null || draggedSlot.itemStack.item.id != itemStack.item.id || itemStack.amount == itemStack.item.maxAmount)
        {
            ItemStack otherItemStack = playerInventory.SetItemStack(draggedSlot.itemStack, numberInInventory);
            playerInventory.SetItemStack(otherItemStack, draggedSlot.numberInInventory);
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
