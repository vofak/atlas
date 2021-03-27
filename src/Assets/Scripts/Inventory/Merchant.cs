using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Script for an NPC that the player can trade with
 */ 
public class Merchant : Inventory, IInteractable
{

    private bool active = true;
    private PlayerInventory playerInventory;
    private MerchantInventoryUI merchantUI;

    public override void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        merchantUI = GameObject.FindGameObjectWithTag("Merchant").GetComponent<MerchantInventoryUI>();

        for(int i = 0; i < items.Length; ++i)
        {
            if(items[i] != null)
            {

                ItemStack newStack = ScriptableObject.CreateInstance<ItemStack>();
                newStack.Init(items[i]);
                items[i] = newStack;
            }
        }
    }

    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            active = value;
        }
    }

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }
    
    public void Interact()
    {
        merchantUI.SetMerchant(this);
        merchantUI.ShowInventory();
    }

    /*
     * Adds an item from the slot to the players inventory if he has the money and takes the money
     */
    public bool Sell(int numberInInventory)
    {
        bool sold = false;
        ItemStack itemStack = items[numberInInventory];
        if (itemStack == null)
        {
            return false;
        }
        int price = itemStack.item.price;
        if (playerInventory.money >= price)
        {
            sold = playerInventory.AddItem(itemStack.item);
            RemoveAmount(numberInInventory, 1);
            this.AddMoney(price);
            playerInventory.AddMoney(-price);
        }
        else
        {
            Debug.Log("Not enough coin");
        }
        UpdateUI();
        return sold;
    }

    /*
     * Adds an item from the slot to the players inventory if he has the money and takes the money
     */
    public int Sell(int from, int to)
    {
        int sold = 0;
        ItemStack itemStack = items[from];
        if (itemStack == null)
        {
            return 0;
        }
        int price = itemStack.item.price * itemStack.amount;
        if (playerInventory.money >= price)
        {
            sold = playerInventory.AddItemStack(itemStack, to);
            RemoveAmount(from, sold);
            this.AddMoney(price);
            playerInventory.AddMoney(-price);
        }
        else
        {
            Debug.Log("Not enough coin");
        }
        UpdateUI();
        return sold;
    }

    /*
     * Adds an item from the slot to the merchants inventory if he has the money and gives the money to the player
     */
    public bool Buy(int numberInInventory)
    {
        bool bought = false;
        ItemStack itemStack = playerInventory.items[numberInInventory];
        if(itemStack == null)
        {
            return false;
        }
        int price = itemStack.item.price;
        if (this.money >= price)
        {
            bought = this.AddItem(itemStack.item);
            playerInventory.RemoveAmount(numberInInventory, 1);
            this.AddMoney(-price);
            playerInventory.AddMoney(price);
        }
        else
        {
            Debug.Log("Merchant is poor");
        }
        UpdateUI();
        return bought;
    }

    /*
     * Adds an item from the slot to the merchants inventory if he has the money and gives the money to the player
     */
    public int Buy(int from, int to)
    {
        int bought = 0;
        ItemStack itemStack = playerInventory.items[from];
        if (itemStack == null)
        {
            return 0;
        }
        int price = itemStack.item.price * itemStack.amount;
        if (this.money >= price)
        {
            bought = this.AddItemStack(itemStack, to);
            playerInventory.RemoveAmount(from, bought);
            this.AddMoney(-price);
            playerInventory.AddMoney(price);
        }
        else
        {
            Debug.Log("Merchant is poor");
        }
        UpdateUI();
        return bought;
    }
}
