using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool isMainInventory = false; // is this the main player inventory 
    public GameObject handItem = null; // the current item in the player hand- if it's the main inventory 
    public GameObject[] inventoryItems = new GameObject[10];
    private int _inventoryCount = 0; 
    public void AddItem(GameObject newGM)
    {
        if (_inventoryCount == inventoryItems.Length)
        {
            // no place to add more
            return; 
        }
        
        if (isMainInventory && handItem == null)
        {
            // add the item to the hand of the player
            handItem = newGM; 
        }

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null)
            {
                // add to the free space
                inventoryItems[i] = newGM;
                _inventoryCount += 1; 
                
            }
        }
    }

    // TODO need to think what this function gets 
    public void RemoveItem(GameObject itemToRemove)
    {
        bool isInHand = false; 
        
        if (isMainInventory && handItem == itemToRemove)
        {
            handItem = null;
            isInHand = true; 
        }

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] != null && inventoryItems[i] == itemToRemove)
            {
                if (!isInHand) 
                {
                    // the item goes back to where it was found
                    inventoryItems[i].SetActive(true); 
                }
                
                inventoryItems[i] = null;
            }
        }
    }

    // TODO need to think what this function gets 
    public bool IsInInventory(GameObject itemToCheck)
    {
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
}
