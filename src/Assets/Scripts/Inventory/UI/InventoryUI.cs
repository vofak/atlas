using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * script for inventory GUI
 */ 
public class InventoryUI : MonoBehaviour {

    [SerializeField]
    internal Transform itemsParent;
    [SerializeField]
    internal GameObject inventoryPanel;

    internal ItemSlot[] slots;

	// Use this for initialization
	public virtual void Start () {
        InitializeSlots();
        HideInventory();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    /*
     * finds its slots and sets their numbers in the inventory
     */ 
    internal void InitializeSlots()
    {
        slots = itemsParent.GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].numberInInventory = i;
        }
    }

    public virtual void HideInventory()
    {
        inventoryPanel.SetActive(false);
    }

    public virtual void UpdateUI()
    {
        // to be overriden
    }

    public virtual void ShowInventory()
    {
        UpdateUI();
        inventoryPanel.SetActive(true);
    }
}
