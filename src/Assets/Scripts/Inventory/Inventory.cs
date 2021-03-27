using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Script enabling an object to store items
 */ 
public class Inventory : MonoBehaviour{
    
    public int inventorySize;
    public ItemStack[] items;
    public int money;
    

    public delegate void OnInventoryChange();
    public OnInventoryChange onInventoryChangeCallback;
    

    public virtual void Start()
    {
        items = new ItemStack[inventorySize];
    }


    /*
     * returns true if there is at least the given amount of an item in the inventory
     */ 
    public bool Contains(Item item, int amount)
    {
        return Contains(item.id, amount);
    }

    /*
    * returns true if there is at least the given amount of an itemwith the given id in the inventory
    */
    public bool Contains(int itemID, int amount)
    {

        return AmountOf(itemID) >= amount;
    }

    /*
    * returns amount of the items with the given id in the inventory
    */
    public int AmountOf(int itemID)
    {
        int counter = 0;
        foreach (ItemStack itemStack in items)
        {
            if (itemStack != null && itemStack.item.id == itemID)
            {
                counter += itemStack.amount;
            }
        }
        return counter;
    }

    /*
    * returns amount of the items  in the inventory
    */
    public int AmountOf(Item item)
    {
        return AmountOf(item.id);
    }

    /*
     * Attempts to add the item to the first free slot in the inventory. Returns true if the attempt was succesfull
     */ 
    public bool AddItem(Item newItem)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] != null && items[i].item.id == newItem.id && newItem.maxAmount > items[i].amount)
            {
                items[i].amount++;
                UpdateUI();
                return true;
            }
        }
        
        for (int i = 0; i < inventorySize; i++)
        {
            if(items[i] == null)
            {
                items[i] = ScriptableObject.CreateInstance<ItemStack>();
                items[i].Init(newItem, 1);
                UpdateUI();
                return true;
            }
        }
        return false;
    }

    //returns how many items from the stack were possible to add
    public int AddItemStack(ItemStack newItemStack)
    {
        if(newItemStack == null || newItemStack.amount == 0)
        {
            return 0;
        }
        int amountToAdd = newItemStack.amount;
        int amountAdded = 0;

        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] != null && items[i].item.id == newItemStack.item.id && items[i].item.maxAmount > items[i].amount)
            {
                if(items[i].item.maxAmount >= amountToAdd + items[i].amount)
                {
                    items[i].amount += amountToAdd;
                    UpdateUI();
                    return newItemStack.amount;
                } else {

                    int amountCurrentlyAdded = items[i].item.maxAmount - items[i].amount;
                    items[i].amount += amountCurrentlyAdded;
                    amountToAdd -= amountCurrentlyAdded;
                    amountAdded += amountCurrentlyAdded;
                }
            }
        }

        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] == null)
            {
                items[i] = ScriptableObject.CreateInstance<ItemStack>();
                items[i].Init(newItemStack);
                UpdateUI();
                return newItemStack.amount;
            }
        }
        UpdateUI();
        
        
        return amountAdded;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    
    /*
     * adds an item stack to the slot with given number. Returns amount of succesfully added items
     */ 
    public int AddItemStack(ItemStack newItemStack, int numberOfSlot)
    {
        if(numberOfSlot >= items.Length || newItemStack == null || newItemStack.amount == 0)
        {
            return 0;
        }
        if (items[numberOfSlot] == null)
        {
            ItemStack adding = ScriptableObject.CreateInstance<ItemStack>();
            adding.Init(newItemStack);
            items[numberOfSlot] = adding;
            UpdateUI();
            return newItemStack.amount;
        }

        if (items[numberOfSlot].item.id != newItemStack.item.id)
        {
            return 0;
        }

        int amountToAdd = newItemStack.amount;
        int amountAdded = 0;
        
        if (items[numberOfSlot].item.maxAmount >= amountToAdd + items[numberOfSlot].amount)
        {
            items[numberOfSlot].amount += amountToAdd;
            UpdateUI();
            return newItemStack.amount;
        }
        else
        {
            amountAdded = items[numberOfSlot].item.maxAmount - items[numberOfSlot].amount;
            items[numberOfSlot].amount += amountAdded;
            UpdateUI();
            return amountAdded;
        }
        
    }

    //returns the original itemstack at the position
    public ItemStack SetItemStack(ItemStack newItemStack, int position)
    {
        if(position >= items.Length /*|| newItemStack == null*/)
        {
            return null;
        }

        ItemStack originalItemStack = items[position];
        items[position] = newItemStack;
        UpdateUI();
        return originalItemStack;
    }

    public void RemoveItem(Item item)
    {
        for(int i = inventorySize-1; i >= 0; i--)
        {
            if (items[i] != null && items[i].item.id == item.id)
            {
                items[i].amount -= 1;
                if(items[i].amount == 0)
                {
                    items[i] = null;
                }
                break;
            }
        }
        UpdateUI();
    }

    public void RemoveItem(int numberOfSlot)
    {
        if(items[numberOfSlot] != null)
        {
            items[numberOfSlot].amount -= 1;
            if (items[numberOfSlot].amount == 0)
            {
                items[numberOfSlot] = null;
            }
        }
        UpdateUI();
    }

    public void RemoveItemStack(int numberOfSlot)
    {
        items[numberOfSlot] = null;
        UpdateUI();
    }

    public bool RemoveAmount(int numberOfSlot, int amount)
    {
        if(items[numberOfSlot].amount < amount)
        {
            return false;
        }
        items[numberOfSlot].amount -= amount;
        if(items[numberOfSlot].amount == 0)
        {
            items[numberOfSlot] = null;
        }
        UpdateUI();
        return true;
    }
    
    internal virtual void UpdateUI()
    {
        onInventoryChangeCallback.Invoke();
    }
}
