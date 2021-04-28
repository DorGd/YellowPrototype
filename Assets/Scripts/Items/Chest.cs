using System;
using UnityEngine;

public class Chest : Interactable, IHideable, IHidingPlace
{
    ItemType CurrItemType = ItemType.Chest;

    public bool open = false;
    public Inventory inventory;
    private Interactable _curHandItem;
    
    private void Start()
    {
        inventory = gameObject.AddComponent<Inventory>();
    }
    
    /**
     * if chest is open:
     *     if we have wrench as a hand object- we can close the chest 
     *     if we have other object as a hand object- we can put it in the chest
     * if chest is close:
     *     we can pick it up as hand pbject
     */
    public override Action[] CalcInteractions()
    {
        _curHandItem = GameManager.Instance.inventory.GetHandItem();
        
        // chest is open
        if (open)
        {
            // we have a wrench in the hand
            if (GameManager.Instance.inventory.IsInInventory(ItemType.Wrench, true))
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
        // don't need to check if there's a place for the items because this is a hand items and can always be picked up
        return new Action[] {PickUp};
    }
    
    /**
     * pick the chest up as hand object
     */
    public void PickUp()
    {
        Debug.Log("Pickup");
        
        GameManager.Instance.inventory.AddItem(this, true);
    }
    
    /**
     * open the chest
     */
    public void Open() 
    {
        Debug.Log("Open chest");
        
        open = true;
    }
    
    /**
     * close the chest
     */
    public void Close()
    {
        Debug.Log("Close chest");
        
        open = false;
    }

    /**
     * hide the current hand item in the chest
     */
    public void Hide()
    {
        Debug.Log("Hide hand item in chest");

        GameManager.Instance.inventory.RemoveItem(_curHandItem); // remove from global inventory
        inventory.AddItem(_curHandItem, false); // add to local inventory
    }
}