using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Inventory of the player
 */ 
public class PlayerInventory : Inventory {

    public Equipment[] equipment;

    private int equipmentNb = 6;

    Atlas.Core.PlayerController playerController;
    

    public override void Start()
    {
        base.Start();
        equipment = new Equipment[equipmentNb];
        playerController = GetComponent<Atlas.Core.PlayerController>();
    }

    /*
     * Moves the equipment from the equipment slot to a normal slot
     */ 
    public void Unequip(int slot)
    {
        if (slot >= equipment.Length)
        {
            return;
        }

        if (equipment[slot] != null)
        {
            AddItem(equipment[slot]);
        }
        
        equipment[slot] = null;

        if (slot == (int)Equipment.EquipSlot.MAIN_HAND)
        {
        playerController.isArmed = false;
        }
        else if (slot == (int) Equipment.EquipSlot.OFF_HAND)
        {
            playerController.isShielded = false;
        }

        UpdateUI();
    }

    /*
     * removes an equipment from the inventory
     */ 
    public void RemoveEquipment(int slot)
    {
        if (slot >= equipment.Length)
        {
            return;
        }

        equipment[slot] = null;
        UpdateUI();

        if (slot == (int)Equipment.EquipSlot.MAIN_HAND)
        {
            playerController.isArmed = false;
        }
        else if (slot == (int)Equipment.EquipSlot.OFF_HAND)
        {
            playerController.isShielded = false;
        }
    }

    /*
     * moves an item to equipment slot
     */ 
    public void Equip(Equipment newEquipment)
    {
        int slot = (int)newEquipment.slot;
        Unequip(slot);
        equipment[slot] = newEquipment;

        if (newEquipment.slot == Equipment.EquipSlot.MAIN_HAND)
        {
            playerController.isArmed = true;
        }
        else if (newEquipment.slot == Equipment.EquipSlot.OFF_HAND)
        {
            playerController.isShielded = true;
        }

        UpdateUI();
    }

}
