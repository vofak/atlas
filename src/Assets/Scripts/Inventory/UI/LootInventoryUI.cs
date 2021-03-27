using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * script handling the GUI of loot inventory
 */ 
public class LootInventoryUI : InventoryUI {

    [SerializeField]
    private GameObject prefabLootInventoryItemSlot;

    private Interacter interacter;
    internal Lootable lootInventory;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        
        
        if (lootInventory != null && Vector3.Distance(lootInventory.transform.position, lootInventory.interacter.transform.position) > lootInventory.interacter.interactingRange)
        {
            HideInventory();
            lootInventory = null;
        }
    }

    /*
     * Sets the current inventory that can be shown
     */
    public void SetInventory(Lootable newInventory)
    {
        lootInventory = newInventory;
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i] != null)
            {
                DestroyImmediate(slots[i].gameObject);
            }
            
        }
        for (int i = 0; i < lootInventory.inventorySize; i++)
        {
            Instantiate(prefabLootInventoryItemSlot, itemsParent);
        }


        InitializeSlots();
        
        for (int i = 0; i < slots.Length; i++)
        {
            LootInventoryItemSlot currSlot = (LootInventoryItemSlot) slots[i];
            currSlot.lootInventory = newInventory;
            
        }
        lootInventory.onInventoryChangeCallback += UpdateUI;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        for (int i = 0; i < lootInventory.inventorySize; i++)
        {
            slots[i].SetItemStack(lootInventory.items[i]);
        }
        if (lootInventory.IsEmpty())
        {
            HideInventory();
        }
    }

    public override void HideInventory()
    {
        if (inventoryPanel.activeSelf)
        {
            base.HideInventory();
            Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.NOTHING);
        }
    }

    public override void ShowInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            base.ShowInventory();
            Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.LOOT);
        }
    }
}
