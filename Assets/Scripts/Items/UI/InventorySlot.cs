using System;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    private Interactable _curItem;
    private Image cur_image;
    
    
    private void Start()
    {
        _curItem = null;
    }
    
    
    
    /**
     * Add item to this slot
     */
    public void AddItem(Interactable newItem)
    {
        _curItem = newItem;
        
        transform.GetChild(0).GetComponent<Image>().sprite = _curItem.item.sprite; // TODO notice that reference to the location in hierarchy
    }

    /**
     * Remove the item from this slot
     */
    public void RemoveItem()
    {
        _curItem = null;
    }

    /**
     * Is this slot empty?
     */
    public bool IsEmpty()
    {
        if (_curItem != null)
        {
            return true; 
        }

        return false; 
    }

    /**
     * Get the item type of the item currently in this slot. 
     */
    public ItemType Contains()
    {
        return _curItem.GetItemType();
    }
    
}
