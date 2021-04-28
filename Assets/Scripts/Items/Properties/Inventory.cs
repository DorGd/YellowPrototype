using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool isMainInventory = false; // is this the main player inventory 
    public Interactable handItem = null; // the current item in the player hand- if it's the main inventory 
    public Interactable[] inventoryItems = new Interactable[10];
    private int _inventoryCount = 0; 
    public int Count 
    {
        get { return _inventoryCount; } 
    }
    /**
     * add an item as hand item (if main inventory) and if wanted (toHand = true) or to the inventory
     */
    public void AddItem(Interactable newItem, bool toHand)
    {
        // this is the main inventory and we want to add to the hand of the player. 
        if (isMainInventory && toHand)
        {
            if (handItem != null)
            {
                RemoveItem(handItem); // remove the item that is currently in hand
            }
            handItem = newItem; 
            newItem.gameObject.SetActive(false); // remove item from the scene
            return;
        }

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null)
            {
                // add to the free space
                inventoryItems[i] = newItem;
                _inventoryCount += 1;
                newItem.gameObject.SetActive(false); // remove item from the scene
                return;
            }
        }
    }

    /**
     * Remove an item- for hand items and inventory items 
     * place the item that was removed next to where the player currently at.
     * TODO when we will decide how to get the player location, add offset.
     * TODO change to according to type 
     */
    public void RemoveItem(Interactable itemToRemove)
    {
        // remove hand item 
        if (isMainInventory && handItem == itemToRemove)
        {
            handItem = null;
        }
        
        // remove from the inventory
        else
        {
            for (int i = 0; i < inventoryItems.Length; i++)
            {

                if (inventoryItems[i] != null && inventoryItems[i] == itemToRemove)
                {
                    inventoryItems[i] = null;
                    _inventoryCount--; 
                }
            }
        }
  
        
        // get the item back to the scene
        itemToRemove.gameObject.SetActive(true); 
        // TODO need to understand how to get the player coords and than locate the item with an offset
        itemToRemove.gameObject.transform.position = GameManager.Instance.PlayerTransform.position; // locate the object that was removed next to the player

    }
    
    /**
     * check if an item is in the inventory- for both hand items and inventory items
     */
    public bool IsInInventory(ItemType item,  bool inHand)
    {
        // looking for the item in the hand of the player
        if (inHand)
        {
            return handItem.GetType() == item; 
        }
        
        // looking for the item in the inventory
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].GetType() == item)
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
        isMainInventory = true; 
    }

   
    /**
     * return the game object of the item the player have in it's hand. 
     */
    public Interactable GetHandItem()
    {
        if (isMainInventory)
        {
            return handItem;
        }
        return null; // doesn't even have a hand pbject
    }
    
    /**
     * Checks if there's place in the inventory or it's full
     */
    public bool CanAdd()
    {
        return _inventoryCount != inventoryItems.Length; 
    }

    public bool IsEmpty()
    {
        if (_inventoryCount == 0)
        {
            return true;
        }

        return false;
    }
}
