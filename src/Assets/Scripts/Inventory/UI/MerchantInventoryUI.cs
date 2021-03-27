using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * inventory of an NPC that can trade with us
 */ 
public class MerchantInventoryUI : InventoryUI
{
    [SerializeField]
    private Transform merchantsItemsParent;
    [SerializeField]
    private Text merchantMoneyText;
    [SerializeField]
    private Text myMoneyText;

    private PlayerInventory playerInventory;
    private Merchant merchant;
    private MerchantItemSlot[] merchantSlots;
    



    public override void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        slots = itemsParent.GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].numberInInventory = i;
        }

        merchantSlots = merchantsItemsParent.GetComponentsInChildren<MerchantItemSlot>();
        for (int i = 0; i < merchantSlots.Length; i++)
        {
            merchantSlots[i].numberInInventory = i;
            merchantSlots[i].isPlayersSlot = false;
        }

        HideInventory();
    }

    public override void Update()
    {
        base.Update();
    }

    /*
     * sets the currrent merchant that we can trade with
     */ 
    public void SetMerchant(Merchant newMerchant)
    {
        merchant = newMerchant;

        for (int i = 0; i < merchantSlots.Length; i++)
        {
            merchantSlots[i].merchant = merchant;

        }
        for (int i = 0; i < slots.Length; i++)
        {
            ((MerchantItemSlot)slots[i]).merchant = merchant;

        }
        merchant.onInventoryChangeCallback += UpdateUI;
        playerInventory.onInventoryChangeCallback += UpdateUI;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        for (int i = 0; i < merchant.inventorySize; i++)
        {
            merchantSlots[i].SetItemStack(merchant.items[i]);
        }
        merchantMoneyText.text = merchant.money.ToString();


        for (int i = 0; i < playerInventory.inventorySize; i++)
        {
            slots[i].SetItemStack(playerInventory.items[i]);
        }
        myMoneyText.text = playerInventory.money.ToString();
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
        Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.MERCHANT);
    }
}
