using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool isMainInventory = false; // is this the main player inventory 
    public GameObject handItem = null; // the current item in the player hand- if it's the main inventory 
    public GameObject[] inventoryItems = new GameObject[10];
    private int _inventoryCount = 0; 
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
            MeshRenderer meshRenderer = newItem.GetComponentInChildren<MeshRenderer>();
            Vector3 scale = meshRenderer.transform.lossyScale;
            Transform handItemTransform = GameObject.FindGameObjectWithTag("Player").transform.Find("GFX").Find("HandItem");
            handItemTransform.gameObject.GetComponent<MeshRenderer>().enabled = true;
            handItemTransform.gameObject.GetComponent<MeshRenderer>().material = meshRenderer.material;
            handItemTransform.localScale = new Vector3(scale.x, scale.y / 2.4f, scale.z);
            newItem.SetActive(false);
        }

        else if (isMainInventory && handItem != null && toHand)
        {
            handItem.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up;
            RemoveItem(handItem);
            if (newItem == null)
                return;
            handItem = newItem;
            MeshRenderer meshRenderer = newItem.GetComponentInChildren<MeshRenderer>();
            Vector3 scale = meshRenderer.transform.lossyScale;
            Transform handItemTransform = GameObject.FindGameObjectWithTag("Player").transform.Find("GFX").Find("HandItem");
            handItemTransform.gameObject.GetComponent<MeshRenderer>().enabled = true;
            handItemTransform.gameObject.GetComponent<MeshRenderer>().material = meshRenderer.material;
            handItemTransform.localScale = new Vector3(scale.x, scale.y / 2.4f, scale.z);
            newItem.SetActive(false);
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
            Transform handItemTransform = GameObject.FindGameObjectWithTag("Player").transform.Find("GFX").Find("HandItem");
            handItemTransform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        for (int i = 0; i < inventoryItems.Length; i++)
        {

            if (inventoryItems[i] != null && inventoryItems[i] == itemToRemove)
            {
                if (isMainInventory)
                {
                    inventoryItems[i].gameObject.SetActive(true);
                }
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
}
