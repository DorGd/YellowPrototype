using System;
using UnityEngine;
using UnityEngine.UI;

public class HPInventoryUI : MonoBehaviour
{
    // manage UI
    public InventorySlot[] slots = new InventorySlot[4]; 

    // only UI components
    public GameObject inventoryPanel;
    public Text inventoryName;
    
    // inventory 
    public HPInventory _curInventory;
    
    
    /**
        * Load the inventory UI with a specific Hiding place inventory 
        */
    public void Load_Inventory(string name, HPInventory subInventory)
    {
        _curInventory = subInventory;
        ClearInventory();
        // set the name
        inventoryName.text = name;
         
        // add to the slots
        Interactable[] items = subInventory.GetItems();
        foreach (var item in items)
        {
            if (item != null)
            {
                AddItem(item);
            }
        }
    }
    
    /**
        * Add an item to the inventory slot
        */
    public void AddItem(Interactable item)
    {
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
        * Remove the item from the UI
        */
    public void Removeitem(ItemType item, int slotIndex = -1)
    {
        if (slotIndex >= 0)
        {
            // Can remove this item from the inventory 
            if (!slots[slotIndex].IsEmpty() && slots[slotIndex].GetItemType() == item)
            {
                slots[slotIndex].RemoveItem();
                return;
            }
        }
        foreach (InventorySlot slot in slots)
        {
            // Can remove this item from the inventory 
            if (!slot.IsEmpty() && slot.GetItemType() == item)
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
    }

    /**
        * close the inventory panel
        */
    public void CloseInventory()
    {
        if (_curInventory == null)
            return;
        _curInventory.StopExchange();
        ClearInventory();
        inventoryPanel.SetActive(false);
    }

    internal bool CanAdd()
    {
        return (_curInventory != null && _curInventory.CanAdd());
    }

    public HPInventory GetInventory()
    {
        return _curInventory;
    }
    
    private void ClearInventory()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.RemoveItem();
        }
    }
}
