using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInventory : Inventory
{
    
    public InventoryUI _inventoryUI;
    public event Action onFirstTimeUse;
    public event Action onFirstTimeHandItemUse;

    /**
     * Set as main inventory
     */
    public void Start()
    {
        IsMainInventory = true; 
    }

       
    /**
     * add an item as hand item (if main inventory) and if wanted or to the inventory
     */
    public override void AddItem(Interactable newItem, InventorySlot fromSlot = null)
    {
        Interactable previousHandItem = null;
        
        // this is the main inventory and we want to add to the hand of the player. 
        if (newItem.isHandItem)
        {
            onFirstTimeHandItemUse?.Invoke();
            if (HandItem != null)
            {
                previousHandItem = GetItem(HandItem.GetItemType());
                DeleteItem(HandItem.GetItemType());
            }
            HandItem = newItem; 
            newItem.gameObject.SetActive(false); // remove item from the scene
            _inventoryUI.AddItem(newItem, true); // Add the item to UI
            
            // had a hand object already- place it where the item we picked up was 
            if (previousHandItem != null)
            {
                if (fromSlot != null)
                {
                    FindObjectOfType<HPInventoryUI>().GetInventory().AddItem(previousHandItem);
                }
                else
                {
                    GameObject previousHandItemGM = previousHandItem.gameObject;
                    previousHandItemGM.SetActive(true);
                    previousHandItemGM.transform.position = newItem.transform.position;
                }
            }
            return;
        }

        onFirstTimeUse?.Invoke();

        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if (InventoryItems[i] == null)
            {
                if (newItem.GetItemType() == ItemType.Helmet)
                {
                    GameManager.Instance.PlayerAI.transform.GetComponentInChildren<HeadHelmet>().Enable();
                }
                // add to the free space
                InventoryItems[i] = newItem;
                InventoryCount += 1;
                newItem.gameObject.SetActive(false); // remove item from the scene
                _inventoryUI.AddItem(newItem); // Add the item to UI
                return;
            }
        }
    }
    
    
    /**
     * Delete an item- for hand items and inventory items 
     */
    public override void DeleteItem(ItemType item, int slot = -1)
    {
        // delete hand item 
        if (HandItem != null && HandItem.GetItemType() == item)
        {
            HandItem = null;
            _inventoryUI.RemoveItem(item, true, slot); // Remove the item from UI
        }
        
        // delete from the inventory
        else
        {
            if (slot >= 0)
            {
                if (InventoryItems[slot] != null && InventoryItems[slot].GetItemType() == item)
                {
                    if (item == ItemType.Helmet)
                    {
                        GameManager.Instance.PlayerAI.transform.GetComponentInChildren<HeadHelmet>().Disable();
                    }
                    InventoryItems[slot] = null;
                    InventoryCount--;
                    _inventoryUI.RemoveItem(item, false, slot); // Remove the item from UI
                    return;
                }
            }
            for (int i = 0; i < InventoryItems.Length; i++)
            {
                if (InventoryItems[i] != null && InventoryItems[i].GetItemType() == item)
                {
                    if (item == ItemType.Helmet)
                    {
                        GameManager.Instance.PlayerAI.transform.GetComponentInChildren<HeadHelmet>().Disable();
                    }
                    InventoryItems[i] = null;
                    InventoryCount--; 
                    _inventoryUI.RemoveItem(item, false, slot); // Remove the item from UI
                    return;
                }
            }
        }
    }

    public InventoryUI GetInventoryUI()
    {
        return _inventoryUI; 
    }

}
