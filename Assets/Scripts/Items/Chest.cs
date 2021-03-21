using System;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable, IHideable
{
    public bool open = false;
    public GameObject wrench;
    public Inventory inventory;
    private GameObject _curHandItem;
    private void Start()
    {
        inventory = gameObject.AddComponent<Inventory>();
    }
    
    public Action[] CalcInteractions()
    {
        
        _curHandItem = GameManager.Instance.inventory.GetHandItem();

        
        // chest is open
        if (open)
        {
            // we have a wrench in the hand
            if (GameManager.Instance.inventory.IsInInventory(wrench, true))
            {
                return new Action[] {Close, Hide};
            }
            
            // we have another item in hand
            if (_curHandItem != null)
            {
                return new Action[] {Hide};
            }
        }

        // chest is closed
        else
        {
            if (_curHandItem == null)
            {
                return new Action[] {PickUp};
            }
        }
        
        
        // nothing to do
        return new Action[] { };
    }
    
    public void PickUp()
    {
        Debug.Log("Pickup");
        
        GameManager.Instance.inventory.AddItem(this.gameObject, true);
        
        // TODO need to implement if the object is held in the players hand or in the inventory
        gameObject.SetActive(false);
        
    }
    
    // TODO probably need to define when to open the chest
    public void Open() 
    {
        Debug.Log("Open chest");
        
        open = true;
    }
    
    public void Close()
    {
        Debug.Log("Close chest");
        
        open = false;
    }

    public void Hide()
    {
        Debug.Log("Hide hand item in chest");

        GameManager.Instance.inventory.RemoveItem(_curHandItem); // remove from global inventory
        _curHandItem.SetActive(false); 
        inventory.AddItem(_curHandItem, false); // add to local inventory
    }
}