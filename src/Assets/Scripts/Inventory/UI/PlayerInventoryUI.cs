using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * script for the players inventory
 */ 
public class PlayerInventoryUI : InventoryUI {

    [SerializeField]
    private Transform equipmentParent;
    [SerializeField]
    private Text moneyText;

    private EquipmentSlot[] equipmentSlots;
    private PlayerInventory playerInventory;

	// Use this for initialization
	public override void Start () {
        base.Start();
        equipmentSlots = equipmentParent.GetComponentsInChildren<EquipmentSlot>();
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].numberInInventory = i;
        }
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        playerInventory.onInventoryChangeCallback += UpdateUI;
    }
	
	// Update is called once per frame
	public override void Update () {

    }

    public override void UpdateUI()
    {
        for (int i = 0; i < playerInventory.inventorySize; i++)
        {
            slots[i].SetItemStack(playerInventory.items[i]);
        }
        for (int i = 0; i < playerInventory.equipment.Length; i++)
        {
            if(playerInventory.equipment[i] == null)
            {
                equipmentSlots[i].SetItemStack(null);
                continue;
            }
            ItemStack newItemStack = ScriptableObject.CreateInstance<ItemStack>();
            newItemStack.Init(playerInventory.equipment[i], 1);
            equipmentSlots[i].SetItemStack(newItemStack);
        }
        moneyText.text = playerInventory.money.ToString();
    }

    public override void HideInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            return;
        }
        base.HideInventory();
        Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.NOTHING);
    }

    public override void ShowInventory()
    {
        if (inventoryPanel.activeSelf)
        {
            return;
        }
        base.ShowInventory();
        Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.INVENTORY);
    }
}
