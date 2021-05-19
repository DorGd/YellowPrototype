using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    // manage UI
    public InventorySlot[] slots = new InventorySlot[12]; 
    public InventorySlot handItemSlot;   

    // only UI components
    public GameObject inventoryPanel;
    public GameObject inventoryButton;

    /**
     * Add an item to the inventory slot
     */
    public void AddItem(Interactable item, bool toHand = false)
    {
        if (toHand)
        {
            handItemSlot.AddItem(item);
            return;
        }
        
        foreach (InventorySlot slot in slots)
        {
            // Can add this item to the inventory 
            if (slot.IsEmpty())
            {
                slot.AddItem(item);
                break;
            }
        }
    }
    
    /**
     * TODO think about when and how to do that (gonna be a button that does this- when wanting to move the item to other)
     */
    public void Removeitem(ItemType item, bool fromHand = false)
    {
        if (fromHand && handItemSlot.Contains() == item)
        {
            handItemSlot.RemoveItem();
            return;
        }
        
        foreach (InventorySlot slot in slots)
        {
            // Can add this item to the inventory 
            if (!slot.IsEmpty() && slot.Contains() == item)
            {
                slot.RemoveItem();
                break;
            }
        }
    }


    /**
     * open the inventory panel 
     */
    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        inventoryButton.SetActive(false);

    }

    /**
     * close the inventory panel
     */
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        inventoryButton.SetActive(true);

    }
}
