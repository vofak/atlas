using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Item that can be used and will be removed after use
 */ 
[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item {

    public static Atlas.Core.Stats playerStats;

    public int health = 0;
    public int mana = 0;

    public void InitConsumable(int id, string itemName, string description, int maxAmount, int price, Rarity rarity, Sprite sprite, int health, int mana)
    {
        InitItem(id, itemName, description, maxAmount, price, rarity, sprite);
        this.health = health;
        this.mana = mana;

    }

    /*
     * Restores the stats to the player
     */ 
    public override void Use()
    {
        base.Use();

        if (playerStats == null)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Atlas.Core.Stats>();
        }
        
        playerStats.health.Increase(health);
        playerStats.mana.Increase(mana);
    }

    public override bool RemoveAfterUse()
    {
        return true;
    }

    /*
     * returns the string that will be shown by tooltip
     */ 
    internal override string getContentString()
    {
        string str = "";
        str += "<size=30><color=#89FFE8FF><b>" + itemName + " </b></color></size>\n";
        str += ToFriendlyString(rarity) + "\n";
        if (health > 0)
        {
            str += "<color=#00FF00FF>Use: restores " + health + " health</color>\n";
        }
        if (mana > 0)
        {
            str += "<color=#00FF00FF>Use: restores " + mana + " mana</color>\n";
        }
        str += "\n";
        if (description.Length > 0)
        {
            str += description + "\n";
        }
        str += "price: " + price + "\n";
        return str;
    }
}