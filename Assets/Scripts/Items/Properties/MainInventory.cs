using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInventory : Inventory
{
    
    public InventoryUI _inventoryUI;

    /**
     * Set as main inventory
     */
    public void Start()
    {
        IsMainInventory = true; 
    }

       
    /**
     * add an item as hand item (if main inventory) and if wanted (toHand = true) or to the inventory
     */
    public override Interactable AddItem(Interactable newItem, bool toHand = false)
    {
        Interactable previousHandItem = null;
        
        // this is the main inventory and we want to add to the hand of the player. 
        if (toHand)
        {
            if (HandItem != null)
            {
                previousHandItem = GetItem(HandItem.GetItemType());
                DeleteItem(HandItem.GetItemType());
            }
            HandItem = newItem; 
            newItem.gameObject.SetActive(false); // remove item from the scene
            _inventoryUI.AddItem(newItem, true); // Add the item to UI
            return previousHandItem;
        }

        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if (InventoryItems[i] == null)
            {
                // add to the free space
                InventoryItems[i] = newItem;
                InventoryCount += 1;
                newItem.gameObject.SetActive(false); // remove item from the scene
                _inventoryUI.AddItem(newItem, false); // Add the item to UI
                return null;
            }
        }
        return null;
    }
    
    
    /**
     * Delete an item- for hand items and inventory items 
     */
    public override void DeleteItem(ItemType item)
    {
        // delete hand item 
        if (HandItem != null && HandItem.GetItemType() == item)
        {
            HandItem = null;
            _inventoryUI.Removeitem(item, true); // Remove the item from UI
        }
        
        // delete from the inventory
        else
        {
            for (int i = 0; i < InventoryItems.Length; i++)
            {
                if (InventoryItems[i] != null && InventoryItems[i].GetItemType() == item)
                {
                    InventoryItems[i] = null;
                    InventoryCount--; 
                    _inventoryUI.Removeitem(item, false); // Remove the item from UI
                }
            }
        }
    }

    public InventoryUI GetInventoryUI()
    {
        return _inventoryUI; 
    }

}
