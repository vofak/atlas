using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

/*
 * script for the slot in an inventory
 */ 
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    internal Image itemImage;
    [SerializeField]
    internal Text amountText;

    internal ItemStack itemStack;
    internal PlayerInventory playerInventory;
    internal int numberInInventory;
    internal Tooltip tooltip;
    internal RectTransform rectTransform;
    internal float sizeMultiplier;



    // Use this for initialization
    public virtual void Start() {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
        rectTransform = GetComponent<RectTransform>();
        sizeMultiplier = Screen.height / 1080f;
    }
    
    /*
     * sets tooltip to item in this slot if thre is any item in this slot and the mouse hovers over this slot
     */ 
    private void OnGUI()
    {

        Vector2 leftBottomPos = new Vector2(rectTransform.position.x, rectTransform.position.y) - rectTransform.sizeDelta * sizeMultiplier / 2;
        Rect rect = new Rect(leftBottomPos, rectTransform.sizeDelta * sizeMultiplier);
        if (itemStack == null)
        {
            return;
        }
        if (rect.Contains(new Vector2(Event.current.mousePosition.x, Screen.height - Event.current.mousePosition.y)))
        {
            tooltip.SetItem(itemStack.item);
        }
    }


    public void SetItemStack(ItemStack newItemStack)
    {
        itemStack = newItemStack;

        UpdateItemSlot();
    }

    internal virtual void UpdateItemSlot()
    {
        // it is supposed to be overridden
    }
    


    internal virtual void OnLeftClick()
    {

    }

    internal virtual void OnRightClick()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

}
