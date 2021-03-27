using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * an item that can be equipped to give the player some abilities and/or stats
 */ 
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    static PlayerInventory playerInventory;

    public EquipSlot slot;

    public int armorStat;
    public int damageStat;
    public int magicDamageStat;
    public int critChanceStat;

    public int healthRegen;
    public int manaRegen;


    public void InitEquipment(int id, string itemName, string description, int maxAmount, int price, Rarity rarity, Sprite sprite, EquipSlot slot, int armorStat, int damageStat)
    {
        InitItem(id, itemName, description, maxAmount, price, rarity, sprite);
        this.slot = slot;
        this.armorStat = armorStat;
        this.damageStat = damageStat;

    }

    /*
     * equips the equipment to the appropriate slot
     */ 
    public override void Use()
    {
        base.Use();

        if (playerInventory == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            playerInventory = player.GetComponent<PlayerInventory>();
        }

        playerInventory.Equip(this);
    }

    /*
     * returns the string that will be shown by tooltip
     */ 
    internal override string getContentString()
    {
        string str = "";
        str += "<size=30><color=#89FFE8FF><b>" + itemName + " </b></color></size>\n";
        str += ToFriendlyString(rarity) + "\n";
        str += ToFriendlyString(slot) + "\n";
        if (damageStat > 0)
        {
            str += "+" + damageStat + " damage\n";
        }
        if (armorStat > 0)
        {
            str += "+" + armorStat + " armor\n";
        }
        str += "\n";
        if (description.Length > 0)
        {
            str += description + "\n";
        }
        str += "price: " + price + "\n";
        return str;
    }

    public override bool RemoveAfterUse()
    {
        return true;
    }

    #region EquipSlot

    public enum EquipSlot
    {
        HEAD,
        CHEST,
        LEGS,
        FEET,
        MAIN_HAND,
        OFF_HAND
    }

    public string ToFriendlyString(EquipSlot slot)
    {
        switch (slot)
        {
            case EquipSlot.HEAD:
                return "Head";
            case EquipSlot.CHEST:
                return "Chest";
            case EquipSlot.FEET:
                return "Feet";
            case EquipSlot.LEGS:
                return "Legs";
            case EquipSlot.OFF_HAND:
                return "Off Hand";
            case EquipSlot.MAIN_HAND:
                return "Main Hand";
        }
        return "";
    }
    #endregion
}
