using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * script for the button that allows the player to remove something from the inventory
 *
 */
public class DropSlot : MonoBehaviour, IDropHandler{

    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    /*
     * removes dragged item from the inventory
     */ 
    public virtual void OnDrop(PointerEventData eventData)
    {
        EquipmentSlot draggedEquipmentSlot = eventData.pointerDrag.GetComponent<EquipmentSlot>();
        if (draggedEquipmentSlot != null && draggedEquipmentSlot.itemStack != null)
        {
            Debug.Log("Removed " + draggedEquipmentSlot.itemStack.item.itemName + " from the inventory");
            playerInventory.RemoveEquipment(draggedEquipmentSlot.numberInInventory);
            return;
        }


        ItemSlot draggedSlot = eventData.pointerDrag.GetComponent<ItemSlot>();
        if (draggedSlot != null && draggedSlot.itemStack != null)
        {
            Debug.Log("Removed " + draggedSlot.itemStack.item.itemName + " from the inventory");
            playerInventory.RemoveItemStack(draggedSlot.numberInInventory);
        }
    }
}
