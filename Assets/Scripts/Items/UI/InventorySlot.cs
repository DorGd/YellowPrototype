using System;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public Interactable _curItem;
    public Sprite empty_sprite;

    /**
     * Add item to this slot
     */
    public void AddItem(Interactable newItem)
    {
        _curItem = newItem;
        transform.GetChild(0).GetComponent<Image>().sprite = _curItem.item.sprite; 
    }

    /**
     * Remove the item from this slot
     */
    public void RemoveItem()
    {
        _curItem = null;
        transform.GetChild(0).GetComponent<Image>().sprite = empty_sprite;
    }

    /**
     * Is this slot empty?
     */
    public bool IsEmpty()
    {
        return _curItem == null;
    }

    /**
     * Get the item type of the item currently in this slot. 
     */
    public ItemType GetItemType()
    {
        return _curItem.GetItemType();
    }
    
    
    // ------------------- Slots Button functions ---------------- //
    
    
    /**
     * Move from the HP inventory to the main inventory 
     */
    public void MoveFromHpToMain()
    {
        // is this possible?
        // there is an item in this slot and theres place in the other inventory
        if (_curItem == null || !GameManager.Instance.inventory.CanAdd())
        {
            return;
        }
        
        // Remove the item in this slot in the HP inventory
        // TODO- this might not be this specific one, but could be other item of the same type
        Interactable removedItem = _curItem;
        FindObjectOfType<HPInventoryUI>().GetInventory().DeleteItem(_curItem.GetItemType());
        
        // Enter the item to the main inventory
        GameManager.Instance.inventory.AddItem(removedItem, removedItem.isHandItem? this : null);
    }
    
    /**
     * Move from the main inventory to the HP inventory 
     */
    public void MoveFromMainToHp()
    {
        
        // is this possible?
        // there is an item in this slot and theres place in the other inventory
        if (_curItem == null || !FindObjectOfType<HPInventoryUI>().GetInventory().CanAdd())
        {
            return;
        }

        // Enter the item to the HP inventory
        FindObjectOfType<HPInventoryUI>().GetInventory().AddItem(_curItem);

        // Remove the item in this slot in the main inventory
        GameManager.Instance.inventory.DeleteItem(_curItem.GetItemType());
        // TODO- this might not be this specific one, but could be other item of the same type
    }
}
