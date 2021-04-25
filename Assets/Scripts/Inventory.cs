using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool isMainInventory = false; // is this the main player inventory 
    public GameObject handItem = null; // the current item in the player hand- if it's the main inventory 
    public GameObject[] inventoryItems = new GameObject[10];
    private int _inventoryCount = 0;
    public int Count 
    {
        get { return _inventoryCount; } 
    }

    public void AddItem(GameObject newItem, bool toHand)
    {
        if (_inventoryCount == inventoryItems.Length)
        {
            // no place to add more
            return; 
        }
        
        if (isMainInventory && handItem == null && toHand)
        {
            // add the item to the hand of the player
            handItem = newItem; 
        }

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null)
            {
                // add to the free space
                inventoryItems[i] = newItem;
                _inventoryCount += 1;
                break;
            }
        }
    }

    public void RemoveItem(GameObject itemToRemove)
    {
        if (isMainInventory && handItem == itemToRemove)
        {
            handItem = null;
        }

        for (int i = 0; i < inventoryItems.Length; i++)
        {

            if (inventoryItems[i] != null && inventoryItems[i] == itemToRemove)
            {
                inventoryItems[i].gameObject.SetActive(true);
                inventoryItems[i] = null;
                _inventoryCount--; 
            }
        }
    }

    public bool IsInInventory(GameObject itemToCheck, bool inHand)
    {
        if (inHand) // looking for the item in the hnd of the player
        {
            Debug.Log("looking fro wrench in hand");
            if (handItem == itemToCheck)
            {
                Debug.Log("is in hand");

                return true; // the item is in hand
            }

            return false;  // the item is not in hand
        }
        
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == itemToCheck)
            {
                return true; 
            }
        }
        return false; 
    }

    public void SetMainInventory()
    {
        isMainInventory = true; 
    }
    
    public GameObject GetHandItem()
    {
        return handItem; 
    }

    public bool IsFull()
    {
        if (_inventoryCount == inventoryItems.Length)
        {
            return true; 
        }

        return false; 
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
