using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * class representing inventory item
 */ 
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public int id;
    public string itemName;
    public string description;
    public int maxAmount;
    public int price;
    public Rarity rarity;
    public Sprite sprite;

    public void InitItem(int id, string itemName, String description, int maxAmount, int price, Rarity rarity, Sprite sprite)
    {
        this.id = id;
        this.itemName = itemName;
        this.description = description;
        this.maxAmount = maxAmount;
        this.price = price;
        this.rarity = rarity;
        this.sprite = sprite;
    }

    /*
     * returns the string that will be shown by tooltip
     */ 
    internal virtual string getContentString()
    {
        string str = "";
        str += "<size=30><color=#89FFE8FF><b>" + itemName + " </b></color></size>\n";
        str += ToFriendlyString(rarity) + "\n";
        str += "\n";
        if (description.Length > 0)
        {
            str += description + "\n";
        }
        str += "price: " + price + "\n";
        return str;
    }

    public virtual void Use()
    {
        Debug.Log("using item " + itemName);
    }

    public virtual bool RemoveAfterUse()
    {
        return false;
    }

    #region Rarity

    public enum Rarity
    {
        TRASH,
        COMMON,
        RARE,
        EPIC,
        LEGENDARY,
        QUEST_ITEM
    }

    public string ToFriendlyString(Rarity slot)
    {
        switch (slot)
        {
            case Rarity.COMMON:
                return "<color=#FFFFFFFF>Common</color>";
            case Rarity.EPIC:
                return "<color=#7252FFFF>Epic</color>";
            case Rarity.LEGENDARY:
                return "<color=#FFCB00FF>Legendary</color>";
            case Rarity.RARE:
                return "<color=#59CCFFFF>Rare</color>";
            case Rarity.TRASH:
                return "<color=#BCBCBCFF>Trash</color>";
            case Rarity.QUEST_ITEM:
                return "<color=#FFFF00FF>Quest Item</color>";
        }
        return "";
    }

    #endregion
}
