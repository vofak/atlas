using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : Inventory, IInteractable {

    [HideInInspector]
    public Interacter interacter;
    
    private LootInventoryUI lootInventoryUI;
    public bool active = false;

    public Item[] beginningItems;
    
    
    // Use this for initialization
    public override void Start()
    {
        //base.Start();
        interacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Interacter>();
        lootInventoryUI = GameObject.FindGameObjectWithTag("LootInventory").GetComponent<LootInventoryUI>();

        inventorySize = beginningItems.Length;
        items = new ItemStack[inventorySize];

        for(int i = 0; i < inventorySize; ++i)
        {
            Item it = beginningItems[i];
            ItemStack s = ScriptableObject.CreateInstance<ItemStack>();
            s.Init(it, 1);
            items[i] = s;
        }
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    internal override void UpdateUI()
    {
        base.UpdateUI();
        if (IsEmpty())
        {
            active = false;
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
        if (!active)
        {
            return;
        }

        lootInventoryUI.SetInventory(this);
        lootInventoryUI.ShowInventory();
    }
}
