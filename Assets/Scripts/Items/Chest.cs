using System;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable, IHideable
{
    public bool open = false;
    public GameObject[] wrenches;
    public Inventory inventory;
    public Material closedMaterial;
    public Material openMaterial;
    private GameObject _curHandItem;
    private void Start()
    {
        inventory = gameObject.AddComponent<Inventory>();
        openMaterial = GetComponentInChildren<MeshRenderer>().material;
    }

    public Action[] CalcInteractions()
    {
        
        _curHandItem = GameManager.Instance.inventory.GetHandItem();

        
        // chest is open
        if (open)
        {
            // we have a wrench in the hand
            foreach (GameObject item in GameManager.Instance.inventory.inventoryItems)
            {
                if (item != null && item.name.StartsWith("Wrench"))
                {
                    return new Action[] { Close, Put };
                }
            }

            bool isWrenchInChest = false;
            foreach (GameObject item in inventory.inventoryItems)
            {
                if (item != null && item.name.StartsWith("Wrench"))
                {
                    isWrenchInChest = true;
                }
            }

            // we have another item in hand
            if (_curHandItem != null && _curHandItem.name.StartsWith("Mineral"))
            {
                if (isWrenchInChest)
                    return new Action[] { Put, Take };
                else
                    return new Action[] { Put, PickUp };
            }
            else if (_curHandItem == null && isWrenchInChest)
            {
                return new Action[] { Take, PickUp };
            }
            else
            {
                return new Action[] { PickUp };
            }
        }

        // chest is closed
        else
        {
            return new Action[] {PickUp};
        }
        
        
        // nothing to do
        return new Action[] { };
    }
    
    public void PickUp()
    {
        Debug.Log("Pickup");
        
        GameManager.Instance.inventory.AddItem(this.gameObject, true);
        
        // TODO need to implement if the object is held in the players hand or in the inventory
        //gameObject.SetActive(false);
        
    }
    
    // TODO probably need to define when to open the chest
    public void Open() 
    {
        Debug.Log("Open chest");
        GetComponentInChildren<MeshRenderer>().material = openMaterial;
        open = true;
    }
    
    public void Close()
    {
        Debug.Log("Close chest");
        GetComponentInChildren<MeshRenderer>().material = closedMaterial;
        open = false;
    }

    public void Put()
    {
        Debug.Log("Hide hand item in chest");

        GameManager.Instance.inventory.RemoveItem(_curHandItem); // remove from global inventory
        _curHandItem.SetActive(false); 
        inventory.AddItem(_curHandItem, false); // add to local inventory
    }

    public void Take()
    {
        Debug.Log("Get wrench from chest");
        foreach (GameObject item in inventory.inventoryItems)
        {
            if (item != null && item.name.StartsWith("Wrench"))
            {
                GameManager.Instance.inventory.AddItem(item, true);
                inventory.RemoveItem(item);
                return;
            }
        }
    }
}