using System;
using UnityEngine;

public enum ItemType
{
    Chest,
    Helmet,
    MasterKey, 
    HelmetPlace, 
    Wrench, 
    SecurityBadge, 
    Mineral, 
    Vent
}

public abstract class Inventory : MonoBehaviour
{
    protected bool IsMainInventory = false; // is this the main player inventory 
    public Interactable HandItem = null; // the current item in the player hand- if it's the main inventory 
    public Interactable[] InventoryItems = new Interactable[4];
    protected int InventoryCount = 0;

    public int Count
    {
        get { return InventoryCount; }
    }

    // ----------- function that require access to UI ------------//

    /**
     * add an item as hand item (if main inventory) or to the inventory
     */
    public abstract void AddItem(Interactable newItem, InventorySlot fromSlot = null);

    /**
     * Delete an item- for hand items and inventory items 
     */
    public abstract void DeleteItem(ItemType item, int slot = -1);

    
    // ----------- function that doesn't require access to UI ------------//

    
    /**
     * Return the item wanted item  
     */
    public Interactable GetItem(ItemType item)
    {
        // get the hand item 
        if (IsMainInventory && HandItem.GetItemType() == item)
        {
            return HandItem;
        }

        // get from the inventory
        for (int i = 0; i < InventoryItems.Length; i++)
        {

            if (InventoryItems[i] != null && InventoryItems[i].GetItemType() == item)
            {
                return InventoryItems[i];
            }
        }
        
        // couldn't find the item
        return null;
    }

    /**
     * check if an item is in the inventory- for both hand items and inventory items
     */
    public bool IsInInventory(ItemType item,  bool inHand = false)
    {
        // looking for the item in the hand of the player
        if (IsMainInventory && inHand && HandItem != null)
        {
            return HandItem.GetItemType() == item; 
        }
        
        // looking for the item in the inventory
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if (InventoryItems[i] != null && InventoryItems[i].GetItemType() == item)
            {
                return true; 
            }
        }
        return false; 
    }

    /**
     * set the inventory as main inventory - that means that it's connected to our player and contains an active option
     * to use the hand item.
     * default setting for inventory: isMainInventory = false
     */
    public void SetMainInventory()
    {
        IsMainInventory = true; 
    }

   
    /**
     * return the game object of the item the player have in it's hand. 
     */
    public Interactable GetHandItem()
    {
        if (IsMainInventory)
        {
            return HandItem;
        }
        return null; // doesn't even have a hand object
    }
    
    /**
     * Checks if there's place in the inventory or it's full
     */
    public bool CanAdd()
    {
        return InventoryCount != InventoryItems.Length; 
    }

    /**
     * check if the inventory is empty
     */
    public bool IsEmpty()
    {
        return InventoryCount == 0;
    }

    /**
     * Get the inventory items.
     */
    public Interactable[] GetItems()
    {
        return InventoryItems;
    }

}
